using Godot;
using Godot.Collections;
using System;

public partial class HydrationStatUpdaterWidget : StreamerWidget
{
    [Export]
    public StreamStat HydrationStat;

    [Export]
    public int deteriorationInterval = 60;
    [Export]
    public int hydrationIncreasePerSip = 20;

    public override Enum[] StreamerbotEventRequests
    {
        get => new Enum[]{
                    StreamerbotEventTypes.Command.Triggered,
                    StreamerbotEventTypes.Twitch.RewardRedemption
        };
        set { }
    }



    public override void _Ready()
    {
        base._Ready();
        _DecreaseHydration();
    }

    public override void OnEventDataReceived(string source, string type, Dictionary data)
    {
        if (type == "RewardRedemption")
        {
            RewardRedemption reward = new(data);
            if (reward.RewardName == "Hydrate")
            {
                IncreaseHydration();
            }
        }

        if (type == "RewardRedemption")
        {
            CommandData command = new(data);
            if (command.Name == "Hydrate")
            {
                IncreaseHydration();
            }
        }
    }

    private void IncreaseHydration()
    {
        HydrationStat.Value += hydrationIncreasePerSip;
    }

    private async void _DecreaseHydration()
    {
        while (true)
        {
            GD.Print("waiting");
            var timer = GetTree().CreateTimer(deteriorationInterval);
            await ToSignal(timer, Timer.SignalName.Timeout);
            GD.Print("THURSTY");

            HydrationStat.Value--;
        }
    }

}

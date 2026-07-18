using Godot;
using Godot.Collections;
using SB.Events;
using System;

public partial class HydrationStatUpdaterWidget : StreamerWidget
{
    [Export]
    public StreamStat HydrationStat;

    [Export]
    public int deteriorationInterval = 60;
    [Export]
    public int hydrationIncreasePerSip = 20;

    public override EventType[] StreamerBotEventRequests
    {
        get => new []{ Command.Triggered, Twitch.RewardRedemption};
        set { }
    }



    public override void _Ready()
    {
        base._Ready();
        _DecreaseHydration();
    }
    public override void OnEventDataReceived(EventType type, Dictionary data)
    {
        switch (type.Name) {
            case "RewardRedemption":
                RewardRedemption reward = new(data);
                if (reward.RewardName == "Hydrate")
                {
                    IncreaseHydration();
                }
                return;

            case "Triggered":
                CommandData command = new(data);
                if (command.Name == "Hydrate")
                {
                    IncreaseHydration();
                }
                return;
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
            var timer = GetTree().CreateTimer(deteriorationInterval);
            await ToSignal(timer, Timer.SignalName.Timeout);
            HydrationStat.Value--;
        }
    }

}

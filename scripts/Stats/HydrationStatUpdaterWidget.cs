using Godot;
using System;

public partial class HydrationStatUpdaterWidget : StreamerWidget
{
    [Export]
    public StreamStat HydrationStat;

    [Export] 
    public int intervall = 30;
    [Export]
    public int hydrationIncreasePerSip = 10;

    public override Enum[] StreamerbotEventRequests { 
        get =>  new Enum[]{
                    StreamerbotEventTypes.Command.Triggered
        };
        set{}
    }

/*

    public override void _Ready()
    {
        base._Ready();
        _DecreaseHydration();
    }

    public override void OnEventDataReceived(string answer)
    {
        CommandData command = new();
        command.GetDataFromAnswer(answer);

        if (command.Name == "Hydrate")
        {
            IncreaseHydration();
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
            var timer = GetTree().CreateTimer(intervall);
            await ToSignal(timer, Timer.SignalName.Timeout);

            HydrationStat.Value--;
        }
    }
*/
}

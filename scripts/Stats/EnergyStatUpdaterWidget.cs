using Godot;
using Godot.Collections;
using SB.Events;
using System;
using System.Reflection;

public partial class EnergyStatUpdaterWidget : StreamerWidget
{
    [Export]
    public StreamStat EnergyStat;

    [Export]
    public int streamLength = 180;
    public override EventType[] StreamerBotEventRequests
    {
        get => new[] { Obs.StreamingStarted};
        set { }
    }

    public override void _Ready()
    {
        base._Ready();
        if (EnergyStat == null)
            return;

        ResetEnergy();

        _WaitUntilDecreaseEnergy();
    }

    public override void OnEventDataReceived(EventType type, Dictionary data)
    {
        ResetEnergy();
    }

    public void ResetEnergy()
    {
        EnergyStat.Value = EnergyStat.MaxValue;
        //ResourceSaver.Save(EnergyStat);
    }

    private async void _WaitUntilDecreaseEnergy()
    {
        while (true)
        {
            var timer = GetTree().CreateTimer(streamLength*60/EnergyStat.MaxValue);
            await ToSignal(timer, Timer.SignalName.Timeout);

            EnergyStat.Value--;
            //ResourceSaver.Save(EnergyStat);
        }
    }


}

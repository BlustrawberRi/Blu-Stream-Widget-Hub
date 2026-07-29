using Godot;
using Godot.Collections;
using SB.Events;
using System;

public partial class ProductivityStatUpdaterWidget : StreamerWidget
{
    [Export] public StreamStat productivityStat;

    public override EventType[] StreamerBotEventRequests
    {
        get => new[]{ Command.Triggered };
        set { }
    }

    public override void _Ready()
    {
        base._Ready();
    }


    public override void OnEventDataReceived(EventType type, Dictionary data)
    {
        CommandData command = new(data);
        switch (command.Name )
        {
            case "Finish Todo":
                // Todo: create scripts for each stat (looking for a specific command or whatever)
                IncreaseProductivity(1);
                break;
            case "Add PP":
                IncreaseProductivity(1);
                break;
            case "Remove PP":
                ReduceProductivity(1);
                break;
        }
    }

    private void IncreaseProductivity(int i)
    {
        productivityStat.Value += i;
        ResourceSaver.Save(productivityStat);

        if (productivityStat.Value >= productivityStat.MaxValue)
        {
            GD.PrintRich("[rainbow][wave]WE DID IT! Productivity Level Up![/wave][/rainbow]");
        }
    }
    private void ReduceProductivity(int i)
    {
        IncreaseProductivity(-i);
    }
}

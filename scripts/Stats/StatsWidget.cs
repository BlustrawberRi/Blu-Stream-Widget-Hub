using Godot;
using System;
using Godot.Collections;
using System.CodeDom.Compiler;
using System.Security.Cryptography.X509Certificates;

public partial class StatsWidget : StreamerWidget
{
    [Export] public StreamStat productivityStat;
    //public Array<StreamStat> streamStats = new Array<StreamStat>();
    [Export] public PackedScene statContainer;

    public override Enum[] StreamerbotEventRequests
    {
        get
        {
            return new Enum[]{
                    StreamerbotEventTypes.Command.Triggered};
        }
        set{}
    }

    private BoxContainer statsListContainer;

    public override void _Ready()
    {
        base._Ready();
        _CreateProductivityStat();
    }


    public override void OnEventDataReceived(string message)
    {
        CommandData command = new();
        command.GetDataFromAnswer(message);

        if (command.Name == "Finish Todo")
        {
            // Todo: create scripts for each stat (looking for a specific command or whatever)
            IncreaseProductivity();
        }
    }

    private void IncreaseProductivity()
    {
        productivityStat.Value++;
        ResourceSaver.Save(productivityStat);

        if (productivityStat.Value >= productivityStat.MaxValue) {
            GD.PrintRich("[rainbow][wave]WE DID IT! Productivity Level Up![/wave][/rainbow]");
            
        }
    }

    private void _CreateProductivityStat(){

        statsListContainer = GetNode<BoxContainer>("%StatsListContainer");
        if (statsListContainer == null)
            GD.PrintErr("Stats List Container is not set.");

        var stat = statContainer.Instantiate<StatContainer>();
        statsListContainer.AddChild(stat);
        stat.Stat = productivityStat;
        GD.Print(stat);
    }
}


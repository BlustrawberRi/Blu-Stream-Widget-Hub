using Godot;
using System;
using Godot.Collections;
using System.CodeDom.Compiler;
using System.Security.Cryptography.X509Certificates;

public partial class StatsWindow : Control
{
    [Export] public StreamStat[] stats;
    //public Array<StreamStat> streamStats = new Array<StreamStat>();
    [Export] public PackedScene statContainer;


    private BoxContainer statsListContainer;

    public override void _Ready()
    {
        base._Ready();
        _CreateProductivityStat();
        //_SubscribeToStatChanges();
    }

    private void _CreateProductivityStat()
    {
        foreach (var stat in stats)
        {
            statsListContainer = GetNode<BoxContainer>("%StatsListContainer");
            if (statsListContainer == null)
                GD.PrintErr("Stats List Container is not set.");

            var container = statContainer.Instantiate<StatContainer>();
            statsListContainer.AddChild(container);
            container.Stat = stat;
            GD.Print(stat);
        }
    }
}


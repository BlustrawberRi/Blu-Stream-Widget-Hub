using Godot;
using System;

public partial class Productivity : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _ExitTree()
	{
		GD.Print("ExitTree Productivity");
		base._ExitTree();
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

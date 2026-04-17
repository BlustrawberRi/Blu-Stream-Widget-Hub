using Godot;
using System;

public partial class StatLoader : Control
{
	private string SaveStatPath => "user://stats/"; //make this global
																		 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // find every file in safe path folder
		// check if resource / StreamStat type
		// Load
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;

public partial class Main : Node
{
	Window _MainWindow;
	[Export] Window _SubWindow;

	public override void _Ready()
	{
		_MainWindow = GetWindow();
		if (_SubWindow != null)
		{
			_SubWindow.World2D = _MainWindow.World2D;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

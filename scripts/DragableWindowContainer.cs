using Godot;
using System;

public partial class DragableWindowContainer : Container
{
    private bool isDragged = false;
    private Vector2 initialMousePos;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isDragged)
			MoveWindowWithMouse();
	}

	private void MoveWindowWithMouse()
	{
		var currentMousePos = GetGlobalMousePosition();
		var moveDelta = currentMousePos - initialMousePos;

		this.Position = Position + moveDelta;
		initialMousePos = currentMousePos;
	}
	
	public void OnHeaderPressed()
	{
		GD.Print("Pressed");
		isDragged = true;
		initialMousePos = GetGlobalMousePosition();
	}
	public void OnHeaderReleased()
	{
		isDragged = false;
	}
}

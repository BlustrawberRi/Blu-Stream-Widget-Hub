using Godot;
using System.Collections.Generic;

[Tool] 
public partial class DragableWindowContainer : Container
{
    [Export] 
    public string windowTitleString = ""; 

    [Export]
    public Button Handle;


    private bool isDragged = false;
    private Vector2 initialMousePos;

    public override void _Ready()
    {
        base._Ready();

        ChangeWindowTitle(windowTitleString);

        if (Handle != null)
        {
            Handle.Pressed += OnHandlePressed; 
            Handle.ButtonUp += OnHandleReleased;            
            //GD.Print("Subscribed to drag Handle");
        }
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Engine.IsEditorHint())
        {
            UpdateConfigurationWarnings();
        }

		if (isDragged)
			MoveWindowWithMouse();
	}

    public void ChangeWindowTitle(string title)
    {
        RichTextLabel WindowTitle = FindChild("WindowTitleName") as RichTextLabel;
        if (WindowTitle != null)
            WindowTitle.Text = windowTitleString;
    }

    

    public void AddWindowContent(Node content)
    {
        Container windowContentContainer = FindChild("WindowContentContainer") as Container;
        windowContentContainer.AddChild(content);
    }

	private void MoveWindowWithMouse()
	{
		var currentMousePos = GetGlobalMousePosition();
		var moveDelta = currentMousePos - initialMousePos;

		this.Position = Position + moveDelta;
		initialMousePos = currentMousePos;
	}
	
	public void OnHandlePressed()
	{
		GD.Print("Pressed");
		isDragged = true;
		initialMousePos = GetGlobalMousePosition();
	}
	public void OnHandleReleased()
	{
		isDragged = false;
	}

    //only works with [Tool]
	//this is only worth it, if I do not have to touch this code, because it WILL delete all references in every Node that uses this class
    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = new List<string>();

        if (Handle == null)
            warnings.Add("A reference to a button that will act as a handle for dragging is still missing. Dragging will not work without it.");

        return warnings.ToArray();
        //return base._GetConfigurationWarnings();
    }

	
}

using Godot;
using System.Collections.Generic;

[Tool] 
public partial class DragableWindowContainer : Control
{
    [Export] 
    public string windowTitleString = "";

    [Export] public Button Handle;
    [Export] public Control RightResizeControl;
    [Export] public Control LeftResizeControl;
    [Export] public Control TopResizeControl;
    [Export] public Control BottomResizeControl;


    private bool isDragged = false;
    private bool isResized = false;
    private Vector2 initialMousePos;
    private enum ResizeDirection { left, right, top, bottom }
    private ResizeDirection resizeDirection;

    public override void _Ready()
    {
        base._Ready();

        ChangeWindowTitle(windowTitleString);

        if (Handle != null)
        {
            Handle.Pressed += OnHandlePressed;
            Handle.ButtonUp += OnHandleReleased;
        }

        if (LeftResizeControl != null)
            LeftResizeControl.GuiInput += (OnLeftRezizeInput);

        if (RightResizeControl != null)
            RightResizeControl.GuiInput += OnRightRezizeInput;

        if (TopResizeControl != null) 
            TopResizeControl.GuiInput += OnTopRezizeInput;

        if (BottomResizeControl != null) 
            BottomResizeControl.GuiInput += OnBottomRezizeInput;
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
            WindowTitle.Text = "[b]"+windowTitleString + "[/b]";
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
		// GD.Print("Pressed");
		isDragged = true;
        initialMousePos = GetGlobalMousePosition();
	}
    public void OnHandleReleased()
    {
        isDragged = false;
    }

    private void OnLeftRezizeInput(InputEvent @input)
    {
        OnResizeEvent(@input, ResizeDirection.left);
    }
    private void OnRightRezizeInput(InputEvent @input)
    {
        OnResizeEvent(@input, ResizeDirection.right);
    }
    private void OnTopRezizeInput(InputEvent @input)
    {
        OnResizeEvent(@input, ResizeDirection.top);
    }
    private void OnBottomRezizeInput(InputEvent @input)
    {
        OnResizeEvent(@input, ResizeDirection.bottom);
    }

    private void OnResizeEvent(InputEvent @input, ResizeDirection dir)
    {
        if (CheckForResizeDrag(@input))
        {
            Godot.Vector2 dirMultiplier = new();
            switch (dir)
            {
                case ResizeDirection.left:
                    dirMultiplier = new(1, 0);
                    break;
                case ResizeDirection.right:
                    dirMultiplier = new(-1, 0);
                    break;
                case ResizeDirection.top:
                    dirMultiplier = new(0, 1);
                    break;
                case ResizeDirection.bottom:
                    dirMultiplier = new(0, -1);
                    break;
            }

            var mouse = @input as InputEventMouseMotion;
            if (Size - mouse.Relative * dirMultiplier < GetMinimumSize()) 
                return;
            
            Size -= mouse.Relative * dirMultiplier;
            if ( dir == ResizeDirection.left || dir == ResizeDirection.top)
                Position += mouse.Relative * dirMultiplier;
        }
    }
    
    private bool CheckForResizeDrag(InputEvent @input)
    {
        if (@input is InputEventMouseButton mbutton)
        {
            if (mbutton.Pressed)
            {
                isResized = true;
                initialMousePos = GetGlobalMousePosition();
                GD.Print(mbutton);
            }
            else if (mbutton.Canceled || !mbutton.Pressed)
            {
                isResized = false;
            }
        } else if (@input is InputEventMouseMotion mouse)
        {
            if (!isResized) return false;
            return true;
        }
        return false;
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

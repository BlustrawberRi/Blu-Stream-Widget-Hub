using Godot;
using System;

[Tool, GlobalClass]
public partial class ShiftingTextureProgressBar : Godot.Range
{
	[ExportGroup("Textures")]
	[Export] public Texture2D TextureProgressMask
    {
		get => _textureProgressMask;
        set
        {
			_textureProgressMask = value;
			_OnTextureSet(_progressMaskNode, value);
		}
    }

    [Export] public Texture2D TextureProgressFill
    {
		get => _textureProgressFill;
		set
		{
			_textureProgressFill = value;
			_OnTextureSet(_progressNode, value);
		}
	}


    [Export] public Texture2D TextureUnder;
	[Export] public Texture2D TextureOver;

    private Texture2D _textureProgressMask;
    private Texture2D _textureProgressFill;
	private Sprite2D _progressMaskNode = new();
	private Sprite2D _progressNode = new();

	public override void _Ready()
	{
		ValueChanged += _ValueChanged;

		_progressMaskNode.Texture = _textureProgressMask;
		_progressMaskNode.ClipChildren = ClipChildrenMode.Only;
		this.AddChild(_progressMaskNode);
		_progressMaskNode.Owner = this;

		_progressNode.Texture = _textureProgressFill;
		_progressMaskNode.AddChild(_progressNode);

	}

	

    private void _ValueChanged(double newValue)
	{
		//move
		var maskTransform = _progressMaskNode.GlobalTransform;
		float transformDelta = (float)(_progressMaskNode.GetRect().Size.Y / (MaxValue - MinValue));
		_progressNode.GlobalTransform = maskTransform.Translated(new Vector2(0, -(float)newValue*transformDelta + _progressMaskNode.GetRect().Size.Y)); 
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
	private void _OnTextureSet(Sprite2D _textureNode, Texture2D texture)
	{
		GD.Print(_textureNode);
		if (texture == null)
		{
			// _textureNode?.Texture = null;
			_textureNode?.Hide();
			// _textureNode?.Dispose();
			_textureNode = null;
			return;
		}
		if (_textureNode == null)
			return;

		_textureNode.Texture = texture;
		_textureNode.Show();
	}

}

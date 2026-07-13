using Godot;
using System;
using System.Runtime.CompilerServices;

[Tool, GlobalClass]
public partial class ShiftingTextureProgressBar : Godot.Range
{
	public enum FillMode
	{
		BottomToTop, TopToBottom, LeftToRight, RightToLeft
	}
	[Export]
	public FillMode fillMode
	{
		get => _fillMode;
		set
		{
			_fillMode = value;
			// NotifyPropertyListChanged();
			_UpdateProgressBar();
		}
	}

	private FillMode _fillMode = FillMode.BottomToTop;
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
	private NinePatchRect _progressMaskNode = new();
	private NinePatchRect _progressNode = new();

	public override void _Ready()
	{
		
		Changed += _UpdateProgressBar;
		ValueChanged += _UpdateProgressBar;

		_CreateTextureNodes();
		_UpdateProgressBar();
	}
	
    public override void _ExitTree()
	{
		Changed -= _UpdateProgressBar;
		ValueChanged -= _UpdateProgressBar;
		base._ExitTree();
    }


    private void _CreateTextureNodes()
    {
        _progressMaskNode.Texture = _textureProgressMask;
        _progressMaskNode.ClipChildren = ClipChildrenMode.Only;
        this.AddChild(_progressMaskNode);
		_progressMaskNode.Owner = this;
		_progressMaskNode.SetAnchorsPreset(LayoutPreset.FullRect, true);

        _progressNode.Texture = _textureProgressFill;
        _progressMaskNode.AddChild(_progressNode);
		_progressNode.SetAnchorsPreset(LayoutPreset.FullRect, true);
	}

	private void _UpdateProgressBar()
    {
		_UpdateProgressBar(Value);
    }
	private void _UpdateProgressBar(double newValue)
	{
		//move
		Vector2 stepDelta = _progressMaskNode.GetRect().Size / (float)(MaxValue - MinValue);
		Vector2 modeProduct = new();

		switch (fillMode)
		{
			case FillMode.TopToBottom:
				modeProduct = new Vector2(0, -1);
				break;
			case FillMode.BottomToTop:
				modeProduct = new Vector2(0, 1);
				break;
			case FillMode.LeftToRight:
				modeProduct = new Vector2(-1, 0);
				break;
			case FillMode.RightToLeft:
				modeProduct = new Vector2(1, 0);
				break;
		}

		_progressNode.Position = (_progressMaskNode.GetRect().Size-(float)newValue*stepDelta)*modeProduct;
	}

	private void _OnTextureSet(NinePatchRect _textureNode, Texture2D texture)
	{
		GD.Print(_textureNode);
		if (texture == null)
		{
			_textureNode?.Hide();
			_textureNode = null;
			return;
		}
		if (_textureNode == null)
			return;

		_textureNode.Texture = texture;
		_textureNode.Show();
	}


}

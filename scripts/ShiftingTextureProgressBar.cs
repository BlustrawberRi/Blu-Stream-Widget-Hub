using System;
using Godot;

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
			UpdateProgressBar();
		}
	}

	private FillMode _fillMode = FillMode.BottomToTop;
	[ExportGroup("Textures")]
	[Export] public Texture2D TextureProgressMask
    {
		get => textureProgressMask;
        set
        {
			textureProgressMask = value;
			_OnTextureSet(_progressMaskNode, value);
		}
    }
    [Export] public Texture2D TextureProgressFill
    {
		get => textureProgressFill;
		set
		{
			textureProgressFill = value;
			_OnTextureSet(_progressNode, value);
		}
	}
    [Export] public Texture2D TextureUnder
	{
		get => textureUnder;
		set
		{
			textureUnder = value;
			_OnTextureSet(_underNode, value);
		}
	}
	[Export] public Texture2D TextureOver
	{
		get => textureOver;
		set
		{
			textureOver = value;
			_OnTextureSet(_overNode, value);
		}
	}

	private Texture2D textureProgressMask;
	private Texture2D textureProgressFill;
	private Texture2D textureUnder;
	private Texture2D textureOver;

	[ExportToolButton("Set Size To UnderTexture")] public Callable SetSizeButton => new(this, MethodName.SetSizeToTexture);

	[ExportGroup("Fill Options")]
	[ExportSubgroup("StretchMargin")]
	[Export(PropertyHint.None, "suffix:px")] public int Left { get => left; set { left = value;SetFillStretchMargin(); } }
    [Export(PropertyHint.None, "suffix:px")] public int Top { get => top; set { top = value; SetFillStretchMargin(); } }
	[Export(PropertyHint.None, "suffix:px")] public int Right { get => right; set { right = value; SetFillStretchMargin(); } }
	[Export(PropertyHint.None, "suffix:px")] public int Bottom { get => bottom; set { bottom = value;SetFillStretchMargin(); } }
	private int left = 0;
	private int top = 0;
	private int right = 0;
	private int bottom = 0;

	[ExportSubgroup("Margin")]
	[Export(PropertyHint.Range, "-100,100,or_greater,or_less,suffix:%")] public float MarginLeft { get => marginLeft; set { marginLeft = value;SetMargin(); } }
	[Export(PropertyHint.Range, "-100,100,or_greater,or_less,suffix:%")] public float MarginTop { get => marginTop; set { marginTop = value;SetMargin(); } }
	[Export(PropertyHint.Range, "-100,100,or_greater,or_less,suffix:%")] public float MarginRight { get => marginRight; set { marginRight = value;SetMargin(); } }
	[Export(PropertyHint.Range, "-100,100,or_greater,or_less,suffix:%")] public float MarginBottom { get => marginBottom; set { marginBottom = value; SetMargin(); } }
	private float marginLeft = 0;
	private float marginTop = 0;
	private float marginRight = 0;
	private float marginBottom = 0;

	private NinePatchRect _progressMaskNode ;
	private NinePatchRect _progressNode;
	private NinePatchRect _underNode;
	private NinePatchRect _overNode;

	public void SetSizeToTexture()
    {
		var texSize = textureUnder.GetSize();
		SetDeferred(PropertyName.Size, texSize);
		UpdateProgressBar();
		SetMargin();
    }

	public override void _EnterTree()
	{
		_CreateTextureNodes();
		UpdateProgressBar();

		SetFillStretchMargin(); // todo just when we change margin
		SetMargin();

		Changed += UpdateProgressBar;
		ValueChanged += UpdateProgressBar;
		Resized += SetMargin;

	}
	
    public override void _ExitTree()
	{
		GD.Print("exiting");
		Changed -= UpdateProgressBar;
		ValueChanged -= UpdateProgressBar;
		Resized -= SetMargin;

		_overNode.QueueFree();
		_underNode.QueueFree();
		_progressNode.QueueFree();
		_progressMaskNode.QueueFree();

		base._ExitTree();
    }


	private void _CreateTextureNodes()
	{
		_progressNode = new();
		_progressMaskNode = new();
		_underNode = new();
		_overNode = new();

		_underNode.Texture = TextureUnder;
		_underNode.Name = "UnderTexture";
		this.AddChild(_underNode);
		// _underNode.Owner = GetTree().EditedSceneRoot;
		_underNode.SetAnchorsPreset(LayoutPreset.FullRect, true);

		_progressMaskNode.Texture = TextureProgressMask;
		_progressMaskNode.Name = "MaskTexture";
		_progressMaskNode.ClipChildren = ClipChildrenMode.Only;
		this.AddChild(_progressMaskNode);
		// _progressMaskNode.Owner = GetTree().EditedSceneRoot;
		_progressMaskNode.SetAnchorsPreset(LayoutPreset.FullRect, true);
	
		_progressNode.Texture = TextureProgressFill;
		_progressMaskNode.AddChild(_progressNode);
		_progressNode.SetAnchorsPreset(LayoutPreset.FullRect, true);

		_overNode.Texture = TextureOver; 
		_overNode.Name = "OverTexture";
		this.AddChild(_overNode);
		// _overNode.Owner = GetTree().EditedSceneRoot;
		_overNode.SetAnchorsPreset(LayoutPreset.FullRect, true);

	}

	private void UpdateProgressBar()
    {
		UpdateProgressBar(Value);
    }
	private void UpdateProgressBar(double newValue)
	{
		if(_progressNode == null) 
			return;
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

		_progressNode.Position = (_progressMaskNode.GetRect().Size - (float)newValue * stepDelta) * modeProduct;

	}

    private void SetMargin()
	{
		if (_progressMaskNode == null)
			return;

		_progressMaskNode.SetDeferred(PropertyName.Size, Size - new Vector2(Size.X / 100 * (MarginLeft + MarginRight), Size.Y / 100 * (MarginTop + MarginBottom)));
		_progressMaskNode.Position = new Vector2(Size.X / 100 * MarginLeft, Size.Y / 100 * MarginTop);
    }

    private void _OnTextureSet(NinePatchRect _textureNode, Texture2D texture)
	{
		if (_textureNode == null) //shouldnt happen?
			return;

		_textureNode.Texture = texture;
		//_textureNode.Show();
	}

	private void SetFillStretchMargin()
	{
		if (_progressMaskNode == null)
			return;

		_progressNode.PatchMarginLeft = Left;
		_progressNode.PatchMarginRight = Right;
		_progressNode.PatchMarginTop = Top;
		_progressNode.PatchMarginBottom = Bottom;

		_progressMaskNode.PatchMarginLeft = Left;
		_progressMaskNode.PatchMarginRight = Right;
		_progressMaskNode.PatchMarginTop = Top;
		_progressMaskNode.PatchMarginBottom = Bottom;
	}

}

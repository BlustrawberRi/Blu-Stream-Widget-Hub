using Godot;
using System;

public partial class SpeechBubble : PanelContainer
{
    [Export] RichTextLabel textLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		if (textLabel == null) {
            GD.PushWarning("Speech Bubble Text label has not been set for " + this);
        }
    }


}

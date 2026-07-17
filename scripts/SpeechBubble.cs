using Godot;
using System;
using System.Threading.Tasks;

public partial class SpeechBubble : PanelContainer
{
    [Export] AudioStreamPlayer TalkSoundPlayer;
    [Export] RichTextLabel textLabel;
    [Export(PropertyHint.Range, "0,100,suffix:chars/s")] float textSpeed;
    [Export(PropertyHint.Range, "0,100,suffix:chars")] int talkSpeed;


    [Signal] private delegate void TalkFinishedEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (textLabel == null)
        {
            GD.PushWarning("Speech Bubble Text label has not been set for " + this);
        }
    }

    

    /// <summary>
    /// Display text in the Speachbubble.
    /// </summary>
    /// <param name="dialogueText">What should be said in the bubble. Can be RichText.</param>
    /// <param name="delay">In seconds. How long textbubble should wait for next bubble.</param>
    public async void Talk(string dialogueText, float delay, bool withSound = true)
    {
        textLabel.Text = dialogueText;
        if (textSpeed != 0)
            for (int i = 0; i <= textLabel.GetTotalCharacterCount(); i++)
            {
                textLabel.VisibleCharacters = i;
                await ToSignal(GetTree().CreateTimer(1 / textSpeed), SceneTreeTimer.SignalName.Timeout);
                if (i%talkSpeed==1 && withSound)
                {
                    TalkSoundPlayer.PitchScale = (float)GD.RandRange(0.7f, 1.3f);
                    TalkSoundPlayer.Play();
                }
            }
            
        await ToSignal(GetTree().CreateTimer(delay), SceneTreeTimer.SignalName.Timeout);
        EmitSignal(SignalName.TalkFinished);
    }

}

using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

public partial class MilkMerchantWidget : StreamerWidget
{
    [Export] AnimationPlayer animationPlayer;
    [Export(PropertyHint.File, "*.json")] string milkDefinitionJson;

    public bool IsActive = false;
    private Dictionary choices = new();
    private bool waitForChoice = false;
    [Export] private Array<string> customers = new();
    public override Enum[] StreamerbotEventRequests
    {
        get
        {
            return new Enum[]{
                //StreamerbotEventTypes.Command.Triggered,
                StreamerbotEventTypes.Twitch.Raid,
                StreamerbotEventTypes.Twitch.RewardRedemption,
                StreamerbotEventTypes.Twitch.ChatMessage //todo: subscribe dynamically
            };

        }
        set { }
    }

    [Signal] public delegate void DialogueChangedEventHandler(string text);
    [Signal] public delegate void TriggeredEventHandler();
    [Signal] public delegate void FinishedEventHandler();

    [Signal] public delegate void ChoiceMadeEventHandler();

    public override async void _Ready()
    {
        if (milkDefinitionJson == null)
        {
            GD.PushWarning("No Definition file for the milk merchant was selected.");
            await PlayIntro(null, "Sorry, we are currently out of Milk.");
            return;
        }

        var json = Godot.FileAccess.GetFileAsString(milkDefinitionJson);
        choices = Json.ParseString(json).AsGodotDictionary().GetValueOrDefault("choices").AsGodotDictionary();

        if (choices == null)
        {
            GD.PushWarning("Milk Definition File was empty.");
            await PlayIntro(null, "Sorry, we are currently out of Milk.");
        }

        base._Ready();
    }

    public override void OnEventDataReceived(string source, string type, Dictionary data)
    {
        if (type == "RewardRedemption")
        {
            RewardRedemption rr = new(data);
            if (rr.RewardName == "Get some Milk")
            {
                customers.Add(rr.UserLogin);

                if (!IsActive)
                    PlayIntro(rr.UserLogin);
            }
        }
        if (waitForChoice && type == "ChatMessage")
        {
            //GD.Print(data.ToString().Replace(",", ",\n").Replace("{", "{\n\t"));
            var msg_data = data.GetValueOrDefault("message").AsGodotDictionary();
            string user = msg_data.GetValueOrDefault("username").ToString();

            if (customers.Count != 0 && user == customers[0])
            {
                string message = msg_data.GetValueOrDefault("message").ToString();

                OnMilkChoice(message);

            }
        }
    }


    public async void Deactivate()
    {
        Talk("CU :3");
        await ToSignal(GetTree().CreateTimer(3), SceneTreeTimer.SignalName.Timeout);
        customers.RemoveAt(0);

        if (customers.Count == 0) {
            animationPlayer?.Play("turn_off");
            await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
            animationPlayer?.Play("RESET");
            IsActive = false;
            return;
        }

        await PlayIntro(customers[0]);
    }

    private async Task PlayIntro(string userName, string alternateGreeting = null)
    {
        string greeting = alternateGreeting ?? "Hello " + userName + "! What type of milk would you like?";
        Talk( greeting);

        if (!IsActive)
        {
            IsActive = true;
            EmitSignal(SignalName.Triggered);
            animationPlayer?.Play("turn_on");
            await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        }
        animationPlayer?.Play("dialogue_on");
        await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        waitForChoice = true;
    }

    private async void OnMilkChoice(string message)
    {

        foreach (var choice in choices)
        {
            GD.Print(choice);
            if (choice.Key.AsString() == message)
            {
                waitForChoice = false;
                Talk(choice.Value.AsString());
                await ToSignal(GetTree().CreateTimer(5), SceneTreeTimer.SignalName.Timeout);
                Deactivate();
                return;
            }
        }

        Talk("Wasn't a choice, buddy, please select from the menu...");

    }


    private void Talk(string dialogue)
    {
        EmitSignal(SignalName.DialogueChanged, dialogue);
        //todo: await text change
    }

}

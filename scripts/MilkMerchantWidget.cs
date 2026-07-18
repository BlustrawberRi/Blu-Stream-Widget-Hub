using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using SB.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public partial class MilkMerchantWidget : StreamerWidget
{
    [Export(PropertyHint.Range, "0,60,suffix:s")] int RaidWaitTime = 5;
    [Export] AnimationPlayer animationPlayer;
    [Export] Container milkChoicesContainer;
    [Export(PropertyHint.File, "*.json")] string milkDefinitionJson;

    public bool IsActive = false;
    private Dictionary choices = new();
    private bool waitForChoice = false;
    [Export] public Array<string> customers = new();
    public string CurrentCustomer { get; set; }

    public override EventType[] StreamerBotEventRequests
    {
        get => new[] { Twitch.Raid, Twitch.RewardRedemption, Twitch.ChatMessage };// TODO: subscribe dynamically 
        set { }
    }

    [Signal] public delegate void DialogueChangedEventHandler(string text);
    [Signal] public delegate void TriggeredEventHandler();
    [Signal] public delegate void FinishedEventHandler();
    [Signal] public delegate void ChoiceMadeEventHandler();

    public override async void _Ready()
    {
        var shop = this.GetNode<Control>("Shop");
        shop.Visible = false;
        await LoadMilkChoices();
        FillMenu();

        base._Ready();
    }

    private async Task LoadMilkChoices()
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
    }
    private void FillMenu()
    {
        if (milkChoicesContainer == null) {
            GD.PushWarning("Milk choices can't be displayed, due to missing reference to a GUI container.");
            return;
        }

        int i = 1;
        foreach (var choice in choices){
            RichTextLabel label = new();
            label.Text = i +" - "+ choice.Key.ToString();
            label.Name = choice.Key.ToString() + "Label";
            label.FitContent = true;
            label.BbcodeEnabled = true;

            milkChoicesContainer.AddChild(label);
            i++;
        }
    }

    public override async void OnEventDataReceived(EventType type, Dictionary data)
    {
        switch (type.Name) 
        {
            case "ChatMessage":
                if (!waitForChoice)
                    return;
                //GD.Print(data.ToString().Replace(",", ",\n").Replace("{", "{\n\t"));
                var msg_data = data.GetValueOrDefault("message").AsGodotDictionary();
                string user = msg_data.GetValueOrDefault("username").ToString();

                if (customers.Count != 0 && user == customers[0])
                {
                    string message = msg_data.GetValueOrDefault("message").ToString();

                    OnMilkChoice(message);
                }
                return;

            case "Raid":
                string userName = data.GetValueOrDefault("from_broadcaster_user_login").ToString();
                if (userName == "") return;
                await ToSignal(GetTree().CreateTimer(RaidWaitTime), SceneTreeTimer.SignalName.Timeout);
                customers.Add(userName);
                break;

            case "RewardRedemption":
                RewardRedemption rr = new(data);
                if (rr.RewardName == "Get some Milk")
                {
                    customers.Add(rr.UserLogin);
                }
                break;
                
            default:
                return;
        
        }

        if (!IsActive)
            Activate();
    }

    public async Task Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            EmitSignal(SignalName.Triggered);
            Talk("");
            animationPlayer?.Play("turn_on");
            await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        }

        CurrentCustomer = customers[0] ?? "";
        await PlayIntro();
    }

    public async Task Deactivate()
    {
        animationPlayer?.Play("turn_off");
        await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        animationPlayer?.Play("RESET");
        IsActive = false;
        EmitSignal(SignalName.Finished);
    }

    private async Task PlayIntro(string customer = null, string alternateGreeting = null)
    {
        if (customer == null)
            customer = CurrentCustomer;
        if (customer == "")
            customer = "Blu";
        
        //Todo check for usernames and give custom greetings

        string greeting = alternateGreeting ?? "Hello " + customer + "! What type of milk would you like?";
        
        animationPlayer?.Play("dialogue_on");
        await ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        Talk(greeting);

        waitForChoice = true;
    }

    public async void PlayOutro()
    {
        Talk("See you :3", 2);
        customers.RemoveAt(0);

        if (customers.Count == 0)
        {
            await Deactivate();
            return;
        }

        PlayIntro(customers[0]);
    }


    private async void OnMilkChoice(string message)
    {
        string user_choice = "";
        // numbers to right choice
        foreach (var choice in choices)
        {
            if (Regex.Match(message, choice.Key.AsString()).Success)
            {
                user_choice = choice.Key.AsString();
                break;
            }
        }
        if (user_choice=="" && message.IsValidInt())
        {
            int choice_num = message.ToInt();
            user_choice = choices.Keys.ElementAt(choice_num - 1).AsString();
        }

        try
        {
            string answer = choices[user_choice].AsString();
            EmitSignal(SignalName.ChoiceMade);
            await Talk(answer);
            PlayOutro();
            return;
        } catch (Exception)
        {
            Talk("Wasn't a choice, buddy, please select from the menu [rainbow]"+CurrentCustomer+"[/rainbow]...");
        }

    }



    private async Task Talk(string dialogue, int talkTime = 3)
    {
        EmitSignal(SignalName.DialogueChanged, dialogue);
        
        await ToSignal(GetTree().CreateTimer(talkTime), SceneTreeTimer.SignalName.Timeout);
        //todo: await text change
    }

}

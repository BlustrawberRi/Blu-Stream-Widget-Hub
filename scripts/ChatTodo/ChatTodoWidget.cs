using Godot;
using Godot.Collections;
using SB.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;



/// <summary>
/// Receives the Todo and Finish Todo command from Streamer.Bot over commands.
/// </summary>
/// <remarks>
/// Data needed:
/// - <b>Command/Triggered</b>: command, message, user.display, user.id, user.type
/// </remarks>
public partial class ChatTodoWidget : StreamerWidget
{
    public override EventType[] StreamerBotEventRequests
    {
        get => new[] { Twitch.RewardRedemption, Command.Triggered };
        set { }
    }

    [Signal]
    public delegate void TodoReceivedEventHandler(string todo, ChatUser user, string timestamp);
    [Signal]
    public delegate void TodoDoneEventHandler(ChatUser user, string timestamp);

    public override void OnEventDataReceived(EventType type, Dictionary data)
    {
        ChatUser user = new();
        string cmd = "";
        string msg = "";

        switch (type.Name)
        {
            case "Triggered":
                CommandData command = new CommandData(data);
                msg = command.Message;
                cmd = command.Name;
                user = command.User;
                break;
        
            case "RewardRedemption":
                RewardRedemption red = new RewardRedemption(data);
                cmd = red.RewardName;
                msg = red.RawImput;
                user.Display = red.UserName;
                    user.Id = red.UserId;
                break;
        }



        if (cmd == "Add Todo") 
            OnNewTodoArrived(msg, user);
    
        else if (cmd == "Finish Todo")
            MarkTodoDone(user);

}

private void OnNewTodoArrived(String todo, ChatUser user)
{
    if (todo == "") return;
    AddNewTodo(todo, user);
}

public void AddNewTodo(String todo, ChatUser user)
{
    GD.PrintRich("Chat Todo Widget: [wave]New Todo from [b]" + user.Display + "[/b]: " + todo + "[/wave]");
    EmitSignal(SignalName.TodoReceived, todo, user, Time.GetDatetimeStringFromSystem());
}

private void MarkTodoDone(ChatUser user)
{
    if (user == null) return;
    EmitSignal(SignalName.TodoDone, user);
    GD.PrintRich("[rainbow]TODO DONE[/rainbow]");
}

}

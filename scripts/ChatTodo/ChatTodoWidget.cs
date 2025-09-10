using Godot;
using Godot.Collections;
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
    public override Enum[] StreamerbotEventRequests {
        get
        {
            return new Enum[]{
                    StreamerbotEventTypes.Command.Triggered};
        }
        set{}
    }

    [Signal]
    public delegate void TodoReceivedEventHandler(string todo, ChatUser user, string timestamp);
    [Signal]
    public delegate void TodoDoneEventHandler(ChatUser user, string timestamp);

    public override void OnEventDataReceived(string answer)
    {

        CommandData command = new CommandData();
        command.GetDataFromAnswer(answer);


        GD.Print(command.Name);
        if (command.Name == "Todo")
        {
            OnNewTodoArrived(command);
        }
        else if (command.Name == "Finish Todo")
            MarkTodoDone(command);

    }

    private void OnNewTodoArrived ( CommandData command)
    {
        var todo = command.Message;
        if (todo == "") return;

        var user = command.User;

        AddNewTodo(todo, user);
    }
    
    public void AddNewTodo(String todo, ChatUser user)
    {
        GD.PrintRich("Chat Todo Widget: [wave]New Todo from [b]"+user.Display+"[/b]: "+todo+"[/wave]");
        EmitSignal(SignalName.TodoReceived, todo, user, Time.GetDatetimeStringFromSystem());
    }

    private void MarkTodoDone(CommandData command)
    {
        var user = command.User;
        if (user == null) return;
        EmitSignal(SignalName.TodoDone, user);
        GD.PrintRich("[rainbow]TODO DONE[/rainbow]");
    }

}

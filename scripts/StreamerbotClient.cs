using Godot;
using System;
using Godot.Collections;

/// <summary>
/// A websocket client to connect to a streamer.bot instance. 
/// </summary>
[GlobalClass] [Icon("res://editor/icons/StreamerBotClient.svg")]
public partial class StreamerbotClient : WebsocketClient
{
    [Export]
    private Dictionary<String, Array<String>> eventRequestList = new Dictionary<string, Array<string>>();

    //private Dictionary<StreamerWidget, Dictionary<String, Array<String>>> WidgetEventRequests ;
    private Array<StreamerWidget> ConnectedWidgets = new Array<StreamerWidget>();


    /// <summary>
    /// Subscribe to a set of events from the connected Streamer.bot instance. The events are picked by <see cref="StreamerWidget"/> instances and requested over <see cref="AddEventRequests"/>.
    /// </summary>
    public void Subscribe()
    {
        var getEventsDic = new Dictionary
    {
        {"id", "godot-subEvent"},
        {"request", "Subscribe"},
        {"events", eventRequestList}
    };

        if (!SendToServer(Json.Stringify(getEventsDic, "\t")))
            GD.Print("Event Request could not be sent.");
    }

    /// <summary>
    /// Add a request for an Event the client should subscribe to. This will enable StreamerbotClient to send the corresponding answers from the connected streamer.bot instance to thw widget that requested the event.
    /// </summary>
    /// <param name="eventTypes">The events the widget wants data from. Choose from <see cref="StreamerBotEventTypes"/></param>
    /// <param name="widget">The widget requesting the event data. StreamerbotClient will send the answer to that widget.</param>
    public void AddEventRequests(Enum[] eventTypes, StreamerWidget widget)
    {
        GD.PrintRich("[b]" + widget.Name + " added Requests [/b]:");
        //var eventTypeList = new Godot.Collections.Array();
        foreach (var et in eventTypes)
        {
            GD.PrintRich("[rainbow]+[/rainbow] " + et.GetType().Name + " " + new Array<String>() { et.ToString() });

            bool hasEventSource = eventRequestList.ContainsKey(et.GetType().Name);
            bool hasEventType = hasEventSource ? eventRequestList[et.GetType().Name].Contains(et.ToString()) : false;
            if (hasEventSource)
            {
                if (!hasEventType)
                    eventRequestList[et.GetType().Name].Add(et.ToString());
            }
            else
            {
                if (!hasEventType)
                    eventRequestList.Add(et.GetType().Name, new Array<String>() { et.ToString() });
            }

        }

        ConnectedWidgets.Add(widget);
    }

    protected override void _OnConnectionEstablished()
    {
        base._OnConnectionEstablished();
        Subscribe();
    }

    protected override void _OnAnswerReceived(string answer)
    {
        if (answer == "") return;
        base._OnAnswerReceived(answer);
        _FilterAnswer(answer);
    }

    private void _FilterAnswer(string answer)
    {
        var answerDic = Json.ParseString(answer).AsGodotDictionary();
        if (!answerDic.ContainsKey("event")) return;

        foreach (var widget in ConnectedWidgets)
        {
            foreach (var eventType in widget.StreamerbotEventRequests)
            {
                if ((answerDic["event"].AsGodotDictionary())?["source"].AsString() != eventType.GetType().Name)
                    continue;

                GD.Print("Answer is " + eventType.GetType().Name + "/" + eventType.ToString());
                widget.OnEventDataReceived(answer);
                break;
            }
        }

    }

}


using Godot;
using System;
using Godot.Collections;
using System.Linq;
using System.Diagnostics.Tracing;
//using System.Collections.Generic;


[GlobalClass, Icon("res://editor/icons/StreamerbotClient.svg")]
/// <summary>
/// A websocket client to connect to a streamer.bot instance. 
/// </summary>
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
            AddEventRequest(et, widget);
        }

        if (!ConnectedWidgets.Contains(widget))
            ConnectedWidgets.Add(widget);
    }

    public void AddEventRequest(Enum eventType, StreamerWidget widget)
    {
        GD.PrintRich("[rainbow]+[/rainbow] " + eventType.GetType().Name + " " + new Array<String>() { eventType.ToString() });

        bool hasEventSource = eventRequestList.ContainsKey(eventType.GetType().Name);
        bool hasEventType = hasEventSource && eventRequestList[eventType.GetType().Name].Contains(eventType.ToString());
        if (hasEventSource)
        {
            if (!hasEventType)
                eventRequestList[eventType.GetType().Name].Add(eventType.ToString());
        }
        else
        {
            if (!hasEventType)
                eventRequestList.Add(eventType.GetType().Name, new Array<String>() { eventType.ToString() });
        }
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

    /// <summary>
    /// Goes through every Widget that is subscribed and look for a matching StreamerBot event that the answer has. Then calls this widget's ONEventDataReceived. 
    /// </summary>
    /// <param name="answer"></param>
    private void _FilterAnswer(string answer)
    {
        var answerDic = Json.ParseString(answer).AsGodotDictionary();
        if (!answerDic.ContainsKey("event")) return;

        string eventSourceStr = (answerDic["event"].AsGodotDictionary())?["source"].AsString();
        string eventTypeStr = (answerDic["event"].AsGodotDictionary())?["type"].AsString();

        GD.PrintRich("[wave]New " + eventSourceStr + "/" + eventTypeStr + " Event arrived![/wave]");
        foreach (var widget in ConnectedWidgets)
        {
            foreach (var eventType in widget.StreamerbotEventRequests)
            {
                // GD.Print("-> Widget:"+ widget.Name +":"+ eventType.GetType().Name+" "+ eventType.ToString());
                if (eventSourceStr != eventType.GetType().Name)
                    continue;
                if (eventTypeStr != eventType.ToString())
                    continue;

                GD.PrintRich("[b]Sending data to " + widget.Name + ".[/b]");

                Variant data = new Variant();
                answerDic.TryGetValue("data", out data);
                //GD.Print(data);

                widget.OnEventDataReceived(eventSourceStr, eventTypeStr, data.AsGodotDictionary());
                break;
            }
        }

    }

}


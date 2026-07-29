using Godot;
using System;
using Godot.Collections;
using SB.Events;


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
    private const string SUBSCRIBE_ID = "godot-subEvent-BluWiHu";
    private const string GET_EVENTS_ID = "godot-getEvents-BluWiHu";

    /// <summary>
    /// Subscribe to a set of events from the connected Streamer.bot instance. The events are picked by <see cref="StreamerWidget"/> instances and requested over <see cref="AddEventRequests"/>.
    /// </summary>
    public void Subscribe()
    {
        var getEventsDic = new Dictionary
        {
            {"id", SUBSCRIBE_ID},
            {"request", "Subscribe"},
            {"events", eventRequestList}
        };

        if (!SendToServer(Json.Stringify(getEventsDic, "\t")))
            GD.Print("Event Request could not be sent.");
    }

    public void GetEvents()
    {
        var getEventsDic = new Dictionary
        {
            {"id", GET_EVENTS_ID},
            {"request", "GetEvents"}
        };

        if (!SendToServer(Json.Stringify(getEventsDic, "\t")))
            GD.Print("GetEvents Request could not be sent.");
    }

    /// <summary>
    /// Add a request for an Event the client should subscribe to. This will enable StreamerbotClient to send the corresponding answers from the connected streamer.bot instance to thw widget that requested the event.
    /// </summary>
    /// <param name="eventTypes">The events the widget wants data from. Choose from <see cref="StreamerBotEventTypes"/></param>
    /// <param name="widget">The widget requesting the event data. StreamerbotClient will send the answer to that widget.</param>
    public void AddEventRequests(EventType[] eventTypes, StreamerWidget widget)
    {
        GD.PrintRich("[b]" + widget.Name + " added Requests [/b]:");
        if (eventTypes == null) return;
        foreach (var et in eventTypes)
        {
            AddEventRequest(et, widget);
        }

    }

    public void AddEventRequest(EventType eventType, StreamerWidget widget)
    {
        GD.PrintRich("\t[rainbow]+[/rainbow] " + eventType.FullName );

        bool hasEventSource = eventRequestList.ContainsKey(eventType.SourceName);
        bool hasEventType = hasEventSource && eventRequestList[eventType.SourceName].Contains(eventType.Name);
        if (hasEventSource)
        {
            if (!hasEventType)
                eventRequestList[eventType.SourceName].Add(eventType.Name); // add another event item to the List with the Source key
        }
        else
        {
            if (!hasEventType)
                eventRequestList.Add(eventType.SourceName, new Array<String>() { eventType.Name });
        }

        if (!ConnectedWidgets.Contains(widget))
            ConnectedWidgets.Add(widget);
    }

    protected override void _OnConnectionEstablished()
    {
        base._OnConnectionEstablished();
        Subscribe();
        // GetEvents();
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
        if (answerDic == null)
        {
            GD.PrintErr("Answer wasn't in the right format: \n" + answer);
            return;
        }
        if (answerDic.ContainsKey("id")){
            switch (answerDic["id"].ToString())
            {
                case SUBSCRIBE_ID:
                    break;
                case GET_EVENTS_ID:
                    _GetStreamerbotEvents(answerDic);
                    break;
            }
        }
        if (answerDic.ContainsKey("event"))
        {
            _ProcessEventData(answerDic);
        }

    }

    private void _GetStreamerbotEvents(Dictionary answerDic)
    {
        GD.PrintT(answerDic);
        // throw new NotImplementedException();
    }


    /// <summary>
    /// Process Data of streamerbot events
    /// </summary>
    /// <param name="answerDic"></param> <summary>
    /// 
    /// </summary>
    /// <param name="answerDic"></param>
    private void _ProcessEventData(Dictionary answerDic)
    {
        if (!answerDic.ContainsKey("event")) return;

        string eventSourceStr = (answerDic["event"].AsGodotDictionary())?["source"].AsString();
        string eventTypeStr = (answerDic["event"].AsGodotDictionary())?["type"].AsString();

        GD.PrintRich("[wave]New " + eventSourceStr + "/" + eventTypeStr + " Event arrived![/wave]");
        foreach (var widget in ConnectedWidgets)
        {
            if (_GetWidgetEventRequests(widget, eventSourceStr, eventTypeStr, out EventType eventType))
            {
                Variant data = new Variant();
                answerDic.TryGetValue("data", out data);
                GD.PrintRich("[b]Sending data to " + widget.Name + ".[/b]");
                widget.OnEventDataReceived(eventType, data.AsGodotDictionary());
            }
        }
    }


    private bool _GetWidgetEventRequests(StreamerWidget widget, string eventSourceStr, string eventTypeStr, out EventType eventType)
    {
        if (widget.StreamerBotEventRequests != null)
        foreach (var reqType in widget.StreamerBotEventRequests)
        {
            if (reqType.source.Name != eventSourceStr) continue;
            if (reqType.Name != eventTypeStr) continue;
            eventType = reqType;
            return true;
        }

        eventType = null;
        return false;
    }
}


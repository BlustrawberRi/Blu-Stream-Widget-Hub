using Godot;
using Godot.Collections;
using System;
using SB.Events;

/// <summary>
/// Baseclass for widgets who respond to <see cref="StreamerbotClient"/> event answers.
/// </summary>
[GlobalClass] [GodotClassName("StreamerWidget")]  [Icon("res://editor/icons/StreamerWidget.svg")]
public abstract partial class StreamerWidget : Node
{
    /// <summary>
    /// The client connected to the streamer.bot instance to receive event data from.
    /// </summary>
    [Export]
    public StreamerbotClient StreamerbotClient;

    

    /// <summary>
    /// The streamer.bot events whose data is needed.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    /// public override Enum[] StreamerbotEventRequests
    /// {
    ///     get
    ///     {
    ///         return new Enum[]{
    ///             StreamerbotEventTypes.Twitch.Raid,
    ///             StreamerbotEventTypes.Twitch.RewardRedemption,
    ///             StreamerbotEventTypes.Twitch.ChatMessage 
    ///         };
    ///     }
    ///     set { }
    /// } </code>
    /// </remarks>
    /// <value>Use the enums in <see cref="StreamerbotEventTypes"/>.</value>
    // public abstract Enum[] StreamerbotEventRequests { get; set; }
    public virtual EventType[] StreamerBotEventRequests { get; set; }

    //[Export] public Array<StreamerBotTrigger> StreamerbotEventRequest = new Array<StreamerBotTrigger>();
    //todo: if this is changed, resub to more streamer bot events

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        if (StreamerbotClient == null)
        {
            StreamerbotClient = GetNode<StreamerbotClient>("/root/SBClient");
            //StreamerbotClient = GetNode<StreamerbotClient>("../%StreamerbotClient");
        }

        RequestEvents();
    }


    /// <summary>
    /// Asks StreamerbotClient for event types to Subscribe to.
    /// </summary>
    protected void RequestEvents()
    {
        StreamerbotClient.AddEventRequests(StreamerBotEventRequests, this);
    }

    public void RequestEvent(EventType request) {
        if (StreamerbotClient == null)
        {
            GD.PushWarning("Widget missing reference to Streamerbot client.");
            return;
        }

        StreamerbotClient.AddEventRequest(request, this);
    }

    /// <summary>
    /// Work with event data as it arrives.
    /// </summary>
    /// <remarks>
    /// This method is triggered when <see cref="StreamerbotClient"/> receives the event data requested over <see cref="RequestEvents"/>.
    /// </remarks>
    /// <param name="message">A JSON string that has the event data.</param>
    // public abstract void OnEventDataReceived(string source, string type, Dictionary data);
    public abstract void OnEventDataReceived(EventType type, Dictionary data);


}

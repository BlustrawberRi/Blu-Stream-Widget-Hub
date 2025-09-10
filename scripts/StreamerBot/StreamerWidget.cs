using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

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
    /// <value>Use the enums in <see cref="StreamerbotEventTypes"/>.</value>
    public virtual Enum[] StreamerbotEventRequests { get ; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        if (StreamerbotClient == null) {
            StreamerbotClient = GetNode<StreamerbotClient>("../%StreamerbotClient");
        }

        RequestEvents();
    }


    /// <summary>
    /// Asks StreamerbotClient for event types to Subscribe to.
    /// </summary>
    protected void RequestEvents()
    {
        if (StreamerbotClient == null)
        {
            GD.PushWarning("Widget missing reference to Streamerbot client.");
            return;
        }
        StreamerbotClient.AddEventRequests(StreamerbotEventRequests, this);
    }

    /// <summary>
    /// Work with event data as it arrives.
    /// </summary>
    /// <remarks>
    /// This method is triggered when <see cref="StreamerbotClient"/> receives the event data requested over <see cref="RequestEvents"/>.
    /// </remarks>
    /// <param name="message">A JSON string that has the event data.</param>
    public virtual void OnEventDataReceived(string answer) {}


}

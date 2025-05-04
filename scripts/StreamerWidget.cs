using Godot;
using System;
using System.Linq;
using System.Reflection.Metadata;

public abstract partial class StreamerWidget : Node
{
    [Export]
    public StreamerbotClient StreamerbotClient;

    public abstract Enum[] StreamerbotEventRequests { get; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        RequestEvents();
    }

    
    /// <summary>
    /// Asks StreamerbotClient for EventTypes to Subscribe to.
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

    public abstract void OnAnswerReceived(string message);


}

using Godot;
using Godot.Collections;
using SB;
using SB.Events;
using System;

public partial class RaidParticleWidget : StreamerWidget
{
	[Export] public CpuParticles2D RaidEffect;
	// public override Enum[] StreamerbotEventRequests
	// {
	// 	get
	// 	{
	// 		return new Enum[]{
	// 			StreamerbotEventTypes.Twitch.Raid
	// 		};

	// 	}
	// 	set { }
	// }

    public override EventType[] StreamerBotEventRequests {
		get => new[] { Twitch.Raid };
		set { } 
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		base._Ready();
    }

	// public override void OnEventDataReceived(string source, string type, Dictionary data)
	public override void OnEventDataReceived(EventType type, Dictionary data)
	{
		var raid = new TwitchRaid(data);
		RaidEffect.Amount = raid.viewers;
		RaidEffect.Emitting = true;
	}
	

}

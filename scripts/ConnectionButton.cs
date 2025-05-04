using Godot;
using System;

public partial class ConnectionButton : Button
{
	[Export] public WebsocketClient StreamerBotConnection;

	private ConnectState connectState;
	private enum ConnectState {
		CONNECT, DISCONNECT
	}
	public override void _Ready()
	{
		if (StreamerBotConnection == null)
			GD.PushWarning(this.Name + " has no Streamer.bot Connection reference set! It will not work.");
	}
	
	public override void _Pressed()
	{
		GD.Print("Pressed!");
		if (ActionMode != ActionModeEnum.Release || StreamerBotConnection == null)
			return;
		
		switch (connectState)
		{
			case ConnectState.CONNECT:
				StreamerBotConnection.Connect();
				break;
			case ConnectState.DISCONNECT:
				StreamerBotConnection.Disconnect();
				break;
		}

	}

	public void SetConnectState(bool connect) 
	{
		this.Disabled = false;

		if (connect) {
			Text = "Connect";
			connectState = ConnectState.CONNECT;
			return;
		}
		
		Text = "Disconnect";
		connectState = ConnectState.DISCONNECT;

	}

	public void Disable()
	{
		Text = "Connecting...";
		this.Disabled = true;
	}

}

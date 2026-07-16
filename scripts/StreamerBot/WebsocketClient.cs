using Godot;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// Calls the Websocket (for StreamerBot) and receive data. 
/// You have to change the dictionary in SendRequest() to receive more data from Streamer.bot.
/// </summary>
public abstract partial class WebsocketClient : Node
{
	[Export] public string ip = "127.0.0.1";
	[Export] public int port = 8080;

	[Signal] public delegate void ConnectionRequestedEventHandler(); 
	[Signal] public delegate void ConnectionEstablishedEventHandler(); 
	[Signal] public delegate void ConnectionClosedEventHandler(); 
	[Signal] public delegate void AnswerReceivedEventHandler(String message);

	private bool waitingForConnection = false;

	private WebSocketPeer ws = new WebSocketPeer();

	private string lastMessage = "";

	public override void _Ready()
	{
		SetProcess(false);
		Connect();
	}

	public override void _Process(double delta)
	{
		ws.Poll();
		var state = ws.GetReadyState();
		if (state == WebSocketPeer.State.Open) 
		{
			if (waitingForConnection)
			{
                _OnConnectionEstablished();
            }
			while (ws.GetAvailablePacketCount() > 0)
            {
                _CollectPackets();
            }
            _OnAnswerReceived(lastMessage);
        }
		else if (state == WebSocketPeer.State.Closing) {
			
			GD.Print("Websocket closing...");
			return;
		}
		else if (state == WebSocketPeer.State.Closed)
		{
        	_OnConnectionClosed();	
		}
	}

    //Todo add timeout
    public void Connect() 
	{
		ws.ConnectToUrl("ws://" +ip+ ":" +port);
        _OnConnectionRequested();
	}

	public void Disconnect()
	{
		ws.Close();
    }

	/// <summary>
	/// Change getEventDic to receive other signals from Streamerbot.
	/// </summary>
	public bool SendToServer(String message)
	{
		var state = ws.GetReadyState();
		if (state != WebSocketPeer.State.Open) 
			return false;
		
		ws.SendText(message);
		
		GD.PrintRich("[b]Sending to "+ip+":"+port+":[/b] "+message);
        return true;
    }

    private void _CollectPackets()
    {
        lastMessage += ws.GetPacket().GetStringFromUtf8();
    }

    protected virtual void _OnConnectionRequested() 
	{
        GD.Print(this.Name + " connecting to StreamerBot...");
        waitingForConnection = true;
        SetProcess(true);
        EmitSignal(SignalName.ConnectionRequested);
	}

    protected virtual void _OnConnectionEstablished()
	{
        GD.PrintRich("[rainbow]Connection to Streamer Bot succesful![/rainbow]");
        waitingForConnection = false;
        EmitSignal(SignalName.ConnectionEstablished);
    }

    protected virtual void _OnConnectionClosed()
	{

        //var reason = ws.GetCloseReason();
        GD.Print("Websocket closed.");

        SetProcess(false);

        EmitSignal(SignalName.ConnectionClosed);
	}

    protected virtual void _OnAnswerReceived(string answer)
    {
        if (answer == "") return;

        //GD.PrintRich("\n- " + Json.Stringify(Json.ParseString(answer), "\t"));
		EmitSignal(SignalName.AnswerReceived, answer);

        lastMessage = "";
	}
}

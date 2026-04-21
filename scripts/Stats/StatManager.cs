using Godot;
using System;

public partial class StatManager : Node
{
	[Export] public StreamStat[] Stats;

	private static string StatSaveDir => "user://stats/"; //make this global

   
    public override void _EnterTree()
    {
        base._EnterTree();
		LoadStats();
	}

	public override void _ExitTree()
	{
		//SaveStats();
		base._ExitTree();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			SaveStats();
			//GetTree().Quit(); // default behavior
		}
	}


	public void LoadStats()
	{
		GD.Print("Loading Stats...");
		foreach (var Stat in Stats)
		{
			if (!Stat.saveAcrossSessions) continue;


			GD.Print("Loading " + Stat.StatName);
			string statSavePath = StatSaveDir + Stat.StatName + ".tres";
			if (!ResourceLoader.Exists(statSavePath, "StreamStat"))
			{
				GD.Print("There is no data for Stat " + Stat.StatName + " saved.");
				continue;
			}

			StreamStat loadedCopy = ResourceLoader.Load<StreamStat>(statSavePath, "StreamStat", ResourceLoader.CacheMode.Ignore).Duplicate(true) as StreamStat; //does duplicate nested sub-resources

			Stat.Value = loadedCopy.Value;
			Stat.MinValue = loadedCopy.MinValue;
			Stat.MaxValue = loadedCopy.MaxValue;
			Stat.StopAtMaxValue = loadedCopy.StopAtMaxValue;

		}
	}
	public void SaveStats()
	{
		GD.Print("Saving Stats...");
		foreach (var Stat in Stats)
		{
			if (!Stat.saveAcrossSessions) continue;

			GD.Print("Saving " + Stat.StatName);
			string statSavePath = StatSaveDir + Stat.StatName + ".tres";
			ResourceSaver.Save(Stat, statSavePath);
		}
    }

	/// <summary>
	/// Return the stream stat with the desired name.
	/// </summary>
	/// <param name="statName">Name of the stream stat.</param>
	/// <returns></returns>
	public StreamStat GetStat(string statName)
	{
		return null;
	}
}

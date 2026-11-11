using System;
using Godot;

[GlobalClass]
public partial class StreamStat : Resource
{
    [Export] public string StatName { get; set; }
    //private string SaveStatPath => "res://stats/" + StatName + ".tres"; //make this global

    [Export] public string SaveStatOverride;
    //"user://" + StatSaveDir + "\\" + Stat.StatName + ".tres";
    [Export]
    public int Value
    {
        get { return _value; }
        set
        {
            if (StopAtMaxValue && value > MaxValue)
                return;
            _value = StopAtMaxValue ?
            Math.Min(MaxValue, Math.Max(value, MinValue))
            : Math.Max(value, MinValue);

            GD.Print(this.StatName + " updated to " + _value);
            Save();

            EmitSignal(SignalName.ValueChanged, _value);
            EmitChanged();
            if (_value >= MaxValue)
            {
                EmitSignal(SignalName.StatMaxAchieved);
            }
        }
    }


    private int _value;
    [Export] public int MinValue = 0;
    [Export] public int MaxValue = 100;
    [Export] public bool StopAtMaxValue = false;
    [Export] public bool saveAcrossSessions = false;

    [Signal] public delegate void ValueChangedEventHandler(int value);

    [Signal] public delegate void StatMaxAchievedEventHandler();

    private void Save()
    {
        if (this.ResourcePath != "")
        {
            if (Engine.IsEditorHint())
                ResourceSaver.Save(this);

            if (saveAcrossSessions)
                ResourceSaver.Save(this, "user://" + SaveStatOverride + ResourceName + ".tres");
        }
    }

    // private void Load()
    // {
    //     if (FileAccess.FileExists(SaveStatPath))
    //     {
    //         ResourceLoader.Load(SaveStatPath).Duplicate(true);
    //     }
    // }

}




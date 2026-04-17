using System;
using Godot;

[GlobalClass]
public partial class StreamStat : Resource
{
    [Export] public string StatName { get; set; }
    //private string SaveStatPath => "res://stats/" + StatName + ".tres"; //make this global
    [Export]
    public int Value
    {
        get { return _value; }
        set
        {
            _value = StopAtMaxValue ?
            Math.Min(MaxValue, Math.Max(value, MinValue))
            : Math.Max(value, MinValue);

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
            ResourceSaver.Save(this);
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




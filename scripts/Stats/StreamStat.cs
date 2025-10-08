using System;
using Godot;


public partial class StreamStat : Resource
{
    [Export] public string StatName { get; set; }
    [Export] public int Value {
        get { return _value; }
        set
        {
            _value = StopAtMaxValue ?
            Math.Min(MaxValue, Math.Max(value, MinValue))
            : Math.Max(value, MinValue);

            if (this.ResourcePath != "")
            {
                ResourceSaver.Save(this);
            }

            EmitSignal(SignalName.ValueChanged, _value);
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

    [Signal] public delegate void ValueChangedEventHandler(int value);

    [Signal] public delegate void StatMaxAchievedEventHandler();

}




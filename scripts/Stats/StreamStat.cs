using Godot;


public partial class StreamStat : Resource
{
    [Export] public string StatName { get; set; }
    [Export] public int Value {
        get { return _value; }
        set
        {
            _value = value;
            EmitSignal(SignalName.ValueChanged, _value);
            if ( _value >= MaxValue)
                EmitSignal(SignalName.StatMaxAchieved);
        }
    }

    private int _value;
    [Export] public int MaxValue { get; set; }

    [Signal] public delegate void ValueChangedEventHandler(int value);

    [Signal] public delegate void StatMaxAchievedEventHandler();

}




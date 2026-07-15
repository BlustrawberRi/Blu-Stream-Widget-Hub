using Godot;
using System;
using System.Data;

/// <summary>
/// Injects the Stat Container GUI with values.
/// </summary>
public partial class StatContainer : Control
{
    [Export] public StreamStat Stat {
        get { return _stat; }
        set 
        {
            _stat = value;
            if (value == null)
                EmptyStatInfo();


            value.ValueChanged += OnStatValueChanged;
            UpdateStatInfo();

        }
    }


    private StreamStat _stat;

    public string StatName { get; set; }
    [Export] private RichTextLabel StatNameLabel;
    [Export] private Godot.Range StatProgressBar;   


    public override void _Ready()
    {
        base._Ready();

        // StatNameLabel = GetNode<RichTextLabel>("StatNameLabel");
        // StatProgressBar = GetNode<ProgressBar>("StatProgressBar");

        UpdateStatInfo();
    }


    private void UpdateStatInfo() 
    {
        if (_stat == null) {
            GD.PrintErr("Reference of StatContainer "+ this.Name +" is empty.");
            EmptyStatInfo();
            return;
        }

        UpdateLabelText(_stat.StatName);
        UpdateProgressBar(_stat.Value, _stat.MaxValue);

    }

    private void EmptyStatInfo()
    {
        UpdateLabelText("Empty");
        if (StatProgressBar != null)
        {
            //StatProgressBar.Indeterminate = true;
        }
    }

    private void UpdateLabelText(string text) {
        if (StatNameLabel == null)
            return;
        StatNameLabel.Text = text;
        // else GD.PushWarning(this + " has no Label yet to update text.");
    }

    private void UpdateProgressBar(int value, int maxValue) {
        if (StatProgressBar == null)
            return;
        
        StatProgressBar.MaxValue = maxValue;
        StatProgressBar.Value = value;
        
    }

    private void OnStatValueChanged(int value)
    {
        UpdateStatInfo();
    }
    
}

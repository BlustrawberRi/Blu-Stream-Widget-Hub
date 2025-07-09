using Godot;
using System;
using System.Data;

public partial class StatContainer : HBoxContainer
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
    private Label StatNameLabel;
    private ProgressBar StatProgressBar;   


    public override void _Ready()
    {
        base._Ready();

        StatNameLabel = GetNode<Label>("StatNameLabel");
        StatProgressBar = GetNode<ProgressBar>("StatProgressBar");

        UpdateStatInfo();
    }


    private void UpdateStatInfo()
    {
        if (_stat == null) {
            GD.PrintErr("StreamStat reference is empty. Will not auto fill data.");
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
            StatProgressBar.Indeterminate = true;
        }
    }

    private void UpdateLabelText(string text) {
        if (StatNameLabel != null)
            StatNameLabel.Text = text;
        else GD.Print(this + " has no Label yet.");
    }

    private void UpdateProgressBar(int value, int maxValue) {
        if (StatProgressBar != null)
        {
            if(StatProgressBar.Indeterminate)
                StatProgressBar.Indeterminate = false;
            StatProgressBar.MaxValue = maxValue;
            StatProgressBar.Value = value;
        }
    }

    private void OnStatValueChanged(int value)
    {
        UpdateStatInfo();
    }
    
}

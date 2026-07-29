using Godot;
using System;
using System.Threading.Tasks;

public partial class ProductivityGoalWidget : StatContainer
{
	[Export] public CpuParticles2D MaxEffect;
	[Export] public RichTextLabel label;
	[Export] public int visibleTime;
	private SceneTreeTimer timer;
	public override void _Ready()
    {
		Visible = false;

		Stat.Changed += OnStatChanged;
		Stat.StatMaxAchieved += OnStatMaxAchieved;

    }

	private void OnStatChanged()
	{
		CelebrateStatChanged();
		ShowNumber();
	}

    private void ShowNumber()
    {
		if (label == null)
			return;

		label.Text = Stat.Value+"PP /"+Stat.MaxValue;
    }

    private void OnStatMaxAchieved()
	{
		CelebrateStatChanged();
	}

	new public async Task Show()
	{
		Modulate = new Color(Modulate, a: 0);
		Visible = true;
		for (float i = 0; i <= 1; i += 0.1f)
		{
			Modulate = new Color(Modulate, a: i);
			await ToSignal(GetTree().CreateTimer(0.1), SceneTreeTimer.SignalName.Timeout);
		}

	}

	new public async Task Hide()
	{
		for (float i = 1; i >= 0; i -= 0.1f)
		{
			Modulate = new Color(Modulate, a: i);
			await ToSignal(GetTree().CreateTimer(0.1), SceneTreeTimer.SignalName.Timeout);
		}
		Visible = false;
		Modulate = new Color(Modulate, a: 1);
	}

	public async Task CelebrateStatChanged()
	{
		if (this.Visible)
        {
			timer.TimeLeft = visibleTime;
			MaxEffect.Amount = Stat.Value;
			MaxEffect.Emitting = true;
			return;
        }
		var showTask = Show();
		await showTask;
		MaxEffect.Visible = true;
		MaxEffect.Amount = Stat.Value;
		MaxEffect.Emitting = true;
		await new SignalAwaiter(MaxEffect, CpuParticles2D.SignalName.Finished, this);
		timer = GetTree().CreateTimer(visibleTime);
		await ToSignal(timer, SceneTreeTimer.SignalName.Timeout);
		Hide();
	}
}

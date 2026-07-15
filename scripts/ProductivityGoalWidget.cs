using Godot;
using System;
using System.Threading.Tasks;

public partial class ProductivityGoalWidget : StatContainer
{
	[Export] public CpuParticles2D MaxEffect;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		Visible = false;
		Stat.Changed += OnStatChanged;
		Stat.StatMaxAchieved += OnStatMaxAchieved;
    }

	private void OnStatChanged()
	{
		CelebrateStatChanged();
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
	public async Task CelebrateStatChanged()
    {
		await Show();
		MaxEffect.Visible = true;
		MaxEffect.Amount = Stat.Value;
		MaxEffect.Emitting = true;
    }
}

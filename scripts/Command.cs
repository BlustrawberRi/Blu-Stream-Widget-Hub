public class Command {
	public string name;
	public string param;
	public string userDisplayName;

	public Command (string name, string param, string userDisplayName)
	{
		this.name = name;
		this.param = param;
		this.userDisplayName = userDisplayName;
	}
	private Command() {}
} 

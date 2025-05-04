using Godot;


public partial class ChatUser : GodotObject
{
    public int id;
    public string display;
    public UserType type;

    public enum UserType
    {
        casual = 1,
        sub = 2,
        mod = 3,
        owner = 4
    }

}


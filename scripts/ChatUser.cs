using Godot;


public partial class ChatUser : GodotObject
{
    public int Id { get; set; }
    public string Display { get; set; }
    public UserType Type { get; set; }

    public string Subscribed { get;  set; }

    public enum UserType
    {
        casual = 1,
        sub = 2,
        mod = 3,
        owner = 4
    }

}


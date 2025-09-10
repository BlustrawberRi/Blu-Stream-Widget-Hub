using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Adds items to the todo list.
/// </summary>
public partial class TodoItemGenerator : Node
{
	[Export] public PackedScene todoItemContainer;
    //[Export] public Container todoListContainer;

    [Signal]
    public delegate void TodoNodeCreatedEventHandler(Node todoNode);

	private List<TodoItemContainer> todoItems = new List<TodoItemContainer>();

	
	public void UpdateTodoList(string todoText, ChatUser user, string timestamp)
	{
        var existingTodo = FindOpenTodoItemByUser(user);
        if (existingTodo == null) {
            CreateTodoItem(todoText, user, timestamp);
		}
		else {
            existingTodo.UpdateText(user.Display, todoText);
			//todo: Emit Signal TodoUpdated
		}
	}

	public void CreateTodoItem(string todoText, ChatUser user, string timestamp) 
	{
		TodoItemContainer instance = todoItemContainer.Instantiate<TodoItemContainer>();
        GD.Print(instance.Name);

		instance.UpdateText(user.Display,todoText);
        EmitSignal(SignalName.TodoNodeCreated, instance);
        todoItems.Add(instance);
        this.GetParent().AddChild(instance);
    }

	public void ToggleDone(ChatUser user)
	{
        GD.Print("done");
        FindOpenTodoItemByUser(user)?.SetDone();
		// todo: check if todo is already marked done, because user might have more than one
	}

	private TodoItemContainer FindOpenTodoItemByUser(ChatUser user)
	{
		var existingTodo = todoItems.Find( i => i.userName == user.Display && !i.isDone );
		return existingTodo;
	}


}

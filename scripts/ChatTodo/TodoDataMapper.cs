using Godot;
using System;
using System.Collections.Generic;

public partial class TodoDataMapper : Node
{
	[Export] public PackedScene todoItemContainer;
	[Export] public Container todoListContainer;

	private List<TodoItemContainer> todoItems = new List<TodoItemContainer>();

	
	public void GenerateTodoItem(string todoText, ChatUser user, string timestamp) 
	{
		
		var existingTodo = FindOpenTodoItemByUser(user);
		if (existingTodo != null)
		{
			existingTodo.SetTodo(todoText);
			return;
		}
		

		TodoItemContainer instance = todoItemContainer.Instantiate<TodoItemContainer>();
		todoListContainer.AddChild(instance);

		todoItems.Add(instance);

		instance.SetUserName(user.Display);
		instance.SetTodo(todoText);

	}

	public void ToggleDone(ChatUser user)
	{
		FindOpenTodoItemByUser(user)?.SetDone();
		// todo: check if todo is already marked done, because user might have more than one
	}

	private TodoItemContainer FindOpenTodoItemByUser(ChatUser user)
	{
		var existingTodo = todoItems.Find( i => i.userName == user.Display && !i.isDone );
		return existingTodo;
	}


}

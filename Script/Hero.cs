using Godot;
using System;
using Fsm.Script;

public class Hero : KinematicBody2D
{
	int grav = 20;
	Vector2 velocity = new Vector2();
	int maxFallSpeed = 100;
	StateMachine<Hero> machine;

	public override void _Ready()
	{
		machine = new StateMachine<Hero>(this);
		StateManager<Hero> manager = machine.GetManager();
		manager.StateNewInstanse("idle", 0)
			.SetOnStateEnter(hero => Console.WriteLine("enter idle"))
			.SetStateLogic(hero => 
			{
				Console.WriteLine("test");
				velocity.y = Math.Min(maxFallSpeed, velocity.y += grav);
				hero.MoveAndSlide(velocity, Vector2.Up);
			})
			.SetOnStateExit(hero => Console.WriteLine("exit idle"));
		manager.StateNewInstanse("fall", 1)
			.SetOnStateEnter(hero => Console.WriteLine("enter fall"))
			.SetStateLogic(hero =>
			{
				velocity.y = Math.Min(maxFallSpeed, velocity.y += grav);
				hero.MoveAndSlide(velocity, Vector2.Up);
			})
			.SetOnStateExit(hero => Console.WriteLine("exit fall"))
			.SetEnterCondition(hero => velocity.y >= 0 && IsOnFloor());
		//manager.StateNewInstanse("run", 2);
		//manager.StateNewInstanse("jump", 3);
	}

	public override void _PhysicsProcess(float delta)
	{
		machine.Run();
	}
}

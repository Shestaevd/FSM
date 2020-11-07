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
			.SetStateLogic(hero => 
			{
				velocity.y = Math.Min(maxFallSpeed, velocity.y += grav);
				hero.MoveAndSlide(velocity, Vector2.Up);
			});
		manager.StateNewInstanse("fall", 1)
			.SetStateLogic(hero =>
			{
				velocity.y = Math.Min(maxFallSpeed, velocity.y += grav);
				hero.MoveAndSlide(velocity, Vector2.Up);
			})
			.SetEnterCondition(hero => velocity.y >= 0);
		//manager.StateNewInstanse("run", 2);
		//manager.StateNewInstanse("jump", 3);
	}

	public override void _PhysicsProcess(float delta)
	{
		machine.Run();
	}
}

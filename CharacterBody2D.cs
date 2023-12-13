using Godot;
using System;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float acceletation = 4.0f;
	public const float rotationSpeed = 5f;
	public float flightSpeed = 0f;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		// Adds friction
		flightSpeed = Mathf.MoveToward(flightSpeed, 0, acceletation / 3f);

		if (Input.IsActionPressed("ui_right")) 
			Rotation += Mathf.DegToRad(rotationSpeed);
		if (Input.IsActionPressed("ui_left")) 
			Rotation -= Mathf.DegToRad(rotationSpeed);
		if (Input.IsActionPressed("ui_up"))
			flightSpeed += acceletation;
		if (Input.IsActionPressed("ui_down"))
			flightSpeed -= acceletation;
		
		Vector2 velocity = Vector2.FromAngle(Rotation + Mathf.DegToRad(-90));
		GD.Print("Прошлая скорость: " + flightSpeed);
		GD.Print("Новая скорость: " + Velocity.Length());
		GD.Print("До: " + velocity);
		GD.Print(flightSpeed);
		
		velocity *= flightSpeed;

		GD.Print("После: " + velocity);
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta * 10;

		Velocity = velocity;
		MoveAndSlide();
	}
}

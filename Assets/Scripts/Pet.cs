using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pet : MonoBehaviour
{
	// Member variables
	private int base_speed = 100;
    private float speed_modifier = 2.0f;
    private Stats pet_stats;
    private AudioClip eat_sfx;
    private AudioClip deny_sfx;
    private bool can_eat;
    private Interaction next_food;
	private float speed;
    private float rng;
    private float velocity;
    private State current_state;
	private float timer;
	private float wait_time;

	// Constants
	const float WAIT_MAX = 4.0f;

	// Enums
	public enum State { IDLE, WALK, EAT }

	// Core functions
	private void Start()
	{
		speed = base_speed * speed_modifier
		rng = RandomNumberGenerator.new()
		velocity = Vector2(rng.randf_range(-1.0, 1.0), rng.randf_range(-1.0, 1.0))
		current_state = State.IDLE
		pet_stats = Stats.new()
		eat_sfx = preload("res://audio/sfx/eat.mp3")
		eat_sfx.loop = true
		deny_sfx = preload("res://audio/sfx/denied.wav")
		print(eat_sfx)
		timer = 0
		wait_time = rng.randf_range(1.0, WAIT_MAX)
	}

	private void Update()
	{
		match current_state:
			State.IDLE:
				can_eat = true
				$AnimatedSprite2D.animation = "idle"
				$AnimatedSprite2D.speed_scale = 1.0 * speed_modifier
				$AnimatedSprite2D.play()

				_update_timer(_get_next_state(), delta)
			State.WALK:
				$AnimatedSprite2D.animation = "walk"


				if velocity.length() > 0:
					# TODO: play walk SFX
					$AnimatedSprite2D.play()
					$AnimatedSprite2D.speed_scale = 1.0 * speed_modifier
				else:
					$AnimatedSprite2D.stop()
					$AnimatedSprite2D.speed_scale = 1.0


				if velocity.x < 0:
					$AnimatedSprite2D.flip_h = true
				else:
					$AnimatedSprite2D.flip_h = false


				_update_timer(State.IDLE, delta)
			State.EAT:
				if !$PetSfx.playing && can_eat:
					can_eat = false
					$PetSfx.stream = eat_sfx
					$PetSfx.pitch_scale = 1.0 * speed_modifier
					$PetSfx.play()
					_reset_timer(eat_sfx.get_length())
			
				$AnimatedSprite2D.animation = "eat"
				$AnimatedSprite2D.speed_scale = 1.0
				$AnimatedSprite2D.play()

				_update_timer(State.IDLE, delta)
		# end match statement
	
		_update_stats()
	}

	private void FixedUpdate()
	{
		if current_state == State.WALK
		{
			speed = base_speed * speed_modifier
			velocity = velocity.normalized() * speed


			var collision_info = move_and_collide(velocity * delta)
			if collision_info:
				velocity = velocity.bounce(collision_info.get_normal())
		}
	}

	// Other functions	
	public void _update_stats()
	{
		/* TODO: compare current stat to requested stat,
		   change stat based on the difference between those
		   so that hunger or illness, and mood changes, set in slowly?
		   pass
		*/
	}


	public void _reset_timer(float duration)
	{
		timer = 0;
		wait_time = duration;
	}

	public void _update_timer(state: State, delta: float, duration: float = rng.randf_range(1.0, WAIT_MAX))
	{
		timer += delta
		if (timer >= wait_time):
			current_state = state
			$PetSfx.stop()
			_reset_timer(duration)
	}

	// Choose a state given the current state
	public void _get_next_state()
	{
		var new_state: State

		match current_state:
		State.IDLE:
				new_state = State.WALK


		return new_state;
	}


	public void eat(interaction:Interaction)
	{
		if interaction.tab_category == Constants.Tab_Categories.FOOD:
			if current_state != State.EAT:
				next_food = interaction
				current_state = State.EAT
			else:
				$"../SfxPlayer".stream = deny_sfx
				$"../SfxPlayer".pitch_scale = 1.0
				$"../SfxPlayer".play()
				print("Couldn't feed pet because pet is already eating something.")
		else:
			print("Tried to feed pet a non-food item. How'd you do that?")
	}
}
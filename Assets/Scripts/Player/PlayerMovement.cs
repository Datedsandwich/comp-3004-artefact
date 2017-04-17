using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// Class only handles movement of player
	public float speed = 10;				// Speed of player left/right movement.
	private Vector3 movement;				// For player movement.
	public float jumpVelocity = 20;			// For jump height.
	public float jumpReduction = 10;		// The degree to which variable jump is variable.
	public Vector3 maxVelocityCap;			// To cap velocity.
	public Vector3 playersHead;				// The position of players head.

	private bool hasJumped = false;				// To check if player has pressed jump.
	private bool cutJumpShort = false;			// If true, player has stopped holding button.
	private bool shouldCalculateEdge = false;	// True if player is falling past a ledge he can grab.

	public Rigidbody rigidbody;
	private Animator anim;
	private PlayerStates state;
	public LayerMask layerMask;
	public LayerMask ledgeMask;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	void Awake()
	{
		// Set up references.
		rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		state = GetComponent<PlayerStates>();
	}
	// --------------------------------------------------------------------
	// Called 50 times a second. Put physics stuff in here.
	void FixedUpdate()
	{
		ApplyJumpPhysics();
		CapVelocity();
	}
	// --------------------------------------------------------------------
	// Called once every frame.
	void Update()
	{
		if (movement != Vector3.zero && !anim.GetBool(state.isPushingBool)) 
		{
			rigidbody.transform.rotation = Quaternion.LookRotation (movement);
		}

		// Create a sphere at the player's feet. If the sphere collides with anything on the layer(s) layerMask
		// return true
		if (Physics.CheckSphere(transform.position, 0.3f, layerMask))
		{
			// If sphere collides, we're touching ground
			anim.SetBool(state.isGroundedBool, true);
			anim.SetBool(state.isClimbingBool, false);
		}
		else
		{
			// otherwise, we're in the air
			anim.SetBool(state.isGroundedBool, false);
		}

		if (anim.GetBool(state.isClimbingBool)) 
		{
			rigidbody.useGravity = false;
			rigidbody.velocity = Vector3.zero;
			if (Input.GetAxis ("Vertical") < 0) 
			{
				anim.SetBool (state.isClimbingBool, false);
			}
		} 
		else 
		{
			rigidbody.useGravity = true;
		}

	}
	// --------------------------------------------------------------------
	// To handle movement.
	public void ManageMovement(float h, float v)
	{
		if (anim.GetBool (state.isClimbingBool))
		{
			return;
		}
		// Find the new forward and right vectors to move along.
		Vector3 forwardMove = Vector3.Cross(Camera.main.transform.right, Vector3.up);
		Vector3 horizontalMove = Camera.main.transform.right;
		
		// Multiply the direction vectors by the Input.GetAxis floats.
		movement = forwardMove * v + horizontalMove * h;
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		// Set velocity for animation states
		state.SetVelocity(movement);
		// Move the player to it's current position plus this movement.
		rigidbody.MovePosition(transform.position + movement);
	}
	// --------------------------------------------------------------------
	// To make player jump.
	public void Jump() 
	{
		hasJumped = true;
		anim.SetBool(state.isClimbingBool, false);
	}
	// --------------------------------------------------------------------
	// To make player jump. TODO: Better
	public void CutJumpShort() 
	{
		cutJumpShort = true;
	}
	// --------------------------------------------------------------------
	private void ApplyJumpPhysics()
	{
		if (hasJumped)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);
			rigidbody.useGravity = true;
			hasJumped = false;
		}

		// Cancel the jump when the button is no longer pressed
		if (cutJumpShort)
		{
			if (rigidbody.velocity.y > jumpReduction)
			{
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpReduction, rigidbody.velocity.z);
			}
			cutJumpShort = false;
		}
	}
	// --------------------------------------------------------------------
	// To cap velocity so player doesn't fall too fast.
	void CapVelocity() 
	{
		Vector3 _velocity = GetComponent<Rigidbody>().velocity;
		_velocity.x = Mathf.Clamp (_velocity.x, -maxVelocityCap.x, maxVelocityCap.x);
		_velocity.y = Mathf.Clamp (_velocity.y, -maxVelocityCap.y, maxVelocityCap.y);
		_velocity.z = Mathf.Clamp (_velocity.z, -maxVelocityCap.z, maxVelocityCap.z);
		rigidbody.velocity = _velocity;
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// This class handles the interaction between the various Player scripts.
	public GameObject panel;
	// Following MVC, this is a Controller class, though not the only controller in this system
	private PlayerMovement playerMovement;							// Handles player movement. Controller.
	private CharacterAnimationController animationController;		// Controls which animations will play. Controller.
	private PlayerStates state;										// Stores the state of the player. Model.
	private Animator anim;
	// The View is the Unity GameObject and Animator. They don't need seperate classes here.
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Awake() 
	{
		// Setting up references to other objects.
		playerMovement = GetComponent<PlayerMovement>();
		animationController = GetComponent<CharacterAnimationController>();
		state = GetComponent<PlayerStates>();
		anim = GetComponent<Animator>();
	}
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update ()
	{
		// Input handling.
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		// Take input and pass it to the PlayerMovement script.
		// If we're pulling, use a different function for movement handling
		playerMovement.ManageMovement (horizontalInput, verticalInput);

		// Jump.
		if (Input.GetButtonDown ("Jump") && anim.GetBool(state.isGroundedBool)) 
		{
			playerMovement.Jump();
		}

		if (Input.GetButtonDown ("Jump") && anim.GetBool(state.isClimbingBool)) 
		{
			playerMovement.Jump();
		}

		// Cut jump short for variable height.
		if (Input.GetButtonUp ("Jump") && !anim.GetBool(state.isGroundedBool)) 
		{
			playerMovement.CutJumpShort();
		}
	}
	// --------------------------------------------------------------------
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.Objective)
		{
			panel.SetActive(true);
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}

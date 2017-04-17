using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// Singleton Principle. SceneManager is static and creates a reference to itself. Allowing it to be called from any object with SceneManager.Instance.
	public static SceneManager Instance {get; private set;}

	public string nextLevel;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	public void Awake()
	{
		// Singleton Principle. SceneManager is static and creates a reference to itself. Allowing it to be called from any object with SceneManager.Instance.
		Instance = this;
	}
	// --------------------------------------------------------------------
	void Update()
	{
		/*if(Input.GetKey(KeyCode.F1))
		{
			LoadLevel("Room 1");
		}

		if(Input.GetKey(KeyCode.F2))
		{
			LoadLevel("Room 2");
		}

		if(Input.GetKey(KeyCode.F3))
		{
			LoadLevel("Room 3");
		}

		if(Input.GetKey(KeyCode.F4))
		{
			LoadLevel("Room 4");
		}*/
	}
	// --------------------------------------------------------------------
	public void LoadLevel(string levelName)
	{
		// This is basically all we need.
		// If no levelName is passed through, go to Main Menu
		// otherwise, we load the appropriate level
		if(string.IsNullOrEmpty(levelName))
		{
			Application.LoadLevel("Main Menu");
		}
		else
		{
			Application.LoadLevel(levelName);
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}

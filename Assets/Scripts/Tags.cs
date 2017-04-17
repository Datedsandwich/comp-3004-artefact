using UnityEngine;
using System.Collections;

public class Tags : MonoBehaviour 
{
	// This file stores all the tags. Whenever checking a tag, reference this script. Ensures that if I need to change a tag in future, I only need to change it in one file.
	public static string UI = "UI";
	public static string Player = "Player";
	public static string Enemies = "Enemies";
	public static string Environment = "Environment";
	public static string Objective = "Objective";
}

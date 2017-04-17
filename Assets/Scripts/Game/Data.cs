using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class Data : MonoBehaviour 
{
	public string DataString;
	public string FilePath;

	public void Awake()
	{
		FilePath = Application.dataPath + "/Saved/";

		// If the directory doesn't exist, create it
		if (!Directory.Exists(FilePath))
		{
			Directory.CreateDirectory(FilePath);
		}
	}

	public void Add(string value)
	{
		// Add to the string
		DataString += value;
	}

	public void AppendFilePath(string value)
	{
		// Add to the file path
		FilePath += value;	
	}

	public void Save()
	{
		// Save data string to the text file
		System.IO.File.AppendAllText(FilePath, Environment.NewLine + DataString);
	}
}

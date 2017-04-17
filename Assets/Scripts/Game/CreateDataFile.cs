using UnityEngine;
using System.Collections;

public class CreateDataFile : MonoBehaviour 
{
	public Data data;
	public string levelName;

	public void Start()
	{
		data.AppendFilePath(levelName);

		// If the file already exists, return
		if(System.IO.File.Exists(data.FilePath))
		{
			return;
		}
		else
		{
			// but if it doesn't, create it
			System.IO.File.WriteAllText(data.FilePath, levelName);
		}
	}
}

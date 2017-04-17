using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugFilePath : MonoBehaviour
{
	public Data data;
	public Text header;

	void Update()
	{
		header.text = data.FilePath;
	}
}
using UnityEngine;
using System.Collections;

public class UIControllerStart : MonoBehaviour {

	public void LoadGame()
	{
		Application.LoadLevel (1);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}

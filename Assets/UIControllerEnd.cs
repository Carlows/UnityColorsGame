using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControllerEnd : MonoBehaviour {
	public Text placeholderScore;

	void Start()
	{
		int level = PlayerPrefs.GetInt ("level");
		placeholderScore.text = string.Format ("Nivel: {0}", level);
		PlayerPrefs.DeleteKey ("level");
	}

	public void LoadGame()
	{
		Application.LoadLevel (1);
	}

	public void EndGame()
	{
		Application.LoadLevel (0);
	}
}

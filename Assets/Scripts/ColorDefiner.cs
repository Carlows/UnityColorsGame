using UnityEngine;
using System.Collections;

public class ColorDefiner : MonoBehaviour {

	public Color color;

	// Use this for initialization
	void Start () {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

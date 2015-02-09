using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public GameObject buttonPrefab;
	public GameObject panel;

	// Cuantos necesitas tocar hasta que el rate de desaceleracion disminuya
	public int rateDeaceleration = 2;
	// Diferencia inicial entre los colores
	public int initialAlphaDifference = 100;
	// Desaceleracion de la diferencia
	public int deacelerationRate = 10;
	// Spacing entre cuadros
	public Vector2 cellSpacing = new Vector2(5f, 5f);
	// Timer limit
	public float TimerLimit = 60.0f;
	// Text Timer
	public Text textTimer;
	// Level text
	public Text levelText;

	// A que nivel ira aumentando ese rate
	private int rateGridUpdate = 1;

	private float panelX;
	private float panelY;
	private RectTransform panelRectTransform;
	private GridLayoutGroup grid;
	private int rateGridUpdateCounter = 0;
	private int deacelerationCounter = 0;

	private int numeroBotones = 2;
	private int currentLevel = 0;
	private float timer;


	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;

		panelRectTransform = panel.GetComponent<RectTransform>();
		grid = panel.GetComponent<GridLayoutGroup> ();
		panelX = panelRectTransform.rect.width;
		panelY = panelRectTransform.rect.height;

		timer = TimerLimit;

		UpdateGame ();
	}

	public void UpdateGame()
	{		
		var children = new List<GameObject>();
		foreach (Transform child in panel.transform) children.Add(child.gameObject);
		foreach (GameObject child in children) {
			Destroy(child);
		}

		if(rateGridUpdateCounter >= rateGridUpdate)
		{
			numeroBotones++;
			rateGridUpdate += 1;
			rateGridUpdateCounter = 0;
		}

		if(deacelerationCounter >= rateDeaceleration)
		{
			if(initialAlphaDifference < 60)
			{
				deacelerationRate = 4;
				rateDeaceleration = 4;
			}	
			else if(initialAlphaDifference < 40)
				deacelerationRate = 3;
			else if(initialAlphaDifference < 25)
			{
				deacelerationRate = 2;
				rateDeaceleration = 8;
			}
			else if(initialAlphaDifference < 15)
			{
				deacelerationRate = 1;
				rateDeaceleration = 12;
			}

			initialAlphaDifference = Mathf.Clamp(initialAlphaDifference - deacelerationRate, 1, 100);
			deacelerationCounter = 0;
		}

		rateGridUpdateCounter++;
		deacelerationCounter++;
		currentLevel++;

		if(panelX < panelY){
			grid.startAxis = GridLayoutGroup.Axis.Horizontal;
			grid.cellSize = new Vector2 ((panelX/numeroBotones) - cellSpacing.x, (panelX/numeroBotones) - cellSpacing.y);
		}else{
			grid.startAxis = GridLayoutGroup.Axis.Vertical;
			grid.cellSize = new Vector2 ((panelY/numeroBotones) - cellSpacing.x, (panelY/numeroBotones) - cellSpacing.y);
		}

		grid.spacing = cellSpacing;

		Color32 randomColor = UtilityFunctions.NextRandomBeautifulColor ();
		//ColorModel randomColor = colors.Colors [Random.Range (0, (colors.Colors.Count - 1))];
		int randomButtonIndex = Random.Range (0, ((numeroBotones * numeroBotones) - 1));

		for (int index = 0; index < numeroBotones * numeroBotones; index++) {
			GameObject newItem = Instantiate(buttonPrefab) as GameObject;
			Button newItemButton = newItem.GetComponent<Button>();
			newItem.transform.SetParent(panel.transform);
			newItemButton.image.color = randomColor;

			if(index == randomButtonIndex)
			{
				Color32 newColor = randomColor;
				// posible bug en el color, si el valor es menos de 100, al restarse pasara a 255
				byte red = (byte)((newColor.r - initialAlphaDifference) % 255);
				byte green = (byte)((newColor.g - initialAlphaDifference) % 255);
				byte blue = (byte)((newColor.b - initialAlphaDifference) % 255);
				newColor = new Color32(red, green, blue, 255);
				newItemButton.image.color = newColor;
				newItemButton.onClick.AddListener(() => {
					UpdateGame();
				});
			}
		}

		levelText.text = string.Format ("Nivel: {0}", currentLevel);
	}

	private void ToEndScene()
	{
		PlayerPrefs.SetInt("level", currentLevel);
		Application.LoadLevel (2);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		string minutes = Mathf.Floor(timer / 60).ToString("00");
		string seconds = Mathf.Floor(timer % 60).ToString("00");
		
		if (timer <= 0.0f)
		{
			Invoke("ToEndScene", 1.0f);
		}
		else if(timer <= 10.0f)
		{
			textTimer.color = new Color32(202, 18, 67, 255);
			textTimer.text = string.Format ("{0}:{1}", minutes, seconds);
		}
		else
		{			
			textTimer.text = string.Format ("{0}:{1}", minutes, seconds);
		}
	}
}

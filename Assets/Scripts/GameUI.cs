using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {


	public GameObject[] PlayerUI;
	public Image[] playersHealth;
	public Text[] playersScore;
	public UICard[] templates;

	public RectTransform[] p1cards;
	public RectTransform[] p2cards;

	public static GameUI Instance = null;
	
	void Awake() {
		if (GameUI.Instance == null){
			Instance = this;
		}
		else if (Instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad(GameUI.Instance);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetHealth(int player, float amount){
		playersScore[player].text = amount+"";
		playersHealth[player].fillAmount = Mathf.Abs(amount);
	}

	public void CreateCard(int player, int number, Card card){

		RectTransform parent;
		if (player == 1) {
			parent = p2cards [number];
		} else {
			parent = p1cards [number];
		}

		UICard instance = Instantiate (templates [player]).GetComponent<UICard>();
		instance.transform.SetParent (parent, false);

		instance.Setup (card);
	}

	public void RemoveCard (int player, int number){
		UICard card = GetCard (player, number);
		if (card != null)
			Destroy (card.gameObject);
	}

	public void SelectCard(int player, int number, bool selected){
		UICard card = GetCard (player, number);
		if (selected) {
			card.Select();
		} else {
			card.Unselect();
		}
	}

	UICard GetCard(int player, int number){
		RectTransform parent;
		if (player == 1) {
			parent = p2cards [number];
		} else {
			parent = p1cards [number];
		}
		
		return parent.GetComponentInChildren<UICard> ();
	} 
}

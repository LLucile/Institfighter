using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICard : MonoBehaviour {

	public Text functionName;
	public RectTransform active;
	public RectTransform inactive;
	public Card m_card;

	void Awake(){
		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup(Card card){
		m_card = card;
		Select (false);
		if (card is Operateur) {
			functionName.fontSize = 72;
		}
		functionName.text = card.name;
		gameObject.SetActive (true);
	}

	public void Select(bool shouldBeSelected){
		inactive.gameObject.SetActive (!shouldBeSelected);
		active.gameObject.SetActive (shouldBeSelected);
	}
}

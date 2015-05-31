using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICard : MonoBehaviour {

	public Text functionName;
	public RectTransform active;
	public RectTransform inactive;

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
		Unselect ();
		if (card is Operateur) {
			functionName.fontSize = 72;
		}
		functionName.text = card.name;
		gameObject.SetActive (true);
	}

	public void Select(){
		inactive.gameObject.SetActive (false);
		active.gameObject.SetActive (true);
	}

	public void Unselect(){
		inactive.gameObject.SetActive (true);
		active.gameObject.SetActive (false);
	}
}

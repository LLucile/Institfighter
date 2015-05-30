using UnityEngine;
using System.Collections;

public class Deck : MonoBehaviour {

	public Card[] cards;

	public static Deck instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

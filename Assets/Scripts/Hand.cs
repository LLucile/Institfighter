using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	// attributes
	Card CL = new Fonction();
	Card CR = new Fonction();
	Card CT = new Operateur();
	Card CB = new Operateur();
	int T = 0;
	// Use this for initialization
	void Start () {
		// TODO get some random cards
	}
	
	// Update is called once per frame
	void Update () {
		//update timers
		T = T - Time.deltaTime;
	}

	public int GetHandSlotWaitingTime(Actions a){
		return T;
	}

	public Card GetHandSlotCard(Actions a){
		switch (a) {
			case Actions.Left:
				return CL;
			case Actions.Right:
				return CR;
			case Actions.Bottom:
				return CB;
			case Actions.Top:
				return CT;
		}	
	
	}

	public void SetHandSlotTime(int time){
		T = time;
	}
	

	public Card GetNewCards(){
		if ((TL < 10000) && (TL > 0)) {
			// TODO get new card
		}
		if ((TL < 10000) && (TL > 0)) {
			// TODO get new card
		}
		if ((TL < 10000) && (TL > 0)) {
			// TODO get new card
		}
		if ((TL < 10000) && (TL > 0)) {
			// TODO get new card
		}
	}


}

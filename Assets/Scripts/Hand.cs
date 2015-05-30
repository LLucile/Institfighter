using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	// attributes
	Card CL = new Card();
	Card CR = new Card();
	Card CT = new Card();
	Card CB = new Card();
	int TL = 0;
	int TR = 0;
	int TT = 0;
	int TB = 0;
	// Use this for initialization
	void Start () {
		// TODO get some random cards
	}
	
	// Update is called once per frame
	void Update () {
		//update timers
		TL = TL - Time.deltaTime;
		TR = TR - Time.deltaTime;
		TT = TT - Time.deltaTime;
		TB = TB - Time.deltaTime;	
	}

	public int GetHandSlotWaitingTime(Actions a){
		switch (a) {
			case Actions.Left:
				return TL;
			case Actions.Right:
				return TR;
			case Actions.Bottom:
				return TB;
			case Actions.Top:
				return TT;
		}	
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

	public void SetHandSlotCard(Actions a, int time){
		switch (a) {
			case Actions.Left:
				TL = time;
			case Actions.Right:
				TR = time;
			case Actions.Bottom:
				TB = time;
			case Actions.Top:
				TT = time;
		}
	}

	public void StartTimer(int timing){
		if (TL > 10000) {
			TL = timing;
		}
		if (TT > 10000) {
			TT = timing;
		}
		if (TB > 10000) {
			TB = timing;
		}
		if (TR > 10000) {
			TR = timing;
		}	
	}

	public Card GetNewCards(){
	
	}


}

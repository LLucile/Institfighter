using UnityEngine;
using System.Collections;

public class Hand {

	// attributes
	Card CL;
	Card CR;
	Card CT;
	Card CB;
	float T = 0;
	// Use this for initialization
	void Start () {
		CL = GameManager.heap.PickFonction ();
		CR = GameManager.heap.PickFonction ();
		CT = GameManager.heap.PickOperateur ();
		CB = GameManager.heap.PickOperateur ();

	}
	
	// Update is called once per frame
	void Update () {
		//update timers
		T = T - Time.deltaTime;
	}

	public float GetHandSlotWaitingTime(Actions a){
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
			default:
				return null;
		}	
	
	}

	public void SetHandSlotTime(float time){
		T = time;
	}
	

	public void GetNewCards(){
		if ((T < 10000) && (T > 0)) {
			CL = GameManager.heap.PickFonction ();
			CR = GameManager.heap.PickFonction ();
			CT = GameManager.heap.PickOperateur ();
			CB = GameManager.heap.PickOperateur ();
		}
	}


}

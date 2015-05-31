using UnityEngine;
using System.Collections;

public class Hand {

	// attributes
	public Card CL ;
	public Card CR ;
	public Card CT ;
	public Card CB ;
	float T = -1;

	public Hand(){
		Debug.Log ("Successfully called Hand");
		CL = GameManager.Instance.heap.PickFonction ();
		CR = GameManager.Instance.heap.PickFonction ();
		CT = GameManager.Instance.heap.PickFonction ();
		CB = GameManager.Instance.heap.PickFonction ();
		T = -1;
		Debug.Log ("Successfully instanciated an hand");
	}
	// Update is called once per frame
	public void HandUpdate () {
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
			CL = GameManager.Instance.heap.PickFonction ();
			CR = GameManager.Instance.heap.PickFonction ();
			CT = GameManager.Instance.heap.PickOperateur ();
			CB = GameManager.Instance.heap.PickOperateur ();
		}
	}


}

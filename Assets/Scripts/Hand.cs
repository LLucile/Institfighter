using UnityEngine;
using System.Collections;

public class Hand {

	public int playerNumber;
	// attributes
	public Card CL ;
	public Card CR ;
	public Card CT ;
	public Card CB ;
	float T = -1;

	public Hand(int player){
		playerNumber = player;

		Debug.Log ("Successfully called Hand");
		CL = TakeCard(Position.Left, Types.Function);
		CR = TakeCard(Position.Right, Types.Function);
		CT = TakeCard(Position.Top, Types.Operator);
		CB = TakeCard(Position.Bottom, Types.Operator);
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

		if ((int)a < 4) {
			GameUI.Instance.SelectCard(playerNumber, (int)a, true);
		}

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
			CL = TakeCard(Position.Left, Types.Function);
			CR = TakeCard(Position.Right, Types.Function);
			CT = TakeCard(Position.Top, Types.Operator);
			CB = TakeCard(Position.Bottom, Types.Operator);
		}
	}

	public Card TakeCard(Position cardNumber, Types op){
		Card picked;
		if (op == Types.Function) {
			picked = GameManager.Instance.heap.PickFonction ();
		} else {
			picked = GameManager.Instance.heap.PickOperateur ();
		}

		GameUI.Instance.CreateCard(playerNumber, (int)cardNumber, picked);

		return picked;
	}
}

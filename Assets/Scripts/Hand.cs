using UnityEngine;
using System.Collections;

public class Hand {

	public int playerNumber;
	// attributes
	public Card CL ;
	public Card CR ;
	public Card CT ;
	public Card CB ;
	float TL = -1;
	float TR = -1;
	float TB = -1;
	float TT = -1;

	public Hand(int player){
		playerNumber = player;

		Debug.Log ("Successfully called Hand");
		CL = TakeCard(Position.Left, Types.Function);
		CR = TakeCard(Position.Right, Types.Function);
		CT = TakeCard(Position.Top, Types.Operator);
		CB = TakeCard(Position.Bottom, Types.Operator);
		TR = -1;
		TL = -1;
		TB = -1;
		TT = -1;
		Debug.Log ("Successfully instanciated an hand");
	}
	// Update is called once per frame
	public void HandUpdate () {
		//update timers
		TL = TL - Time.deltaTime;
		TR = TR - Time.deltaTime;
		TB = TB - Time.deltaTime;
		TT = TT - Time.deltaTime;

		//update cards if necessary
		GetNewCards();
	}

	public float? GetHandSlotWaitingTime(Actions a){
		switch (a) {
		case Actions.Left:
			return TL;
		case Actions.Right:
			return TR;
		case Actions.Bottom:
			return TB;
		case Actions.Top:
			return TT;
		default:
			return null;
		}	

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

	public void SetHandSlotTime(Actions a, float time){
		switch (a) {
		case Actions.Left:
			TL = time;
			break;
		case Actions.Right:
			TR = time;
			break;
		case Actions.Bottom:
			TB = time;
			break;
		case Actions.Top:
			TT = time;
			break;
		}		
	}

	public void SetTime(float time){
		if (TB > 10000) {
			TB = time;
			GameUI.Instance.RemoveCard (playerNumber, (int) Position.Bottom);
		}
		if (TL > 10000) {
			TL = time;
			GameUI.Instance.RemoveCard (playerNumber, (int) Position.Left);
		}
		if (TR > 10000) {
			TR = time;
			GameUI.Instance.RemoveCard (playerNumber, (int) Position.Right);
		}
		if (TT > 10000) {
			TT = time;
			GameUI.Instance.RemoveCard (playerNumber, (int) Position.Top);
		}
	}
	

	public void GetNewCards(){
		if ((TL < 0.5) && (TL > 0)) {
			CL = TakeCard (Position.Left, Types.Function);
		}
		if ((TR < 0.5) && (TR > 0)) {
			CR = TakeCard (Position.Right, Types.Function);
		}
		if ((TT < 0.5) && (TT > 0)) {
			CT = TakeCard (Position.Top, Types.Operator);
		}
		if ((TB < 0.5) && (TB > 0)) {
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

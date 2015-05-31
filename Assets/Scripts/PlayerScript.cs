using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	// Attributes
	// controls
	public string sButton1 = "Horizontal";
	public string sButton2 = "Vertical";
	public string sButtonValidate = "Ok";
	public string sButtonCancel = "Back";

	//oponnent score
	public float opponentScore = 10;

	// hand
	private Hand ownCards;

	// expression browsing variables
	private int expressionScroller = 0;
	private Card[] expression = new Card[2];

	// some useful variables
	private Actions lastAction = Actions.None;

	// Use this for initialization
	void Start () {
		Debug.Log ("PlayerScript started");
		ownCards = new Hand();
		//Debug.Log ("Cards : " + ownCards.CB.name + " " + ownCards.CT.name + " " + ownCards.CR.name + " " + ownCards.CL.name);
		Debug.Log ("Start successfull");
	} 

	void Update(){
		// get the user input
		Actions tempx = GetAction ();
		if ( ((int?) tempx) < 4) { //if the user tried to select a card

			float waitingTime = ownCards.GetHandSlotWaitingTime(tempx);
			if(waitingTime <= 0){ // if the card is immediatly available
				Debug.Log ("Grabbed a card from the hand !");
				// add IT to the expression and set it as unavailable in the hand
				lastAction = tempx;
				expression[expressionScroller] = ownCards.GetHandSlotCard(lastAction);
				Debug.Log ("Card is "+ expression[expressionScroller].name);
				expressionScroller ++;
				ownCards.SetHandSlotTime(Mathf.Infinity);
			}
			else{
				//Debug.Log ("Waiting time is not zero : " + waitingTime);
			}
		}
		if (tempx == Actions.Cancel) {

			expression[expressionScroller] = null;
			ownCards.SetHandSlotTime(0);
			expressionScroller --;
		}
		if(IsValidExpression() ){
			// TODO display that the expression is valid
			if(tempx == Actions.Validate){
				Debug.Log ("validate !");
				ownCards.SetHandSlotTime(5*CountCard ());

				//check that the calculus is mathematically ok
				float? tempScore = ComputeOpponentScore ();
				if(tempScore != null){
					opponentScore = (float) tempScore;
					Debug.Log(opponentScore);
				}
				else{
					// TODO forbidden mathematical operation feedback (0 points)
				}
				expressionScroller = 0;
				expression[0] = null;
				expression[1] = null;
				expression[2] = null;
				ownCards.GetNewCards();
			}
		}
		else{
			//TODO display that it is not a valid expression
		}

		// check 
		ownCards.HandUpdate ();
		
	}

	float? ComputeOpponentScore(){
		float? score0 = null;
		float? score1 = null;
		float? score2 = null;
		if (expression [0] is Fonction) {
			Fonction Firstmember = (Fonction) expression [0];
			score0 = Firstmember.Execute(opponentScore);
			if(Firstmember.Function == Functions.b){
				Operateur Secondmember = (Operateur) expression[1];
				return Secondmember.Execute(opponentScore, Firstmember.Execute(opponentScore));
			}
			else{
				Operateur Secondmember = (Operateur) expression[1];
				Fonction Thirdmember = (Fonction) expression[2];
				score0 = Firstmember.Execute(opponentScore);
				score2 = Thirdmember.Execute(opponentScore);
				score1 = Secondmember.Execute(score0, score2);
				return score1;
			}
		}
		else {
			Operateur Firstmember = (Operateur) expression[0];
			Fonction Secondmember = (Fonction) expression[1];
			return Firstmember.Execute (opponentScore, Secondmember.Execute(opponentScore));
		}

	}

	Actions GetAction(){
		float h = Input.GetAxis (sButton1);
		float v = Input.GetAxis (sButton2);
		if (h < -0.5) {
			return Actions.Left;
		}
		if (h > 0.5) {
			return Actions.Right;
		}
		if (v > 0.5) {
			return Actions.Top;
		}
		if (v < -0.5) {
			return Actions.Bottom;
		}
		if (Input.GetButtonDown (sButtonValidate)) {
			return Actions.Validate;
		}
		if (Input.GetButtonDown (sButtonCancel)) {
			return Actions.Cancel;
		} else {
			return Actions.None;
		}
	}

	int CountCard(){
		int n = 0;
		if (expression [0] != null)
			n++;

		if (expression [1] != null)
			n++;

		if (expression [2] != null)
			n++;

		return n;
	}

	bool IsValidExpression(){
		if (expressionScroller == 0) {
			return false;
		}
		if (expression [0] != null) {
			if (expression [0] is Fonction) {
				Fonction expression0 = (Fonction) expression[0];
				if (expression [1] == null) {
					return true;
				} 
				else {
					if (expression [1] is Operateur) {
						if(expression[2] != null){
							if(expression[2] is Fonction){
								return true;
							}
							else{
								return false;
							}
						}
						else{
							if(expression0.Function == Functions.b){
								return true;
							}
							else{
								return false;
							}
						}
					} 
					else {
						return false;
					}
				}
			} 
			else {
				if(expression[1] is Fonction){
					Fonction expression1 = (Fonction) expression[1];
					if(expression1.Function == Functions.b){
						return true;
					}
					else{
						return false;
					}
				}
				else{
					return false;
				}
			}
		}
		else {
			return false;
		}
	}

}
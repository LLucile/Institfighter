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
	public float? opponentScore = 10;

	// hand
	public Hand ownCards = new Hand();

	// expression browsing variables
	private int expressionScroller = 0;
	private Card[] expression = new Card[2];

	// some useful variables
	private int? lastAction = null;

	// Use this for initialization
	void Start () {

	} 

	void Update(){
		// get the user input
		int? tempx = GetAction ();

		if (tempx < 4) { //if the user tried to select a card
			int waitingTime = ownCards.GetHandSlotWaitingTime(tempx);
			if(waitingTime <= 0){ // if the card is immediatly available
				// add IT to the expression and set it as unavailable in the hand
				lastAction = tempx;
				expression[expressionScroller] = ownCards.GetHandSlotCard(lastAction);
				expressionScroller ++;
				ownCards.SetHandSlotCard(Mathf.Infinity);
			}
		}
		if (tempx == Actions.Cancel) {
			expression[expressionScroller] = null;
			ownCards.SetHandSlotCard(0);
			expressionScroller --;
		}
		if(IsValidExpression() ){
			// TODO display that the expression is valid
			if(tempx = Actions.Validate){
				ownCards.SetHandSlotTime(5*CountCard ());

				//check that the calculus is mathematically ok
				int tempScore = ComputeOpponentScore ();
				if(tempScore != null){
					opponentScore = tempScore;
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
		
	}

	private float? ComputeOpponentScore(){
		if ( (expression [0] != null) && (expression [0] is Function) ) {
			float? score0 = expression [0].Execute (opponentScore);
			if (score0 == null) {
				return opponentScore;
			}
		}
		if (expression [1] != null) {
			if(expression[1] is Function){
				float? score3 = expression[0].Execute (opponentScore, expression[1].Execute (0));
			}
			else{
				float? score2 = expression [2].Execute (opponentScore);
				if (score0 == null) {
					return opponentScore;
				}
				float? score3 = expression [1].Execute (score0, score2);
				if (score0 == null) {
					return opponentScore;
				}
			}
		}
		return score3;
	}

	Actions GetAction(){
		int h = Input.GetButtonDown (sbutton1);
		int v = Input.GetButtonDown (sbutton2);
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
		if (expression [0] != null) {
			if (expression [0] is Fonction) {
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
							return false;
						}
					} 
					else {
						return false;
					}
				}
			} 
			else {
				if(expression[1].Function == Functions.b){
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
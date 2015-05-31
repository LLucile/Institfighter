using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	// Attributes
	public int playerNumber;
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
	private Card[] expression = new Card[3];

	// some useful variables
	private Actions lastAction = Actions.None;

	// Use this for initialization
	void Start () {
		Debug.Log ("PlayerScript started");
		ownCards = new Hand(playerNumber);
		//Debug.Log ("Cards : " + ownCards.CB.name + " " + ownCards.CT.name + " " + ownCards.CR.name + " " + ownCards.CL.name);
		Debug.Log ("Start successfull");
	} 

	void Update(){
		// get the user input
		//Debug.Log ("w i");
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
		if(tempx == Actions.Validate){
			Debug.Log ("Expression valid !");
			// TODO display that the expression is valid
			if(IsValidExpression() ){
				Debug.Log ("validate !");
				ownCards.SetHandSlotTime(5*CountCard ());
				Debug.Log ("n cards =" + CountCard ());
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
		float? score = opponentScore;
		if (expression [0] is Fonction) {
			Fonction expression0 = expression [0] as Fonction;
			if(expression[1] is Operateur){
				Operateur expression1 = expression[1] as Operateur;
				if(expression[2] is Fonction){
					Fonction expression2 = expression[2] as Fonction;
					score = expression1.Execute (expression0.Execute (opponentScore), expression2.Execute(opponentScore));
				}
				else{
					Debug.Log ("wrong expression function+operator+operator");
				}
			}
			else{
				Debug.Log ("wrong expression fonction + not operator");
			}
		} 
		else {
			Operateur expression0 = expression[0] as Operateur;
			if(expression[1] is Fonction){
				Fonction expression1 = expression[1] as Fonction;
				if(expression1.Function == Functions.b){
					if(expression[2] == null){
						Debug.Log ("valid expression");
						score = expression0.Execute (expression1.Execute (opponentScore), opponentScore);
					}
					else{
						Debug.Log("not valid expression operateur+fonction.b+smthg");
					}
				}
				else{
					Debug.Log ("wrong expression operator + not fonction.b");
				}
			}
			else{
				Debug.Log ("wrong expression operator + operator");
			}
		}
		return score;
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
		if(expression[0] is Fonction && expression[1] is Operateur && expression[2] is Fonction){
			return true;
		} else if(expression[0] is Fonction && expression[1]==null && expression[2]==null) {
			return true;
		} else if(expression[0]==null && expression[1] is Operateur && expression[2] is Fonction) {
			Fonction expression2 = expression [0] as Fonction;
			if(expression2.Function==Functions.b){
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
		//if (expression [0] is Fonction) {
			//Debug.Log ("expression0 is a function");
			//Fonction expression0 = expression [0] as Fonction;
			//if (expression [1] is Operateur) {
				//Debug.Log ("expression1 is an operator");
				//if (expression [2] is Fonction) {
					//Debug.Log ("expression2 is a function");
					//Debug.Log ("Valid expression !");
					//return true;
				//} else {
					//Debug.Log ("expression2 is not a function");
					//Debug.Log ("[IsValidExpression] wrong expression function+operator+operator");
					//return false;
				/*}
			} else {
				//Debug.Log ("[IsValidExpression]wrong expression fonction.b + not operator");
				return false;
			}
		} else if (expression [0] is Operateur) {
			if (expression [1] is Fonction) {
				Fonction expression1 = expression [1] as Fonction;
				if (expression1.Function == Functions.b) {
					if (expression [2] == null) {
						//Debug.Log ("[IsValidExpression]valid expression");
						return true;
					} else {
						//Debug.Log ("[IsValidExpression]not valid expression operateur+fonction.b+smthg");
						return false;
					}
				} else {
					//Debug.Log ("[IsValidExpression]wrong expression operator + not fonction.b");
					return false;
				}
			} else {
				//Debug.Log ("[IsValidExpression]wrong expression operator + operator");
				return false;
			}
		} 
		else {
			// void expression
			return false;
		}*/
	}
}
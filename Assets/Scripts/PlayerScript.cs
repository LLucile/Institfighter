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
	private Card[] expression = new Card[4];

	// some useful variables
	private Actions[] lastAction = new Actions[4];
	private int lastActionScroller = 0;
	private Actions tempx;

	// Use this for initialization
	void Start () {
		ownCards = new Hand(playerNumber);
		//Debug.Log ("Cards : " + ownCards.CB.name + " " + ownCards.CT.name + " " + ownCards.CR.name + " " + ownCards.CL.name);
		for (int i = 0; i < lastAction.Length; i++) {
			lastAction[i] = Actions.None;
		}
		Debug.Log ("PlayerScript successfully started");
	} 

	void Update(){
		// get the user input
		// Debug.Log ("w i");
		tempx = GetAction ();
		if ( ((int?) tempx) < 4) { //if the user tried to select a card
			float? waitingTime = ownCards.GetHandSlotWaitingTime(tempx);
			if(waitingTime <= 0 ){ // if the card is immediatly available
				Debug.Log ("Grabbed a card from the hand !");
				// add IT to the expression and set it as unavailable in the hand
				lastAction[lastActionScroller] = tempx;
				Debug.Log ("expressionScroller = " + expressionScroller);
				Debug.Log ("last action is "+lastAction);
				expression[expressionScroller] = ownCards.GetHandSlotCard(lastAction[lastActionScroller]);
				Debug.Log ("Card is " + expression[expressionScroller].name);
				expressionScroller ++;
				ownCards.SetHandSlotTime(lastAction[lastActionScroller], Mathf.Infinity);
				lastActionScroller ++;
				Debug.Log ("last action is " + lastAction);
			}
			else{
				Debug.Log ("Waiting time is not zero : " + waitingTime);
			}
		}
		if (tempx == Actions.Cancel) {
			Debug.Log("Received cancel order");
			for(int i = lastActionScroller; i >= 0; i--){
				if(lastAction[i] != Actions.None){
					//unselect card in UI
					GameUI.Instance.SelectCard(playerNumber, (int)lastAction[i], false);
					//set card as available
					ownCards.SetHandSlotTime(lastAction[i],-1);
					expression[expressionScroller] = null;
					lastAction[i] = Actions.None;
					expressionScroller --;
					Debug.Log("expressionScroller = " +expressionScroller);
				}
				lastActionScroller = 0;
			}
		}
		if(tempx == Actions.Validate){
			Debug.Log ("validate");
			bool isValidExpression = IsValidExpression();

			GameUI.Instance.onPlayerValidateSelectedCards(isValidExpression);

			// TODO display that the expression is valid
			if(isValidExpression){
				Debug.Log ("IT IS VALID !");
				int waitingtime = 1*CountCard ();
				Debug.Log ("waiting time = "+waitingtime);
				ownCards.SetTime(waitingtime);

				//check that the calculus is mathematically ok
				float? tempScore = ComputeOpponentScore ();
				expressionScroller = 0;
				Debug.Log ("expressionScroller after Validate = " + expressionScroller);
				if(tempScore != null){
					opponentScore = (float) tempScore;
					Debug.Log(opponentScore);
					GameUI.Instance.SetHealth(playerNumber, opponentScore);
				}
				else{
					// TODO forbidden mathematical operation feedback (sad noise)
				}
				expression[0] = null;
				expression[1] = null;
				expression[2] = null;
				ownCards.getCard = true;
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
		Fonction first = expression[0] as Fonction;
		Operateur second = expression[1] as Operateur;
		Fonction third = expression[2] as Fonction;
		float? firstResult;
		float? thirdResult;
		if(first != null && second != null && third != null){
			firstResult = first.Execute(score);
			thirdResult = third.Execute(score);
			if(firstResult == firstResult && thirdResult == thirdResult){
				score = second.Execute(firstResult, thirdResult);
			}
		}
		if(first != null && second == null && third == null){
			firstResult = first.Execute(score);
			if(firstResult == firstResult)
				score = firstResult;
		}
		if(first == null && second != null && third != null){
			thirdResult = third.Execute(score);
			if(thirdResult == thirdResult)
				score = second.Execute(score, thirdResult);
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
			Debug.Log("f+o+f");
			return true;
		} else if(expression[0] is Fonction && expression[1]==null && expression[2]==null) {
			Debug.Log("f");
			return true;
		} else if(expression[0]==null && expression[1] is Operateur && expression[2] is Fonction) {
			Debug.Log("o+f");
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
	}

}
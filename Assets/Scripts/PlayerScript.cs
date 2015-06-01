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
	private Actions tempx;

	// Use this for initialization
	void Start () {
		Debug.Log ("PlayerScript started");
		ownCards = new Hand(playerNumber);
		//Debug.Log ("Cards : " + ownCards.CB.name + " " + ownCards.CT.name + " " + ownCards.CR.name + " " + ownCards.CL.name);
		Debug.Log ("Start successfull");
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
				lastAction = tempx;
				Debug.Log ("expressionScroller = " + expressionScroller);
				Debug.Log ("last action is "+lastAction);
				expression[expressionScroller] = ownCards.GetHandSlotCard(lastAction);
				Debug.Log ("Card is "+ expression[expressionScroller].name);
				expressionScroller ++;
				ownCards.SetHandSlotTime(lastAction, Mathf.Infinity);
				Debug.Log ("last action is "+lastAction);
			}
			else{
				Debug.Log ("Waiting time is not zero : " + waitingTime);
			}
		}
		if (tempx == Actions.Cancel) {
			if(lastAction != Actions.None){
				GameUI.Instance.SelectCard(playerNumber, (int)lastAction, false);
				Debug.Log(expressionScroller);
				expression[expressionScroller] = null;
				ownCards.SetHandSlotTime(lastAction,-1);
				expressionScroller --;
			}
		}
		if(tempx == Actions.Validate){
			bool isValidExpression = IsValidExpression();
			GameUI.Instance.onValidate(isValidExpression);
			// TODO display that the expression is valid
			if(isValidExpression){

				Debug.Log ("IT IS VALID !");
				int waitingtime = 1*CountCard ();
				Debug.Log ("waiting time = "+waitingtime);
				ownCards.SetTime(waitingtime);
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
		Fonction first = expression[0] as Fonction;
		Operateur second = expression[1] as Operateur;
		Fonction third = expression[2] as Fonction;
		if(first != null && second != null && third != null){
			float? firstResult = first.Execute(score);
			float? thirdResult = third.Execute(score);
			if(firstResult is float && thirdResult is float){
				score = second.Execute(firstResult, thirdResult);
			}
		}
		if(first != null && second == null && third == null)
			score = first.Execute(score);
		if(first == null && second != null && third != null)
			score = second.Execute(score, third.Execute(score));
		return score;
		/*if (expression [0] is Fonction) {
			Fonction Firstmember = expression[0] as Fonction;
			Debug.Log ("Is first member fonction ?" + (Firstmember != null));
			if(Firstmember.Function == Functions.b){
				if(expression[1] is Operateur){
					Operateur Secondmember = expression[1] as Operateur;
					Debug.Log ("Is second member operateur ?" + (Secondmember != null));
					if(expression[2] == null){
						score = Secondmember.Execute (Firstmember.Execute(opponentScore), opponentScore);
						Debug.Log ("New Score = "+score);
						return score;
					} else{
						//not valid expression
						Debug.Log ("Not valid expression Function.b + Operateur + whatever");
						return opponentScore;
					}
				} else{
					//not valid expression
					Debug.Log ("Not valid expression Function.b + Fonction");
					return opponentScore;
				}
			} else{
				if(expression[1] is Operateur){
					Operateur Secondmember = expression[1] as Operateur;
					Debug.Log ("Is second member operateur ?" + (Secondmember != null));
					if(expression[2] is Fonction){
						Fonction Thirdmember = expression[2] as Fonction;
						Debug.Log ("Is third member fonction ?" + (Thirdmember != null));
						score = Secondmember.Execute(Firstmember.Execute(opponentScore), Thirdmember.Execute(opponentScore));
						Debug.Log ("New score = " + score);
						return score;
					}
					else{
						// this expression is not Function Operator Function type do not chance opponent score
						Debug.Log ("Wrong expression Fonction + Operateur + Operateur");
						return opponentScore;
					}
				}
				else{
					Debug.Log ("Wrong expression Fonction + Fonction + whatever");
					return opponentScore;
					//this expression is not Function Operator Function do not change score
				}
			}
		}
		else if (expression [0] is Operateur) {
			Operateur Firstmember = expression[0] as Operateur;
			Debug.Log ("Is first member operateur ?" + (Firstmember != null));
			if(expression[1] is Fonction){
				Fonction Secondmember = expression[1] as Fonction;
				Debug.Log ("Is second member fonction ?" + (Secondmember != null));
				if(Secondmember.Function == Functions.b){
					if(expression[2] == null){
						score = Firstmember.Execute (opponentScore, Secondmember.Execute(opponentScore));
						Debug.Log ("New score is "+ score);
					}
					else{
						//not valid expression
						Debug.Log ("Wrong expression Operateur + Fonction.b + whatever");
						return opponentScore;
					}
				}
				else{
					Debug.Log ("Wrong expression Operateur + whatever not Function.b");
					return opponentScore;
				}
			}
			else{
				Debug.Log ("Wrong expression Operateur + whatever not even Function");
				return opponentScore;
			}
		}
		else{
			Debug.Log ("Holy Crap ! Unidentified Flying Object !");
			return opponentScore;
		}
		Debug.LogError ("WE WERE LIKE NEVER SUPPOSED TO GET THERE !");
		return opponentScore;
		*/
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
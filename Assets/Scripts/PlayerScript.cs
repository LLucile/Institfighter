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
	private Card[] expression = new Card[3];

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
		//Debug.Log ("w i");
		Actions tempx = GetAction ();
		if ( ((int?) tempx) < 4) { //if the user tried to select a card
			Debug.Log ("Card Selection : " + tempx);
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
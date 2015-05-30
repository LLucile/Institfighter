using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	//Attributes
	//controls
	public string sButton1 = "Horizontal";
	public string sButton2 = "Vertical";
	public string sButtonValidate = "Ok";
	public string sButtonCancel = "Back";
	
	//other attributes
	private Card[] cards = new Card[3];
	private Card[] expression = new Card[2];
	public float opponentScore = 0;
	private int expressionScroller = 0;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i ++) {
			// TODO get a set of random cards
		}
	}
	
	void Update () {
		// get input
		int inputC0 = false;
		int inputC1 = false;
		int inputC2 = false;
		int inputC3 = false;
		int h = Input.GetButtonDown (sbutton1);
		int v = Input.GetButtonDown (sbutton2);
		if (h < -0.5) {
			inputC0 = true;
		}
		if (h > 0.5) {
			inputC1 = true;
		}
		if (v > 0.5) {
			inputC2 = true;
		}
		if (v < -0.5) {
			inputC3 = true;
		}
		int inputValidate = Input.GetButtonDown (sButtonValidate);
		int inputCancel = Input.GetButtonDown (sButtonCancel);

		// compose expression
		if (inputC1) {
			expression [expressionScroller] = cards[0];
			// TODO display function or operator
			expressionScroller ++;
		}
		if (inputC2) {
			expression [expressionScroller] = cards[1];
			// TODO display function or operator
			expressionScroller ++;
		}
		if (inputC3) {
			expression [expressionScroller] = cards[2];
			// TODO display function or operator
			expressionScroller ++;
		}
		if (inputC4) {
			expression [expressionScroller] = cards[3];
			// TODO display function or operator
			expressionScroller ++;
		}
		if (inputCancel) {
			expression [expressionScroller] = null;
			// TODO display function or operator
			expressionScroller --;
		}
		
		// attribute score
		if(inputValidate){
			if(IsValidExpression ()){
				// compute new opponent score
				int tempScore = ComputeOpponentScore ();
				if(tempScore != null){
					opponentScore = tempscore;
				}
				else{
					// TODO forbidden mathematical operation feedback
				}
				//reset expression to void
				expressionScroller = 0;
				expression[0] = null;
				expression[1] = null;
				expression[2] = null;
			}
			else{
				// TODO Not valid expression feedback
			}
		}
	}
	
	private float ComputeOpponentScore(){
		if ( (expression [0] != null) && (expression [0] is Function) ) {
			int score0 = expression [0].Execute (opponentScore);
			if (score0 == null) {
				return opponentScore;
			}
		}
		if (expression [1] != null) {
			if(expression[1] is Function){
				int score3 = expression[0].Execute (opponentScore, expression[1].Execute (0));
			}
			else{
				int score2 = expression [2].Execute (opponentScore);
				if (score0 == null) {
					return opponentScore;
				}
				int score3 = expression [1].Execute (score0, score2);
				if (score0 == null) {
					return opponentScore;
				}
			}
		}
		return score3;
	}
	
	private bool IsValidExpression(){
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


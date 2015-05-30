using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Attributes
	//controls
	public string sButton1 = "";
	public string sButton2 = "";
	public string sButton3 = "";
	public string sButton4 = "";
	public string sButtonValidate = "";
	public string sButtonCancel = "";
	
	//other attributes
	private Card[] cards = new Card[3];
	private Card[] expression = new Card[2];
	public double opponentScore = 0;
	private int expressionScroller = 0;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			// get a set of random cards
			// TODO add the code !!!!
		}
	}
		
	void Update () {
			// get input
			int inputC1 = Input.GetButtonDown (sbutton1);
			int inputC2 = Input.GetButtonDown (sbutton2);
			int inputC3 = Input.GetButtonDown (sbutton3);
			int inputC4 = Input.GetButtonDown (sbutton4);
			int inputValidate = Input.GetButtonDown (sButtonValidate);
			int inputCancel = Input.GetButtonDown (sButtonCancel);
			
			// compose expression
			if (inputC1) {
				expression [expressionScroller] = cards[0];
				// TODO display function or operator
				expressionScroller++;
			}
			if (inputC2) {
				expression [expressionScroller] = cards[1];
				// TODO display function or operator
				expressionScroller++;
			}
			if (inputC3) {
				expression [expressionScroller] = cards[2];
				// TODO display function or operator
				expressionScroller++;
			}
			if (inputC4) {
				expression [expressionScroller] = cards[3];
				// TODO display function or operator
				expressionScroller++;
			}
			if (inputCancel) {
				expression [expressionScroller] = null;
				// TODO display function or operator
				expressionScroller--;
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
						// TODO forbidden mathematical operation signal
					}
					//reset expression to void
					expressionScroller = 0;
					expression[0] = null;
					expression[1] = null;
					expression[2] = null;
				}
				else{
					// TODO Not valid expression signal
				}
			}
		}
		
		private double ComputeOpponentScore(){
			if (expression [0] != null) {
				int score0 = expression [0].execute (opponentScore);
				if (score0 == null) {
					return opponentScore;
				}
			}
			if (expression [2] != null) {
				int score2 = expression [2].execute (opponentScore);
				if (score0 == null) {
					return opponentScore;
				}
				int score3 = expression [1].execute (score0, score2);
				if (score0 == null) {
					return opponentScore;
				}
			}
			return score3;
		}
		
		private bool IsValidExpression(){
			if (expression [0] != null) {
				if (expression [0] is Function) {
					if (expression [1] == null) {
						return true;
					} 
					else {
						if (expression [1] is Operator) {
							if(expression[2] !=null){
								if(expression[2] is Function){
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
					return false;
				}
			} 
			else {
				return false;
			}
		}		
	}
	

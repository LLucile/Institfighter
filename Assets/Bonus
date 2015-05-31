using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Questionnaire : MonoBehaviour {

	public Text createQuestion;
	public Text createAnswer;

	int currentQuestion, p;
	Question[] questionArray; 

	void Awake(){
		questionArray = new Question[]{
		    new Question ("8 x 7 ?", new string[]{"42", "52", "56"}, 2),
			new Question ("Quel est l'aire d'un cercle ?", new string[]{"πR^2", "π2R", "Rπ^2"}, 1),
			new Question ("Quel est la limite de x^2, quand x tend vers moins l'infinie ?", new string[]{"-oo", "+oo", "1"}, 2),
			new Question ("Quel est la limite de 1/x , quand x tend vers plus l'infinie ?", new string[]{"-oo", "+oo", "0"}, 3),
			new Question ("Quel est la dérivée de cos(x) ?", new string[]{"-sin(x)", "sin(x)", "cos(x)"}, 1),
			new Question ("Quel est la dérivée de sin(x) ?", new string[]{"sin(x)", "-sin(x)", "cos(x)"}, 3),
			new Question ("Quel est la dérivée de 4x ?", new string[]{"4x**2", "3x**4", "4x**3"}, 3),
			new Question ("9*8=", new string[]{"72", "82", "68"}, 1),
			new Question ("Quel est la primitive de 1/x ?", new string[]{"-1/x**2", "ln(x)", "-1"}, 2),
			new Question ("Quel est la primitiv<<e de exp(x) ?", new string[]{"ln(x)", "xexp(x)", "exp(x)"}, 3),
			new Question ("Que vaut exp(0)?", new string[]{"0", "1", "ln(1)"}, 2),
			new Question ("Quel nombre fait partie de la famille des entiers relatifs Z ?", new string[]{"-5", "0.02", "1/6"}, 1)
		};

	}

	void Update(){
		p =this.Afficher (createAnswer, createQuestion);
		this.Reponse (p);
	}

	public int Afficher(p){ 

		currentQuestion = (int) Random.Range(0,11);

		string[] reponse = questionArray[currentQuestion].GetAnswer();
		string s = "";
	 	
		string str = questionArray[currentQuestion].GetQuestion();
		createQuestion.text = str;

		for (int i= 0; i < 3; i++ ){
<			s = reponse[i] + "" ;

			createAnswer.text = s;
			Debug.Log(s);
		}

		return currentQuestion;
	}

	public bool Reponse(int quest){
		int gA = questionArray[quest].GetGoodAnswer();
		int playresp=0;
		bool bol = true;

		//take the player answer
		while(bol){
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				playresp=1;
				bol=false;
			}

			else if(Input.GetKeyDown(KeyCode.UpArrow)){
			  	playresp=2;
				bol=false;
			}


			else if(Input.GetKeyDown(KeyCode.RightArrow)){
			  	 playresp=3;
				 bol=false;
			}
		}
		// control the player answer

		if(playresp == gA ){
			return true;
		}
		else{
			return false;
		 }
	}
}

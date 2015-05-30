using UnityEngine;
using System.Collections;

[System.Serializable]
public class Card { 

	protected int weight;
	protected string name;
	public void Execute();
}

public class Operateur : Card {

	private Operators Operator;
	public Operateur(int weight, Operators Operator){
		this.weight = weight;
		this.Operator = Operator;
	}
	public void Execute(float f1, float f2){
		float res;
		switch(Operator)
		{
			case 0:
				res = f1 + f2;
				break;
			case 1:
				res = f1 - f2;
				break;
			case 2:
				res = f1*f2;
				break;
			case 3:
				res = f1/f2;
				break;
		}
		if(float.IsNaN(res))
			res=null;
		return res;
	}

}

public class Fonction : Card {
	public Functions Function;
	private Texture2D graph;
	public int constant;

	public Fonction(int weight, Functions Function, string name){
		this.weight = weight;
		this.Function = Function;
		this.name = name;
		if(Function == Functions.ax || Function == Functions.b || Function == Functions.sqrtXExpA || Function == Functions.pow)
			ConstantGeneration();
		else
			this.constant = null;
		this.graph=getGraph();
	}
	public void ConstantGeneration(){
		if(Function == Functions.sqrtXExp){
			do{
				int r = (int) Random.Range(-10.0F, 10.0F);
			}while(r==0.0F);
		else
			int r = (int) Random.Range(-10.0F, 10.0F);
		constant = r;
		name = constant.ToString()+" * "+name;
	}
	public Texture2D getGraph(){
		//lier avec fonctions shaders
	}
	public void Execute(float score){
		float res;
		switch(Function)
		{
			case 0:
				res = Mathf.Exp(score);
				break;
			case 1:
				res = Mathf.Log(score);
				break;
			case 2:
				res = constant*score;
				break;
			case 3:
				res = constant;
				break;
			case 4:
				res = Mathf.Pow(score,1/constant);
				break;
			case 5:
				res = Mathf.Pow(score,constant);
				break;
			case 6:
				res = Mathf.Sin(score);
				break;
			case 7:
				res = Mathf.Asin(score);
				break;
			case 8:
				res = (Mathf.exp(score)-Mathf.exp(-score))/2;
				break;
			case 9:
				res = Mathf.Log(score+Mathf.Sqrt(1+Mathf.Pow(score,2)));
				break;
			case 10:
				res = Mathf.Cos(score);
				break;
			case 11:
				res = Mathf.Acos(score);
				break;
			case 12:
				res = (Mathf.exp(score)+Mathf.exp(-score))/2;
				break;
			case 13:
				res = Mathf.Log(score+(Mathf.Sqrt(1+Mathf.Pow(score,2))*Mathf.Sqrt(1-Mathf.Pow(score,2))));
				break;
			case 14:
				res = Mathf.Tan(score);
				break;
			case 15:
				res = Mathf.Atan(score);
				break;
			case 16:
				res = (Mathf.exp(score)-Mathf.exp(-score))/(Mathf.exp(score)+Mathf.exp(-score));
				break;
			case 17:
				res = 1/2*(Mathf.Log(1+score)-Mathf.Log(1-score));
				break;
		}
		if(float.IsNaN(res))
				res=null;
		return res;
}
using UnityEngine;
using System.Collections;

public class Fonction : Card {
	public Functions Function;
	private Texture2D graph;
	public int? constant;
	
	public Fonction(int weight, Functions Function, string name){
		this.m_id = (int) Function;
		this.weight = weight;
		this.Function = Function;
		this.name = name;
		if(Function == Functions.ax || Function == Functions.b || Function == Functions.sqrtXExpA || Function == Functions.pow)
			ConstantGeneration();
		else
			this.constant = null;
		//this.graph=getGraph();
	}
	public void ConstantGeneration(){
		int r;
		if(Function == Functions.sqrtXExpA){
			do{
				r = (int) Random.Range(-0.0F, 10.0F);
			}while(r==0.0F);
		}
		else
			r = (int) Random.Range(-10.0F, 10.0F);
		constant = r;
		name = constant.ToString()+" * "+name;
	}
	//public Texture2D getGraph(){
		//lier avec fonctions shaders
	//}
	public float? Execute(float? score){
		float? res = null;
		switch(Function)
		{
		case Functions.e:
			res = Mathf.Exp((float) score);
			break;
		case Functions.ln:
			res = Mathf.Log((float) score);
			break;
		case Functions.ax:
			res = (float?) (constant*score);
			break;
		case Functions.b:
			res = (float?) constant;
			break;
		case Functions.sqrtXExpA:
			res = Mathf.Pow((float) score,1f/(float)constant);
			break;
		case Functions.pow:
			res = Mathf.Pow((float) score,(float)constant);
			break;
		case Functions.sin:
			res = Mathf.Sin((float) score);
			break;
		case Functions.arcsin:
			res = Mathf.Asin((float) score);
			break;
		case Functions.sinh:
			res = (Mathf.Exp((float) score)-Mathf.Exp(-(float) score))/2;
			break;
		case Functions.arcsinh:
			res = Mathf.Log((float) score+Mathf.Sqrt(1+Mathf.Pow((float) score,2)));
			break;
		case Functions.cos:
			res = Mathf.Cos((float) score);
			break;
		case Functions.arccos:
			res = Mathf.Acos((float) score);
			break;
		case Functions.cosh:
			res = (Mathf.Exp((float) score)+Mathf.Exp(-(float) score))/2;
			break;
		case Functions.arccosh:
			res = Mathf.Log((float) score+(Mathf.Sqrt(1+Mathf.Pow((float) score,2))*Mathf.Sqrt(1-Mathf.Pow((float) score,2))));
			break;
		case Functions.tan:
			res = Mathf.Tan((float) score);
			break;
		case Functions.tanh:
			res = Mathf.Atan((float) score);
			break;
		case Functions.arctan:
			res = (Mathf.Exp((float) score)-Mathf.Exp(-(float) score))/(Mathf.Exp((float) score)+Mathf.Exp(-(float) score));
			break;
		case Functions.arctanh:
			res = 1/2*(Mathf.Log(1+(float) score)-Mathf.Log(1-(float) score));
			break;
		}
		if (float.IsNaN ((float) res) |  (res == Mathf.Infinity) | float.IsNegativeInfinity((float) res) | float.IsPositiveInfinity((float) res)){
			Debug.Log("res = "+res);
			res = null;
		}

		return res;
	}
}
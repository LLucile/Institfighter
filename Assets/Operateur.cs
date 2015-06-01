using UnityEngine;
using System.Collections;

public class Operateur : Card {
	
	private Operators Operator;
	public Operateur(int weight, Operators Operator, string name){
		this.m_id = (int) Operator;
		this.weight = weight;
		this.Operator = Operator;
		this.name = name;
	}
	public float? Execute(float? f1, float? f2){
		float? res=null;
		switch(Operator)
		{
		case Operators.Plus:
			res = f1 + f2;
			break;
		case Operators.Minus:
			res = f1 - f2;
			break;
		case Operators.Multiply:
			res = f1*f2;
			break;
		case Operators.Divide:
			res = f1/f2;
			break;
		}
		return res;
	}
	
}
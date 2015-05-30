using UnityEngine;
using System.Collections;

[System.Serializable]
public class Card { 

	public int weight;
	public void Execute();
}

public class Operateur : Card {

	public Operators Operator;
	public Operateur(int weight, Operators Operator){
		this.weight = weight;
		this.Operator = Operator;
	}
	public void Execute(){}

}

public class Fonction : Card {
	public Functions Function;
	public Texture2D graph;
	public Fonction(int weight, Functions Function){
		this.weight = weight;
		this.Function = Function;
		this.graph=getGraph();
	}
	public Texture2D getGraph(){
		//lier avec fonctions shaders
	}
	public void Execute(Operateur op=null, Fonction carte2=null){
		if(){}
	}
}
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Card { 

	public int weight;
	public void Execute();
}

public class Operateur : Card {

	public Operators Operator;
	public Operators(int ){

	}
	public void Execute(){}

}

public class Fonction : Card {
	public Functions function;
	public Texture2D graph;
	public void Execute(){}
	public getGraph(){
		//lier avec fonctions shaders
	}
}
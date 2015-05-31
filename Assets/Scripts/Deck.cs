using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Deck {
	
	public List<Operateur> ope;
	public List<Fonction> fun;

	public Deck(){
		ope = new List<Operateur>();
		fun = new List<Fonction>();
		Debug.Log ("Creating a deck of cards");
	
		// -------------------------------------------------------------
		//     INITIALISATION DES CARTES
		// -------------------------------------------------------------

		ope.Add(new Operateur(5, Operators.Plus, "+"));
		ope.Add(new Operateur(5, Operators.Minus, "-"));
	    ope.Add(new Operateur(3, Operators.Multiply, "x"));
	    ope.Add(new Operateur(3, Operators.Divide, "/"));
	    fun.Add(new Fonction(3 ,Functions.e            ,"exp"));
	    fun.Add(new Fonction(3 ,Functions.ln           ,"log"));
	    fun.Add(new Fonction(10,Functions.ax           ,"lin"));
	    fun.Add(new Fonction(10,Functions.b            ,"const"));
	    fun.Add(new Fonction(5 ,Functions.sqrtXExpA    ,"n_sqrt"));
	    fun.Add(new Fonction(5 ,Functions.cos          ,"cos"));
	    fun.Add(new Fonction(5 ,Functions.arccos       ,"acos"));
	    fun.Add(new Fonction(5 ,Functions.sin          ,"sin"));
	    fun.Add(new Fonction(5 ,Functions.arcsin       ,"asin"));
	    fun.Add(new Fonction(5 ,Functions.sinh         ,"sinh"));
	    fun.Add(new Fonction(5 ,Functions.arcsinh      ,"asin"));
	    fun.Add(new Fonction(5 ,Functions.cosh         ,"cosh"));
	    fun.Add(new Fonction(5 ,Functions.arccosh      ,"acosh"));
	    fun.Add(new Fonction(5 ,Functions.tan          ,"tan"));
	    fun.Add(new Fonction(5 ,Functions.arctan       ,"atan"));
	    fun.Add(new Fonction(5 ,Functions.tanh         ,"tanh"));
	    fun.Add(new Fonction(5 ,Functions.arctanh      ,"atanh"));

		Debug.Log ("is fun empty ? " + (fun == null));
		Debug.Log ("Deck of cards has been created");
	}
    // ---------------------------------------------------------------
    // METHODES
    // ---------------------------------------------------------------
	        
    public Fonction PickFonction(){
		Debug.Log("bou");
		//int cumsum=0,r;
		Debug.Log ("is fun empty ? " + (fun == null));
		Debug.Log ("First fun card is " + fun [0].Function);
		int cumsum=0,r;
		Fonction O=fun[0];
		// sort the cards
		float sum = fun.Sum(P=>P.weight);
		r = (int) UnityEngine.Random.Range(1f,sum);
		// Console.WriteLine(sum);
		fun = fun.OrderBy(o=>o.weight).ToList();
		for(int i=0; i<fun.Count;i++){
			cumsum += fun[i].weight;
			if(cumsum > r){
				O = fun[i];
				break;
			}
		}
		Debug.Log ("coucou je suis sorti de PickFonction()");
		return O;
	}
	public Operateur PickOperateur(){
		Debug.Log("bou");
		int cumsum=0,r;
		Operateur O=ope[0];
		// sort the cards
		float sum = ope.Sum(P=>P.weight);
		r = (int) UnityEngine.Random.Range(1f,sum);
		// Console.WriteLine(sum);
		ope = ope.OrderBy(o=>o.weight).ToList();
		for(int i=0; i<ope.Count;i++){
			cumsum += ope[i].weight;
			if(cumsum > r){
				O = ope[i];
				break;
			}
		}
		Debug.Log ("Coucou je sors de PickOperator");
		return O;
	}
	
}
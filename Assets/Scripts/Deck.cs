using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Deck : MonoBehaviour {
	
	public List<Operateur> ope;
	public List<Fonction> fun;

	public Deck(){
		List<Operateur> ope = new List<Operateur>();
		List<Fonction> fun = new List<Fonction>();
	
		// -------------------------------------------------------------
		//     INITIALISATION DES CARTES
		// -------------------------------------------------------------

		ope.Add(new Operateur(5, Operators.Plus));
		ope.Add(new Operateur(5, Operators.Minus));
	    ope.Add(new Operateur(3, Operators.Multiply));
	    ope.Add(new Operateur(3, Operators.Divide));
	    fun.Add(new Fonction(3 ,Functions.e            ,"Exponetielle"));
	    fun.Add(new Fonction(3 ,Functions.ln           ,"Logarithme"));
	    fun.Add(new Fonction(10,Functions.ax           ,"Linéaire"));
	    fun.Add(new Fonction(10,Functions.b            ,"constante"));
	    fun.Add(new Fonction(5 ,Functions.sqrtXExpA    ,"racine nieme"));
	    fun.Add(new Fonction(5 ,Functions.cos          ,"cosinus"));
	    fun.Add(new Fonction(5 ,Functions.arccos       ,"arc-cosinus"));
	    fun.Add(new Fonction(5 ,Functions.sin          ,"sinus"));
	    fun.Add(new Fonction(5 ,Functions.arcsin       ,"arc-sin"));
	    fun.Add(new Fonction(5 ,Functions.sinh         ,"sinus hyperbolique"));
	    fun.Add(new Fonction(5 ,Functions.arcsinh      ,"arc-sinus"));
	    fun.Add(new Fonction(5 ,Functions.cosh         ,"cosinus hyperbolique"));
	    fun.Add(new Fonction(5 ,Functions.arccosh      ,"arc-cosinus hyperbolique"));
	    fun.Add(new Fonction(5 ,Functions.tan          ,"tangente"));
	    fun.Add(new Fonction(5 ,Functions.arctan       ,"arc-tangeante"));
	    fun.Add(new Fonction(5 ,Functions.tanh         ,"tangente hyperbolique"));
	    fun.Add(new Fonction(5 ,Functions.arctanh      ,"arc-tangeante hyperbolique"));
        Fonction p = this.PickFonction();
        Console.WriteLine(p.weight);
	}
    // ---------------------------------------------------------------
    // METHODES
    // ---------------------------------------------------------------
	        
    public Fonction PickFonction(){
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
		return O;
	}
	public Operateur PickOperateur(){
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
		return O;
	}
	
}
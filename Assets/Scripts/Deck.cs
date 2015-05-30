using UnityEngine;
using System.Collections;

public static class Deck : MonoBehaviour {

	public List<Operateur> ope = new List<Operator>();
	public List<Fonction> fun = new List<Function>();

	// -------------------------------------------------------------
	// 	INITIALISATION DES CARTES
	// -------------------------------------------------------------
	ope.Add(new Operateur()  {weight=5, Operator=Plus});
	ope.Add(new Operateur()  {weight=5, Operator=Minus});
	ope.Add(new Operateur()  {weight=3, Operator=Multiply});
	ope.Add(new Operateur()  {weight=3, Operator=Divide});

	fun.Add(new Fonction()   {weight=3 ,Function=e 			,name="Exponetielle"});
	fun.Add(new Fonction()   {weight=3 ,Function=ln 		,name="Logarithme" });
	fun.Add(new Fonction()   {weight=10,Function=ax 		,name="Linéaire"});
	fun.Add(new Fonction()   {weight=10,Function=b 			,name="constante"});
	fun.Add(new Fonction()   {weight=5 ,Function=sqrtXExpA 	,name="racine n-ieme"});
	fun.Add(new Fonction()   {weight=5 ,Function=cos 		,name="cosinus"});
	fun.Add(new Fonction()   {weight=5 ,Function=arccos 	,name="arc-cosinus"});
	fun.Add(new Fonction()   {weight=5 ,Function=sin 		,name="sinus"});
	fun.Add(new Fonction()   {weight=5 ,Function=arcsin 	,name="arc-sin"});
	fun.Add(new Fonction()   {weight=5 ,Function=sinh 		,name="sinus hyperbolique"});
	fun.Add(new Fonction()   {weight=5 ,Function=arcsinh 	,name="arc-sinus"});
	fun.Add(new Fonction()   {weight=5 ,Function=cosh 		,name="cosinus hyperbolique"});
	fun.Add(new Fonction()   {weight=5 ,Function=arccosh 	,name="arc-cosinus hyperbolique"});
	fun.Add(new Fonction()   {weight=5 ,Function=tan 		,name="tangente"});
	fun.Add(new Fonction()   {weight=5 ,Function=arctan 	,name="arc-tangeante"});
	fun.Add(new Fonction()   {weight=5 ,Function=tanh 		,name="tangente hyperbolique"});
	fun.Add(new Fonction()   {weight=5 ,Function=arctanh 	,name="arc-tangeante hyperbolique"});


	// ---------------------------------------------------------------
	// METHODES
	// =--------------------------------------------------------------

	Operateur Pick(int chooser){
		int cumsum;

		// sort the cards
		double sum = ope.Sum(P=>P.weight);
		// Console.WriteLine(sum);
		ope = ope.OrderBy(o=>o.weight).ToList();

		for(int i=0; i<ope.Count;i++){
			cumsum += ope[i].weight;
			if(cumsum > chooser){
				return ope[i];
			}
		}
	}

	Fonction Pick(int chooser){
		int cumsum;
		fun = fun.OrderBy(o=>o.weight).ToList();

		for(int i=0; i<fun.Count;i++){
			cumsum += fun[i].weight;
			if(cumsum > chooser){
				return fun[i];
			}
		}
	}
	
}

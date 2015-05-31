﻿using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	// attributes
	Card CL;
	Card CR;
	Card CT;
	Card CB;
	float T = 0;
	// Use this for initialization
	void Start () {
		Card CL = GameManager.heap.PickFonction ();
		Card CR = GameManager.heap.PickFonction ();
		Card CT = GameManager.heap.PickOperateur ();
		Card CB = GameManager.heap.PickOperateur ();

	}
	
	// Update is called once per frame
	void Update () {
		//update timers
		T = T - Time.deltaTime;
	}

	public float GetHandSlotWaitingTime(Actions a){
		return T;
	}

	public Card GetHandSlotCard(Actions a){
		switch (a) {
			case Actions.Left:
				return CL;
			case Actions.Right:
				return CR;
			case Actions.Bottom:
				return CB;
			case Actions.Top:
				return CT;
			default:
			return null;
		}	
	
	}

	public void SetHandSlotTime(int time){
		T = time;
	}
	

	public void GetNewCards(){
		if ((T < 10000) && (T > 0)) {
			Card CL = GameManager.heap.PickFonction ();
			Card CR = GameManager.heap.PickFonction ();
			Card CT = GameManager.heap.PickOperateur ();
			Card CB = GameManager.heap.PickOperateur ();
		}
	}


}

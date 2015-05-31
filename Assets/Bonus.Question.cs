using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question {
	public string question;
	public string[] answer;
	public int goodanswer;

	public Question(string q, string[] a , int g){
		this.question=q;
		this.answer=a;
		this.goodanswer=g;
	}

	public string GetQuestion(){
		return question;
	}
	public string[] GetAnswer(){
		return answer;
	}
	public int GetGoodAnswer(){
		return goodanswer;
	}
}

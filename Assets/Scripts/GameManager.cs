﻿using UnityEngine;
using System.Collections;

// Game States
// for now we are only using these two
public enum GameState { INTRO, MAIN, MINIGAME }

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {
	public static Deck heap;
<<<<<<< HEAD
=======
	void Start () {
		heap = new Deck();
	}
>>>>>>> origin/master
	protected GameManager() {}
	private static GameManager instance = null;
	public event OnStateChangeHandler OnStateChange;
	public  GameState gameState { get; private set; }

	void Start() {
		heap = new Deck();
		Debug.Log("first card is" + heap.operator[0]);
	}
	
	public static GameManager Instance{
		get {
			if (GameManager.instance == null){
				DontDestroyOnLoad(GameManager.instance);
				GameManager.instance = new GameManager();
			}
			return GameManager.instance;
		}
		
	}
	
	public void SetGameState(GameState state){
		this.gameState = state;
		OnStateChange();
	}
	
	public void OnApplicationQuit(){
		GameManager.instance = null;
	}
	
}
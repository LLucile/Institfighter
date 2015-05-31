using UnityEngine;
using System.Collections;

// Game States
// for now we are only using these two
public enum GameState { INTRO, MAIN, MINIGAME }

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {
	public Deck heap;
	protected GameManager() {}
	public static GameManager Instance = null;
	public event OnStateChangeHandler OnStateChange;
	public  GameState gameState { get; private set; }

	void Awake() {
		Debug.Log ("Game Manager started");
		heap = new Deck();
		//Debug.Log (
		Debug.Log ("is heap.fun empty ? " + (heap.ope == null));
		if (GameManager.Instance == null){
			Instance = this;
		}
		else if (Instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad(GameManager.Instance);
	}

	
	public void SetGameState(GameState state){
		this.gameState = state;
		OnStateChange();
	}
	
	public void OnApplicationQuit(){
		GameManager.Instance = null;
	}
	
}
using UnityEngine;
using System.Collections;

public enum GameState { INTRO, MAIN, MINIGAME }

public class GameManager : MonoBehaviour {
	public float maxScore;

	public Deck heap;
	public static GameManager Instance = null;
	public GameState gameState { get; private set; }

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
	}
	
	public void OnApplicationQuit(){
		GameManager.Instance = null;
	}
}
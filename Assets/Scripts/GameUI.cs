using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject[] PlayerUI;
	public Image[] playersHealth;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetHealth(int player, float amount){
		playersHealth[player].fillAmount = Mathf.Abs(amount);
	}
}

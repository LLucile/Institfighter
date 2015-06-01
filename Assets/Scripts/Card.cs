using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Card { 
	public int m_id; // Value from Functions nor Opertors enums.
	public int weight;
	public string name;
	//abstract public float? Execute(float? score, float? score2);
}
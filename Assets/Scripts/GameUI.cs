using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject[] PlayerUI;
	public Image[] playersHealth;
	public Text[] playersScore;
	public UICard[] templates;

	public RectTransform[] p1cards;
	public RectTransform[] p2cards;

	public CameraShake cameraShake;

	public Renderer graphViewer;

	public static GameUI Instance = null;
	
	void Awake() {
		if (GameUI.Instance == null){
			Instance = this;
		}
		else if (Instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad(GameUI.Instance);
	}

	// Use this for initialization
	void Start () {
		resetGraphViewer();
	}
	
	// Update is called once per frame
	void Update ()
	{
		updateGraphInterpol ();
	}

	public void SetHealth(int player, float amount)
	{
		if (player == 2)
		{
			graphViewer.material.SetFloat("_Score", amount);
		}
		playersScore[player].text = amount+"";
		playersHealth[player].fillAmount = Mathf.Abs(Mathf.Min(amount, GameManager.Instance.maxScore)/GameManager.Instance.maxScore);
	}

	public void CreateCard(int player, int number, Card card){

		RectTransform parent;
		if (player == 1) {
			parent = p2cards [number];
		} else {
			parent = p1cards [number];
		}

		UICard instance = Instantiate (templates [player]).GetComponent<UICard>();
		instance.transform.SetParent (parent, false);

		instance.Setup (card);
	}

	public void RemoveCard (int player, int number){
		Debug.Log("GameUi::RemoveCard");
		UICard card = GetCard (player, number);
		if (card != null)
			Destroy (card.gameObject);
	}

	public void SelectCard(int player, int number, bool selected)
	{
		Debug.Log("GameUi::SelectCard");
		UICard card = GetCard (player, number);
		card.Select(selected);

		if (player == 1)
			updateGraphFunctionNorOperator (selected, card.m_card);
	}
	
	public void Shake(float intensity){
		cameraShake.ShakeCamera(intensity, 4f, new Vector3());
	}

	public void resetGraphViewer (){
		graphViewer.material.SetFloat ("_FuncA", -1f);
		graphViewer.material.SetFloat ("_FuncB", -1f);
		graphViewer.material.SetFloat ("_Operator", -1f);
		graphViewer.material.SetFloat ("_Score", Mathf.PI);
		graphViewer.material.SetFloat("_FuncInterpolA", 0f);
		graphViewer.material.SetFloat("_FuncInterpolB", 0f);
		graphViewer.material.SetFloat("_FuncInterpolResult", 0f);
	}

	UICard GetCard(int player, int number){
		RectTransform parent;
		if (player == 1) {
			parent = p2cards [number];
		} else {
			parent = p1cards [number];
		}
		
		return parent.GetComponentInChildren<UICard> ();
	}

	public void onValidate (bool isValidExpression)
	{
		resetGraphViewer ();
	}
	
	private void updateGraphFunctionNorOperator (bool selected, Card card)
	{
		if (selected) {
			if (card is Fonction) {
				float funcId = graphViewer.material.GetFloat ("_FuncA");
				if (funcId < 0f) {
					graphViewer.material.SetFloat ("_FuncA", (float)card.m_id);
					graphViewer.material.SetFloat ("_FuncInterpolA", 0f);
				} else {
					graphViewer.material.SetFloat ("_FuncB", (float)card.m_id);
					graphViewer.material.SetFloat ("_FuncInterpolB", 0f);
				}

				Fonction funcCard = card as Fonction;
				switch ((Functions) card.m_id)
				{
				case Functions.ax:
				case Functions.sqrtXExpA:
				case Functions.pow:
					graphViewer.material.SetFloat ("_ValA", (float) funcCard.constant);
					break;
				case Functions.b:
					graphViewer.material.SetFloat ("_ValB", (float) funcCard.constant);
					break;
				}
			} else //if (card is Operateur)
				graphViewer.material.SetFloat ("_Operator", (float)card.m_id);
		} else {
			if (card is Fonction)
			{
				float funcId = graphViewer.material.GetFloat ("_FuncA");
				if (funcId == card.m_id)
				{
					graphViewer.material.SetFloat("_FuncA", -1);
					graphViewer.material.SetFloat ("_FuncInterpolA", 0f);
					graphViewer.material.SetFloat("_FuncInterpolResult", 0f);
				}
				else
				{
					graphViewer.material.SetFloat("_FuncB", -1);
					graphViewer.material.SetFloat ("_FuncInterpolB", 0f);
					graphViewer.material.SetFloat("_FuncInterpolResult", 0f);
				}
			}
			else if (card is Operateur)
			{
				graphViewer.material.SetFloat("_Operator", -1);
				graphViewer.material.SetFloat("_FuncInterpolResult", 0f);
			}
		}
	}

	private void updateGraphInterpol()
	{
		float interpolA = 0f;
		float interpolB = 0f;
		float funcAId = graphViewer.material.GetFloat("_FuncA");
		if (funcAId >= 0f)
		{
			interpolA = graphViewer.material.GetFloat("_FuncInterpolA");
			if (interpolA < 1f)
			{
				interpolA = Mathf.Min(interpolA + Time.deltaTime, 1f);
				graphViewer.material.SetFloat("_FuncInterpolA", interpolA);
			}
		}
		
		float funcBId = graphViewer.material.GetFloat("_FuncB");
		if (funcBId >= 0f)
		{
			interpolB = graphViewer.material.GetFloat("_FuncInterpolB");
			if (interpolB < 1f)
			{
				interpolB = Mathf.Min(interpolB + Time.deltaTime, 1f);
				graphViewer.material.SetFloat("_FuncInterpolB", interpolB);
			}
		}

		float opId = graphViewer.material.GetFloat("_Operator");
		if ((funcAId >= 0f) && (interpolA == 1f) &&
		    (funcBId >= 0f) && (interpolB == 1f) &&
		    (opId >= 0f))
		{
			float interpol = graphViewer.material.GetFloat("_FuncInterpolResult");
			if (interpol < 1f)
			{
				interpol = Mathf.Min(interpol + Time.deltaTime, 1f);
				graphViewer.material.SetFloat("_FuncInterpolResult", interpol);
			}
		}
	}
}

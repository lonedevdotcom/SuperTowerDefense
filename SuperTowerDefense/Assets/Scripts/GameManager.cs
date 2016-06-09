using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	private int score = 0;
	private int credits = 0;
	private int health = 100;

	public Text scoreText;
	public Text creditsText;
	public Text healthText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public int GetScore() {
		return score;
	}

	public void SetScore(int newScore) {
		score = newScore;
		scoreText.text = "" + score;
	}

	public int GetCredits() {
		return credits;
	}

	public void SetCredits(int newCredits) {
		credits = newCredits;
		creditsText.text = "" + credits;
	}

	public int GetHealth() {
		return health;
	}

	public void SetHealth(int newHealth) {
		Debug.Log ("Setting health to " + newHealth);
		health = newHealth;
		healthText.text = "" + health;
	}
}

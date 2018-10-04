using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    int score;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}

    public void scoreHit(int scorePerHit)
    {
        score += scorePerHit;
        scoreText.text = score.ToString();
    }

    public void scoreDeath()
    {
        score /= 2;
        scoreText.text = score.ToString();
    }
}

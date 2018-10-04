using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    ScoreBoard scoreBoard;

    private void OnTriggerEnter(Collider other)
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        gameObject.SendMessage("StopMovements");
        deathFX.SetActive(true);
        scoreBoard.scoreDeath();

        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

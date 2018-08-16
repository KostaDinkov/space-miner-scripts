using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject[] Hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Button ResetButton;
    public Text GameOverText;
    public Text ScoreText;
    public GameObject Player;

    private int gameScore;
    private bool gameOver = false;    
    

    void Start()
    {
        
        this.ResetButton.onClick.AddListener(this.RestartLevel);
        this.Player.transform.position = new Vector3(0,0,0);
        this.gameScore = 0;
        StartCoroutine(SpawnWave());
        this.GameOverText.gameObject.SetActive(false); 
        this.ResetButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        while (!this.gameOver)
        {             
            for (int i = 0; i < hazardCount; i++)
            {
                var hazard = Hazards[Random.Range(0, Hazards.Length)];
                var position = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                var rotation = Quaternion.identity;
                Instantiate(hazard, position, rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        
    }

    public void UpdateScore(int score)
    {
        this.gameScore += score;
        ScoreText.text = $"Score: {gameScore}";
    }

    public void GameOver()
    {
        this.gameOver = true;
        this.GameOverText.gameObject.SetActive(true);
        this.ResetButton.gameObject.SetActive(true);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
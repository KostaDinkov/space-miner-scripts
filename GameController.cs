using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Levels;

    public static GameController instance;
    private bool gameOver = false;

    void Awake()
    {
        //Make sure there is only one instance of the GameController class (Singleton)
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        //TODO move all game state to GameData


        GameData.LevelCount = this.Levels.Length;
        GameProgressBar.instance.Init(GameData.LevelCount);
        UIManager.instance.ActivateLevel(GameData.CurrentLevelIndex);
        InitLevel();
    }


    void InitLevel()
    {
        this.Player.transform.position = new Vector3(0, 0, 0);
        GameData.CurrentLevel = Instantiate(Levels[GameData.CurrentLevelIndex]);
    }


    public void RestartLevel()
    {
        Destroy(GameData.CurrentLevel);
        InitLevel();
    }

    public void NextLevel()
    {
        if (GameData.CurrentLevelIndex + 1 >= this.Levels.Length)
        {
            return;
        }

        Destroy(GameData.CurrentLevel);
        GameData.CurrentLevelIndex += 1;
        InitLevel();
        UIManager.instance.ActivateLevel(GameData.CurrentLevelIndex);
    }
}
using UnityEngine;

public class GameProgressBar : MonoBehaviour
{
    public GameObject ProgressIndicator;
    private GameObject[] levels;
    private const float IndicatorAlpha = 0.5f;
    public static GameProgressBar instance;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
    }

    public void Init(int lvlCount)
    {
        this.levels = new GameObject[lvlCount];
        for (int i = 0; i < lvlCount; i++)
        {
            var indicator = Instantiate(ProgressIndicator, transform.position + new Vector3(i / 4f, 0, 0), transform.rotation);
            this.levels[i] = indicator;
                
            var color = indicator.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            indicator.GetComponent<SpriteRenderer>().color = color;

        }
    }

    public void ActivateLevel(int level)
    {
        var indicator = levels[level].GetComponent<SpriteRenderer>();
        var currentColor = indicator.color;
        currentColor.a = 1;
        indicator.color = currentColor;
    }
}
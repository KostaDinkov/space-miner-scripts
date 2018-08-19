using UnityEngine;

public class TargetDestination : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.instance.NextLevelButton.interactable = true;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

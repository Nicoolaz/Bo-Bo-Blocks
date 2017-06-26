using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LowPolyWater");
        }
	}
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("LowPolyWater");
    }
}

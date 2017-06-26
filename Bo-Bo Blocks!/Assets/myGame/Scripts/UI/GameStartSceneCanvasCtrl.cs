using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartSceneCanvasCtrl : MonoBehaviour {
    Button startBtn;
    
    void Awake()
    {
        InitialReferences();
    }

    void InitialReferences()
    {
        startBtn = transform.Find("StartGame").GetComponent<Button>();

        startBtn.onClick.AddListener(StartGame);
    }
    void StartGame()
    {
        SceneManager.LoadScene("LowPolyWater");
    }
}

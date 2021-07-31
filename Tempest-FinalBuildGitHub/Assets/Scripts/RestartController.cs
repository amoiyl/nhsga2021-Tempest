using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private void Awake()
    {
       scoreText.text = PlayerPrefs.GetInt("FinalScore", 0) + "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

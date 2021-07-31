using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreHandler : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    
    void Start()
    {
        scoreText.text = PlayerPrefs.GetFloat("FinalScore", 0) + "";
    }
}

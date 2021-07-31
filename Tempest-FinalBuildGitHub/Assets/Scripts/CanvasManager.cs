using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public List<GameObject> lifeIcons;
    public GameObject livesCanvas;

    void Start()
    {
    }
    
    public void SetLife(int lives)
    {
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            if (i < lives) {
                lifeIcons[i].SetActive(true);
            } else {
                lifeIcons[i].SetActive(false);
            }
        }
    }

}

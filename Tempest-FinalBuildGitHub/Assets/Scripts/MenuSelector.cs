using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    [SerializeField]
    public GameObject[] options = new GameObject[3];

    private int currentSelection = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            ChooseScene();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection = (currentSelection + 1 + options.Length) % options.Length;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelection = (currentSelection - 1 + options.Length) % options.Length;
        }
        transform.position = new Vector3(transform.position.x, options[currentSelection].transform.position.y, transform.position.z);
    }

    private void ChooseScene() {
        switch (currentSelection)
        {
            case 0:
                SceneManager.LoadScene("GamePlay");
                break;
            case 1:
                SceneManager.LoadScene("Options");
                break;
            default:
                return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlaneObject
{
    public GameObject canvasManager;
    public Animator playerAnimator;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && LevelManager.Instance.canPlayerMove)
        {
            StartCoroutine(Lerp(IncrementLocation(-1), 0.05f));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && LevelManager.Instance.canPlayerMove)
        {
            StartCoroutine(Lerp(IncrementLocation(1), 0.05f));
        }

        try
        {
            mapManager.SelectLocationColor(objectLocation);
        }
        catch {}
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            LevelManager.Instance.RestartLevel();
            Destroy(collider.gameObject);
        }
    }
}

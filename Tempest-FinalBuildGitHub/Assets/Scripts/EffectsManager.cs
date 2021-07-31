using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public AudioSource exp1;
    public AudioSource shoot;
    public AudioSource exp2;

    static EffectsManager Instance;
    public static EffectsManager instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<EffectsManager>();
                if (Instance == null)
                {
                    GameObject em = new GameObject();
                    em.name = "EffectsManager";
                    Instance = em.AddComponent<EffectsManager>();
                    DontDestroyOnLoad(em);
                }
            }
            return Instance;
        }

    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playShoot()
    {
        shoot.Play();
    }

    public void playExp1()
    {
        exp1.Play();
    }
    public void playExp2()
    {
        exp2.Play();
    }

}

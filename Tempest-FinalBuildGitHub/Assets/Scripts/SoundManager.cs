using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SoundManager : MonoBehaviour
{
    public AudioSource introFade;
    public AudioSource intro;

    public AudioSource mainFade;
    public AudioSource mainMusic;

    public AudioSource deathMusic;

    public IEnumerator IntroTran;
    public IEnumerator MainTran;
    public IEnumerator EndingTran;

    public bool loop;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        IntroTran = TransitionToIntro();
        MainTran = TransitionToMain();
        EndingTran = TransitionToEnding();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log(scene.name);
        if (scene.name == "MainMenu")
        {
            StartCoroutine(IntroTran);
        }
        else if (scene.name == "GamePlay")
        {
            StartCoroutine(MainTran);
        }
        else if (scene.name == "GameOver") {
            StartCoroutine(EndingTran);
        }
    }

    private IEnumerator TransitionToIntro()
    {
        deathMusic.Stop();
        StopCoroutine(EndingTran);

        introFade.Play();
        yield return new WaitWhile(() => introFade.isPlaying);
        intro.loop = true;
        intro.Play();
    }

    private IEnumerator TransitionToMain() {
        intro.loop = false;
        introFade.Stop();
        intro.Stop();
        StopCoroutine(IntroTran);

        mainFade.Play();
        yield return new WaitWhile(() => mainFade.isPlaying);
        mainMusic.loop = true;
        mainMusic.Play();
    }

    private IEnumerator TransitionToEnding() {
        mainMusic.loop = false;
        mainMusic.Stop();
        mainFade.Stop();
        StopCoroutine(MainTran);

        deathMusic.Play();
        yield break;
    }

}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAni;
    public GameObject sceneTransition;
    public bool isChangeing;

    private void Start()
    {
        StartCoroutine(LoadScene2());
    }

    public void ExitGame()
    {
        if (!isChangeing)
        {
            Application.Quit();
            Debug.Log("Game closed");
        }
    }
    
    public void HelpButton()
    {
        if (!isChangeing)
        {
            Debug.Log("Nope");
        }
    }

    public void NextScene()
    {
        if (!isChangeing)
        {
            StartCoroutine(LoadScene(1));
        }
    }
    
    public void LastScene()
    {
        if (!isChangeing)
        {
            StartCoroutine(LoadScene(-1));
        }
    }
    
    IEnumerator LoadScene(int transitionNum)
    {
        isChangeing = true;
        sceneTransition.SetActive(true);
        transitionAni.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + transitionNum);
    }
    
    IEnumerator LoadScene2()
    {
        isChangeing = true;
        sceneTransition.SetActive(true);
        transitionAni.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        sceneTransition.SetActive(false);
        isChangeing = false;
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                transform.GetComponent<CountDownTimer>().enabled = true;
                break;
        }
    }
}

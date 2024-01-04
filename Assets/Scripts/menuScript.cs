using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public void OnPlayButton() {

        SceneManager.LoadScene(1);
    }

    public void OnQuitButton() {

        SceneManager.LoadScene(3);
    }

    public void OnRetruntButton()
    {

        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("ForestMap");
            SceneManager.LoadScene("CreepyThings", LoadSceneMode.Additive);
        }
    }
}

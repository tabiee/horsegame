using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneStuff : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

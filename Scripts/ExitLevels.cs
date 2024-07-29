using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevels : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip nextlevel;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("LoadScene", delay);
            AudioSource.PlayClipAtPoint(nextlevel, Camera.main.transform.position);
        }
    }
    void LoadScene()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        int nextscene = currentscene + 1;
        if (nextscene == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextscene);
        }
    }
}
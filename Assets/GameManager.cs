using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (!instance) instance = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ReloadScene();
    }

    public void Die()
    {
        ReloadScene();
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

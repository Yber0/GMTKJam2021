using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] private AudioSource source;
    public AudioClip[] snapSounds;
    public Animator transition;
    public float transitionTime;
    private bool loading = false;

    private void Awake()
    {
        if (!instance) instance = this;
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ReloadScene();
    }
    public void ReloadScene()
    {
        if (!loading)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            loading = true;
        }
    }
    public void LoadNextScene()
    {
        if (!loading)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            loading = true;
        }
    }
    public void LoadScene(int buildIndex)
    {
        if (!loading)
        {
            StartCoroutine(LoadLevel(buildIndex));
            loading = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void PlaySoundClip()
    {
        var index = Random.Range (0, snapSounds.Length);
        source.clip = snapSounds[index];
        source.Play();
    }

    IEnumerator LoadLevel(int levelindex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelindex);
        
        loading = false;
    }
}

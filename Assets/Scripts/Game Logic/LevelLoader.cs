using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator fadeTransition;
    [SerializeField] private Animator musicTransition;
    public float transitionTime = 1f;

    ///<Summary>Load the next level based on build index order.</Summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    ///<Summary>Play a fading transition between levels and load the scene.</Summary>
    private IEnumerator LoadLevel(int _levelIndex)
    {
        fadeTransition.SetTrigger("Start");

        if (musicTransition != null)
        {    
            musicTransition.SetTrigger("Stop");
        }

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(_levelIndex);
    }
}

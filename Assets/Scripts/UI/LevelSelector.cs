using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString()) ;
    }
}

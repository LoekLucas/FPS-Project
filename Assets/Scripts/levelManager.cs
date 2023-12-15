using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    // Variables
    public string sceneName;


    public void changescene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

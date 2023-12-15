using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    // Variables
    public string sceneName; // Asks for a scene name


    public void changescene()
    {
        SceneManager.LoadScene(sceneName); // Loads scene with given scene name
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerModePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pvpButton()
    {
        SceneManager.LoadScene("PlayScenePvP");
    }
    public void pvcButton()
    {
        SceneManager.LoadScene("PlayScenePvC");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerMenu : MonoBehaviour
{

    public AudioSource ads;
    public AudioClip song;
    // Start is called before the first frame update
    void Start()
    {
        ads.PlayOneShot(song);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("ModePlay");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public float timeLeft;

    bool timeOn = false;

    public Text timeText;

    public bool isTimeup;
    public AudioSource adSource;

    public AudioClip endSound;

    // Start is called before the first frame update
    void Start()
    {
        isTimeup = false;
        timeOn = true;

    }

    // Update is called once per frame
    void Update()
    {
        timeRunner();
    }

    void timeRunner()
    {
        if (timeLeft>0)
        {
            timeLeft -= Time.deltaTime;
            timeUpdate(timeLeft);
        }
        else
        {
            Debug.Log("Time Up");
            timeLeft = 0;
            timeOn = false;
            isTimeup = true;

        }
    }

    void timeUpdate(float currentTime)
    {
        currentTime += 1;
        int second = (int)currentTime;
        timeText.text = second.ToString();
    }
}


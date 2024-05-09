using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text leftScoreText;
    public Text rightScoreText;

    int leftScoreInt;
    int rightScoreInt;
    int incresScore;

    public GameObject goalPanel;

    public GameObject timeUPanel;

    GameObject _ball;

    GameObject _player;

    GameObject _clock;

    GameObject _ai;

    public AudioSource adS;
    public AudioClip soundStart;
    public AudioClip soundEnd;
    public AudioClip soundAudince;


    // Start is called before the first frame update
    void Start()
    {
        adS.PlayOneShot(soundStart);

        incresScore = 1;
        leftScoreInt=0;
        rightScoreInt=0;

        leftScoreText.text = leftScoreInt.ToString();
        rightScoreText.text = rightScoreInt.ToString();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
        _clock = GameObject.FindGameObjectWithTag("Clock");
        _ai = GameObject.FindGameObjectWithTag("Ai");
    }

    // Update is called once per frame
    void Update()
    {
        scoreIncrement();
    }

    void scoreIncrement()
    {
        if (_ball.GetComponent<Ball>().leftGoal == true)
        {
            rightScoreInt+=incresScore;
            rightScoreText.text = rightScoreInt.ToString();
            _ball.GetComponent<Ball>().leftGoal = false;
            
            StartCoroutine(pauseTime());

        }

        if (_ball.GetComponent<Ball>().rightGoal == true)
        {
            leftScoreInt+=incresScore;
            leftScoreText.text = leftScoreInt.ToString();

            _ball.GetComponent<Ball>().rightGoal = false;
            StartCoroutine(pauseTime());
            
        }
        CheckTimeUp();
    }

    IEnumerator  pauseTime()
    {
        pauseGame();
        goalPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        scorePlayer();
        goalPanel.SetActive(false);
    }

    void pauseGame()
    {
        incresScore = 0;
        _ball.GetComponent<Ball>().jumpForce = 0;
        _player.GetComponent<Player>().isShoot = false;
        _player.GetComponent<Player>().moveSpeed = 0;
        _ai.GetComponent<Ai>().speed = 0;
        
    }
    void scorePlayer()
    {
        _ball.GetComponent<Ball>().scoreBall();
        _player.GetComponent<Player>().scorePlayer();
        _ai.GetComponent<Ai>().scoreAi();

        incresScore = 1;
    }

    void CheckTimeUp()
    {
        if (_clock.GetComponent<Clock>().isTimeup == true)
        {
            pauseGame();
            adS.PlayOneShot(soundEnd);
            adS.PlayOneShot(soundAudince);
            timeUPanel.SetActive(true);
        }
    }


}

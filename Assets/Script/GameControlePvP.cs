using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlePvP : MonoBehaviour
{
    public Text leftScoreText;
    public Text rightScoreText;

    int leftScoreInt;
    int rightScoreInt;
    int incresScore;

    public Image leftFlag;
    public Image rightFlag;
    public Image winFlag;

    public GameObject goalPanel;

    public GameObject timeUPanel;

    GameObject _ball;

    GameObject[] _player;

    GameObject _clock;


    public AudioSource adS;
    public AudioClip soundStart;
    public AudioClip soundEnd;
    public AudioClip soundAudince;
    public AudioClip soundGoal;

    string currentSceneName;

    bool isgoal;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    void Start()
    {
        isgoal = false;
        adS.PlayOneShot(soundStart);

        incresScore = 1;
        leftScoreInt = 0;
        rightScoreInt = 0;

        leftScoreText.text = leftScoreInt.ToString();
        rightScoreText.text = rightScoreInt.ToString();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectsWithTag("Player");
        _clock = GameObject.FindGameObjectWithTag("Clock");

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
            rightScoreInt += incresScore;
            rightScoreText.text = rightScoreInt.ToString();
            _ball.GetComponent<Ball>().leftGoal = false;
            adS.PlayOneShot(soundGoal);
            StartCoroutine(pauseTime());
            isgoal = true;
        }

        if (_ball.GetComponent<Ball>().rightGoal == true)
        {
            leftScoreInt += incresScore;
            leftScoreText.text = leftScoreInt.ToString();

            _ball.GetComponent<Ball>().rightGoal = false;
            adS.PlayOneShot(soundGoal);
            StartCoroutine(pauseTime());
            isgoal = true;

        }
        CheckTimeUp();
    }

    IEnumerator pauseTime()
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
        _player[0].GetComponent<Player>().isShoot = false;
        _player[0].GetComponent<Player>().moveSpeed = 0;
        _player[1].GetComponent<Player>().isShoot = false;
        _player[1].GetComponent<Player>().moveSpeed = 0;

    }
    void scorePlayer()
    {
        _ball.GetComponent<Ball>().scoreBall();

        _player[0].GetComponent<Player>().scorePlayer();
        _player[1].GetComponent<Player>().scorePlayer();


        incresScore = 1;
    }

    void CheckTimeUp()
    {
        if ((_clock.GetComponent<Clock>().isTimeup == true))
        {
            if (leftScoreInt != rightScoreInt)
            {
                pauseGame();
                adS.PlayOneShot(soundEnd);
                adS.PlayOneShot(soundAudince);
                if (leftScoreInt > rightScoreInt)
                {
                    winFlag.sprite = leftFlag.sprite;
                }
                if (leftScoreInt < rightScoreInt)
                {
                    winFlag.sprite = rightFlag.sprite;
                }
                timeUPanel.SetActive(true);
            }

        }
    }

    public void RePlay()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}

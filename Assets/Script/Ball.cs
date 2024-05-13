using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float jumpForce;
    Rigidbody2D m_rb;

    GameObject _player;

    GameObject _playerAi;

    public bool leftGoal;
    public bool rightGoal;

    Vector3 startPosition;

    public AudioSource adSource;

    public AudioClip ballSound;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAi = GameObject.FindGameObjectWithTag("Ai");
        m_rb = GetComponent<Rigidbody2D>();

        leftGoal = false;
        rightGoal = false;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scoreBall()
    {
        jumpForce = 200;
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            m_rb.AddForce(Vector2.up*jumpForce);
            adSource.PlayOneShot(ballSound);
        }

        if (col.gameObject.CompareTag("TopGoal") )
        {
            m_rb.AddForce(Vector2.up * (jumpForce));
        }

       

        if (col.gameObject.CompareTag("BackGoalLeft"))
        {
            leftGoal = true;
        }
        if (col.gameObject.CompareTag("BackGoalRight"))
        {
            rightGoal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<Player>().isShoot = true;
        }

        if (col.gameObject.CompareTag("Ai"))
        {

            _playerAi.GetComponent<Ai>().isShootAi = true;
        }

        if (col.gameObject.CompareTag("CheckJump"))
        {
            _playerAi.GetComponent<Ai>().isJumpAi = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<Player>().isShoot = false;
        }

        if (col.gameObject.CompareTag("Ai"))
        {
            _playerAi.GetComponent<Ai>().isShootAi = false;
            _playerAi.GetComponent<Ai>().animator.SetBool("isShoot", false);
        }

        if (col.gameObject.CompareTag("CheckJump"))
        {
            _playerAi.GetComponent<Ai>().isJumpAi = false;
            
        }
    }
}

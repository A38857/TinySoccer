using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public float    defenceRange,
                    speed,
                    dataMoveSpeed;

    public float shootForce;
    public float jumpForce;

    public Transform deference;

    public Transform _goal;
    public Transform moveLimitLeft,
                     moveLimitRight;

    GameObject _ball;

    public bool isShootAi;
    public bool isJumpAi;
    public bool isGroundAi;

    public Transform _ballTransf;
    Vector3 startPosition;

    Rigidbody2D rbAi;

    public Animator animator;

    public AudioSource adSource;

    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        dataMoveSpeed = speed;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        rbAi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
 
        Mover();
        shootBall();
        if(isGroundAi == true) jumpAi();

    }


    void Mover()
    {
        Debug.Log(_ball.transform.localPosition.x + "    :   " + transform.position.x);
        //Debug.Log(_ball.transform.position.x + "    :   " + transform.position.x);
        if (_ball.transform.position.x <= transform.position.x)
        {
            animator.SetFloat("Speed",0.5f);
            if (Mathf.Abs(_ball.transform.position.x - transform.position.x) < defenceRange)
            {
 
                rbAi.velocity = new Vector2(-speed, rbAi.position.y);
            }
            else
            {
                rbAi.velocity = new Vector2(0, rbAi.position.y);
                animator.SetFloat("Speed", 0);
            }
        }
        else if((_ball.transform.position.x >= transform.position.x))
        {
            animator.SetFloat("Speed", 0.5f);
            rbAi.velocity = new Vector2(speed, rbAi.position.y);
        }

    }


    void shootBall()
    {

        if (isShootAi == true)
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-40, 20));
            Debug.Log("Shoot");
            animator.SetBool("isShoot", true);
            adSource.PlayOneShot(shootSound);
        }

    }

    void jumpAi()
    {
        Debug.Log("Is Jump: "+isJumpAi);
        if (isJumpAi == true)
        {
            animator.SetBool("isJump", true);
            rbAi.AddForce(Vector2.up * jumpForce);
            isGroundAi = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJump", false);
            isGroundAi = true;
        }

    }

    public void scoreAi()
    {
        speed = dataMoveSpeed;
        transform.position = startPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D m_rd;
    public float moveSpeed,
                dataMoveSpeed,
                jumpForce;

    bool isGround;

    float moveX;

    GameObject _ball;

    public int idPlayer;

    public bool isShoot;

    public Animator animator;

    public AudioSource adSource;

    public AudioClip shootSound;

    Vector3 startPosition;

    int shotX;

    // Start is called before the first frame update
    void Start()
    {
        dataMoveSpeed = moveSpeed;
         isShoot=false;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        m_rd = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        moveX = 0;

        if (transform.localScale.x == -1) shotX = -350;
        else if (transform.localScale.x == 1) shotX = 350;
    }

    // Update is called once per frame
    void Update()
    {
        if (idPlayer == 0)
        {
            movePlayer1();
            jumpPlayer1();
            shootBallPlayer1();
        }

        else if(idPlayer == 1)
        {
            movePlayer2();
            jumpPlayer2();
            shootBallPlayer2();
        }




    }

    void jumpPlayer2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround == true)
        {
            animator.SetBool("isJump", true);
            m_rd.AddForce(Vector2.up * jumpForce);
            isGround = false;
        }
    }
    void jumpPlayer1()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround == true)
        {
            animator.SetBool("isJump", true);
            m_rd.AddForce(Vector2.up * jumpForce);
            isGround = false;
        }
    }
    void movePlayer2( )
    {

        animator.SetFloat("Speed", Mathf.Abs(moveX));

        transform.position += Vector3.right * moveX * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow)) moveX = 1;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) moveX = -1;

        if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow)) moveX = 0;

    }

    void movePlayer1()
    {

        animator.SetFloat("Speed", Mathf.Abs(moveX));

        transform.position += Vector3.right * moveX * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A)) moveX = -1;

        if (Input.GetKeyDown(KeyCode.D)) moveX = 1;

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) moveX = 0;

    }


    void shootBallPlayer1()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("isShoot", true);
            if (isShoot == true)
            {

                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(350, 250));
                Debug.Log("Shoot");
                adSource.PlayOneShot(shootSound);

            }
            StartCoroutine(enableShoot());
        }
        


    }
    void shootBallPlayer2()
    {


        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("isShoot", true);
            if (isShoot == true)
            {
                Debug.Log("Shoot: "+shotX);
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2( shotX, 250));
                //Debug.Log("Shoot");
                adSource.PlayOneShot(shootSound);

            }
            StartCoroutine(enableShoot());
        }



    }

    private IEnumerator enableShoot()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShoot", false);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJump", false);
            isGround = true;
        }

    }

    public void scorePlayer() 
    {
        moveSpeed = dataMoveSpeed;
        transform.position = startPosition;
    }
}

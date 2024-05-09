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

    float moveHorizontal;

    GameObject _ball;

    public bool isShoot;

    public Animator animator;

    public AudioSource adSource;

    public AudioClip shootSound;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        dataMoveSpeed = moveSpeed;
         isShoot=false;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        m_rd = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animator.GetBool("isJump"));
        movePlayer();
        if (Input.GetKeyDown(KeyCode.UpArrow)   && isGround==true)
        {
 
            animator.SetBool("isJump", true);
            Debug.Log("Up");
            jumpPlayer();
            

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            animator.SetBool("isShoot", true);      
            shootBall();
            //Debug.Log(animator.GetBool("isShoot"));
        }



    }

    void jumpPlayer()
    {

        m_rd.AddForce(Vector2.up*jumpForce);
        isGround = false;
    }
    void movePlayer( )
    {

        moveHorizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        transform.position += Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime;

    }


    void shootBall()
    {
        if (isShoot == true )
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(350, 250));
            Debug.Log("Shoot");
            adSource.PlayOneShot(shootSound);

        }
        StartCoroutine(enableShoot());


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

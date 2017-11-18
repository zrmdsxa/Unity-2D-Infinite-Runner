using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_moveSpeed = 1.0f;
    public float m_jumpForce = 10.0f;

    bool m_onGround = false;
    bool m_stoppedJumping = true;
	bool m_alive = true;

    float m_origJumpForce;

    private Rigidbody m_rb;
    private Animator m_anim;
    private AudioSource m_as;


    void Awake()
    {
        m_origJumpForce = m_jumpForce;
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_as = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.Instance.GetCurrentState == StateManager.m_states.PLAY)
        {

            //player is moving left/right
            if (Input.GetAxis("Horizontal") != 0)
            {

                //stops getting stuck on edges
                Vector3 pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * 0.1f;
                transform.position = pos;

                //move player using velocity
                Vector3 vel = m_rb.velocity;
                vel.x = Input.GetAxis("Horizontal") * m_moveSpeed * Time.deltaTime;
                m_rb.velocity = vel;

            }

            //player is jumping
            if (Input.GetButtonDown("Jump") && m_onGround)
            {
                m_onGround = false;
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                //Debug.Log("Jumped");
                Vector3 pos = transform.position;
                pos.y += 0.01f;
                transform.position = pos;

                m_as.Stop();
                m_as.Play();
            }

            if (Input.GetButtonUp("Jump") && !m_stoppedJumping)
            {

                if (m_rb.velocity.y > 0)
                {
                    Vector3 velocity = m_rb.velocity;
                    velocity.y = 0;
                    m_rb.velocity = velocity;
                }

                m_stoppedJumping = true;
                m_jumpForce = m_origJumpForce;
            }

            if (m_rb.velocity.magnitude < 0.1f)
            {
                m_anim.SetBool("moving", false);
                //Debug.Log(m_rb.velocity.magnitude);
            }
            else
            {
                m_anim.SetBool("moving", true);
                //Debug.Log(m_rb.velocity.magnitude);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
		Debug.Log(other.gameObject.tag);
        //Debug.Log("collided");
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle")
        {
            m_onGround = true;

            //Debug.Log("on ground");
        }
        else
        {
            //TODO:check for other collisions
        }
    }

	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Death")
        {
			m_alive = false;
			m_anim.SetBool("alive", false);
			GameManager.Instance.PlayerDied();
			Debug.Log("Player died");
		}
	}

    public void Restart(){
        m_alive = true;
        m_onGround = false;
        m_stoppedJumping = true;
        m_anim.SetBool("alive", true);
        m_anim.SetBool("moving", false);
    }
}

using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float fSpeed;
    public float collidertime;
    public float turnTime;
    public float touchspeed;
    
    Vector3 forward;
    Vector3 movement;
    public Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camLayLength = 100f;
    float a = 0f;
    float b = 0f;

    public GameObject UPcollider;
    public GameObject Downcollider;
    public GameObject Turncollider;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (speed == 0)
        {
            if (Input.GetKey(KeyCode.R) || Input.touchCount > 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        //Animating();
    }



    void FixedUpdate ()
    {
        forward = transform.forward * fSpeed;
        Move(forward);
    }
    

    void Move (Vector3 forward)
    {
        movement.Set(forward.x, forward.y, forward.z);
        movement = movement * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void Turning (bool verright, bool verleft)
    {
        if (verright)
        {
            playerRigidbody.transform.Rotate(0f, 90f, 0f);
        }
        if (verleft)
        {
            playerRigidbody.transform.Rotate(0f, -90f, 0f);
        }
    }
    /*
    void Animating()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y > touchspeed)
        {
            anim.SetTrigger("Jump");
            StartCoroutine(disabelcolliderdown());
        }

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        if (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y < -touchspeed)
        {
            anim.SetTrigger("Slide");
            StartCoroutine(disabelcolliderUp());
        }
        
    }*/

    public IEnumerator disabelcolliderUp()
    {
        UPcollider.SetActive(false);
        speed = speed / 2;
        yield return new WaitForSeconds(collidertime / 2);
        speed = speed * 2;
        yield return new WaitForSeconds(collidertime/2);
        
        UPcollider.SetActive(true);
    }

    public IEnumerator disabelcolliderdown()
    {
        Downcollider.SetActive(false);
        //speed = speed / 2;
        yield return new WaitForSeconds(collidertime);
        //speed = speed * 2;
        Downcollider.SetActive(true);
    }
}

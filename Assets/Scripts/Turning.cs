using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour {

    public float Turntime = 1;
    public float touchSpeed = 10f;
    public float touchSpeedVertical;
    public float touchdeltaposition = 50f;
    
    public bool Win = false;

    Vector2 startPos;
    Vector2 direction;
    bool directionChosen;

    bool verright = false;
    bool verleft = false;

    float i = 0f;

    void Update()
    {


        bool r = Input.GetKeyDown(KeyCode.RightArrow);
        bool l = Input.GetKeyDown(KeyCode.LeftArrow);

        if (r)
        {
            verright = true;
        }
        else if (l)
        {
            verleft = true;
        }

        if (Input.touchCount >0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            if(direction.x > touchSpeed)
            {
                verright = true;
                directionChosen = false;
            }
            else if (direction.x < -touchSpeed)
            {
                verleft = true;
                directionChosen = false;
            }
            else if (direction.y > touchSpeedVertical)
            {
                GetComponentInParent<PlayerController>().anim.SetTrigger("Jump");
                StartCoroutine(GetComponentInParent<PlayerController>().disabelcolliderdown());
                directionChosen = false;
            }
            else if (direction.y < -touchSpeedVertical)
            {
                GetComponentInParent<PlayerController>().anim.SetTrigger("Slide");
                StartCoroutine(GetComponentInParent<PlayerController>().disabelcolliderUp());
                directionChosen = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (verright || verleft || i != 0)
        {
            i = i + Time.deltaTime;
            if (Turntime -i < 0.05)
            {
                i = 0f;
                verright = false;
                verleft = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Turn" && i != 0)
        {
            GetComponentInParent<PlayerController>().Turning(verright, verleft);
            i = 0f;
            verleft = false;
            verright = false;
        }
        if(other.tag == "Win")
        {
            Win = true;
            GetComponentInParent<Animator>().SetTrigger("Win");
            GetComponentInParent<PlayerController>().speed = 0;
        }
    }
}

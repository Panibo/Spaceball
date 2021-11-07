using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float force = 2.0f;

    private Rigidbody2D rigidbody;

    private Timer timer;

    private bool enabled = false;

    private int upCount = 0;
    private int downCount = 0;
    private int rightCount = 0;
    private int leftCount = 0;

    //private SpriteRenderer upArrow;
   // private SpriteRenderer downArrow;
    //private SpriteRenderer rightArrow;
    //private SpriteRenderer leftArrow;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        try
        {
            timer = gameObject.GetComponent<Timer>();
        }
        catch
        {
            Debug.LogError("Timer not found");
        }

        //upArrow = GameObject.Find("upArrow").GetComponent<SpriteRenderer>();
        //downArrow = GameObject.Find("downArrow").GetComponent<SpriteRenderer>();
        //rightArrow = GameObject.Find("rightArrow").GetComponent<SpriteRenderer>();
        //leftArrow = GameObject.Find("leftArrow").GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.sqrMagnitude < 0.1f && !enabled &&
            (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)))
        {

            StartCoroutine(TimeCoroutine());

        }

            if (Input.GetKeyDown(KeyCode.W) && enabled)
            {
                //upArrow.enabled = true;
                upCount++;
            }
            if (Input.GetKeyDown(KeyCode.S) && enabled)
            {
                //downArrow.enabled = true;
                downCount++;
            }
            if (Input.GetKeyDown(KeyCode.D) && enabled)
            {
                //rightArrow.enabled = true;
                rightCount++;
            }
            if (Input.GetKeyDown(KeyCode.A) && enabled)
            {
                //leftArrow.enabled = true;
                leftCount++;
            }
        

    }

    IEnumerator TimeCoroutine()
    {

        Pause();
        enabled = true;
        yield return new WaitForSeconds(3);
        Resume();
        enabled = false;
        rigidbody.AddForce(new Vector2(0, force * upCount), ForceMode2D.Impulse);
        rigidbody.AddForce(new Vector2(0, -force * downCount), ForceMode2D.Impulse);
        rigidbody.AddForce(new Vector2(force * rightCount, 0), ForceMode2D.Impulse);
        rigidbody.AddForce(new Vector2(-force * leftCount, 0) , ForceMode2D.Impulse);

        upCount = 0;
        downCount = 0;
        rightCount = 0;
        leftCount = 0;

        //upArrow.enabled = false;
        //downArrow.enabled = false;
        //rightArrow.enabled = false;
        //leftArrow.enabled = false;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    private void Pause()
    {
        Debug.Log("Pause");
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        timer.timerStart();
    }

    private void Resume()
    {
        Debug.Log("Resume");
        rigidbody.constraints = RigidbodyConstraints2D.None;
    }
}

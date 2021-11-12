using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private float force = 1.0f;

    private Rigidbody2D myRigidbody;

    private Timer timer;

    private bool enabled = false;

    private int upCount = 0;
    private int downCount = 0;
    private int rightCount = 0;
    private int leftCount = 0;

    public GameObject arrowsObject;

    public GameObject directionArrow;
    private SpriteRenderer directionArrowRenderer;
    private Transform directionArrowTransform;

    private List<Arrow> arrows = new List<Arrow>();

    private int maxKeyPresses = 7;
    private struct Arrow
    {
        public SpriteRenderer arrowRenderer;
        public Arrow(SpriteRenderer spriteRenderer)
        {
            this.arrowRenderer = spriteRenderer;
            this.arrowRenderer.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();

        try
        {
            timer = gameObject.GetComponent<Timer>();
        }
        catch
        {
            Debug.LogError("Timer not found");
        }

        for (int i = 0; i < 4; i++)
        {
            arrows.Add(new Arrow(arrowsObject.GetComponentsInChildren<SpriteRenderer>()[i]));
        }

        directionArrowRenderer = directionArrow.GetComponent<SpriteRenderer>();
        directionArrowTransform = directionArrow.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (myRigidbody.velocity.sqrMagnitude < 0.1f)
        {
            animator.SetBool("isMoving", false);
        } else
        {
            animator.SetBool("isMoving", true);
        }

        if (myRigidbody.velocity.sqrMagnitude < 0.1f && !enabled &&
            (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)))
        {

            StartCoroutine(StopTime(3.0f));

        }

        if (Input.GetKeyDown(KeyCode.W) && enabled)
        {
            if(upCount <= maxKeyPresses)
            {
                StartCoroutine(ArrowFlash(arrows[0]));
                upCount++;
                downCount--;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && enabled)
        {   
            if (downCount <= maxKeyPresses)
            {
                StartCoroutine(ArrowFlash(arrows[1]));
                downCount++;
                upCount--;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && enabled)
        { 
            if (rightCount <= maxKeyPresses)
            {
                StartCoroutine(ArrowFlash(arrows[2]));
                rightCount++;
                leftCount--;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && enabled)
        {    
            if (leftCount <= maxKeyPresses)
            {
                StartCoroutine(ArrowFlash(arrows[3]));
                leftCount++;
                rightCount--;
            }
        }

        Vector2 arrowDirection = new Vector2(rightCount - leftCount, upCount - downCount);

        if (arrowDirection.magnitude > 0.5f)
        {
            directionArrowRenderer.enabled = true;
            float scale = Mathf.InverseLerp(0, 20, arrowDirection.magnitude) + 0.2f;
            if(scale > 1.0f)
            {
                scale = 1.0f;
            }

            Debug.Log(scale);
            directionArrowTransform.localScale = new Vector3(scale, scale, scale);
            Vector3 rotation = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, arrowDirection));
            directionArrowTransform.eulerAngles = rotation;
        }
        else
        {
            directionArrowRenderer.enabled = false;
        }

    }

    private IEnumerator StopTime(float seconds)
    {
        enabled = true;
        float now = seconds;
        timer.StartTimer(3.0f);
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        while (now > 0)
        {
            yield return new WaitForSeconds(1.0f);
            now--;
        }
        myRigidbody.constraints = RigidbodyConstraints2D.None;

        timer.StopTimer();

        enabled = false;
        myRigidbody.AddForce(new Vector2(0, force * upCount), ForceMode2D.Impulse);
        myRigidbody.AddForce(new Vector2(0, -force * downCount), ForceMode2D.Impulse);
        myRigidbody.AddForce(new Vector2(force * rightCount, 0), ForceMode2D.Impulse);
        myRigidbody.AddForce(new Vector2(-force * leftCount, 0), ForceMode2D.Impulse);

        upCount = 0;
        downCount = 0;
        rightCount = 0;
        leftCount = 0;

    }

    private IEnumerator ArrowFlash(Arrow arrow)
    {
        arrow.arrowRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        arrow.arrowRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}

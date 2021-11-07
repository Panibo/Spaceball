using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject canvas;

    private Image fillImage;

    private Text timeText;

    private float step = 0.01f;

    private float start;

    private float end;

    private float now;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            fillImage = canvas.GetComponentsInChildren<Image>()[1];
            timeText = canvas.GetComponentInChildren<Text>();
        }
        catch
        {
            Debug.LogError("Canvas source not given or incomplete.");
        }
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = Mathf.InverseLerp(start, end, now);
        timeText.text = Mathf.CeilToInt(now).ToString();
    }

    public void timerStart()
    {
        start = 3;
        end = 0;
        now = start;

        canvas.SetActive(true);
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        while(now - step >= end)
        {
            yield return new WaitForSeconds(step);
            now -= step;
        }

        canvas.SetActive(false);
    }
}

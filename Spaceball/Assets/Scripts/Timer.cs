using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject canvas;

    private Image fillImage;

    private Text timeText;

    private float start;

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
        fillImage.fillAmount = Mathf.InverseLerp(start, 0, now);
        timeText.text = Mathf.CeilToInt(now).ToString();
        now -= Time.deltaTime;
    }

    public void StartTimer(float seconds)
    {
        start = seconds;
        now = start;
        canvas.SetActive(true);
    }

    public void StopTimer()
    {
        canvas.SetActive(false);
    }

}

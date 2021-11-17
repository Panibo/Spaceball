using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    Button button;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button = GameObject.FindWithTag("ClickButton").GetComponent<Button>();
        button.onClick.AddListener(() => Click());
        
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Click();
        }*/
    }

    void Click()
    {
        audioSource.clip = clickSound;
        
    }
}

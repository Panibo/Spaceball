using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + xOffset, objectToFollow.transform.position.y + yOffset, transform.position.z + zOffset);
    }
}

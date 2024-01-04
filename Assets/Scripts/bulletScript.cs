using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{   
    public float moveSpeed = 30;
    public int time = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        --time;
        transform.RotateAround(new Vector3(0,0,0), Vector3.up, moveSpeed * Time.deltaTime);
        if (time < 0) Destroy(gameObject);
    }
}

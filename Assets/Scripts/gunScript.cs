using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{   

    public GameObject bullet;
    public GameObject arma;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            spawnBullet();
        }
    }

    void spawnBullet() {

        Instantiate(bullet, transform.position, transform.rotation);

    }
}

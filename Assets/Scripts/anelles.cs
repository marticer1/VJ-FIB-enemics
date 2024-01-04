using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anelles : MonoBehaviour
{
    public GameObject destino;
    public GameObject player;
    public bool puedo_tp = false;
    public bool acabo_tp = false;


    public AudioSource sonido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(puedo_tp == false) transform.GetChild(0).gameObject.SetActive(destino.GetComponent<anelles>().puedo_tp);

    }


    private void OnTriggerEnter(Collider other)
    {
        puedo_tp = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!acabo_tp)
        {
            if (player.GetComponent<playerScript>().f && puedo_tp)
            {
                
                puedo_tp = false;
                destino.GetComponent<anelles>().acabo_tp = true;
                acabo_tp = true;
                sonido.Play();
                

                
                player.transform.position = destino.transform.position;

                Invoke("cooldown", 0.1f);

            }
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        puedo_tp = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void cooldown()
    {

        acabo_tp = false;
        destino.GetComponent<anelles>().acabo_tp = false;
    }

}

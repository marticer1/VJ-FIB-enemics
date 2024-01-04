using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public int dano;
    private bool acabo_activar = false;
    public Animator myAnim;
    public AudioSource sonido;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!acabo_activar)
        {
            if(!player.GetComponent<playerScript>().invulnerable)
            {
                player.GetComponent<playerScript>().takedamage(dano);
            }
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(4).gameObject.SetActive(true);
            acabo_activar = true;
            sonido.Play();
            myAnim.SetTrigger("activar");
            Invoke("cooldown", 1f);

            
        }


    }

    private void cooldown()
    {
        acabo_activar = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
    }
}

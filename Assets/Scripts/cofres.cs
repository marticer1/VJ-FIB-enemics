using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cofres : MonoBehaviour
{
    public GameObject player;

    public bool arma1;
    public bool arma2;
    public bool balas1;
    public bool balas2;


    public AudioSource audio_cofre;
    public AudioSource audio_player;

    public Animator myAnim;

    private bool entra = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        if (player.GetComponent<playerScript>().q && !entra)
        {
            
            myAnim.SetBool("abierto", true);
            entra = true;
            
            player.GetComponent<playerScript>().q = false;
            myAnim.SetBool("abierto", true);
            if (arma1) player.GetComponent<ui>().pistola = true;
            if (arma2) player.GetComponent<ui>().pesada = true;
            if (balas1) player.GetComponent<ui>().balas_pistola += Random.Range(1, 10);
            if (balas2) player.GetComponent<ui>().balas_pesada += Random.Range(1, 10);
            if (player.GetComponent<ui>().balas_pistola > 100) player.GetComponent<ui>().balas_pistola = 100;
            if (player.GetComponent<ui>().balas_pesada > 100) player.GetComponent<ui>().balas_pesada = 100;
            transform.GetChild(1).gameObject.SetActive(false);
            audio_cofre.Play();
            if (!arma1 && !arma2 && !balas1 && !balas2) audio_player.Play();
            transform.GetChild(2).gameObject.SetActive(false);
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}

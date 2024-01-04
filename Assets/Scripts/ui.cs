using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ui : MonoBehaviour
{
    // Start is called before the first frame update
    public float vida_maxima ;
    public int balas_pistola;
    public int balas_pesada ;

    public bool pistola;
    public bool pesada;
    public Image Ipistola;
    public Image Ipesada;

    

    public Image barraVida;
    public Text tvida;

    public Text bpistola;
    public Text bpesada;

    public GameObject player;

    public Image game_over1;
    public Image game_over2;

    //  public Text tvida;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_maxima != 0)
        {
            barraVida.fillAmount = player.GetComponent<playerScript>().vida / vida_maxima;
        }
        
        tvida.text = player.GetComponent<playerScript>().vida.ToString();
        bpistola.text = balas_pistola.ToString();
        bpesada.text = balas_pesada.ToString();

        if (pistola)
        {
            Ipistola.fillAmount = 1;
        }
        else
        {
            Ipistola.fillAmount = 0;
        }

        if (pesada)
        {
            Ipesada.fillAmount = 1;
        }
        else
        {
            Ipesada.fillAmount = 0;
        }

        if(player.GetComponent<playerScript>().vida <= 0)
        {
            Invoke("fin1", 0.5f);
        }

    }

    private void fin1()
    {
        game_over1.fillAmount = 1;
        game_over2.fillAmount = 1;
        player.transform.GetChild(2).gameObject.GetComponent<AudioListener>().enabled = false;
        Invoke("fin2", 3f);
    }

    private void fin2()
    {
        SceneManager.LoadScene(0);
    }
}

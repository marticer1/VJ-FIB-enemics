using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class playerScript : MonoBehaviour
{   

    public float playerSpeed = 10;
    

    public float playerJump = 10;
    public float jumpCooldown;
    public bool invulnerable = false;
    private bool isReadyToJump;

    public bool q = false;
    public bool f = false;

    private bool debug = false;

    public Material esquiva;
    public Material normal;
    public LayerMask whatIsGround;
    private bool isGrounded;
    public Rigidbody myRigidbody;
    public Animator myAnim;

    private bool god = false;

    private bool anterior_grounded = true;
    private bool mirando_der;
    public GameObject playerSkin;
    private Quaternion rotacion;
    public Collider playerCollider;

    public AudioSource saltar;
    public AudioSource caminar;
    public AudioSource equipar;
    public AudioSource shield;
    public AudioSource tocar_suelo;
    public AudioSource herir;
    public AudioSource muerte;

   

    public float currentAngle;

    private float move;

    public int vida;
    

    // Start is called before the first frame update
    void Start()
    {
        
        isReadyToJump = true;
        myRigidbody.freezeRotation = true;
        mirando_der = false;

        isGrounded = false;

        playerSpeed = 30;
        /*
        currentAngle = 0;
        
        transform.position = new Vector3(100 * Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                                            transform.position.y,
                                            100 * Mathf.Sin(currentAngle * Mathf.Deg2Rad));*/
        
    }

    // Update is called once per frame
    void Update()
    {
       
        myAnim.SetBool("voltereta", false);
        //y = Input.GetAxis("Vertical");
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f, whatIsGround);
        if (!anterior_grounded && isGrounded) tocar_suelo.Play();
        anterior_grounded = isGrounded;
        MyInput();

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        if (move > 0 && !mirando_der) Girar();
        else if (move < 0 && mirando_der) Girar();
        //animator.SetFloat("VelX", x);

        //saltar
        myAnim.SetBool("grounded", isGrounded);

       
       
    }

    void Girar()
    {
        mirando_der = !mirando_der;

        Vector3 scaleCamera = playerSkin.transform.localScale;
        scaleCamera.z *= -1;
        playerSkin.transform.localScale = scaleCamera;
    }




    private void MyInput()
    {

        /*
        float move = Input.GetAxis("Horizontal");
        currentAngle += move * playerSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, -currentAngle, 0);
        transform.position = new Vector3(100 * Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                                            transform.position.y,
                                            100 * Mathf.Sin(currentAngle * Mathf.Deg2Rad));

        myAnim.SetFloat("speed", Mathf.Abs(move));

        if (move > 0 && !mirando_der) Girar();
        else if (move < 0 && mirando_der) Girar();

        if(move!=  0 && isGrounded) caminar.enabled = true;
        else caminar.enabled = false;
        */
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))  
        {

            if (isGrounded)
            {
                caminar.enabled = true;
            }
            else
            {
                caminar.enabled = false;
            }

            move = Input.GetAxis("Horizontal");
            transform.RotateAround(Vector3.zero, Vector3.up, playerSpeed * -move * Time.deltaTime);
            
           /* 
            if (Input.GetKey(KeyCode.A))
            {   
                
                transform.RotateAround(Vector3.zero, Vector3.up, (playerSpeed) * Time.deltaTime);
                Debug.Log(transform.position);
                if (Physics.CheckBox(transform.position + new Vector3(0, 15, 0), new Vector3(5, 0, 0), Quaternion.identity, whatIsGround)) {
                    
                    transform.RotateAround(Vector3.zero, Vector3.up, (-playerSpeed) * Time.deltaTime);
                }
            }
            else
            {
                transform.RotateAround(Vector3.zero, Vector3.up, (-playerSpeed) * Time.deltaTime);
            }
           */
        }
        else
        {
            caminar.enabled = false;
        }
        
        if (Input.GetKey(KeyCode.Q) && !q)
        {
            myAnim.SetTrigger("q");
            q = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            q = false;

        }

        if (Input.GetKey(KeyCode.F) && !f)
        {
            myAnim.SetTrigger("f");
            f = true;

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            f = false;

        }

        if (Input.GetKey(KeyCode.Space) && isReadyToJump && isGrounded)
        {
            saltar.Play();
            //transform.Rotate(0, 180, 0);
            
            isReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(KeyCode.E)   )
        {
            invulnerable = true;
            myAnim.SetBool("voltereta", true);
            shield.Play();
            transform.GetChild(3).gameObject.SetActive(true);
            GameObject pleft = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            GameObject pright = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            GameObject torso = transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            GameObject mleft = torso.transform.GetChild(0).gameObject;
            GameObject mright = torso.transform.GetChild(1).gameObject;
            GameObject cabeza = torso.transform.GetChild(2).gameObject;
            pleft.GetComponent<Renderer>().material = esquiva;
            pright.GetComponent<Renderer>().material = esquiva;
            torso.GetComponent<Renderer>().material = esquiva;
            mleft.GetComponent<Renderer>().material = esquiva;
            mright.GetComponent<Renderer>().material = esquiva;
            cabeza.GetComponent<Renderer>().material = esquiva;
            
            if (mirando_der)
            {
                transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, (-playerSpeed-50) * Time.deltaTime);
     
                
            }
            else
            {
                transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, (playerSpeed+50) * Time.deltaTime);
            }
            if (!god)
            {
                Invoke("vestidos", 0.5f);
            }
            
            

        }

        //armas

        if (Input.GetKey(KeyCode.Alpha1) && transform.GetComponent<ui>().pistola)
        {
            myAnim.SetBool("equipada", true);
            equipar.Play();
            transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);

        }
        if (Input.GetKey(KeyCode.Alpha2) && transform.GetComponent<ui>().pesada)
        {
            myAnim.SetBool("equipada", true);
            equipar.Play();
            transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.M) && !debug)
        {
            debug = true;
            transform.GetComponent<ui>().balas_pesada = 100;
            transform.GetComponent<ui>().balas_pistola = 100;
            Invoke("tiempo", 0.1f);
        }

        if (Input.GetKey(KeyCode.G) && !debug)
        {
            debug = true;

            transform.GetChild(3).gameObject.SetActive(true);
            GameObject pleft = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            GameObject pright = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            GameObject torso = transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            GameObject mleft = torso.transform.GetChild(0).gameObject;
            GameObject mright = torso.transform.GetChild(1).gameObject;
            GameObject cabeza = torso.transform.GetChild(2).gameObject;
            if (!god)
            {
                pleft.GetComponent<Renderer>().material = esquiva;
                pright.GetComponent<Renderer>().material = esquiva;
                torso.GetComponent<Renderer>().material = esquiva;
                mleft.GetComponent<Renderer>().material = esquiva;
                mright.GetComponent<Renderer>().material = esquiva;
                cabeza.GetComponent<Renderer>().material = esquiva;
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(false);
                pleft.GetComponent<Renderer>().material = normal;
                pright.GetComponent<Renderer>().material = normal;
                torso.GetComponent<Renderer>().material = normal;
                mleft.GetComponent<Renderer>().material = normal;
                mright.GetComponent<Renderer>().material = normal;
                cabeza.GetComponent<Renderer>().material = normal;
            }
            god = !god;
            invulnerable = !invulnerable;
            Invoke("tiempo", 0.2f);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            
        }
    }

    private void tiempo()
    {
        debug = false;
    }

    private void vestidos()
    {
            GameObject pleft = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            GameObject pright = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
            GameObject torso = transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
            GameObject mleft = torso.transform.GetChild(0).gameObject;
            GameObject mright = torso.transform.GetChild(1).gameObject;
            GameObject cabeza = torso.transform.GetChild(2).gameObject;
            invulnerable = false;
            pleft.GetComponent<Renderer>().material = normal;
            pright.GetComponent<Renderer>().material = normal;
            torso.GetComponent<Renderer>().material = normal;
            mleft.GetComponent<Renderer>().material = normal;
            mright.GetComponent<Renderer>().material = normal;
            cabeza.GetComponent<Renderer>().material = normal;
            transform.GetChild(3).gameObject.SetActive(false);


    }
    private void Jump() {
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);
        myRigidbody.AddForce(transform.up * playerJump, ForceMode.Impulse);
    }

    private void ResetJump() {
        
        isReadyToJump = true;
    }

    public void takedamage ( int damage)
    {
        vida -= damage;
        
        if (vida <= 0)
        {
            myAnim.SetTrigger("muerto");
            muerte.Play();

        }
        else
        {
            herir.Play();
        }
    }
}

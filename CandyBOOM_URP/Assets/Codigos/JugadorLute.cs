using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JugadorLute : MonoBehaviour
{
    public float VelocidadMovimiento = 5.0f;

    float X, Y;

    private Rigidbody RB;
    private Vector3 Velocidad;

    private Animator anim;
    private bool bandera = false;

    //Variables para la camara
    public Transform cam;
    float VMouse;
    float HMouse;
    float Yrotacion = 0.0f;
    float Xrotacion = 0.0f;

    public float VelocidadHorizontal;
    public float VelocidadVertical;

    public GameObject recargarSonido;
    GameObject nuevoSonido;
    float tiempo = 0f;

    public Text Vida;
    public int PuntosVida = 0;


    void Start()
    {
        RB = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        //Bloquear el cursor para no verse
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vida.text = "Vida: " + PuntosVida;
        anim.SetTrigger("QuietoJugador");
        tiempo += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && !bandera)
        {
            anim.SetTrigger("Recargar");
            bandera = true;
            nuevoSonido = Instantiate(recargarSonido, cam.position, cam.rotation );
            tiempo = 0;
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            bandera = false;
        }
        if(tiempo > 4)
        {
            Destroy(nuevoSonido);
        }

        if (Input.GetButtonDown("Fire1") && tiempo > 2)
        {
            anim.SetTrigger("Disparo");
            Debug.Log("Di clik");
        }
        if(PuntosVida < 1)
        {
            Destroy(this.gameObject);
        }

        //Movimiento de la camara
        LookMouse();
        //Movimiento del personaje
        Movimiento();
    }
    void LookMouse()
    {
        HMouse = Input.GetAxis("Mouse X") * VelocidadHorizontal * Time.deltaTime;
        VMouse = Input.GetAxis("Mouse Y") * VelocidadVertical * Time.deltaTime;

        Yrotacion -= VMouse;
        Yrotacion = Mathf.Clamp(Yrotacion, -15f, 4f);
        Xrotacion += HMouse;
        transform.Rotate(Yrotacion, Xrotacion , 0f);
        cam.localRotation = Quaternion.Euler(Yrotacion, Xrotacion, 0f);
    }

    void Movimiento()
    {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
         
        transform.Translate(X * Time.deltaTime * VelocidadMovimiento,
            0,
            Y * Time.deltaTime * VelocidadMovimiento);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ZombieGalleta")
        {
            PuntosVida -= 10;
        }
    }
}

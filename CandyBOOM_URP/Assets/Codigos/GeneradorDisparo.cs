using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorDisparo : MonoBehaviour
{
    public GameObject Municion;
    public Transform posicion;
    public float velocidad = 20f;
    public int TipoArma;
    public int MunicionCantidad;
    public int MunicionDisponible;

    public Text MunicionDis;
    public Text disparosDis;

    int disparos = 0;
    float tiempo = 0f;
    float tiempoR = 0f;
    float tiempoA = 0f;
    GameObject nuevo;
    Rigidbody RB;

    int plomo = 0;
    int res;

    public GameObject MunicionPre;

    private void Start()
    {
        while(disparos < MunicionCantidad)
        {
            plomo++;
            disparos++;
        }
        disparos = 0;
        res = plomo;
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        tiempoR += Time.deltaTime;
        tiempoA += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(disparos != MunicionCantidad - 1)
            {
                MunicionDisponible -= MunicionCantidad - disparos;
            }
            tiempoR = 0;
            disparos = 0;
            plomo = res;
        }
        if(disparos < MunicionCantidad && MunicionDisponible > 0)
        {
            if (Input.GetButtonDown("Fire1") && tiempoR > 2 && TipoArma == 0)
            {
                Disparar();
                MunicionDisponible--;
                disparos++;
                plomo--;
            }
            if (Input.GetButtonDown("Fire1") && tiempoR > 2 && TipoArma == 1)
            {
                Disparar();
                Disparar();
                Disparar();
                MunicionDisponible -=3;
                disparos +=3;
                plomo--;
            }
            if (Input.GetMouseButton(0) && tiempoR > 2 && TipoArma == 2 && tiempoA > 0.2)
            {
                Disparar();
                tiempoA = 0;
                MunicionDisponible--;
                disparos++;
                plomo--;
            }
            
            
        }
        if (tiempo > 5)
        {
            Destroy(nuevo);
            tiempo = 0;
        }
        MunicionDis.text = MunicionCantidad + "/" + plomo;
        disparosDis.text = "Munici?n: " + MunicionDisponible;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Municion")
        {
            MunicionDisponible += 15;
            Destroy(other.gameObject);
        }
    }

    void Disparar()
    {
        Debug.Log("Disparamos");
        nuevo = Instantiate(Municion, posicion.position, posicion.rotation);
        RB = nuevo.GetComponent<Rigidbody>();

        RB.velocity = posicion.forward * velocidad;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemgos : MonoBehaviour
{
    public int PuntosVida;
    public Animator Anim;
    AudioSource Audio;
    public GameObject sonidoMuerte;
    GameObject NuevoS;
    CapsuleCollider Colicion;
    GameObject[] jugador;
    NavMeshAgent enemigo;

    float tiempo = 0f;
    bool Existe = false;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Audio = GetComponent<AudioSource>();
        Colicion = GetComponent<CapsuleCollider>();
        jugador = GameObject.FindGameObjectsWithTag("Player");
        enemigo = GetComponent<NavMeshAgent>();
    }

    void follow_player() => enemigo.SetDestination(jugador[0].transform.position);
    

    // Update is called once per frame
    void Update()
    {
        follow_player();
        Anim.SetTrigger("GalletaZombie1");
        Anim.SetTrigger("Pan");

        Vector3 Direccion = (enemigo.destination - transform.position).normalized;
        
        if (PuntosVida < 1)
        {
            Colicion.isTrigger = false;
            enemigo.enabled = false;
            tiempo += Time.deltaTime;
            Anim.SetTrigger("MuerteG");
            Anim.SetTrigger("PanMuerte");
            this.gameObject.tag = "MuerteG";
            Audio.mute = true;
            if (!Existe)
            {
                NuevoS = Instantiate(sonidoMuerte, this.gameObject.transform.position, this.gameObject.transform.rotation);
                Existe = true;
            }
            if(tiempo > 5)
            {
                Colicion.isTrigger = true;
                Vector3 gravedad = new Vector3(0, -9.81f * Time.deltaTime, 0);
                transform.Translate(gravedad, Space.World);
            }
            if (tiempo > 10)
            {
                Destroy(this.gameObject);
                Destroy(NuevoS);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            PuntosVida -= 10;
        }
    }
}

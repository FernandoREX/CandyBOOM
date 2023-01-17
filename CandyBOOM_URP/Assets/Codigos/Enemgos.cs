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
        if (PuntosVida < 1)
        {
            tiempo += Time.deltaTime;
            Anim.SetTrigger("MuerteG");
            Anim.SetTrigger("PanMuerte");
            this.gameObject.tag = "MuerteG";
            Audio.mute = true;
            if (tiempo < 0.1)
            {
                NuevoS = Instantiate(sonidoMuerte, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
            if(tiempo > 5)
            {
                Colicion.isTrigger = true;
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
        if (collision.collider.tag == "Bala")
        {
            Debug.Log("Me Dispararon Ahhhhhh");
            PuntosVida -= 10;
        }
    }
}

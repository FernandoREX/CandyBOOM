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
    public Rigidbody RB;

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
        Debug.Log("NO Me mori" + PuntosVida);
        if (PuntosVida < 1)
        {
            Colicion.isTrigger = false;
            enemigo.enabled = false;
            tiempo += Time.deltaTime;
            Anim.SetTrigger("MuerteG");
            Anim.SetTrigger("PanMuerte");
            this.gameObject.tag = "MuerteG";
            Audio.mute = true;
            if (tiempo == 1)
            {
                NuevoS = Instantiate(sonidoMuerte, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
            if(tiempo > 5)
            {
                RB.constraints = RigidbodyConstraints.FreezePositionY;
                Colicion.isTrigger = true;
            }
            if (tiempo > 10)
            {
                Destroy(this.gameObject);
                Destroy(NuevoS);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "Bala")
        {
            Debug.Log("Me Dispararon Ahhhhhh");
            PuntosVida -= 10;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruccion : MonoBehaviour
{
    public float tiempoDestruccion;

    float tiempo = 0f;

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if(tiempo > tiempoDestruccion)
        {
            Destroy(this.gameObject);
        }
    }
}

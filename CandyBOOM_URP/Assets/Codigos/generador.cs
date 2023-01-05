using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador : MonoBehaviour
{
    public GameObject objeto;
    public float tiempo = 0f;
    float tiemposuma = 0f;

    public float Velocidad = 1f;
    public float distancia = 5f;
    public bool Direccion = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiemposuma += Time.deltaTime;
        Vector3 direccion = Direccion ? Vector3.forward : Vector3.back;

        transform.Translate(direccion * Velocidad * Time.deltaTime);

        if(Mathf.Abs(transform.position.z) >= distancia)
        {
            Direccion = !Direccion;
        }

        if(tiemposuma > tiempo)
        {
            Instantiate(objeto, this.gameObject.transform.position, this.gameObject.transform.rotation);
            tiemposuma = 0f;
        }
    }
}

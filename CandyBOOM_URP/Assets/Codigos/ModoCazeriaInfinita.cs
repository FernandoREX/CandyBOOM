using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModoCazeriaInfinita : MonoBehaviour
{
    public Text Puntaje;
    public Text TiempoEM;
    public Text TEXTOS;

    float Tiempo = 0;
    float Tiempo2 = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tiempo2 -= Time.deltaTime;
        if(Tiempo2 < 0){
            Tiempo += Time.deltaTime;
            Puntaje.text = "Tiempo: " + Mathf.Floor(Tiempo);
            Destroy(TiempoEM);
            Destroy(TEXTOS);
        }else
        {
            TiempoEM.text = ""+Mathf.Floor(Tiempo2);
        }
        
    }
}

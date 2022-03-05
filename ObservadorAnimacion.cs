using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservadorAnimacion : MonoBehaviour
{
    public List<Observable_a> observables_a; //Referencia a los transform que se van a observar, velocidad y rotación

    public float velocidadTotal; //Suma de todas las velocidaded observadas
    public float rotacionTotal; //Suma de todas las rotaciones observadas

    public float acumulada_anim; //Variable que aumenta en base a las velocidades y rotaciones, disminuye automaticamente

    //Limites para descartar valores bajos y altos
    public float velocidadLimitInferior; 
    public float velocidadLimiteSuperior;

    public float rotacionLimiteInferior;
    public float rotacionLimiteSuperior;

    public float disminucion;  //Que tan rápido disminuye la variable acumulada
    public float aumento; //Que tan rápido aumenta la variable acumulada
    public float contador_tiempo;
    private float c_sinmov;
    private float distancia_r;
    private float distancia_l;
    public float distancia_manos;
    public float map_velocidad;
    private bool switch_acumulada;
    public AparicionAnimacion avatar;
 

    void Start()
    {
        foreach(Observable_a obs in observables_a){
            obs.Inicializar();
        }
    }

    void Update()
    {
        velocidadTotal = 0;
        rotacionTotal = 0;
        foreach(Observable_a obs in observables_a){
            obs.Actualizar(Time.deltaTime);
            velocidadTotal += obs.ObtenerVelocidad();
            rotacionTotal += obs.ObtenerRotacion();
        }

        //ACUMULADA 
/*
        if((velocidadTotal >= velocidadLimitInferior && velocidadTotal <= velocidadLimiteSuperior) ||
        (rotacionTotal >= rotacionLimiteInferior && rotacionTotal <= rotacionLimiteSuperior)){
           
            acumulada += aumento * Time.deltaTime; 
            if(acumulada > 100){
                acumulada = 100;
            }
        }else{
            acumulada -= disminucion * Time.deltaTime;
            if(acumulada < 0){
                acumulada = 0;
            }
        }
*/
        //ACUMULADA FIN


        //CONTADOR TIEMPO

        if(velocidadTotal > 0.1f){
           
            c_sinmov = 0;
            contador_tiempo += Time.deltaTime;

        }else{

            c_sinmov += Time.deltaTime;

            if (c_sinmov > 2)
            {
                contador_tiempo = 0;
            }
            
        }

        //CONTADOR TIEMPO FIN


        //DISTANCIA 

        distancia_l = Vector3.Distance(avatar.macaco.GetComponent<Transform>().position, observables_a[0].transform.position);
        distancia_r = Vector3.Distance(avatar.macaco.GetComponent<Transform>().position, observables_a[1].transform.position);
        distancia_manos = distancia_l + distancia_r / 2;

        //DISTANCIA FIN

        //VELOCIDAD 

        map_velocidad = map(velocidadTotal, 0, 3, 0.09f, 1f);

        //VELoCIDAD FIN

        
        

        //FOR LOOP DE LAS ANIMACIONES 
        
        if(acumulada_anim >= 100)
        {
            switch_acumulada = true;
        } else if (acumulada_anim <= 0){ switch_acumulada = false; }

        if(!switch_acumulada)
        {
            acumulada_anim += aumento * Time.deltaTime; 
        } else if(switch_acumulada){ acumulada_anim -= disminucion * Time.deltaTime; }
        
        //FOR LOOP DE LAS ANIMACIONES TERMINA
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
}




[System.Serializable]
public class Observable_a{
    public Transform transform;

    public bool observarVelocidad, observarRotacion;
    private float velocidad;
    private float rotacion;
    
    private Vector3 posAnterior;
    private Vector3 rotAnterior;

    public void Inicializar(){
        posAnterior = transform.position;
        rotAnterior = transform.forward;
    }
    
    public void Actualizar(float escalaTiempo)
    {
        if(observarVelocidad){
            Vector3 movimiento = (transform.position - posAnterior) / escalaTiempo;
            velocidad = movimiento.magnitude;            
        }
        
        if(observarRotacion){
            rotacion = Vector3.Angle(transform.forward, rotAnterior) / escalaTiempo;
        }

        posAnterior = transform.position;
        rotAnterior = transform.forward;
        
    }

    public float ObtenerVelocidad(){
        return velocidad;
    }

    public float ObtenerRotacion(){
        return rotacion;
    }

}



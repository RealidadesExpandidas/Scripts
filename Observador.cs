using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observador : MonoBehaviour
{
    public List<Observable> observables; //Referencia a los transform que se van a observar, velocidad y rotación

    private float velocidadTotal; //Suma de todas las velocidaded observadas
    private float rotacionTotal; //Suma de todas las rotaciones observadas

    private float acumulada; //Variable que aumenta en base a las velocidades y rotaciones, disminuye automaticamente

    //Limites para descartar valores bajos y altos
    public float velocidadLimitInferior; 
    public float velocidadLimiteSuperior;

    public float rotacionLimiteInferior;
    public float rotacionLimiteSuperior;

    public float disminucion;  //Que tan rápido disminuye la variable acumulada
    public float aumento; //Que tan rápido aumenta la variable acumulada
    void Start()
    {
        foreach(Observable obs in observables){
            obs.Inicializar();
        }
    }

    void Update()
    {
        velocidadTotal = 0;
        rotacionTotal = 0;
        foreach(Observable obs in observables){
            obs.Actualizar(Time.deltaTime);
            velocidadTotal += obs.ObtenerVelocidad();
            rotacionTotal += obs.ObtenerRotacion();
        }
        if((velocidadTotal >= velocidadLimitInferior && velocidadTotal <= velocidadLimiteSuperior) ||
        (rotacionTotal >= rotacionLimiteInferior && rotacionTotal <= rotacionLimiteSuperior)){
            acumulada += aumento * Time.deltaTime; 
        }else{
            acumulada -= disminucion * Time.deltaTime;
            if(acumulada < 0){
                acumulada = 0;
            }
        }
    }
}


[System.Serializable]
public class Observable{
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
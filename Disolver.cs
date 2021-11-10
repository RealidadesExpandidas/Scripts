using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolver : MonoBehaviour
{
    
    
    public GameObject dis;// Start is called before the first frame update
    public Transform mano;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float ManoX = mano.GetComponent<Transform>().localPosition.x;
        dis.GetComponent<Renderer>().material.SetFloat("Dissolve", ManoX);
        print(ManoX);
    }
}

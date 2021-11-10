using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolver : MonoBehaviour
{
    
    
    public GameObject dis;// Start is called before the first frame update
    void Start()
    {
        dis.GetComponent<Renderer>().material.SetFloat("Dissolve", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

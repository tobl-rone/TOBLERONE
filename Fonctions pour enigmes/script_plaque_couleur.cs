using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_plaque_couleur : MonoBehaviour
{


    //public Color color;
    public Renderer rend;
    [SerializeField] Texture blutext;
    [SerializeField] Texture redtext;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = blutext;
    }
    
    void Update()
    {

    }

    //changes colors
    void OnCollisionEnter(Collision collision)
    {
        if (rend.material.mainTexture.Equals(redtext))
        {
            rend.material.mainTexture = blutext;
        }
        else
        {
            rend.material.mainTexture = redtext;
        }
    }
}
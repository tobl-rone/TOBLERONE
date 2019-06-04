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
    // Use this for initialization
    void Start()
    {
        //color = Color.blue;
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = blutext;
        //rend.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.color = color;
        //rend.material.mainTexture = texture;
    }

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
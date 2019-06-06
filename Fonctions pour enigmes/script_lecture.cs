using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script_lecture : MonoBehaviour {

    [SerializeField] Text message;
    [SerializeField] string content;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    
	}

    void OnTriggerEnter()
    {
        if(content != null)
            message.text = content;
    }
    void OnTriggerExit(Collider collision)
    {
        message.text = "";
    }

}

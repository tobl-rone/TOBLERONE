using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script_lecture : MonoBehaviour {

    [SerializeField] Text message;
    [SerializeField] string content;
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
    
	}

    //displays the message
    void OnTriggerEnter()
    {
        if(content != null)
            message.text = content;
    }
    //stops displaying the message
    void OnTriggerExit(Collider collision)
    {
        message.text = "";
    }

}

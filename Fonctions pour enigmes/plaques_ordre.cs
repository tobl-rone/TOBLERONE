using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_ordre : MonoBehaviour {

    [SerializeField] int number;
    public GameObject PlaqueDeResolution;
    //public GameObject NewPlate;
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject.Find(PlaqueDeResolution.name).GetComponent<plaques_dynamiques>().Ordre_des_plaques(number);
   
        //gameObject.collider.enabled = true;
        //GetComponent<plaques_ordre>().enabled = false;
        //other.gameObject.GetComponent<plaques_ordre>().enabled = false;
        //Instantiate(NewPlate, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    public bool IsGoodOrder()
    {
        int nbMort = 5;
        int nbMortAvant = number;
        int test;
        for (int i = 1; i <= 5; i++)
        {
            //Debug.Log("boucle = " + i);
            if (GameObject.FindGameObjectWithTag("plaque " + i) != null  ? true : false)
            {
                nbMort--;                
            }
        }
        for (int i = 1; i <= number; i++)
        {
            if (GameObject.FindGameObjectWithTag("plaque " + i) != null  ? true : false)
            {
                nbMortAvant--;
            }
        }
        //Debug.Log("nbMort = " + nbMort + " || nbMortAvant = " + nbMortAvant);
        test = nbMort + 1;
        //Debug.Log("number = " + number + " || test = " + test);

        if (nbMort == nbMortAvant)
            if (number == test)
            {
                //Debug.Log("Hello");
                return true;
            }
        /*
        Debug.Log("Nb de plaques activées =  " + nbMort);
        nbMort ++;
        if(number == nbMort)
        {
            return true;
        }
        */
        return false;
        //count how many plate exist then check if the next one is the current one
    }
}

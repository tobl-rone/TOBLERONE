using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_ordre : MonoBehaviour {

    public GameObject NewPlate;
    [SerializeField] int number;
    public GameObject PlaqueDeResolution;
    void Start ()
    {

	}
	
	void Update ()
    {
        
    }

    //calls Ordre_des_plaques in plaques_dynamiques to give the order of the plate touched then destroys the plate
    private void OnCollisionEnter(Collision other)
    {
        GameObject.Find(PlaqueDeResolution.name).GetComponent<plaques_dynamiques>().Ordre_des_plaques(number);   
        Instantiate(NewPlate, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    //Checks if the if the okate was touch in the right order
    public bool IsGoodOrder()
    {
        int nbMort = 5;
        int nbMortAvant = number;
        int test;
        //checks how many plates were destroyed
        for (int i = 1; i <= 5; i++)
        {
            if (GameObject.FindGameObjectWithTag("plaque " + i) != null  ? true : false)
            {
                nbMort--;                
            }
        }
        //checks how many plates with a lower number than the actual plate were destroyed
        for (int i = 1; i <= number; i++)
        {
            if (GameObject.FindGameObjectWithTag("plaque " + i) != null  ? true : false)
            {
                nbMortAvant--;
            }
        }
        if(nbMort == 0)
        {
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("panneau"))
            {
                Destroy(ObjectFound);
            }
        }

        test = nbMort + 1;

        if (nbMort == nbMortAvant)
            if (number == test)
            {
                return true;
            }       
        return false;
    }
}

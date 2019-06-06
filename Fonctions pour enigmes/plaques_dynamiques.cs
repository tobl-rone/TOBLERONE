using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_dynamiques : MonoBehaviour
{

    // Fonctions : ResetColor, Fin_enigme_couleurs, Plaque_ouvrir_porte
    private Renderer rend;
	public GameObject PlaqueDeReset;
    public GameObject OldDoor;
    public GameObject NewDoor;
    public GameObject NewPlate;
    [SerializeField] string NomFonction;
    [SerializeField] Texture ownTexture;
    [SerializeField] Texture defaultTexture;
    [SerializeField] Texture alternateTexture;   


    void Start ()
    {       
        rend = GetComponent<Renderer>();

        if (NomFonction.Equals("Fin_enigme_couleurs"))
        {
            rend.material.mainTexture = defaultTexture;
        }
       
        else
        {
            rend.material.mainTexture = ownTexture;
        }     
    }

    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {

        switch (NomFonction)
        {
            case "ResetColor":
                ResetColor();
                break;
            case "Fin_enigme_couleurs":
                Fin_enigme_couleurs();
                break;
            case "Plaque_ouvrir_porte":
                Plaque_ouvrir_porte();
                break;
            default:
                Debug.Log("Fonction donnée inconnue");
                break;
        }
    }

    //resets the color of color plates
    private void ResetColor()
    {
        foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
        {
            ObjectFound.GetComponent<Renderer>().material.mainTexture = defaultTexture;
        }
    }
    
    //checks if all the color plates are set to their alternative color; if so, mechanism activates and the plates doing stuff in the puzzle get destroyed, else resets the color of color plates
    private void Fin_enigme_couleurs()
    {
        bool check = true;
        bool found = false;
        //checks if all color plates are set to their alternate color (texture)
        foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
        {            
            found = true;
            if (ObjectFound.GetComponent<Renderer>().material.mainTexture != alternateTexture)
            {
                check = false;
                break;
            }
        }
        if (check&&found)
        {
            //replaces the closed door (OldDoor) by the open door (NewDoor)
            Instantiate(NewDoor, OldDoor.transform.position + Vector3.down, OldDoor.transform.rotation);
            Destroy(OldDoor);    
            //changes the Texture of the plate
            Destroy(this.gameObject);
            //destroys the reset plate
            Destroy(PlaqueDeReset);
            //Destroys color plates
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Destroy(ObjectFound);
            }
        }
        else
        {
            //reset the color of the color plates
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                ObjectFound.GetComponent<Renderer>().material.mainTexture = defaultTexture;
            }
        }
    }
    private void Plaque_ouvrir_porte()
    {
        Instantiate(NewDoor, OldDoor.transform.position + Vector3.down, OldDoor.transform.rotation);
        Destroy(OldDoor);
        Instantiate(NewPlate, GameObject.FindGameObjectWithTag("plaque_porte").transform.position, GameObject.FindGameObjectWithTag("plaque_porte").transform.rotation);

        Destroy(this.gameObject);
    }
  
}


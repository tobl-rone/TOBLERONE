using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plaques_dynamiques : MonoBehaviour
{

    // Fonctions : ResetColor, Fin_enigme_couleurs, Plaque_ouvrir_porte, Plaque_ordre, monstres
    private Renderer rend;
    //Uniquement utilisés pour Fin_enigme_couleurs
    public GameObject PlaqueDeReset;
    //utilisés par toutes les fonctions sauf ResetColor
    public GameObject OldDoor;
    //utilisé par Plaque_ordre
    [SerializeField] string SceneActuelle;

    public GameObject NewPlate;
    //public GameObject NewPlate;
    [SerializeField] string NomFonction;
    //utilisé dans ResetColor,Fin_enigme_couleurs et Plaque_ouvrir_porte
    [SerializeField] Texture ownTexture;
    //utilisé par ResetColor, Fin_enigme_couleurs
    [SerializeField] Texture defaultTexture;
    //utilisé par Fin_enigme_couleurs
    [SerializeField] Texture alternateTexture;
    
    //Uniquement utilisés pour Plaque_ordre
    private bool ordre1;
    private bool ordre2;
    private bool ordre3;
    private bool ordre4;
    private bool ordre5;

    
    void Start ()
    {       
        rend = GetComponent<Renderer>();

        if (NomFonction.Equals("Fin_enigme_couleurs"))
        {
            rend.material.mainTexture = ownTexture;
        }
        else if(NomFonction.Equals("Plaque_ordre"))
        {
            ordre1 = false;
            ordre2 = false;
            ordre3 = false;
            ordre4 = false;
            ordre5 = false;
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
            case "Plaque_ordre":
                Plaque_ordre();
                break;
            case "monstres":
                monstres();
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
    
    //checks if all the color plates are set to their alternative color; if so, mechanism activates and the plates that compose the puzzle get destroyed, else resets the color of color plates
    private void Fin_enigme_couleurs()
    {
        bool check = true;
        bool found = false;
        //checks if all color plates are set to their alternate color (texture)
        foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
        {            
            Debug.Log("HELLO");
            found = true;
            if (ObjectFound.GetComponent<Renderer>().material.mainTexture != alternateTexture)
            {
                check = false;
                break;
            }
        }
        Debug.Log("check = " + check + " || found = " + found);
        if (check&&found)
        {
            //destroys places the closed door (OldDoor)
            Destroy(OldDoor);    
            //changes the Texture of the plate            
            Instantiate(NewPlate, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
            //destroys the reset plate            
            Instantiate(NewPlate, PlaqueDeReset.transform.position, PlaqueDeReset.transform.rotation);
            Destroy(PlaqueDeReset);
            //Destroys color plates
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Instantiate(NewPlate, ObjectFound.transform.position, ObjectFound.transform.rotation);
                Destroy(ObjectFound);
            }
        }
        else
        {
            //puzzle reloads
            SceneManager.LoadScene(SceneActuelle);           
        }
    }
    //When the plate is touched, it opens the door
    private void Plaque_ouvrir_porte()
    {
        
        Destroy(OldDoor);
        Instantiate(NewPlate, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        if (GameObject.Find("monstres_qui_tombent") != null)
        {
            Instantiate(NewPlate, GameObject.Find("monstres_qui_tombent").transform.position, GameObject.Find("monstres_qui_tombent").transform.rotation);
            Destroy(GameObject.Find("monstres_qui_tombent"));
        }
    }

    //checks if all the color plates were touched in the right order; if so, mechanism activates and the plates that compose the puzzle get destroyed, else the puzzle reloads
    //if less than 5 plates were destroyed, reloads the puzzle
    public void Plaque_ordre()
    {
        //checks if the good number of plates is destroyed
        int nb=0;
        for (int i = 1; i <= 5; i++)
        {
            if ((GameObject.FindGameObjectWithTag("plaque " + i) == null ? true : false))
            {
                nb ++;
            }
        }

        if (nb == 5)
        {
            if (ordre1 && ordre2 && ordre3 && ordre4 && ordre5)
            {
                //good order
                Destroy(OldDoor);
                Instantiate(NewPlate, this.gameObject.transform.position, this.gameObject.transform.rotation);
                Destroy(this.gameObject);
            }
            else
            {
                //bad order
                SceneManager.LoadScene(SceneActuelle);                
            }
        }
        else
            SceneManager.LoadScene(SceneActuelle);

    }

    //stores the order the plates were touched
    //to avoid unexpected behavior, the value cannot be changed if the plate involved has been touched in the right order
    public void Ordre_des_plaques(int truc)
    {
        if (truc == 1)
        {
            if(!ordre1)
                ordre1 = GameObject.Find("plaque 1").GetComponent<plaques_ordre>().IsGoodOrder();
        }
        else if (truc == 2)
        {
            if (!ordre2)
                ordre2 = GameObject.Find("plaque 2").GetComponent<plaques_ordre>().IsGoodOrder();
        }
        else if (truc == 3)
        {
            if (!ordre3)
                ordre3 = GameObject.Find("plaque 3").GetComponent<plaques_ordre>().IsGoodOrder();
        }
        else if (truc == 4)
        {
            if (!ordre4)
                ordre4 = GameObject.Find("plaque 4").GetComponent<plaques_ordre>().IsGoodOrder();
        }
        else if (truc == 5)
        {
            if (!ordre5)
                ordre5 = GameObject.Find("plaque 5").GetComponent<plaques_ordre>().IsGoodOrder();
        }
        else
            Debug.Log("Mauvais argument dans l'appel de Ordre_des_plaques");
    }

    void monstres()
    {
        Instantiate(NewPlate, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Debug.Log("En principe, là il y a des monstres qui tombent");
    }


}


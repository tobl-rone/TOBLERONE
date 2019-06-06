using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_dynamiques : MonoBehaviour
{

    // Fonctions : ResetColor, Fin_enigme_couleurs, Plaque_ouvrir_porte
    private Renderer rend;
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

    private void ResetColor()
    {
        foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
        {
            ObjectFound.GetComponent<Renderer>().material.mainTexture = defaultTexture;
        }
    }

    private void Fin_enigme_couleurs()
    {
        bool check = true;
        bool found = false;
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
            
            var truc = GameObject.FindGameObjectWithTag("porte");

            var rend_ = truc.GetComponent<Renderer>();
            rend_.material.mainTexture = alternateTexture;
            rend.material.mainTexture = alternateTexture;
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Destroy(ObjectFound);
            }
        }
        else
        {
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


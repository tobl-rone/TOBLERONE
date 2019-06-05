using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_dynamiques : MonoBehaviour {

    enum Fonction { ResetColor, Fin_enigme_couleurs, Plaque_temporaire_ouvrir_porte }
    private Renderer rend;
    public GameObject NewDoor;
    public GameObject NewPlate;
    [SerializeField] string NomFonction;
    [SerializeField] Texture ownTexture;
    [SerializeField] Texture defaultTexture;
    [SerializeField] Texture alternateTexture;
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = ownTexture;

        //rend.material.mainTexture = blutext;
    }

    void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (NomFonction)
        {
            case "ResetColor":
                ResetColor();
                break;
            case "Fin_enigme_couleurs":
                Fin_enigme_couleurs();
                break;
            case "Plaque_temporaire_ouvrir_porte":
                Plaque_temporaire_ouvrir_porte();
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
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Destroy(ObjectFound);
            }
        }
    }
    private void Plaque_temporaire_ouvrir_porte()
    {
        var truc = GameObject.FindGameObjectWithTag("porte_fermee");
        var chose = truc.transform.position;
        var bidule = truc.transform.rotation;
        Destroy(GameObject.FindGameObjectWithTag("porte_fermee"));
        Instantiate(NewDoor, chose, bidule);
        Instantiate(NewPlate, GameObject.FindGameObjectWithTag("plaque_porte").transform.position, GameObject.FindGameObjectWithTag("plaque_porte").transform.rotation);

        Destroy(this.gameObject);
    }


}

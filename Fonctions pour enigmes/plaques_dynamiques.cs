using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaques_dynamiques : MonoBehaviour {

    enum Fonction { Reset, Fin_enigme_couleurs }
    private Renderer rend;
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
            case "Reset":
                Reset();
                break;
            case "Fin_enigme_couleurs":
                Fin_enigme_couleurs();
                break;
            default:
                Debug.Log("Fonction donnée inconnue");
                break;
        }
    }

    private void Reset()
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
        if (check&&found)        {

            var truc = GameObject.FindGameObjectWithTag("porte_fermee");

            var rend_ = truc.GetComponent<Renderer>();
            rend_.material.mainTexture = alternateTexture;
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Destroy(ObjectFound);
            }
        }
    }
}

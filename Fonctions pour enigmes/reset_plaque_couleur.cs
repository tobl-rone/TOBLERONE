using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_plaque_couleur : MonoBehaviour {

    enum Fonction { Reset, Fin_enigme_couleurs }
    public Renderer rend;
    [SerializeField] string NomFonction;
    [SerializeField] Texture defaulttexture;
    [SerializeField] Texture redtext;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
            //Do something to ObjectFound, like this:
            ObjectFound.GetComponent<Renderer>().material.mainTexture = defaulttexture;
            //rend.material.mainTexture = texture;
        }
    }

    private void Fin_enigme_couleurs()
    {

        bool check = true;
        foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
        {
            //Do something to ObjectFound, like this:
            if (ObjectFound.GetComponent<Renderer>().material.mainTexture != redtext)
            {
                check = false;
                break;
            }
            //rend.material.mainTexture = texture;
        }
        if (check)
        {

            var truc = GameObject.FindGameObjectWithTag("porte_fermee");

            var rend = truc.GetComponent<Renderer>();
            rend.material.mainTexture = redtext;
            foreach (GameObject ObjectFound in GameObject.FindGameObjectsWithTag("plaque_couleur"))
            {
                Destroy(ObjectFound);
            }
        }
    }
}

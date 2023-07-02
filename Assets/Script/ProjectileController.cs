using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //Script � modifier pour l'am�liorer. 
    //Projectile qui suit l'ennemi ?
    //D�truit au bout d'un certains temps ? (Si jamais il rate sa cible)


    [SerializeField]
    private float force = 50.0f;

    [SerializeField]
    private EnnemyController ennemi;

    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        ennemi = GameObject.Find("Ennemi").GetComponent<EnnemyController>();
        rigidBody = GetComponent<Rigidbody>(); // On r�cup�re le rigidbody de l'objet
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(transform.forward * force); // On lui ajoute une force pour le faire bouger
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ennemi") // Si la cible est le joueur
        {
            Debug.Log("Attaque ennemi projectile");
            damage(); // On calcule les d�g�ts
        }
    }

    public void damage()
    {
        ennemi.GetComponent<EnnemyController>().life = ennemi.GetComponent<EnnemyController>().life - 2;
        Debug.Log("moins de vie");
        Destroy(gameObject);
    }
}

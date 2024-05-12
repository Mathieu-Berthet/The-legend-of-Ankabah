using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField]
    private EnnemyController ennemi;

    // Start is called before the first frame update
    void Start()
    {
        ennemi = GameObject.Find("Ennemi").GetComponent<EnnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Quand l'�p�e entre en collision avec l'ennemi
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ennemi") // Si la cible c'est l'ennemi
        {
            Debug.Log("Attaque ennemi");
            damage(); // On calcule les d�g�ts
        }
    }

    public void damage()
    {
        ennemi.life = ennemi.life - 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [SerializeField]
    private float distanceToAttack; // La distance a laquelle l'ennemi va attaquer les h�ros

    private float distanceToHero; // La distance entre le h�ros et l'ennemi

    [SerializeField]
    private GameObject heroToAttack; // H�ros � cibler (celui qui sera le leader du groupe de h�ros)

    [SerializeField]
    private Vector3 originPosition; //Position d'origine de l'ennemi

    [SerializeField]
    private bool isAggressive = true;

    public float actualTime; // Le temps qui passe
    public bool canLerp = true;

    //Ajouter des variables pour ne pas avoir une distance fixe. Chaque ennemi/famille d'ennemi pourrait ne pas avoir la m�me distance d'aggression. 

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(isAggressive)
        {
            distanceToHero = Vector3.Distance(heroToAttack.transform.position, transform.position); // On r�cup�re la distance entre 2 position
            if (distanceToHero < distanceToAttack)
            {
                Attack();


                //Je lance le combat
                Debug.Log("J'attaque le h�ros");

            }
            else
            {
                Debug.Log("Le h�ros est trop loin");
                transform.position = originPosition;

            }
        }
        else
        {
            transform.position = originPosition;
        }
    }


    public void Attack()
    {
        //canLerp = true;
        if(distanceToHero < distanceToAttack && canLerp)
        {
            //Je me dirige vers lui
            actualTime += Time.deltaTime; // On r�cup�re le temps qui passe

            //Il faut surement encore diviser cela, afin que ce soit un peu plus lent. Car il faut bien que le h�ros est le temps de fuir s'il veut pas faire le combat
            transform.position = Vector3.Lerp(transform.position, heroToAttack.transform.position, actualTime/30); // On fait bouger l'ennemi vers la position du joueur, et on utilise le temps pour que ce d�placement se fasse de mani�re smooth



            if(actualTime > 5.1f)
            {
                canLerp = false;
                actualTime = 0.0f;
                //transform.position = heroToAttack.transform.position; // Si le temps est �coul�, on bloque l'ennemi sur la position du h�ros. Cette partie l� sera surement a chang� un peu, ce n'est pas totalement le comportement voulu ici
            }


            ////Si je le touche, je lance le combat ! Chargement de la sc�ne de combat ?
            //A voir plus tard
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward);
    }
}

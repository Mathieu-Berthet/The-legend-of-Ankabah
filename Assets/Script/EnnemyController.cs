using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    public int life = 20;


    [SerializeField]
    private float distanceToAttack; // La distance a laquelle l'ennemi va attaquer les h�ros

    private float distanceToHero; // La distance entre le h�ros et l'ennemi

    [SerializeField]
    private GameObject heroToAttack; // H�ros � cibler (celui qui sera le leader du groupe de h�ros)

    [SerializeField]
    private Vector3 originPosition; //Position d'origine de l'ennemi

    [SerializeField]
    private bool isAggressive = true;

    public float actualTime; // Le temps qui passe pour l'attaque, a voir si cette variable est utile


    public float actualTimeToMove; // Le temps qui passe pour le changement de position

    //Les 3 positions sur lesquelles les ennemis tourneront. A voir si on en mettra plus ou moins selon les lieux/ennemis/groupe etc ...
    public Vector3 position1;
    public Vector3 position2;
    public Vector3 position3;

    //A voir si ceux la sont utile
    public bool canLerp = true;
    public bool canLerpMove = true;

    //Les �tats que peut avoir un ennemi. Ils sont � renommer
    public enum StateEnnemy
    {
        toMove = 0,
        toAttack = 1,
    }

    //Les �tats pour les positions quand les ennemis tournent sur la map. A renommer voir � changer le syst�me
    public enum StatePosition
    {
        pos1 = 0,
        pos2 = 1,
        pos3 = 2,
    }

    public StateEnnemy state;
    public StatePosition ennemyPositions;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        //On initialise les enum
        state = StateEnnemy.toMove;
        ennemyPositions = StatePosition.pos1;

        //On initialise les positions sur lequels ils vont tourner. Id�alement, cela sera fait de mani�re automatique par la suite. Via un NavMesh ??
        position1 = new Vector3(-20.0f, 0.5f, 0.0f);
        position2 = new Vector3(-10.0f, 0.5f, 0.0f);
        position3 = new Vector3(-15.0f, 0.5f, 10.0f);
        

    }

    // Update is called once per frame
    void Update()
    {
        if(isAggressive)
        {
            distanceToHero = Vector3.Distance(heroToAttack.transform.position, transform.position); // On r�cup�re la distance entre 2 position
            if (distanceToHero < distanceToAttack)
            {
                state = StateEnnemy.toAttack;


                Debug.Log("J'attaque le h�ros");

            }
            else
            {
                Debug.Log("Le h�ros est trop loin");
                transform.position = originPosition;
                state = StateEnnemy.toMove;

            }
        }
        else
        {
            //transform.position = originPosition;
        }

        if(state == StateEnnemy.toAttack)
        {
            //Attack();
        }
        else
        {
            //move();
        }

        kill();
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



            if(actualTime > 2.1f)
            {
                canLerp = false;
                actualTime = 0.0f;
                //transform.position = heroToAttack.transform.position; // Si le temps est �coul�, on bloque l'ennemi sur la position du h�ros. Cette partie l� sera surement a chang� un peu, ce n'est pas totalement le comportement voulu ici
            }


            ////Si je le touche, je lance le combat ! Chargement de la sc�ne de combat ?
            //A voir plus tard
        }
    }

    public void move()
    {
        //Premier changement de position
        if (canLerpMove && ennemyPositions == StatePosition.pos1)
        {
            Debug.Log("Coucou 1");
            actualTimeToMove += Time.deltaTime; // On r�cup�re le temps qui passe
            transform.position = Vector3.Lerp(position1, position2, actualTimeToMove); // On fait bouger l'ennemi vers la position suivante, et on utilise le temps pour que ce d�placement se fasse de mani�re smooth

            if (actualTimeToMove > 1.1f)
            {
                //canLerpMove = false;
                actualTimeToMove = 0.0f; // On remet le temps � 0 pour le mouvement suivant. 
                ennemyPositions = StatePosition.pos2; // Si le temps est �coul�, on change l'�tat de la position. 
            }
        }
        //Deuxi�me changement de position
        else if (canLerpMove && ennemyPositions == StatePosition.pos2)
        {
            Debug.Log("Coucou 2");
            actualTimeToMove += Time.deltaTime;
            transform.position = Vector3.Lerp(position2, position3, actualTimeToMove);
            if (actualTimeToMove > 1.1f)
            {
                //canLerpMove = false;
                actualTimeToMove = 0.0f;
                ennemyPositions = StatePosition.pos3;
            }
        }
        //Retour � la premi�re position. 
        else if (canLerpMove && ennemyPositions == StatePosition.pos3)
        {
            Debug.Log("Coucou 3");
            actualTimeToMove += Time.deltaTime;
            transform.position = Vector3.Lerp(position3, position1, actualTimeToMove);
            
            if (actualTimeToMove > 1.1f)
            {
                //canLerpMove = false;
                actualTimeToMove = 0.0f; 
                ennemyPositions = StatePosition.pos1;
            }
        }
    }


    //Quand l'ennemi entre en collision avec le h�ros
    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "Player") // Si la cible est le joueur
        {
            Debug.Log("Attaque heros");
            damage(); // On calcule les d�g�ts
        }
    }

    public void damage()
    {
        heroToAttack.GetComponent<PlayerController>().life = heroToAttack.GetComponent<PlayerController>().life - 2;
    }

    //Pour quand l'ennemi est mort. Reste plus qu'a cr�er le comportement de la mort
    public void kill()
    {
        if (life <= 0)
        {
            Debug.Log("Ennemi : Je suis mort");
        }
    }
}

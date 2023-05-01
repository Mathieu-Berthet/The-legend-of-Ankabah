using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [SerializeField]
    private float distanceToAttack;

    [SerializeField]
    private GameObject heroToAttack;

    private float anotherX;
    private float anotherY;
    private float anotherZ;

    [SerializeField]
    private bool isAggressive = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAggressive)
        {
            distanceToAttack = Vector3.Distance(heroToAttack.transform.position, transform.position);
            if (distanceToAttack < 5.0f)
            {
                Attack();


                //Je lance le combat
                Debug.Log("J'attaque le héros");

            }
        }
    }


    public void Attack()
    {
        if(distanceToAttack < 5.0f)
        {
            //Je me dirige vers lui
            //A continuer, pas la bonne méthode. Revoir ça
            anotherX = Mathf.Lerp(transform.position.x, heroToAttack.transform.position.x, 3.0f);
            anotherZ = Mathf.Lerp(transform.position.z, heroToAttack.transform.position.z, 3.0f);


            ////Je lance le combat ! Chargement de la scène de combat ?
            //A voir plus tard
        }
    }
}

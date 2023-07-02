
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public int life = 20;

    // this sctript is for the movement of the player
    [SerializeField] // see variable on inspector
    private float speed = 3f;

    [SerializeField] // see variable on inspector
    private float mouseSensitivityX = 3f;

    [SerializeField] // see variable on inspector
    private float mouseSensitivityY = 3f;

    [SerializeField]
    [SerializeField]
    private float jumpHeight = 5f;

    [SerializeField]
    private int maxJumps = 2;
    private int jumpsRemaining;

    private bool isJumping = false;

    private PlayerMotor motor;

    [SerializeField]
    private GameObject projectile; // Projectile qui sera lancer


    private void Start()
    {
        motor = GetComponent<PlayerMotor>(); // link PalyerMotor on this class
        
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // update the velocity of the player in real time
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        motor.Move(velocity);
                // Detecter l'input de saut
        if (Input.GetButtonDown("Jump"))
        {
            StartJump();
        }
        // update rotation of the player with a vector3
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRotation, 0) * mouseSensitivityX ;

        motor.Rotate(rotation);

         // update rotation of the camera with a vector3
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRotation, 0, 0) * mouseSensitivityY ;

        motor.RotateCamera(cameraRotation);


        if(Input.GetKeyDown(KeyCode.E))
        {
            launchProjectile();
        }

        kill();
    }

    //Quand le h�ros a plus de PV. Reste plus qu'a cr�er le comportement de la mort
    public void kill()
    {
        if (life <= 0)
        {
            Debug.Log("Heros Je suis mort");
        }
    }

    //Fonction qui instancie le projectile et lui ajoute le script
    public void launchProjectile()
    {
        GameObject projectileLaunched = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileLaunched.AddComponent<ProjectileController>();
    }

    private void StartJump()
    {
         if (jumpsRemaining > 0)
        {
            jumpsRemaining--;

           if (!isJumping)
            {
                isJumping = true;

                // Calculer la vélocité du saut
                float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));

                // Ajouter la vélocité du saut au mouvement vertical
                Vector3 jumpVelocityVector = Vector3.up * jumpVelocity;
                motor.ApplyJump(jumpVelocityVector);

                // Démarrer une coroutine pour réinitialiser l'état de saut après un certain délai
                StartCoroutine(ResetJump());
            }
        }

    }
    
    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.1f); // Délai avant de réinitialiser l'état de saut
        isJumping = false;
    }
private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        ResetJumps();
    }
}
    public void ResetJumps()
{
    jumpsRemaining = maxJumps;
}

}

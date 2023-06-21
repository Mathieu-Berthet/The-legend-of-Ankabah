
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public int life = 20;

    [SerializeField] // see variable on inspector
    private float speed = 3f;

    [SerializeField] // see variable on inspector
    private float mouseSensitivityX = 3f;

    [SerializeField] // see variable on inspector
    private float mouseSensitivityY = 3f;

    [SerializeField]
    private PlayerMotor motor;

    [SerializeField]
    private GameObject projectile; // Projectile qui sera lancer


    private void Start()
    {
        motor = GetComponent<PlayerMotor>(); // link PalyerMotor on this class
        
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

    //Quand le héros a plus de PV. Reste plus qu'a créer le comportement de la mort
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
}

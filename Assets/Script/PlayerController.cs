
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] // see variable on inspector
    private float speed = 3f;

        [SerializeField] // see variable on inspector
    private float mouseSensitivityX = 3f;
            [SerializeField] // see variable on inspector
    private float mouseSensitivityY = 3f;

    private PlayerMotor motor;

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
    }
}

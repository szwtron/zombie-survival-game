using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SC_Camera : MonoBehaviour
{
    //float speed = 2f;
    // public float bulletVelocity = 1f;
    // public float fireRate = 1;
    // private float nextFire = 0.0F;
    //[SerializeField] GameObject bullet;

    // public Vector3 jump;
    // public float jumpForce = 2.0f;
    // public bool isGrounded;
    // Rigidbody rb;
    //
    // public CharacterController controller;
    // public float speed = 6f;

    // Start is called before the first frame update
    // void Start()
    // {
    //     // rb = GetComponent<Rigidbody>();
    //     // jump = new Vector3(0.0f, 0.5f, 0.0f);
    // }

    // void OnCollisionStay(){
    //     isGrounded = true;
    // }

    // Update is called once per frame
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        
        
        // if (Input.GetKey (KeyCode.W)) {
        //     this.transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * speed);
        // } else if (Input.GetKey (KeyCode.S)) {
        //     this.transform.Translate (new Vector3 (0, 0, -1) * Time.deltaTime * speed);
        // }
        // if (Input.GetKey (KeyCode.A)) {
        //     this.transform.Translate (new Vector3 (-1, 0, 0) * Time.deltaTime * speed);
        // } else if (Input.GetKey (KeyCode.D)) {
        //     this.transform.Translate (new Vector3 (1, 0, 0) * Time.deltaTime * speed);
        // }
        //
        // if (Input.GetKey (KeyCode.UpArrow)) {
        //     this.transform.Rotate (Vector3.left * 0.1f);
        // } else if (Input.GetKey (KeyCode.DownArrow)) {
        //     this.transform.Rotate (Vector3.right * 0.1f);
        // }
        // if (Input.GetKey (KeyCode.LeftArrow)) {
        //     this.transform.Rotate (Vector3.down * 0.1f);
        // } else if (Input.GetKey (KeyCode.RightArrow)) {
        //     this.transform.Rotate (Vector3.up * 0.1f);
        // }

        // if (Input.GetKey(KeyCode.Space) && isGrounded) {
        //    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //    isGrounded = false;
        // }
        //
        // float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        // Vector3 targetDirection = new Vector3(h, 0f, v);
        // targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        // targetDirection.y = 0.0f;
        // Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        // this.transform.rotation = targetRotation;
        //
        // this.transform.position += Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * v * Time.deltaTime;
        // this.transform.position += Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * h * Time.deltaTime;

        // if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire) {
        //     nextFire = Time.time + fireRate;
        //     GameObject projectile = Instantiate(bullet)as GameObject;
        //     projectile.transform.position = transform.position + Camera.main.transform.forward * 2;
        //     Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //     rb.velocity= Camera.main.transform.forward * 40;
        // }

        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        // Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //
        // if (direction.magnitude >= 0.1f)
        // {
        //     controller.Move(direction * speed * Time.deltaTime);
        // }


    }
}

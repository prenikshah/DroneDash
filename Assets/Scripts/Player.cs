using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float speed;
    [SerializeField] private float sensitivity = 2f; // Adjust the mouse sensitivity as per your requirements
    public float minX = -1f; // Minimum position on the X-axis
    public float maxX = 1f; // Maximum position on the X-axis
    public float minY = 1f;
    public float maxY = -1f;
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource bgMusic;
    [SerializeField] Laser laser;
    //[SerializeField] Slider sensitivitySlider;

    private bool isGameStart = false;
    private IEnumerator Start()
    {
        //sensitivitySlider.minValue = 2.0f;
        //sensitivitySlider.maxValue = 5.0f;
        //sensitivitySlider.value = sensitivity;
        // Check if the device supports the gyroscope
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            // Initialize the gyroscope
            gyro = Input.gyro;
            gyro.enabled = true;
        }
        yield return new WaitForSeconds(0.5f);
        //GetComponent<Animator>().enabled = false;
        bgMusic.Play();
        centerPoint = Screen.width / 2;
        isGameStart = true;
    }
    public static Player Instance;

    public bool gyroSupported;
    private Gyroscope gyro;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    //    private void FixedUpdate()
    //    {
    //        if (!isGameStart) return;
    //        float horizontalInput = Input.GetAxis("Horizontal");
    //#if UNITY_EDITOR
    //        horizontalMovement += horizontalInput * sensitivity * Time.deltaTime;
    //#endif

    //        float verticalInput = Input.GetAxis("Vertical");
    //        verticalMovement += verticalInput * sensitivity * Time.deltaTime;
    //        // Get the gyroscope rotation rate around the Y-axis (tilt left-right)
    //        if (gyroSupported && Mathf.Abs(Input.gyro.attitude.x) > 0)
    //        {
    //            // Get the gyroscope rotation rate around the Y-axis (tilt left-right)
    //            float gyroYRotation = -Input.gyro.attitude.x;
    //            Debug.Log(gyroYRotation);
    //            // Calculate the horizontal movement based on the gyroscope input
    //            horizontalMovement += (gyroYRotation * sensitivity * Time.deltaTime);
    //            Debug.Log("Horizonatal movement" + horizontalMovement);
    //        }

    //        horizontalMovement = Mathf.Clamp(horizontalMovement, minX, maxX);
    //        verticalMovement = Mathf.Clamp(verticalMovement, minY, maxY);
    //        // Calculate the new position of the camera
    //        Vector3 newPosition = transform.position;
    //        newPosition.x = horizontalMovement;
    //        //newPosition.y = verticalMovement;
    //        // Update the position of the camera 
    //        transform.position = newPosition;

    //        transform.Translate(speed * Time.deltaTime * Vector3.forward);

    //    }
    public float centerPoint;

    private void LateUpdate()
    {
        if (!isGameStart) return;

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log($"Mouse position {Input.mousePosition}");
            var value = transform.position.x;
            if (Input.mousePosition.x <= centerPoint)
            {
                value += -maxX;
                //Debug.Log($"Left {value}");
                horizontalMovement = Mathf.Clamp(value, -maxX, maxX);
            }
            else
            {
                value += maxX;
                horizontalMovement = Mathf.Clamp(value, -maxX, maxX);
                //Debug.Log($"Right {value}");
            }
            Vector3 newPosition = transform.position;
            newPosition.x = horizontalMovement;
            transform.position = newPosition;
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    public void OnValueChange()
    {
        //sensitivity = sensitivitySlider.value;
    }
    public void MakeEffect()
    {
        if (sfxSource.isPlaying)
        {
            //var value = sfxSource.pitch + 0.1f;
            //sfxSource.pitch = Mathf.Clamp(value, 1, 2.5f);
            sfxSource.Stop();
        }
        sfxSource.Play();
    }
    public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

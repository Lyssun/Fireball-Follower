using UnityEngine;

/*
 * Script for the main camera
 * You need to add a Plane as gameObject
 * 
 * It's adapted for mobile device (using the gyro for the rotation.)
 * but we can use a normal webcam.
 * 
 * If you don't see anything, look if your plane is in the good rotation.
 */
public class WebCamScript : MonoBehaviour {
    // Object witch will get the camera preview.
    public GameObject webCamera;

	// Use this for initialization
	void Start () {
        // if it's a mobile ...
        if (Application.isMobilePlatform)
        {
            // create a new camera adapted to the mobile with the good position.
            // it's the new parent of your camera.
            GameObject cameraParent = new GameObject("camParent");
            cameraParent.transform.position = transform.position;
            transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, 90);
        }
        else
            // Rotation on the plan because the mobile one is upside down...
            GameObject.Find("WebcamPlane").transform.Rotate(Vector3.up, -180);

        // Use of the gyro to become a real AR app.
        Input.gyro.enabled = true;
        // We create the texture that will allow us to see the camera.
        WebCamTexture webCameraTexture = new WebCamTexture();
        webCamera.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();
    }
	
    /*
     * Rotate the camera with the information from the gyro of the mobile.
     */
	void Update () {
        transform.localRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
    }
}

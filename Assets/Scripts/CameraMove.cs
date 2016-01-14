using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public float smoothing = 5f;

	void Start () {
	
	}
	
	void Update () {

        Vector3 CamPos = transform.position;

        transform.position = Vector3.Lerp(transform.position, CamPos, smoothing * Time.deltaTime);
	}
}

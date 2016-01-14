using UnityEngine;
using System.Collections;

public class CameraCenter : MonoBehaviour {

    public Transform player;
    public float T;
    public float yVelocity = 0.0f;
    public float yyVelocity = 0.0f;
    public float zzVelocity = 0.0f;
    public float angleVelocity = 0.0f;
    public float smooth = 0.3f;
    public float smoothTime = 0.3f;
    public float anglesmooth = 1f;
    public float distance = 5.0f;
    public GameObject Turn;
    public Transform MainCamera;
    
    float angle = 0f;
    float offset = 0f;
    float yAngle;
    float setY = 5;
    float setZ = -2.5f;
    float setAngle = 30f;

    void Update()
    {
        Vector3 position = player.position;
        if (Turn.GetComponent<Turning>().Win == true)
        {
            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -player.eulerAngles.y, ref yVelocity, smooth);
            position += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
            transform.position = position;
            transform.LookAt(player);
            float newPositionY = Mathf.SmoothDamp(setY, 0.75f, ref yyVelocity, smoothTime);
            setY = newPositionY;
            float newPositionZ = Mathf.SmoothDamp(setZ, 0.5f, ref zzVelocity, smoothTime);
            setZ = newPositionZ;
            float newAngle = Mathf.SmoothDamp(setAngle, 0, ref angleVelocity, anglesmooth);
            setAngle = newAngle;
            MainCamera.localPosition = new Vector3 (0, setY, setZ);
            MainCamera.localEulerAngles = new Vector3(setAngle, 0, 0);
        }
        else
        {
            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, player.eulerAngles.y, ref yVelocity, smooth);
            position += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
            transform.position = position;
            transform.LookAt(player);
        }
    }
}
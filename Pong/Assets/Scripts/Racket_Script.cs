using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket_Script : MonoBehaviour
{
    public bool HasCollided;

    public Transform Ball;
    // Start is called before the first frame update
    void Start()
    {
        HasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Ball);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        HasCollided = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        HasCollided = false;
    }
}

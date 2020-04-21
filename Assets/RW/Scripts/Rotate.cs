using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 speed = new Vector3(300, 50, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, speed * Time.deltaTime, 0);
        transform.Rotate(speed*Time.deltaTime);
    }
}

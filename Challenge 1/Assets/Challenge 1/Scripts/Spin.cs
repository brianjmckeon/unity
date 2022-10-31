using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public GameObject propeller;
    public float rate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        propeller.transform.Rotate(Vector3.back * Time.deltaTime * rate);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedSpin = 360f;

    private void Update()
    {
        transform.Rotate(0, 0, speedSpin*Time.deltaTime);
    }


}

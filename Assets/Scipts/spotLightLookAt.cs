using UnityEngine;
using System.Collections;

public class spotLightLookAt : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target);
    }
}

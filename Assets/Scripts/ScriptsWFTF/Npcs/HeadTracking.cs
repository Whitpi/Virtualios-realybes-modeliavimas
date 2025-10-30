using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeadTracking : MonoBehaviour
{
    
    public Transform whoToLookAt;

    private void Update()
    {
        transform.LookAt(whoToLookAt);
    }

}

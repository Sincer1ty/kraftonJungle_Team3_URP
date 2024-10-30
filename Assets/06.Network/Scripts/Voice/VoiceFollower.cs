using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceFollower : MonoBehaviour
{
    Transform target
    {
        get
        {
            return Camera.main?.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
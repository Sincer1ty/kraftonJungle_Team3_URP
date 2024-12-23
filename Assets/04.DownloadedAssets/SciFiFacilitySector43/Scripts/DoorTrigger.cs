﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Door_Download door;

    void Start()
    {
        door = GetComponentInParent<Door_Download>();
    }

    void OnTriggerEnter(Collider c) {

        door.OpenDoor();

    }

    void OnTriggerExit(Collider c) {
        door.CloseDoor();
    }
}

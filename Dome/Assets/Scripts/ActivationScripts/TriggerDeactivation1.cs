using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeactivation1 : MonoBehaviour
{

    //Der Dialog Trigger wird erst aktiv, wenn der Joule den Trigger ausl�st
    public GameObject enableTargetObject;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            enableTargetObject.gameObject.SetActive(false);

        }
    }

}

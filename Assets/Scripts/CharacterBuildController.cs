using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuildController : MonoBehaviour
{
    [SerializeField] GameObject _cam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ex")
        {
            _cam.GetComponent<Aim>().create = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ex")
        {
            _cam.GetComponent<Aim>().create = true;
        }
    }
}

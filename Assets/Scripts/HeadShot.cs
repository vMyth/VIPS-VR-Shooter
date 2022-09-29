using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    public GameObject head;
    public GameObject bloodGushEffect;

    private void OnDisable()
    {
        head.SetActive(false);
        bloodGushEffect.SetActive(true);
    }
}

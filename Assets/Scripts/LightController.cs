using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light lig;

    public float min, max;

    private void Start()
    {
        lig = GetComponent<Light>();
        //StartCoroutine(MakeLightFlicker());
        StartCoroutine("MakeLightFlicker");
    }

    IEnumerator MakeLightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            lig.enabled = !lig.enabled;
        }
    }
}

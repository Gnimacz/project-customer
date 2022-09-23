using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeController : MonoBehaviour
{
    [SerializeField] Material dayTimeMaterial;
    [SerializeField] Material nightTimeMaterial;
    [SerializeField] GameObject sun;
    Transform oldSunTransform;

    private void Start()
    {
        oldSunTransform = sun.transform;
    }
    public void SetToNight()
    {
        RenderSettings.skybox = nightTimeMaterial;
        sun.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetToDay()
    {
        RenderSettings.skybox = dayTimeMaterial;
        sun.transform.rotation = oldSunTransform.rotation;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAnim : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool activateRotation;
    [SerializeField] private bool activateScale;


    [Header("Rotation")]
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private float speedRotation;

    [Header("Scale")]
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private float speedScale;
    [SerializeField] private float ratioScale;


    private float scaleTime;
    private bool superiorScale;

    private void Update()
    {
        if (activateRotation)
        {
            transform.Rotate(rotationAngle * speedRotation * Time.deltaTime);
        }

        if (activateScale)
        {
            scaleTime += Time.deltaTime;
            if (superiorScale)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, finalScale, speedScale * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale, speedScale * Time.deltaTime);
            }

            if (scaleTime >= ratioScale)
            {
                superiorScale = !superiorScale;
                scaleTime = 0f;
            }
        }
    }


}

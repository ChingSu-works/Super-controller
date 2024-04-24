using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintEffect : MonoBehaviour
{
    [SerializeField] GameObject[] Backgrounds = new GameObject[5];
    Vector3 RotationA, RotationB, RotationC, RotationD, RotationE;
    void Start()
    {
    }

    void Update()
    {
        RotationA = new Vector3(0, 0, sineAmount()/2);
        RotationB = new Vector3(0, 0, cosAmount()/2);
        RotationC = new Vector3(0, 0, sineAmount()/6);
        RotationD = new Vector3(0, 0, cosAmount()/4);
        RotationE = new Vector3(0, 0, sineAmount()/3);

        // Debug.Log(RotationA);
        // Debug.Log(sineAmount());
        // Debug.Log(Time.time);

        Backgrounds[0].transform.Rotate(RotationA);
        Backgrounds[1].transform.Rotate(RotationB);
        Backgrounds[2].transform.Rotate(RotationC);
        Backgrounds[3].transform.Rotate(RotationD);
        Backgrounds[4].transform.Rotate(RotationE);
    }
    public float cosAmount()
    {
        return Mathf.Cos(Time.time);
    }
    
    public float sineAmount()
    {
        return Mathf.Sin(Time.time);
    }
}

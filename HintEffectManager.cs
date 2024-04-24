using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintEffectManager : MonoBehaviour
{
    [SerializeField] Material[] Material0 = new Material[6];
    [SerializeField] Material[] Material1 = new Material[6];
    [SerializeField] Material[] Material2 = new Material[6];

    
    [SerializeField] GameObject[] Backgrounds = new GameObject[5];
    Vector3 RotationA, RotationB, RotationC, RotationD, RotationE;
    void Start()
    {
    }

    void Update()
    {
        RotationA = new Vector3(0, 0, sineAmount()/5);
        RotationB = new Vector3(0, 0, cosAmount()/5);
        RotationC = new Vector3(0, 0, sineAmount()/10);
        RotationD = new Vector3(0, 0, cosAmount()/8);
        RotationE = new Vector3(0, 0, sineAmount()/3);

        Debug.Log(RotationA);
        Debug.Log(sineAmount());
        Debug.Log(Time.time);

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

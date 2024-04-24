using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    public string input;

    public void rdInput(string S)
    {
        input = S;
        Debug.Log(input);
    }
}

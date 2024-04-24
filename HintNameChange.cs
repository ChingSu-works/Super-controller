using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintNameChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    void Update()
    {
        Name.text = this.gameObject.name;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class RobotManagerV2 : MonoBehaviour
{
    [SerializeField] DetectionTest TagManager;
    [SerializeField] GameObject[] HintPrefab = new GameObject[4];
    [SerializeField] GameObject[] PickPrefab;

    GameObject[] HintList = new GameObject[4];
    GameObject[] PickModel = new GameObject[4];

    Vector3[] TagPos = new Vector3[4];
    Quaternion[] TagRotate = new Quaternion[4];
    
    GameObject hitObject;
    public int SelectedID = -1;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            PickModel[i] = Instantiate(PickPrefab[i], new Vector3(-1.0f, -1.0f, -1.0f), new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        }
        for (int i = 0; i < 4; i++)
        {
            HintList[i] = Instantiate(HintPrefab[i], new Vector3(-1.0f, -1.0f, -1.0f), new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }

    private void Update()
    {
        for(int i = 0; i < 3; i ++)
        {
            HintList[i].transform.position = new Vector3(-1.0f, -1.0f, -1.0f);
            PickModel[i].transform.position = new Vector3(-1.0f, -1.0f, -1.0f);
        }

        foreach (var _tag in TagManager._detector.DetectedTags)
        {
            TagPos.SetValue(_tag.Position, _tag.ID);
            TagRotate.SetValue(_tag.Rotation, _tag.ID);

            PickModel[_tag.ID].name = _tag.ID.ToString();
            PickModel[_tag.ID].transform.position = TagPos[_tag.ID];
            PickModel[_tag.ID].transform.rotation = TagRotate[_tag.ID];
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch phase is just began (i.e., the finger just touched the screen)
            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast to see if we hit any GameObject
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the raycast hit any collider on the selectable layer mask
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Get the GameObject that was hit
                    hitObject = hit.collider.gameObject;
                }
            }
        }

        SelectedID = int.Parse(hitObject.name);
        Debug.Log(SelectedID);

        HintList[int.Parse(hitObject.name)].transform.position = hitObject.transform.position;
        HintList[int.Parse(hitObject.name)].transform.rotation = hitObject.transform.rotation;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class RobotManager : MonoBehaviour
{
    [SerializeField] DetectionTest TagDetector = null;
    [SerializeField] GameObject HintPrefab;

    List<GameObject> Hints = new List<GameObject>{};
    List<int> AprilID = new List<int>{};
    List<Vector3> HintsPos = new List<Vector3>{};
    List<Quaternion> HintsRotate = new List<Quaternion>{};

    bool isHintPrefabSet;

    GameObject Hint;
    Vector3 HintPosition;
    Quaternion HintRotation;

    void Start()
    {
    }

    void Update()
    {
        AprilID.Clear();
        HintsPos.Clear();
        HintsRotate.Clear();
        // tagExist = false;
        
        foreach (var Tag in TagDetector._detector.DetectedTags)
        {
            RobotInfo _RobotInfo = new RobotInfo(Tag.ID, Tag.Position, Tag.Rotation);
            var Info2Jsn = JsonUtility.ToJson(_RobotInfo);
            
            AprilID.Add(Tag.ID);
            HintsPos.Add(Tag.Position);
            HintsRotate.Add(Tag.Rotation);

            // tagExist = true;
            isHintPrefabSet = false;

            foreach (var knownHint in Hints)
            {
                if (("Robot" + Tag.ID) == knownHint.name)
                {
                    isHintPrefabSet = true;
                }
            }

            if (!isHintPrefabSet)
            {
                Hint = Instantiate(HintPrefab, Tag.Position, Tag.Rotation);
                Hint.name = "Robot" + Tag.ID;
                Hint.tag = Tag.ID.ToString();

                Hints.Add(Hint);
            }

            for(int i = 0; i < TagDetector._detector.DetectedTags.Count(); i++)
            {
                Hints[i].transform.position = HintsPos[i];
                Hints[i].transform.rotation = HintsRotate[i];
            }

            Debug.Log(TagDetector._detector.DetectedTags.Count());
        }
    }

    // void OnDestroy(GameObject ToRemove)
    // {
    //     Destroy(ToRemove);
    // }
}

public class RobotInfo
{
    public int TagID;
    public Vector3 TagPos;
    public Quaternion TagRot;
    public RobotInfo(int tID, Vector3 TPos, Quaternion TRot)
    {
        TagID = tID;
        TagPos = TPos;
        TagRot = TRot;
    }
}
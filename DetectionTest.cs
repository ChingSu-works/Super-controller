using UnityEngine;
using System.Linq;
using UI = UnityEngine.UI;
using Klak.TestTools;
using System.Collections.Generic;
using System.ComponentModel;
using AprilTag;

sealed class DetectionTest : MonoBehaviour
{
    [SerializeField] ImageSource _source = null;
    [SerializeField] int _decimation = 4;
    [SerializeField] float _tagSize = 0.05f;
    [SerializeField] Material _tagMaterial = null;
    [SerializeField] UI.RawImage _webcamPreview = null;
    [SerializeField] UI.Text _debugText = null;

    public AprilTag.TagDetector _detector;
    TagDrawer _drawer;
    public int TagID, SelectedID;
    public Vector3 TagPosition;
    public Quaternion TagRotation;

    void Start()
    {
        var dims = _source.OutputResolution;
        _detector = new AprilTag.TagDetector(dims.x, dims.y, _decimation);
        _drawer = new TagDrawer(_tagMaterial);
    }

    void OnDestroy()
    {
        _detector.Dispose();
        _drawer.Dispose();
    }

    void LateUpdate()
    {
        TagID = -1;
        _webcamPreview.texture = _source.Texture;

        // Source image acquisition
        var image = _source.Texture.AsSpan();
        if (image.IsEmpty) return;

        // AprilTag detection
        var fov = Camera.main.fieldOfView * Mathf.Deg2Rad;
        _detector.ProcessImage(image, fov, _tagSize);

        // Detected tag visualization
        foreach (var tag in _detector.DetectedTags)
        {
            _drawer.Draw(tag.ID, tag.Position, tag.Rotation, _tagSize);
            TagID = tag.ID;
            TagPosition = tag.Position;
            TagRotation = tag.Rotation;
        }

        // Profile data output (with 30 frame interval)
        if (Time.frameCount % 30 == 0)
            _debugText.text = _detector.ProfileData.Aggregate("Profile (usec)", (c, n) => $"{c}\n{n.name} : {n.time}");
    }
}

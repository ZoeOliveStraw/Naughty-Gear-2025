using System.Collections.Generic;
using Character.Scripts;
using UnityEngine;

namespace NPCs.Scripts
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Sense_Vision : MonoBehaviour
    {
        [Header("Component References")] 
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private Transform eyePosition;
    
        [Header("Vision cone dimensions")]
        [SerializeField] private float sightDistance;
        [SerializeField][Range(0,360)] private float sightHorizontalAngle;
        [SerializeField] private float sightHeight;
        [SerializeField] private int meshResolution = 10;

        [Header("Vision State Materials")] 
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material alertMaterial;
    
        [Header("Tracking information")]
        [SerializeField] private List<string> trackedTags = new();

        private List<Transform> _objectsInFOV = new();
        private List<Transform> _seenObjects = new();

        [SerializeField] private bool debuggingPlayerSeen = false;

        private void Start()
        {
            GenerateViewCone();
        }

        private void FixedUpdate()
        {
            if(_objectsInFOV.Count > 0) RaycastToTrackedObjects();
            if(debuggingPlayerSeen) DebuggingPlayerSeen();
        }

        private void GenerateViewCone()
        {
            meshFilter.mesh = VisionConeGenerator.Generate(sightHorizontalAngle, sightHeight, sightDistance, meshResolution);
            meshCollider.sharedMesh = meshFilter.mesh;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (trackedTags.Contains(col.gameObject.tag))
            {
                _objectsInFOV.Add(col.transform);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (trackedTags.Contains(col.gameObject.tag))
            {
                _objectsInFOV.Remove(col.transform);

                if (_seenObjects.Contains(col.transform))
                {
                    _seenObjects.Remove(col.transform);
                }
            }
        }

        private void RaycastToTrackedObjects()
        {
            foreach (Transform t in _objectsInFOV)
            {
                CheckIfObjectSeen(t);
            }
        }

        private void CheckIfObjectSeen(Transform t)
        {
            List<Transform> detectionPoints = t.gameObject.GetComponent<DetectableObject>().detectionPoints;
            //SEND OUT A RAYCAST AND PUT THE HIT TO hit
            foreach (Transform dp in detectionPoints)
            {
                if (Physics.Raycast(transform.position, dp.position - transform.position, out RaycastHit hit))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    if (hit.transform == t)
                    {
                        if (!_seenObjects.Contains(t))
                        {
                            _seenObjects.Add(t);
                        }
                        return;
                    }
                }
            }
            _seenObjects.Remove(t);
        }

        public bool CanSeeObjectWithTag(string tagToCheck)
        {
            foreach (Transform t in _seenObjects)
            {
                if (t.CompareTag(tagToCheck)) return true;
            }
            return false;
        }
    
        public bool CanSeeObject(Transform obj)
        {
            foreach (Transform t in _seenObjects)
            {
                if (t == obj) return true;
            }
            return false;
        }

        public void SetVisionConeColor(Color c)
        {
            meshRenderer.material.color = c;
        }

        private void DebuggingPlayerSeen()
        {
            if (CanSeeObjectWithTag("Player"))
            {
                Debug.LogWarning("Can see player");
                SetVisionConeColor(Color.yellow);
            }
            else
            {
                Debug.LogWarning("Can't see player");
                SetVisionConeColor(Color.green);
            }
        }
    }
}

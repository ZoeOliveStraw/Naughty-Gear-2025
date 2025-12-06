using System.Collections.Generic;
using UnityEngine;

namespace Character.Scripts
{
    public class DetectableObject : MonoBehaviour
    {
        [SerializeField] public List<Transform> detectionPoints;
    }
}

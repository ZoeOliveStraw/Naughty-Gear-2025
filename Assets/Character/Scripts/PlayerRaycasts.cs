using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Scripts
{
    public class PlayerRaycasts : MonoBehaviour
    {
        [SerializeField] private List<Transform> groundCheckOrigins;
        [FormerlySerializedAs("groundCheckRadius")] [SerializeField] private float groundCheckDistance;
    
        public bool IsGrounded;

        // Update is called once per frame
        void Update()
        {
            GroundCheck();
        }

        private void GroundCheck()
        {
            foreach (Transform groundCheck in groundCheckOrigins)
            {
                Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, groundCheckDistance);
                if (hit.collider != null)
                {
                    IsGrounded = true;
                }
                return;
            }

            IsGrounded = false;
        }
    }
}

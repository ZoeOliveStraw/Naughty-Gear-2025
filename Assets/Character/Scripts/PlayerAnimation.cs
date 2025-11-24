using UnityEngine;

namespace Character.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerRaycasts playerRaycasts;

        // Update is called once per frame
        void Update()
        {
            Vector3 flatMomentum = characterController.velocity;
            flatMomentum.y = 0;
            animator.SetFloat("MoveSpeed", flatMomentum.magnitude);
            animator.SetBool("IsGrounded", playerRaycasts.IsGrounded);
        }
    }
}

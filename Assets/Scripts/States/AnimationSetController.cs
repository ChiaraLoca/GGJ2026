using UnityEngine;

namespace GGJ26.StateMachine
{
    public class AnimationSetController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] public Animator animator;

        [Header("Animation Sets")]
        [SerializeField] private AnimatorOverrideController[] animationSets;

        [Header("Debug")]
        [SerializeField] private int currentSetIndex = -1;

        private void Awake()
        {
            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            if (animator == null)
                Debug.LogError("AnimationSetController: Animator non trovato");
        }

        public void SetAnimationSet(int index)
        {
            if (animationSets == null || animationSets.Length == 0)
            {
                Debug.LogError("AnimationSetController: nessun Animation Set assegnato");
                return;
            }

            if (index < 0 || index >= animationSets.Length)
            {
                Debug.LogError($"AnimationSetController: index {index} fuori range");
                return;
            }

            if (currentSetIndex == index)
                return;

            animator.runtimeAnimatorController = animationSets[index];
            currentSetIndex = index;
        }

        public void SetAnimationSetByName(string setName)
        {
            for (int i = 0; i < animationSets.Length; i++)
            {
                if (animationSets[i].name == setName)
                {
                    SetAnimationSet(i);
                    return;
                }
            }

            Debug.LogWarning($"AnimationSetController: set '{setName}' non trovato");
        }

        public int GetCurrentSetIndex()
        {
            return currentSetIndex;
        }
    }
}

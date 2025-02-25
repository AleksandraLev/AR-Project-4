using UnityEngine;

public class AnimationControllerChest : MonoBehaviour
{
    private Animator animator;
    private int animationIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) // Для мобильных
        {
            animationIndex = (animationIndex + 1) % 2; // Переключение между 0 и 1
            animator.SetInteger("AnimationIndex", animationIndex);
        }
    }
}

using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] ragdollRigidbodies;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

  
    public void EnableRagdoll()
    {
        if (animator != null) animator.enabled = false;

        
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        
        Invoke("MakeKinematic", 5f);
    }

    void MakeKinematic()
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
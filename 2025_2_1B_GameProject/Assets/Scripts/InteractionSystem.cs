using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("interaction setting")]
    public float interactionRange = 2.0f;
    public LayerMask interactionLayermask = 1;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI setting")]
    public Text interactionText;
    public GameObject interactionUI;

    private Transform playerTransform;
    private InteractableObject currentInteractable;

    void Start()
    {
        playerTransform = transform;
        HideInteractionUI();
    }

    void Update()
    {
       CheckForInteractibles();
        HandleInteractionInput();
    }

    void HandleInteractionInput()
    {
        if(currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    void ShowInteractionUI(string text)
    {
        if(interactionUI != null)
        {
            interactionUI.SetActive(true);
        }

        if(interactionText != null)
        {
            interactionText.text = text;
        }
    }

    void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void CheckForInteractibles()
    {
        Vector3 checkPosition = playerTransform.position + playerTransform.forward * (interactionRange * 0.5f);

        Collider[] hitColliders=Physics.OverlapSphere(checkPosition, interactionRange,interactionLayermask);

        InteractableObject closestInteractible = null;
        float closestDistance=float.MaxValue;

        foreach(Collider collider in hitColliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if(interactable != null)
            {
                float distance=Vector3.Distance(playerTransform.position,collider.transform.position);

                Vector3 diirectionToObject=(collider.transform.position - playerTransform.position).normalized;
                float angle = Vector3.Angle(playerTransform.forward, diirectionToObject);

                if (angle < 90f && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractible = interactable;
                }
            }
        }

        if (closestInteractible != currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnPlayerExit();
            }

            currentInteractable = closestInteractible;

            if (currentInteractable != null)
            {
                currentInteractable.OnPlayerEnter();
                ShowInteractionUI(currentInteractable.GetInteractionText());
            }
            else
            {
                HideInteractionUI();
            }
        }
    }
}

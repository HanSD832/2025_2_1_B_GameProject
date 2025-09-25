using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("상호작용정보")]
    public string objectName = "아이템";
    public string interactionText = "[E] 상호작용";
    public InteractionType interactionType = InteractionType.Item;

    [Header("하이라이트 설정")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    public Renderer objectRenderer;
    private Color originalColor;
    private bool isHilighted = false;

    public enum InteractionType
    {
        Item,
        Machine,
        Building,
        NPC,
        Collectible
    }

    protected virtual void HighlightObject()
    {
        if (objectRenderer != null && !isHilighted)
        {
            objectRenderer.material.color = highlightColor;
            objectRenderer.material.SetFloat("_Emission", highlightIntensity);
            isHilighted = true;
        }
    }

    protected virtual void RemoveHighlight()
    {
        if (objectRenderer != null && isHilighted)
        {
            objectRenderer.material.color = originalColor;
            objectRenderer.material.SetFloat("_Emission", 0f);
            isHilighted = false;
        }
    }

    protected virtual void CollectItem()
    {
        Destroy(gameObject);
    }

    protected virtual void OperateMachine()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.green;
        }
    }

    protected virtual void AccessBuilding()
    {
        transform.Rotate(Vector3.up * 90f);
    }

    protected virtual void TalkToNPC()
    {
        Debug.Log($"{objectName}와 대화를 시작합니다.");
    }

    public virtual void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Item:
                CollectItem(); break;
            case InteractionType.Machine:
                OperateMachine(); break;
            case InteractionType.Building:
                AccessBuilding(); break;
            case InteractionType.Collectible:
                CollectItem(); break;
            case InteractionType.NPC:
                TalkToNPC(); break;
        }
    }

    public string GetInteractionText()
    {
        return interactionText;
    }

    protected virtual void Start()
    {
        objectRenderer=GetComponent<Renderer>();
        if(objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        gameObject.layer = 8;
    }

    public virtual void OnPlayerEnter()
    {
        Debug.Log($"{objectName} 감지됨");
        HighlightObject();
    }

    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objectName}] 범위에서 벗어남");
        RemoveHighlight();
    }
}
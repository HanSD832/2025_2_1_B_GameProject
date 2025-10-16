using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QusetGiverNPC : InteractableObject
{
    [Header("NPC Quest settings")]
    public QuestData questToGive;
    public string npcName = "NPC";
    public string questStartMessage = "새로운 퀘스트가 있습니다.";
    public string noQuestMessage = "퀘스트가 없습니다.";
    public string questAlreadyActive = "이미 진행중인 퀘스트가 있습니다.";

    private QuestManager QuestManager;
    void Start()
    {
        base.Start();

        QuestManager = FindObjectOfType<QuestManager>();
        if (QuestManager == null)
        {
            Debug.LogError("Questmanager가 없습니다.");
        }

        interactionText = "[E]" + npcName + "와 대화하기";
    }

    public override void Interact()
    {
        base.Interact();
        QuestManager.StartQuest(questToGive);
    }


}

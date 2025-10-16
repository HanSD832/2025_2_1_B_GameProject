using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QusetGiverNPC : InteractableObject
{
    [Header("NPC Quest settings")]
    public QuestData questToGive;
    public string npcName = "NPC";
    public string questStartMessage = "���ο� ����Ʈ�� �ֽ��ϴ�.";
    public string noQuestMessage = "����Ʈ�� �����ϴ�.";
    public string questAlreadyActive = "�̹� �������� ����Ʈ�� �ֽ��ϴ�.";

    private QuestManager QuestManager;
    void Start()
    {
        base.Start();

        QuestManager = FindObjectOfType<QuestManager>();
        if (QuestManager == null)
        {
            Debug.LogError("Questmanager�� �����ϴ�.");
        }

        interactionText = "[E]" + npcName + "�� ��ȭ�ϱ�";
    }

    public override void Interact()
    {
        base.Interact();
        QuestManager.StartQuest(questToGive);
    }


}

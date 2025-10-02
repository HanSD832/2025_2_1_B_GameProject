using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string questTitle = "���ο� ����Ʈ";

    [TextArea(2, 4)]
    public string description = "����Ʈ ������ �Է��ϼ���";

    [Header("����Ʈ ����")]
    public QuestType questType;
    public int targetAmount = 1;

    [Header("��� ����Ʈ��(delivery)")]
    public Vector3 deliveryPosition;
    public float deliveryRadius = 3f;

    [Header("����/��ȣ�ۿ� ����Ʈ��")]
    public string targetTag = "";

    [Header("����")]
    public int experienceReward = 100;
    public string rewardeMessage = "����Ʈ �Ϸ�";

    [Header("����Ʈ ����")]
    public QuestData nextQuest;

    [System.NonSerialized] public int currentProgress = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompledted = false;

    public void Initialize()
    {
        currentProgress = 0;
        isActive = false;
        isCompledted = false;
    }

    public bool IsComplete()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return currentProgress >= 1;
            case QuestType.Collect:
            case QuestType.Interact:
                return currentProgress >= targetAmount;

            default:
                return false;
        }
    }

    public float GetProgressPercentage()
    {
        if (targetAmount <= 0) return 0;
        return Mathf.Clamp01((float)currentProgress / targetAmount);
    }

    public string GetProgressText()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompledted ? "��� �Ϸ�" : "�������� �̵��ϼ���";
            case QuestType.Collect:
            case QuestType.Interact:
                return$"{currentProgress}/{targetAmount}";
            default:
                return "";
        }
    }
}

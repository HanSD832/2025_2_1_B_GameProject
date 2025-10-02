using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("UI 요소들")]
    public GameObject questUI;
    public Text questTitleText;
    public Text questDescriptionText;
    public Text questProgressText;
    public Button completeButton;

    [Header("퀘스트 목록")]
    public QuestData[] availableQuests;

    private QuestData currentQuest;
    private int currentQuestIndex = 0;

    void UpdateQuestUI()
    {
        if (currentQuest == null) return;

        if (questTitleText != null)
        {
            questTitleText.text = currentQuest.questTitle;
        }

        if (questDescriptionText != null)
        {
            questDescriptionText.text = currentQuest.description;
        }

        if (questProgressText != null)
        {
            questProgressText.text = currentQuest.GetProgressText();
        }
    }

    public void StartQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
        currentQuest.Initialize();
        currentQuest.isActive = true;

        Debug.Log("퀘스트 시작 : " + questTitleText);
        UpdateQuestUI();
        if (questUI != null)
        {
            questUI.SetActive(true);
        }

    }

    void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuest.deliveryPosition);

        if (distance <= currentQuest.deliveryRadius)
        {
            if (currentQuest.currentProgress == 0)
            {
                currentQuest.currentProgress = 1;
            }
        }
        else
        {
            currentQuest.currentProgress = 0;
        }
    }

    public void AddCollectProgress(string itemTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Interact && currentQuest.targetTag == itemTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("아이템 수집" + itemTag);
        }
    }
    
    public void AddInteractProgress(string objectTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Interact && currentQuest.targetTag == objectTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("상호작용 완료" + objectTag);
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest != null || !currentQuest.isCompledted) return;

        Debug.Log("퀘스트 완료" + currentQuest.rewardeMessage);

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(false);
        }

        currentQuestIndex++;
        if (currentQuestIndex < availableQuests.Length)
        {
            StartQuest(availableQuests[currentQuestIndex]);
        }
        else
        {
            currentQuest = null;
            if (questUI != null)
            {
                questUI.gameObject.SetActive(false);
            }
        }
    }

    void CheckQuestProgress()
    {
        if (currentQuest.questType == QuestType.Delivery)
        {
            CheckDeliveryProgress();
        }

        if (currentQuest.IsComplete() && !currentQuest.isCompledted)
        {
            currentQuest.isCompledted = true;
        }

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (availableQuests.Length > 0)
        {
            StartQuest(availableQuests[0]);
        }

        if (completeButton != null)
        {
            completeButton.onClick.AddListener(CompleteCurrentQuest);
        }
    }

    void Update()
    {
        if (currentQuest != null && currentQuest.isActive)
        {
            CheckQuestProgress();
            UpdateQuestUI();
        }
    }
}

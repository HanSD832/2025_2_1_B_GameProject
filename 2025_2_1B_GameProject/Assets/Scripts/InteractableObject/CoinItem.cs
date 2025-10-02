using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("동전 설정")]
    public int coinValue = 10;
    public string questTag = "Coin";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        objectName = "동전";
        interactionText = "[E] 동전 획득";
        interactionType = InteractionType.Item;
    }

    protected override void CollectItem()
    {
        if(QuestManager.instance != null)
        {
            QuestManager.instance.AddCollectProgress(questTag);
        }
        Destroy(gameObject);
    }
}

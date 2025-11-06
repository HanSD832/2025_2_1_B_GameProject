using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("µ¿Àü ¼³Á¤")]
    public int coinValue = 10;
    public string questTag = "Coin";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        objectName = "µ¿Àü";
        interactionText = "[E] µ¿Àü È¹µæ";
        interactionType = InteractionType.Item;
    }

    protected override void CollectItem()
    {
        if(QuestManager.instance != null)
        {
            QuestManager.instance.AddCollectProgress(questTag);
        }

        AchievementManager.instance?.UpdateProgress(AchievementType.CollectCoins, coinValue);

        Destroy(gameObject);
    }
}

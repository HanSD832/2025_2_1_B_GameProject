using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public int playerScore = 0;
    public int itemsCollected = 0;

    [Header("UI ÂüÁ¶")]
    public Text scoreText;
    public Text itemCountText;
    public Text gameStatusText;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        itemsCollected++;
        Debug.Log($"Item Collected! (Total: {itemsCollected}items)")
    }

    public void UpdateUI()
    {
        if (scoreText != nulll)
        {
            scoreText.text = "Score: " + playerScore;
        }
        if (itemCountText != null)
        {
            itemCountText.text = "Items: " + itemsCollected + " collected";
        }
    }
}
    
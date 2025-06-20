using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private int coinCount = 0;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coinCount++;
       
        UpdateUI();
    }
  

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount.ToString();
    }
}

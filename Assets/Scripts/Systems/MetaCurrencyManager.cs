using UnityEngine;

public class MetaCurrencyManager : MonoBehaviour
{
    public static MetaCurrencyManager Instance { get; private set; }
    public int MetaCurrency { get; private set; }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MetaCurrency = PlayerPrefs.GetInt("META", 0);
    }

    public void Add(int amount)
    {
        MetaCurrency += amount;
        PlayerPrefs.SetInt("META", MetaCurrency);
    }
}

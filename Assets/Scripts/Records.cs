using System.Threading;
using TMPro;
using UnityEngine;

public class Records : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordLabel;
    void Start()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (PlayerPrefs.HasKey("record"))
            recordLabel.text = $"Рекорд: {PlayerPrefs.GetInt("record")}       x {PlayerPrefs.GetFloat("mult"):0.00}";
    }
}

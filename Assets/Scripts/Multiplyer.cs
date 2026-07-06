using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class Multiplyer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private MolesProvider molesProvider;
    private float scale = 1f;
    private float startInterval;

    private void Start()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        startInterval = molesProvider.gettingOutInterval;
    }

    public void StartHotMultiplier()
    {
        StartCoroutine(HotTimer());
    }

    public void StartMultiplier()
    {
        StartCoroutine(BasicTimer());
    }

    private IEnumerator HotTimer()
    {
        scale += 6.99f;
        while (scale < 29.99f)
        {
            yield return new WaitForSeconds(0.3f);
            scale += 0.01f;
            molesProvider.gettingOutInterval = startInterval / scale;
            label.text = $"x {scale:0.00}";
            if (scale >= PlayerPrefs.GetFloat("mult"))
                PlayerPrefs.SetFloat("mult", scale);
        }
        label.text = "x 30.00 max";
    }

    private IEnumerator BasicTimer()
    {
        while (scale < 19.99f)
        {
            yield return new WaitForSeconds(0.5f);
            scale += 0.01f;
            molesProvider.gettingOutInterval = startInterval / scale;
            label.text = $"x {scale:0.00}";
            if (scale >= PlayerPrefs.GetFloat("mult"))
                PlayerPrefs.SetFloat("mult", scale);
        }
        label.text = "x 20.00 max";
    }
}

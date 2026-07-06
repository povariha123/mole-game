using UnityEngine;

public class Extra : MonoBehaviour
{
    [Range(0.0f, 5f)]
    [SerializeField] private float time = 0.7f;
    [SerializeField] private float scale = 1.1f;
    

    private void Start()
    {
        if (!(PlayerPrefs.HasKey("extra") && PlayerPrefs.GetInt("extra") == 1))
            gameObject.SetActive(false);
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), scale, time).setLoopPingPong();
    }
}

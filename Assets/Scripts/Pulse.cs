using UnityEngine;

public class Pulse : MonoBehaviour
{
    [Range(0.0f, 5f)]
    [SerializeField] private float time = 0.7f;
    [SerializeField] private float scale = 1.1f;

    private void Start()
    {
        gameObject.LeanScale(new Vector3 (scale, scale, scale), time).setLoopPingPong();
    }
}

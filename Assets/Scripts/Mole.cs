using UnityEngine;
using UnityEngine.Events;

public class Mole : MonoBehaviour
{
    [SerializeField] private ParticleSystem moleParticleSystem;
    private float baseLocalY;
    public UnityEvent hit;
    public bool isOutside;

    private void Start()
    {
        baseLocalY = gameObject.transform.localPosition.y;
    }

    private void Hit()
    {
        gameObject.LeanCancel();
        gameObject.LeanMoveLocalY(baseLocalY - 4f, 0);
        moleParticleSystem.Play();
        isOutside = false;
    }

    public void GetOut()
    {
        gameObject.LeanMoveLocalY(baseLocalY, 0.7f).setEaseInCirc();
        isOutside = true;
    }

    private void OnMouseDown()
    {
        Hit();
        hit.Invoke();
    }
}

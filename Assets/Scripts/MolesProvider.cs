using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MolesProvider : MonoBehaviour
{
    [SerializeField] private Mole[] moles;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject looseSign;
    [SerializeField] private GameObject hideGroup;
    [SerializeField] private AudioSource audioSource;

    public float gettingOutInterval = 2f;   
    private int currentMoles;
    private Mole lastMole;
    private int currentScore = 0;

    private IEnumerator MolesEnumerator()
    {
        yield return new WaitForSeconds(2);
        ToogleColliders(true);

        while (currentMoles < 10)
        {
            int selectedMole = Random.Range(0, moles.Length);
            var sMole = moles[selectedMole];
            if (sMole && !sMole.isOutside)
            {
                sMole.GetOut();
                lastMole = sMole;
                currentMoles++;
                yield return new WaitForSeconds(gettingOutInterval);
            }
            else
            {
                yield return null;
            }
        }

        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        PlayerPrefs.SetInt("extra", 1);

        ToogleColliders(false);

        Vector3 pos = new Vector3(lastMole.transform.position.x, 0, lastMole.transform.position.z - 4f);
        cameraObject.LeanMove(pos, 1f);
        cameraObject.LeanRotate(Vector3.forward, 1f);

        GameObject sign = Instantiate(looseSign, lastMole.transform.parent);
        Vector3 signStartPos = sign.transform.localPosition;

        sign.LeanMoveLocalY(-3, 0);
        sign.LeanMoveLocalY(signStartPos.y, 1.2f).setEaseInBack();
        hideGroup.SetActive(false);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);
    }

    private void ToogleColliders(bool value)
    {
        for (int i = 0; i < moles.Length; i++)
        {
            moles[i].GetComponent<Collider>().enabled = value;
        }
    }

    private void KillMole()
    {
        currentMoles--;
        audioSource.Play();
    }

    private void UpdateScore()
    {
        currentScore++;
        if (currentScore >= PlayerPrefs.GetInt("record"))
        {
            PlayerPrefs.SetInt("record", currentScore);
            YG.YandexGame.NewLeaderboardScores("lb", currentScore);
        }    
        scoreLabel.text = $"СЧЕТ: {currentScore}";
    }

    private void Start()
    {
        ToogleColliders(false);
        foreach (var mole in moles)
        {
            mole.hit.AddListener(UpdateScore);
            mole.hit.AddListener(KillMole);

            float moleY = mole.transform.localPosition.y;
            mole.transform.LeanMoveLocalY(moleY - 4f, 1.72f).setEaseOutBack();
        }
        StartCoroutine(MolesEnumerator());
    }
}

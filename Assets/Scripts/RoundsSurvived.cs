using System.Collections;
using TMPro;
using UnityEngine;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField] private TMP_Text roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }
    
    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.8f);
        while (round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}

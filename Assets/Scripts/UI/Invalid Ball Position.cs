using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InvalidBallPosition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI invalidText;
    [SerializeField] private CueballChecker cueballChecker;
    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        invalidText.text = string.Empty;

        if (cueballChecker)
            cueballChecker.EvtBallOutsideRange.AddListener(OnOutsideRange); 
    }

    private void OnOutsideRange()
    {
        invalidText.text = "Invalid Ball Position";
        invalidText.alpha = 1f; 
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            invalidText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        invalidText.alpha = 0f; 
    }
}

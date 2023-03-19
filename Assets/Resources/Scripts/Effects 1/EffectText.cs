using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class EffectText : MonoBehaviour
{
    public static IEnumerator FloatingTextFadeOut(GameObject objectToFloating, Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        Color32 startColor = startColorA;
        Color32 endColor = endColorA;

        GameObject go = Instantiate(objectToFloating, positionToFloating, Quaternion.identity);
        // go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);
        go.GetComponent<TMP_Text>().text = text;
        go.GetComponent<TMP_Text>().color = startColor;
        float speed = 1.0f;
        float step = speed * Time.deltaTime;
        float t = 0;
        Vector3 vectorTarget = go.transform.position + new Vector3(0, 1, 0);
        while (t < 1)
        {
            go.GetComponent<TMP_Text>().color = Color32.Lerp(startColor, endColor, t);
            go.transform.position = Vector3.MoveTowards(go.transform.position, vectorTarget, step);
            t += Time.deltaTime / 2f;
            yield return null;
        }
        Destroy(go);
    }

    public static IEnumerator CountNumber(Action<string> CallBackText, int numberStart, int numberEnd)
    {
        // int countFps = 1;
        float maxTimeAnimation = 15.0f;
        float timeAnimationCurrent = 0.0f;
        int step = 1;
        int actualCount = numberStart;
        int minorNumber = numberStart;
        int majorNumber = numberEnd;

        // WaitForSeconds w
        if (numberStart > numberEnd)
        {
            minorNumber = numberEnd;
            majorNumber = numberStart;
            step = -step;
        }
        while (actualCount != numberEnd)
        {
            CallBackText("" + actualCount);
            actualCount += step;

            // Max time animation
            if (timeAnimationCurrent >= maxTimeAnimation)
            {
                timeAnimationCurrent = timeAnimationCurrent + Time.deltaTime;
                CallBackText("" + numberEnd);
                yield break;
            }

            // Reduce time for 10 last number
            if (MathF.Abs(actualCount - numberEnd) <= 10 && MathF.Abs(actualCount - numberEnd) > 3)
            {
                yield return new WaitForSeconds(0.1f);
            }

            if (MathF.Abs(actualCount - numberEnd) <= 3)
            {
                yield return new WaitForSeconds(0.25f);
            }

            yield return null;
        }
    }
}

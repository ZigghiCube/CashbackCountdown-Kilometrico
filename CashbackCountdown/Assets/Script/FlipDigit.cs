using UnityEngine;
using TMPro;
using DG.Tweening;

public class FlipDigit : MonoBehaviour
{
    public TextMeshProUGUI digitText;
    private int currentValue = -1;

    public void SetDigit(int value)
    {
        if (value == currentValue) return;
        currentValue = value;

        // Animazione flip
        transform.DORotate(new Vector3(-90, 0, 0), 0.12f).OnComplete(() =>
        {
            digitText.text = value.ToString();
            transform.DORotate(Vector3.zero, 0.12f);
        });
    }
}


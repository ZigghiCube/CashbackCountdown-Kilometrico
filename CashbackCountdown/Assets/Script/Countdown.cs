using UnityEngine;
using TMPro;
using System;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;


    // Imposta il tempo iniziale: 24 ore = 86400 secondi
    private float totalSeconds = 2 * 60 * 60;
    public float OreMattina = 2;
    public float OrePomeriggio = 3;
    public bool start = false;
    public bool canCounter = false;

    void Update()
    {
        if (!canCounter)
        {
            if (System.DateTime.Now.Hour == 10)
            {
                if (System.DateTime.Now.Minute == 30)
                {
                    totalSeconds = OreMattina * 60 * 60;
                    canCounter = true;
                    start = true;
                }
            }
            if (System.DateTime.Now.Hour == 16)
            {
                if (System.DateTime.Now.Minute == 30)
                {
                    totalSeconds = OrePomeriggio * 60 * 60;
                    canCounter = true;
                    start = true;
                }
            }
        }
        else
        {
            if (System.DateTime.Now.Hour == 12 || System.DateTime.Now.Hour == 19)
            {
                if (System.DateTime.Now.Minute == 30)
                {
                    canCounter = false;
                    start = false;
                    countdownText.text = "00 : 00 : 00";
                }
            }
        }


        // Se il timer non è partito, non fare nulla
        if (!start) return;

        if (totalSeconds > 0)
        {
            totalSeconds -= Time.deltaTime;

            int hours = Mathf.FloorToInt(totalSeconds / 3600);
            int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
            int seconds = Mathf.FloorToInt(totalSeconds % 60);

            countdownText.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
        else
        {
            totalSeconds = 0;
            countdownText.text = "00 : 00 : 00";
            start = false; // Ferma il timer
        }
    }
}

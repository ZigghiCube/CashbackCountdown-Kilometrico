using UnityEngine;

public class SplitFlapCountdown : MonoBehaviour
{
    public FlipDigit H1;
    public FlipDigit H2;
    public FlipDigit M1;
    public FlipDigit M2;
    public FlipDigit S1;
    public FlipDigit S2;

    private float totalSeconds;
    public float OreMattina = 2;
    public float OrePomeriggio = 3;
    public bool start = false;
    public bool canCounter = false;

    public GameObject FrameOre;
    public GameObject FrameMinuti;
    public GameObject FrameSecondi;
    public GameObject OvergroundImage;
    private bool jot = true;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (jot)
            {
                jot = false;
                FrameOre.SetActive(true);
                FrameMinuti.SetActive(true);
                FrameSecondi.SetActive(true);
                OvergroundImage.SetActive(false);
            }
            else
            {
                jot = true;
                FrameOre.SetActive(false);
                FrameMinuti.SetActive(false);
                FrameSecondi.SetActive(false);
                OvergroundImage.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            totalSeconds = OreMattina * 60 * 60;
            start = canCounter = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            totalSeconds = OrePomeriggio * 60 * 60;
            start = canCounter = true;
        }
        // --- Attivazione fasce orarie ---
        if (!canCounter)
        {
            if (System.DateTime.Now.Hour == 10 && System.DateTime.Now.Minute == 29 && jot == true)
            {
                jot = false;
                FrameOre.SetActive(true);
                FrameMinuti.SetActive(true);
                FrameSecondi.SetActive(true);
                OvergroundImage.SetActive(false);
            }
            if (System.DateTime.Now.Hour == 16 && System.DateTime.Now.Minute == 29 && jot == true)
            {
                jot = false;
                FrameOre.SetActive(true);
                FrameMinuti.SetActive(true);
                FrameSecondi.SetActive(true);
                OvergroundImage.SetActive(false);
            }

            if (System.DateTime.Now.Hour == 10 && System.DateTime.Now.Minute == 30)
            {
                totalSeconds = OreMattina * 60 * 60;
                start = canCounter = true;
            }

            if (System.DateTime.Now.Hour == 16 && System.DateTime.Now.Minute == 30)
            {
                totalSeconds = OrePomeriggio * 60 * 60;
                start = canCounter = true;
            }
        }
        else
        {
            if ((System.DateTime.Now.Hour == 12 || System.DateTime.Now.Hour == 19) && System.DateTime.Now.Minute == 30 && jot == false)
            {
                start = canCounter = false;
                SetAllToZero();
                                    jot = true;
                FrameOre.SetActive(false);
                FrameMinuti.SetActive(false);
                FrameSecondi.SetActive(false);
                OvergroundImage.SetActive(true);
                return;
            }
        }
                         
        // --- Se timer non attivo, esci ---
        if (!start) return;

        // --- Countdown ---
        totalSeconds -= Time.deltaTime;
        if (totalSeconds < 0) totalSeconds = 0;

        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);

        // ---- Split-Flap ----
        H1.SetDigit(hours / 10);
        H2.SetDigit(hours % 10);
        M1.SetDigit(minutes / 10);
        M2.SetDigit(minutes % 10);
        S1.SetDigit(seconds / 10);
        S2.SetDigit(seconds % 10);
    }

    void SetAllToZero()
    {
        H1.SetDigit(0);
        H2.SetDigit(0);
        M1.SetDigit(0);
        M2.SetDigit(0);
        S1.SetDigit(0);
        S2.SetDigit(0);

    }
}

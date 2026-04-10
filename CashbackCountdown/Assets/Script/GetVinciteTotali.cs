using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GetVinciteTotali : MonoBehaviour
{
    public TextMeshProUGUI VinciteTotaliText;
    public bool isEuro;

    [Header("HTTPs Settings")]
    public string url = ""; 
    public float TimeForRefresh = 60f;
    public float ValorePremio = 20f;


    [Header("Typewriter Settings")]
    public float typingSpeed = 0.05f; // tempo tra una lettera e l’altra

    void Start()
    {
        // refresh continuo ogni 60 sec
        StartCoroutine(AutoRefresh());
    }

    IEnumerator AutoRefresh()
    {
        while (true)
        {
            yield return StartCoroutine(GetWinsFromServer());
            yield return new WaitForSeconds(TimeForRefresh);
        }
    }

    IEnumerator GetWinsFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "?_nocache=" + Time.time);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Errore richiesta: " + www.error);
            yield return StartCoroutine(TypeText("Errore connessione"));
        }
        else
        {
            if (isEuro)
            {
                string valore = www.downloadHandler.text.Trim();
                float quantita = float.Parse(valore);
                float totale = quantita * ValorePremio;
                string message = valore + " euro";
                Debug.Log(valore);
                yield return StartCoroutine(TypeText(message));
            }
            else
            {
                string valore = www.downloadHandler.text.Trim();
                float quantita = float.Parse(valore);
                float totale = quantita * ValorePremio;
                string message = valore + " Km";
                Debug.Log(valore);
                yield return StartCoroutine(TypeText(message));
            }
            

        }
    }

    IEnumerator TypeText(string message)
    {
        VinciteTotaliText.text = ""; // svuota prima
        foreach (char c in message)
        {
            VinciteTotaliText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

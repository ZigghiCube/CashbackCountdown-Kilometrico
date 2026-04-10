using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterLoop : MonoBehaviour
{
    [TextArea(3, 10)]
    [Tooltip("Inserisci qui le frasi da mostrare in sequenza")]
    public string[] messages;

    [Tooltip("Tempo (in secondi) tra una lettera e l’altra")]
    public float typingSpeed = 0.05f;

    [Tooltip("Tempo di attesa prima di iniziare a cancellare")]
    public float holdTime = 1.5f;

    [Tooltip("Tempo (in secondi) tra una lettera e l’altra durante la cancellazione")]
    public float eraseSpeed = 0.03f;

    private TMP_Text textMesh;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        StartCoroutine(TypeLoop());
    }

    IEnumerator TypeLoop()
    {
        int index = 0;

        while (true)
        {
            string message = messages[index];
            yield return StartCoroutine(TypeText(message));
            yield return new WaitForSeconds(holdTime);
            yield return StartCoroutine(EraseText());
            index = (index + 1) % messages.Length; // loop infinito
        }
    }

    IEnumerator TypeText(string message)
    {
        textMesh.text = "";
        foreach (char c in message)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator EraseText()
    {
        while (textMesh.text.Length > 0)
        {
            textMesh.text = textMesh.text.Substring(0, textMesh.text.Length - 1);
            yield return new WaitForSeconds(eraseSpeed);
        }
    }
}

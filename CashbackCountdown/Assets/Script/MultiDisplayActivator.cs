using UnityEngine;

public class MultiDisplayActivator : MonoBehaviour
{
    void Start()
    {
        // Attiva tutti i display connessi (oltre al principale)
        for (int i = 0; i < Display.displays.Length; i++)
        {
            // Display.displays[0] è il display principale (già attivo)
            if (i > 0)
            {
                Display.displays[i].Activate();
                Debug.Log("Activated display: " + i);
            }
        }
    }
}

using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextWaveEffect : MonoBehaviour
{
    [Tooltip("Altezza dell’onda (quanto si muovono le lettere su e giù)")]
    public float amplitude = 5f;      // altezza dell’onda
    [Tooltip("Distanza tra le onde (numero di oscillazioni lungo il testo)")]
    public float frequency = 2f;      // quante onde per secondo
    [Tooltip("Velocità di movimento dell’onda nel tempo")]
    public float speed = 2f;          // velocità di movimento dell’onda

    private TMP_Text textMesh;
    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        textInfo = textMesh.textInfo;

        // Salviamo i vertici originali la prima volta
        if (originalVertices == null || originalVertices.Length < textInfo.meshInfo.Length)
        {
            originalVertices = new Vector3[textInfo.meshInfo.Length][];
        }

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int meshIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[meshIndex].vertices;

            // Inizializza i vertici originali, se serve
            if (originalVertices[meshIndex] == null || originalVertices[meshIndex].Length == 0)
                originalVertices[meshIndex] = vertices.Clone() as Vector3[];

            // Calcola offset sinusoidale in base al tempo e alla posizione del carattere
            float wave = Mathf.Sin(Time.time * speed + i * frequency) * amplitude;

            // Applica l’offset verticale
            Vector3 offset = new Vector3(0, wave, 0);
            vertices[vertexIndex + 0] = originalVertices[meshIndex][vertexIndex + 0] + offset;
            vertices[vertexIndex + 1] = originalVertices[meshIndex][vertexIndex + 1] + offset;
            vertices[vertexIndex + 2] = originalVertices[meshIndex][vertexIndex + 2] + offset;
            vertices[vertexIndex + 3] = originalVertices[meshIndex][vertexIndex + 3] + offset;
        }

        // Applica i cambiamenti al mesh
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textMesh.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

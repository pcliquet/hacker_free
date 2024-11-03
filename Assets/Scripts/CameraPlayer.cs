using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referência para o transform do personagem
    public float smoothing = 0.125f; // Suavidade do movimento da câmera
    public Vector3 offset; // Distância da câmera em relação ao personagem

    private void LateUpdate() {
        // Calcula a posição desejada da câmera com base na posição do personagem e no offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera para que ela siga o personagem suavemente
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }
}


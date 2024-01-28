using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource mousePassAudio;
    public void OnPointerEnter(PointerEventData eventData) {
        mousePassAudio.Play();
    }
}

// MenuBtn.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MenuBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [FormerlySerializedAs("compressClip")]
    [FormerlySerializedAs("_compressClip")]
    [SerializeField]
    private AudioClip clickStartSound;

    [FormerlySerializedAs("uncompressClip")]
    [FormerlySerializedAs("_uncompressClip")]
    [SerializeField]
    private AudioClip clickEndSound;

    [FormerlySerializedAs("_source")]
    [SerializeField]
    private AudioSource source;

    private Vector3 _p;

    void Start()
    {
        _p = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position += new Vector3(0,8,0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = _p;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (clickStartSound) source.PlayOneShot(clickStartSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickEndSound) source.PlayOneShot(clickEndSound);
    }
}

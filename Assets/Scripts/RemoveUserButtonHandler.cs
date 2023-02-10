using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveUserButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public static event Action<int> UserRemove;

    public void OnPointerClick(PointerEventData eventData)
    {
        UserRemove?.Invoke(transform.parent.GetSiblingIndex());
    }
}

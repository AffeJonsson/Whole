using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> handItems;
    public GameObject CurrentHandItem { get; private set; }

    public GameObject ChangeItem(int index)
    {
        if (CurrentHandItem != null)
        {
            CurrentHandItem.SetActive(false);
        }
        if (index < handItems.Count)
        {
            CurrentHandItem = handItems[index];
            CurrentHandItem.SetActive(true);
            return CurrentHandItem;
        }
        return null;
    }

}

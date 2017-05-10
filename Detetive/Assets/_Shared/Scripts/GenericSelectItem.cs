using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericSelectItem<T> : MonoBehaviour {
    
    /// <summary>
    /// Selected item
    /// </summary>
    private T m_item;

    //Callback to return clicked item
    private System.Action<T> m_clickCallback;

    public T GetItem { get { return m_item; }  }

    /// <summary>
    /// Add a selected item
    /// </summary>
    /// <param name="Item"></param>
    /// <param name="callback"></param>
    public virtual void UpdateItem(T Item, System.Action<T> callback)
    {
        m_clickCallback = callback;
        m_item = Item;
    }

    public void OnClick()
    {
        if (m_clickCallback != null)
            m_clickCallback(m_item);
    }


    public virtual void SelectItem()
    {
        //Dummy =X
    }

    public virtual void UnSelectItem()
    {
        //Dummy =X
    }

}

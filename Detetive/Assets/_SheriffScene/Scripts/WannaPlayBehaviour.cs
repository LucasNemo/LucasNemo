using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WannaPlayBehaviour : MonoBehaviour {
    
	public void WannaPlay(bool wannaplay)
    {
        Manager.Instance.SheriffWannaPlay = wannaplay;
        FindObjectOfType<SheriffController>().OnSelectWannaPlay(wannaplay);
    }

}

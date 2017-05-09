using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WannaPlayBehaviour : MonoBehaviour {

    private bool m_sheriffWannaPlay;
    public bool SheriffWannaPlay { get { return m_sheriffWannaPlay; } }

	public void WannaPlay(bool wannaplay)
    {
        m_sheriffWannaPlay = wannaplay;
        FindObjectOfType<SheriffController>().OnSelectWannaPlay(wannaplay);
    }

}

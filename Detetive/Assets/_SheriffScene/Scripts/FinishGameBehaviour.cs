using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameBehaviour : MonoBehaviour
{

    [SerializeField]
    private Text m_finishText;
    private PlayerHunch m_playerHunch;
    void Start()
    {
        m_playerHunch = FindObjectOfType<SheriffController>().GetBestHunch();
        if (m_playerHunch != null)
            FinishGame();
        else
            m_finishText.text = "Ninguém acertou =(";
    }

    private void FinishGame()
    {
        m_finishText.text = "Quem acertou foi o " + m_playerHunch.P +
            "O crime ocorreu no " + m_playerHunch.MH.HR.P +
            "Utilizando a arma" + m_playerHunch.MH.HR.W +
            "e que matou foi " + m_playerHunch.MH.HC;
    }

}

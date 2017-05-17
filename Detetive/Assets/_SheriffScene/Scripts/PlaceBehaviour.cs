﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_readQr, m_addingPlace, m_mainPlaces, m_header;

    //Input name field
    [SerializeField]
    private InputField m_inputField;
    //Place added by player
    private List<Place> m_places;
    //Save last place readed
    private Place m_lastPlace;
    private ReadQRCodeBehaviour m_readQrCodeBehaviour;
    private bool m_hostAlreadyChoosed;

    /// <summary>
    /// Get added places
    /// </summary>
    public List<Place> GetPlaces { get { return m_places; } }

    void Start()
    {
        m_places = new List<Place>();
        m_readQrCodeBehaviour = FindObjectOfType<ReadQRCodeBehaviour>();
    }
    
    public void OpenQrCodeReader()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        m_mainPlaces.SetActive(false);
        m_header.SetActive(false);
        m_readQrCodeBehaviour.ReadQrCode((result) =>
        {
            SceneManager.UnloadScene(Manager.Instance.QRCODE_SCENE);

            int t = 0;

            if (int.TryParse(result, out t))
            {
                Enums.Places place = (Enums.Places)System.Enum.Parse(typeof(Enums.Places), result);
                m_lastPlace = Manager.Instance.Places.FirstOrDefault(x => ((Enums.Places)x.MP) == place);

                if (m_lastPlace != null)
                {
                    m_inputField.text = m_lastPlace.N;
                    ActivePlace();
                }
                else
                {
                    ErrorMessage();
                }
            }
            else
            {
                ErrorMessage();
            }

            m_mainPlaces.SetActive(true);
            m_header.SetActive(true);

        }, false, Manager.Instance.QR_READ_PLACE);
    }

    private void ErrorMessage()
    {
        GenericModal.Instance.OpenAlertMode(Manager.Instance.ON_READ_PLACE_WRONG, Manager.Instance.WARNING_BUTTON, () =>
        {
            ActiveReadQr();
        });
    }

    private void ActivePlace()
    {
        m_readQr.SetActive(false);
        m_addingPlace.SetActive(true);
    }

    private void ActiveReadQr()
    {
        m_addingPlace.SetActive(false);
        m_readQr.SetActive(true);
    }

    public void OnAddPlaceClick()
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);
        if (m_lastPlace != null)
        {
            bool isHost = false;
            
            if (!m_places.Any(x => x.N.Equals(m_inputField.text) || x.MP == m_lastPlace.MP))
            {
                if (!m_hostAlreadyChoosed)
                {
                    GenericModal.Instance.OpenModal(Manager.Instance.IS_HOST_MODAL, "Não", "Sim", ()=>
                    {
                        isHost = false;
                        m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, isHost));
                        m_inputField.text = string.Empty;
                        m_lastPlace = null;
                    },
                    () =>
                    {
                        isHost = true;
                        m_hostAlreadyChoosed = true;
                        m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, isHost));
                        m_inputField.text = string.Empty;
                        m_lastPlace = null;
                    });
                }
                else
                {
                    isHost = false;
                    m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, isHost));
                    m_inputField.text = string.Empty;
                    m_lastPlace = null;
                }
            }
            else
            {
                //Error modal place already added
                GenericModal.Instance.OpenAlertMode(Manager.Instance.PLACE_ALREADY_ADDED, "Tudo bem!", () =>
                {
                    m_inputField.text = string.Empty;
                    m_lastPlace = null;
                });
            }
        }

        ActiveReadQr();
    }

    public void DisablePlace()
    {
        m_mainPlaces.SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_readQr, m_addingPlace;

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
        //TODO - PLEASE REMOVE THE HARDCODED!!!!!!!
        m_readQrCodeBehaviour.ReadQrCode((result) =>
        {
            Enums.Places place = (Enums.Places)System.Enum.Parse(typeof(Enums.Places), result);
            m_lastPlace = Manager.Instance.Places.FirstOrDefault(x => ((Enums.Places)x.MP) == place);
            m_inputField.text = m_lastPlace.N;
            ActivePlace();
        }, false, Manager.Instance.QR_READ_PLACE);
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
        if (m_lastPlace != null)
        {
            if (!m_places.Any(x => x.N.Equals(m_inputField.text) || x.MP == m_lastPlace.MP))
            {
                bool isHost = false;

                if(!m_hostAlreadyChoosed)
                {
                    GenericModal.Instance.OpenModal(Manager.Instance.IS_HOST_MODAL, "Não", "Sim", () =>
                    {
                        GenericModal.Instance.CloseModal();
                    },
                    () =>
                    {
                        isHost = true;
                        m_hostAlreadyChoosed = true;
                    });
                }
                
                m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, isHost));

                m_inputField.text = string.Empty;
                m_lastPlace = null;
            }
            else
            {
                //Error modal
            }
        }
    }

}

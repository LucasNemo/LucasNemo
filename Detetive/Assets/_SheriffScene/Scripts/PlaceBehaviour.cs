using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlaceBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_readQr, m_mainPlaces, m_header;

    //Place added by player
    private List<Place> m_places;
    //Save last place readed
    private Place m_lastPlace;
    private ReadQRCodeBehaviour m_readQrCodeBehaviour;

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

            if (result != Manager.Instance.RESULT_ERROR_BACK)
            {

                int t = 0;

                if (int.TryParse(result, out t))
                {
                    Enums.Places place = (Enums.Places)System.Enum.Parse(typeof(Enums.Places), result);
                    m_lastPlace = Manager.Instance.Places.FirstOrDefault(x => ((Enums.Places)x.MP) == place);

                    if (m_lastPlace != null)
                    {
                        ActivePlace();
                        AddPoliceStation(m_lastPlace);
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
            }
            else
            {
                m_mainPlaces.SetActive(true);
                m_header.SetActive(true);
            }

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

    }

    private void ActiveReadQr()
    {
        m_readQr.SetActive(true);
    }

    public void AddPoliceStation(Place place)
    {
        AudioController.Instance.Play(Manager.Instance.SOUND_CLICK, AudioController.SoundType.SoundEffect2D, 1f, false, true);

        GenericModal.Instance.OpenModal(string.Format(Manager.Instance.IS_HOST_MODAL, Manager.Instance.PlacesNames[(Enums.Places) place.MP].ToUpper()), "Não", "Sim", () =>
        {
            //m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, isHost));
        },
        () =>
        {
            m_places.Add(new Place((Enums.Places)m_lastPlace.MP, true));
            FindObjectOfType<SheriffController>().OnFinishAddPlaces();
        });

        ActiveReadQr();
    }

    public void DisablePlace()
    {
        m_mainPlaces.SetActive(false);
    }

}

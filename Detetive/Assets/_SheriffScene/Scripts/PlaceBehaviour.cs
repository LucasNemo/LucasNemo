using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlaceBehaviour : MonoBehaviour
{
    //Input name field
    [SerializeField]
    private InputField m_inputField;
    //Place added by player
    private List<Place> m_places;
    //Save last place readed
    private Place m_lastPlace;

    [SerializeField]
    private Text m_errormessage;

    [SerializeField]
    private Toggle m_toggle;

    /// <summary>
    /// Get added places
    /// </summary>
    public List<Place> GetPlaces { get { return m_places; } }

    void Start()
    {
        m_places = new List<Place>();
    }

    private void FixedUpdate()
    {
        m_toggle.gameObject.SetActive( !m_places.Any(x => x.IH) );
    }

    public void OpenQrCodeReader()
    {
        FindObjectOfType<ReadQRCodeBehaviour>().ReadQrCode((result) =>
        {
            Enums.Places place = (Enums.Places)System.Enum.Parse(typeof(Enums.Places), result);
            m_lastPlace = Manager.Instance.Places.FirstOrDefault(x => ((Enums.Places)  x.MP) == place);
            m_inputField.text = m_lastPlace.N;
        },false);
    }

    public void OnAddPlaceClick()
    {
        if (m_lastPlace != null)
        {
            if (!m_places.Any(x => x.N.Equals(m_inputField.text) || x.MP == m_lastPlace.MP))
            {
                m_errormessage.gameObject.SetActive(false);
                m_places.Add(new Place(m_inputField.text, (Enums.Places)m_lastPlace.MP, m_toggle.isOn));

                m_inputField.text = string.Empty;
                m_lastPlace = null;
            }
            else
                m_errormessage.gameObject.SetActive(true);
        }
    }

}

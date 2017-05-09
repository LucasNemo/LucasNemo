using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunchBehaviour : MonoBehaviour {

    //Add hunch by users
    private List<PlayerHunch> m_hunchs;

    /// <summary>
    /// Get all hunchs
    /// </summary>
    public List<PlayerHunch> GetHunchs { get { return m_hunchs; } }

	void Start () {
        m_hunchs = new List<PlayerHunch>();
	}
	
    /// <summary>
    /// Method to add a new hunch
    /// </summary>
    public void OnAddHunch()
    {
        //TODO - PLEASE REMOVE THE HARDCODED!!!!!
        FindObjectOfType<ReadQRCodeBehaviour>().ReadQrCode((result) =>
        {
            if(!string.IsNullOrEmpty(result))
            {
                var hunch = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerHunch>(result);
                m_hunchs.Add(hunch);
            }
            else
            {
                print("QrCode is null");
            }
        }, true, "");
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    public enum SupportedLanguages
    {
        Portuguese, English
    }

    public SupportedLanguages CurrentLanguage;

    public static LocalizationManager instance;

    private Dictionary<string, Dictionary<SupportedLanguages, string>> m_texts;

    void Start()
    {
        instance = this;
        ChangeLanguage(SupportedLanguages.Portuguese);
    }


    public void ChangeLanguage(SupportedLanguages newLanguage)
    {
        CurrentLanguage = newLanguage;

        LoadTexts(CurrentLanguage);
    }

    private void LoadTexts(SupportedLanguages newLanguage)
    {
        CurrentLanguage = newLanguage;
        m_texts =  Newtonsoft.Json.JsonConvert.DeserializeObject <Dictionary<string, Dictionary<SupportedLanguages, string>>>(Resources.Load<TextAsset>("Localization").text);
    }

    public string GetText(string key)
    {
        if (m_texts == null || !m_texts.ContainsKey(key))
        {
            Debug.LogError( string.Format("Fail to load key {0} in language {1}", key, CurrentLanguage));
            return "";
        }

        return m_texts[key][CurrentLanguage];
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0,0, 100,100), "Create text file"))
    //    {
    //        m_texts = new Dictionary<string, Dictionary<SupportedLanguages, string>>();

    //        m_texts.Add("InstructionText", new Dictionary<SupportedLanguages, string>
    //        {
    //            { SupportedLanguages.Portuguese, "PARA JOGAR SIGA OS SEGUINTES PASSOS:" },
    //            { SupportedLanguages.English, "FOLLOW THE INSTRUCTIONS:" }
    //        });

    //        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(m_texts));
    //    }
    //}


}

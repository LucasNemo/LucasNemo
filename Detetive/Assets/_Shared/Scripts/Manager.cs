

using System;
using System.Collections.Generic;
using UnityEngine;

public class Manager
{
    #region Singleton
    private static Manager m_instance;

    public static Manager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new Manager();

            return m_instance;
        }
    }
    #endregion

    #region Game Settings
    public int Min_Weapons = 3;
    public int Min_Weapons_Tips = 3;
    public int Min_Character_Tips = 3;
    #endregion

    #region Constants
    
    private const string SAVE_NAME = "GameInfo";

    #endregion

    /// <summary>
    /// All characters in game
    /// </summary>
    public List<Character> Characters { get; private set; }
    /// <summary>
    /// All places in game
    /// </summary>
    public List<Place> Places { get; private set; }
    /// <summary>
    /// All weapons in game
    /// </summary>
    public List<Weapon> Weapons { get; private set; }
    /// <summary>
    /// All character tips referenced in game
    /// </summary>
    public List<CharacterTip> CharacterTips { get; set; }
    /// <summary>
    /// All weapon tips referenced in game
    /// </summary>
    public List<WeaponTip> WeaponsTips { get; set; }
    /// <summary>
    /// All scenario of player
    /// </summary>
    public GameInformation MyGameInformation { get; set; }
    /// <summary>
    /// Player already start or still are on game
    /// </summary>
    public bool isOnGame { get { return MyGameInformation != null ? true : false; } }

    /// <summary>
    /// Sheriff wanna play detective too
    /// </summary>
    public bool SheriffWannaPlay { get; set; }

    public Manager()
    {
        IntializeCards();
        LoadGameInformation();
    }
    
    private void IntializeCards()
    {
        InitializeCharacters();
        InitializePlaces();
        InitializeWeapons();
        //Tips
        InitializeCharacterTips();
        InitializeWeaponsTips();
    }

    #region Persistence 
    
    private void LoadGameInformation()
    {
        if (FileManager.CheckIfFileExistsInPath(Application.persistentDataPath, SAVE_NAME))
        {
            MyGameInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<GameInformation>
                                (FileManager.LoadData(Application.persistentDataPath, SAVE_NAME));
        }
    }
    
    public void SaveGameInformation()
    {
        if (MyGameInformation != null)
        {
            FileManager.SaveData(Newtonsoft.Json.JsonConvert.SerializeObject(MyGameInformation),
                                                    Application.persistentDataPath, SAVE_NAME);
        }
        else
        {
            Debug.LogWarning("Gameinformation is null, initialize before save");
        }
    }

    #endregion
    
    #region Cards

    public Dictionary<Enums.Characters, string> CharactersName = new Dictionary<Enums.Characters, string>
    {
        {Enums.Characters.Advogado_Sr_Marinho, "ADVOGADO SR. MARINHO" },
        {Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, "CHEF DE COZINHA TONY GOURMET" },
        {Enums.Characters.Coveiro_Sergio_Soturno, "COVEIRO SÉRGIO SOTURNO" },
        {Enums.Characters.Dancarina_Srta_Rosa, "DANÇARINA SRTA. ROSA" },
        {Enums.Characters.Florista_Dona_Branca, "FLORISTA DONA BRANCA" },
        {Enums.Characters.Medica_Dona_Violeta, "MÉDICA DONA VIOLETA" },
        {Enums.Characters.Mordomo_James, "MORDOMO JAMES" },
        {Enums.Characters.Sargento_Bicode, "SARGENTO BIGODE" }
    };

 

    private void InitializeCharacters()
    {
        Characters = new List<Character>();
        Characters.Add(new Character( Enums.Characters.Advogado_Sr_Marinho.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Chef_de_Cozinha_Tony_Gourmet.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Coveiro_Sergio_Soturno.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Dancarina_Srta_Rosa.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Florista_Dona_Branca.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Medica_Dona_Violeta.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Mordomo_James.GetHashCode()));
        Characters.Add(new Character( Enums.Characters.Sargento_Bicode.GetHashCode()));
    }

    private void InitializePlaces()
    {
        Places = new List<Place>();
        Places.Add(new Place("Banco", Enums.Places.Banco, false));
        Places.Add(new Place("Boate", Enums.Places.Boate, false));
        Places.Add(new Place("Cemitério", Enums.Places.Cemiterio, false));
        Places.Add(new Place("Estação de Trem", Enums.Places.Estacao_de_Trem, false));
        Places.Add(new Place("Floricultura", Enums.Places.Floricultura, false));
        Places.Add(new Place("Hospital", Enums.Places.Hospital, false));
        Places.Add(new Place("Hotel", Enums.Places.Hotel, false));
        Places.Add(new Place("Mansão", Enums.Places.Mansao, false));
        Places.Add(new Place("Praça Central", Enums.Places.Praca_Central, false));
        Places.Add(new Place("Prefeitura", Enums.Places.Prefeitura, false));
        Places.Add(new Place("Restaurante", Enums.Places.Restaurante, false));
    }


    public Dictionary<Enums.Weapons, string> WeaponsName = new Dictionary<Enums.Weapons, string>
    {
        {Enums.Weapons.Arma_Quimica,"Arma Química" },
        //{Enums.Weapons.Espingarda, "Espingarda" },
        {Enums.Weapons.Faca, "Faca" },
        //{Enums.Weapons.Pa, "Pá" },
        //{Enums.Weapons.Pe_de_Cabra, "Pé de Cabra" },
        {Enums.Weapons.Soco_Ingles, "Soco Inglês"},
        //{Enums.Weapons.Tesoura, "Tesoura" },
        {Enums.Weapons.Veneno, "Veneno" }
    };

    public Room ActiveRoom;

    private void InitializeWeapons()
    {
        Weapons = new List<Weapon>();
        Weapons.Add(new Weapon( Enums.Weapons.Arma_Quimica));
        //Weapons.Add(new Weapon( Enums.Weapons.Espingarda));
        Weapons.Add(new Weapon( Enums.Weapons.Faca));
        //Weapons.Add(new Weapon( Enums.Weapons.Pa));
        //Weapons.Add(new Weapon( Enums.Weapons.Pe_de_Cabra));
        Weapons.Add(new Weapon( Enums.Weapons.Soco_Ingles));
        //Weapons.Add(new Weapon( Enums.Weapons.Tesoura));
        Weapons.Add(new Weapon( Enums.Weapons.Veneno));
    }
    
    private void InitializeCharacterTips()
    {
        CharacterTips = new List<CharacterTip>();
        CharacterTips.Add(new CharacterTip(Enums.Characters.Advogado_Sr_Marinho, Enums.TipsSounds.Nao_foi_Advogado2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Advogado_Sr_Marinho, Enums.TipsSounds.Nao_foi_Advogado_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, Enums.TipsSounds.Nao_foi_Chef_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, Enums.TipsSounds.Nao_foi_Chef2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Coveiro_Sergio_Soturno, Enums.TipsSounds.Nao_foi_Coveiro_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Coveiro_Sergio_Soturno, Enums.TipsSounds.Nao_foi_Coveiro2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Dancarina_Srta_Rosa, Enums.TipsSounds.Nao_foi_Dancarina_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Dancarina_Srta_Rosa, Enums.TipsSounds.Nao_foi_Dancarina2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Florista_Dona_Branca, Enums.TipsSounds.Nao_foi_Florista_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Florista_Dona_Branca, Enums.TipsSounds.Nao_foi_Florista2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Medica_Dona_Violeta, Enums.TipsSounds.Nao_foi_Medica_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Medica_Dona_Violeta, Enums.TipsSounds.Nao_foi_Medica2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Mordomo_James, Enums.TipsSounds.Nao_foi_Mordomo_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Mordomo_James, Enums.TipsSounds.Nao_foi_Mordomo2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Sargento_Bicode, Enums.TipsSounds.Nao_foi_Sargento_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Sargento_Bicode, Enums.TipsSounds.Nao_foi_Sargento2_com_Ruido));
    }
    
    private void InitializeWeaponsTips()
    {
        WeaponsTips = new List<WeaponTip>();
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Arma_Quimica, Enums.TipsSounds.Nao_foi_Arma_Quimica_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Espingarda, Enums.TipsSounds.Nao_foi_Espingarda_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Faca, Enums.TipsSounds.Nao_foi_Faca_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Pa, Enums.TipsSounds.Nao_foi_Pa_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Pe_de_Cabra, Enums.TipsSounds.Nao_foi_Pe_de_Cabra_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Soco_Ingles, Enums.TipsSounds.Nao_foi_Soco_Ingles_com_Ruido));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Tesoura, Enums.TipsSounds.Nao_foi_Tesoura));
        WeaponsTips.Add(new WeaponTip(Enums.Weapons.Veneno, Enums.TipsSounds.Nao_foi_Veneno_com_Ruido));
    }

    #endregion

}

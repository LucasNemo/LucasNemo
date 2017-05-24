

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float COOLDOWN = 20f;

    #endregion

    #region Constants

    #region Scenes Names 
    public readonly string MENU_SCENE = "Menu";
    public readonly string DETETIVE_SCENE = "DetectiveScene";
    public readonly string SHERIFFE_SCENE = "SheriffScene";
    public readonly string QRCODE_SCENE = "QRScanScene";
    #endregion 

    public readonly string NO_PD = "Não foi selecionado nenhum lugar para ser delegacia. Escolha um para continuar!";
    public readonly string READ_FROM_XERIFE = "Realize a leitura do QRCode do Xerife para receber as informações do jogo!";
    public readonly string GO_TO_PD = "Vá para a DELEGACIA {0} e realize a leitura do QRCode para desvendar esse crime!";
    public readonly string READ_FROM_PLACE = "Realize a leitura do QRCode da carta do local / ambiente";
    private const string SAVE_NAME = "GameInfo";
    public readonly string DETECTIVE_URL = "http://www.estrela.com.br/brinquedo/detetive-com-aplicativo/";
    public readonly string DETECTIVE_URL_TERM = "http://sioux.com.br/apps/termos/1";
    public readonly string DETECTIVE_URL_TALK = "http://www.estrela.com.br/fale-conosco/";
    public readonly string CONFIRM_VISIT_MODAL_TITLE = @"VOCÊ SERÁ REDIRECIONADO PARA O SITE DA ESTRELA.";
    public readonly string CONTINUE_GAME_MODAL_DESCRIPTION = "VOCÊ TEM UM JOGO SALVO, DESEJA CONTINUAR  OU COMEÇAR UM NOVO?";
    public readonly string TERMS_MODAL_DESCRIPTION = "VOCÊ SERÁ REDIRECIONADO PARA: Termos de Uso.";
    public readonly string TALK_MODAL_DESCRIPTION = "VOCÊ SERÁ REDIRECIONADO PARA: Fale Conosco.";
    public readonly string IS_HOST_MODAL = "VOCÊ DESEJA QUE {0} SEJA A DELEGACIA?";
    public readonly string QR_READ_PLACE = "Realize a leitura do QRCode do lugar / ambiente";
    public readonly string QR_CODE_READ_CHARACTER = "Jogo carregado! Agora você deve escolher seu personagem.";
    public readonly string PLAYER_WANNA_PLAY = "VOCÊ DESEJA JOGAR TAMBÉM?";
    public readonly string READ_CHARACTER = "LEIA UMA CARTA DE PERSONAGEM";
    public readonly string ON_READ_PLACE_WRONG = "UTILIZE UMA CARTA DE LUGAR PARA LER";
    public readonly string ON_READ_CHARACTER_WRONG = "UTILIZE UMA CARTA DE PERSONAGEM PARA LER";
    public readonly string WARNING_BUTTON = "Entendi!";
    public readonly string FINAL_CONFIRM_HUNCH = "{0}, COM {1} {2}, {3} {4}";
    public readonly string WIN_TITLE = "PARABÉNS! \n VOCÊ CONSEGUIU SOLUCIONAR O CRIME";
    public readonly string WIN_DESCRIPTION = "Você completou em {0} horas e {1} minutos! JOGUE NOVAMENTE PARA INICIAR UM NOVO CASO!";
    public readonly string PLACE_ALREADY_ADDED = "ESTE LUGAR JÁ FOI ADICIONADO, ADICIONE OUTRO CASO QUEIRA QUE SEJA COMO DELEGACIA";
    public readonly string LOOSE_TITLE = "ERRADO!";
    public readonly string LOOSE_DESCRIPTION = "VOCÊ FALHOU EM SOLUCIONAR O CRIME... COMECE OUTRO JOGO COM SEUS AMIGOS E TENTE NOVAMENTE.";
    public readonly string CORRECT_PLACE = "ENCONTRAMOS!!! FOI EXATAMENTE AQUI que acontenceu o crime!!";
    public readonly string WRONG_PLACE = "NÃO FOI DESSA VEZ! Esse NÃO é o local do crime!";
    public readonly string NOT_READY_YET = "O resultado da perícia ainda NÃO saiu!";
    public readonly string SOME_RESULTS = "ATENÇÃO!! Volte para {0} para conferir o resultado da perícia!";
    public readonly string PERICIA_START = "Perícia em andamento! É melhor não perder tempo e ir investigar outros lugares enquanto isso...";
    public readonly string PLAYER_SAVE_TIME = "PLAYER_SAVE_TIME";
    public readonly string REALLY_WANNA_EXIT = "Você deseja realmente sair?";
    public readonly string RESULT_ERROR_BACK = "-1";
    #endregion

    #region PATHS

    public readonly string HUNCH_PATH = "Hunch/";
    public readonly string SOUND_CLICK = "Audio/Generic/click";

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

    /// <summary>
    /// Clear all player data
    /// </summary>
    public void ClearData()
    {   
        FileManager.Delete(Application.persistentDataPath, SAVE_NAME);
        PlayerPrefs.DeleteAll();
        Manager.Instance.MyGameInformation = null;
    }

    /// <summary>
    /// Initiliaze timer with datetime now
    /// </summary>
    public void InitializeTimer()
    {
        PlayerPrefs.SetString(Manager.Instance.PLAYER_SAVE_TIME, System.DateTime.Now.ToString());
    }

    /// <summary>
    /// Get correct saved time
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetTime()
    {
        DateTime oldDate;

        DateTime.TryParse(PlayerPrefs.GetString(Manager.Instance.PLAYER_SAVE_TIME), out oldDate);

        TimeSpan span = new TimeSpan();
        if (oldDate != null)
            span = (DateTime.Now - oldDate);

        return span;
    }

    #endregion
    
    #region Cards

    public Dictionary<Enums.Characters, string> CharactersName = new Dictionary<Enums.Characters, string>
    {
        {Enums.Characters.Advogado_Sr_Marinho, "Advogado Sr. MARINHO" },
        {Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, "Chef De Cozinha Tony GOURMET" },
        {Enums.Characters.Coveiro_Sergio_Soturno, "Coveiro Sérgio SOTURNO" },
        {Enums.Characters.Dancarina_Srta_Rosa, "Dançarina Srta. ROSA" },
        {Enums.Characters.Florista_Dona_Branca, "Florista Dona BRANCA" },
        {Enums.Characters.Medica_Dona_Violeta, "Médica Dona VIOLETA" },
        {Enums.Characters.Mordomo_James, "Mordomo JAMES" },
        {Enums.Characters.Sargento_Bigode, "Sargento BIGODE" }
    };

    public Dictionary<Enums.Places, string> PlacesNames = new Dictionary<Enums.Places, string> {
        { Enums.Places.Banco, "BANCO"},
        { Enums.Places.Boate, "BOATE"},
        { Enums.Places.Cemiterio, "CEMITÉRIO"},
        { Enums.Places.Estacao_de_Trem, "ESTAÇÃO DE TREM"},
        { Enums.Places.Floricultura, "FLORICULTURA"},
        { Enums.Places.Hospital, "HOSPITAL"},
        { Enums.Places.Hotel, "HOTEL"},
        { Enums.Places.Mansao, "MANSÃO"},
        { Enums.Places.Praca_Central, "PRAÇA CENTRAL"},
        { Enums.Places.Prefeitura, "PREFEITURA"},
        { Enums.Places.Restaurante, "RESTAURANTE"}
    };

    public Dictionary<Enums.Weapons, string> WeaponsName = new Dictionary<Enums.Weapons, string>
    {
        {Enums.Weapons.Arma_Quimica,"ARMA QUÍMICA" },
        {Enums.Weapons.Espingarda, "ESPINGARDA" },
        {Enums.Weapons.Faca, "FACA" },
        {Enums.Weapons.Pa, "PÁ" },
        {Enums.Weapons.Pe_de_Cabra, "PÉ DE CABRA" },
        {Enums.Weapons.Soco_Ingles, "SOCO INGLÊS"},
        {Enums.Weapons.Tesoura, "TESOURA" },
        {Enums.Weapons.Veneno, "VENENO" }
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
        Characters.Add(new Character( Enums.Characters.Sargento_Bigode.GetHashCode()));
    }

    private void InitializePlaces()
    {
        Places = new List<Place>();
        Places.Add(new Place(Enums.Places.Banco, false));
        Places.Add(new Place(Enums.Places.Boate, false));
        Places.Add(new Place(Enums.Places.Cemiterio, false));
        Places.Add(new Place(Enums.Places.Estacao_de_Trem, false));
        Places.Add(new Place(Enums.Places.Floricultura, false));
        Places.Add(new Place(Enums.Places.Hospital, false));
        Places.Add(new Place(Enums.Places.Hotel, false));
        Places.Add(new Place(Enums.Places.Mansao, false));
        Places.Add(new Place(Enums.Places.Praca_Central, false));
        Places.Add(new Place(Enums.Places.Prefeitura, false));
        Places.Add(new Place(Enums.Places.Restaurante, false));
    }
    
    public Room ActiveRoom;
    

    private void InitializeWeapons()
    {
        Weapons = new List<Weapon>();
        Weapons.Add(new Weapon( Enums.Weapons.Arma_Quimica));
        Weapons.Add(new Weapon(Enums.Weapons.Espingarda));
        Weapons.Add(new Weapon( Enums.Weapons.Faca));
        Weapons.Add(new Weapon(Enums.Weapons.Pa));
        Weapons.Add(new Weapon(Enums.Weapons.Pe_de_Cabra));
        Weapons.Add(new Weapon( Enums.Weapons.Soco_Ingles));
        Weapons.Add(new Weapon(Enums.Weapons.Tesoura));
        Weapons.Add(new Weapon( Enums.Weapons.Veneno));
    }
    
    private void InitializeCharacterTips()
    {
        CharacterTips = new List<CharacterTip>();
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Advogado_Sr_Marinho, Enums.TipsSounds.Nao_foi_Advogado2_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Advogado_Sr_Marinho, Enums.TipsSounds.Nao_foi_Advogado_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, Enums.TipsSounds.Nao_foi_Chef_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Chef_de_Cozinha_Tony_Gourmet, Enums.TipsSounds.Nao_foi_Chef2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Coveiro_Sergio_Soturno, Enums.TipsSounds.Nao_foi_Coveiro_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Coveiro_Sergio_Soturno, Enums.TipsSounds.Nao_foi_Coveiro2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Dancarina_Srta_Rosa, Enums.TipsSounds.Nao_foi_Dancarina_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Dancarina_Srta_Rosa, Enums.TipsSounds.Nao_foi_Dancarina2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Florista_Dona_Branca, Enums.TipsSounds.Nao_foi_Florista_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Florista_Dona_Branca, Enums.TipsSounds.Nao_foi_Florista2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Medica_Dona_Violeta, Enums.TipsSounds.Nao_foi_Medica_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Medica_Dona_Violeta, Enums.TipsSounds.Nao_foi_Medica2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Mordomo_James, Enums.TipsSounds.Nao_foi_Mordomo_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Mordomo_James, Enums.TipsSounds.Nao_foi_Mordomo2_com_Ruido));
        //CharacterTips.Add(new CharacterTip(Enums.Characters.Sargento_Bigode, Enums.TipsSounds.Nao_foi_Sargento_com_Ruido));
        CharacterTips.Add(new CharacterTip(Enums.Characters.Sargento_Bigode, Enums.TipsSounds.Nao_foi_Sargento2_com_Ruido));
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

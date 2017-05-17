

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
    public readonly string NO_PD = "Não foi selecionado nenhum lugar para ser delegacia. Escolha um para continuar!";
    public readonly string READ_FROM_XERIFE = "Não reconhecemos esse qrcode. Realize a leitura do QRCode do Xerife para receber as informações do jogo!";
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
    public readonly string IS_HOST_MODAL = "VOCÊ DESEJA QUE LUGAR SEJA A DELEGACIA?";
    public readonly string QR_READ_PLACE = "Realize a leitura do QRCode do lugar / ambiente";
    public readonly string PLAYER_WANNA_PLAY = "VOCÊ DESEJA JOGAR TAMBÉM?";
    public readonly string READ_CHARACTER = "LEIA UMA CARTA DE PERSONAGEM";
    public readonly string ON_READ_PLACE_WRONG = "UTILIZE UMA CARTA DE LUGAR PARA LER";
    public readonly string ON_READ_CHARACTER_WRONG = "UTILIZE UMA CARTA DE PERSONAGEM PARA LER";
    public readonly string WARNING_BUTTON = "Entendi!";
    public readonly string FINAL_CONFIRM_HUNCH = "{0}, COM {1} {2}, {3} {4}";
    public readonly string WIN_TITLE = "PARABÉNS! \n VOCÊ CONSEGUIU SOLUCIONAR O CRIME!";
    public readonly string WIN_DESCRIPTION = "JOGUE NOVAMENTE PARA INICIAR UM NOVO CASO!";
    public readonly string LOOSE_TITLE = "ERRADO!";
    public readonly string LOOSE_DESCRIPTION = "VOCÊ FALHOU EM SOLUCIONAR O CRIME... COMECE OUTRO JOGO COM SEUS AMIGOS E TENTE NOVAMENTE.";
    public readonly string CORRECT_PLACE = "O resultado da perícia saiu! FOI EXATAMENTE AQUI que acontenceu o crime!!";
    public readonly string WRONG_PLACE = "Não foi dessa vez! Esse NÃO é o local do crime!";
    public readonly string NOT_READY_YET = "O resultado da perícia ainda NÃO saiu!";
    public readonly string SOME_RESULTS = "Sairam alguns resultados de perícia. Volte ao local da perícia para conferir!";
    public readonly string PERICIA_START = "Perícia em andamento! É melhor não perder tempo e ir investigar outros lugares enquanto isso...";


    #endregion  

    #region PATHS

    public readonly string HUNCH_PATH = "Hunch/";

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
        {Enums.Characters.Sargento_Bigode, "SARGENTO BIGODE" }
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
        {Enums.Weapons.Espingarda, "Espingarda" },
        {Enums.Weapons.Faca, "Faca" },
        {Enums.Weapons.Pa, "Pá" },
        {Enums.Weapons.Pe_de_Cabra, "Pé de Cabra" },
        {Enums.Weapons.Soco_Ingles, "Soco Inglês"},
        {Enums.Weapons.Tesoura, "Tesoura" },
        {Enums.Weapons.Veneno, "Veneno" }
    };

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
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Arma_Quimica, Enums.TipsSounds.Nao_foi_Arma_Quimica_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Espingarda, Enums.TipsSounds.Nao_foi_Espingarda_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Faca, Enums.TipsSounds.Nao_foi_Faca_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Pa, Enums.TipsSounds.Nao_foi_Pa_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Pe_de_Cabra, Enums.TipsSounds.Nao_foi_Pe_de_Cabra_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Soco_Ingles, Enums.TipsSounds.Nao_foi_Soco_Ingles_com_Ruido));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Tesoura, Enums.TipsSounds.Nao_foi_Tesoura));
        //WeaponsTips.Add(new WeaponTip(Enums.Weapons.Veneno, Enums.TipsSounds.Nao_foi_Veneno_com_Ruido));
    }

    #endregion


    public readonly string MENU_SCENE = "Menu";
    public readonly string DETETIVE_SCENE = "DetectiveScene";
    public readonly string SHERIFFE_SCENE = "SheriffScene";
    internal readonly string QRCODE_SCENE = "QRScanScene";
}

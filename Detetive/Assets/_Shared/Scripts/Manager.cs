

using System;
using System.Collections.Generic;

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
    /// All scenario of player
    /// </summary>
    public GameInformation MyGameInformation { get; set; }

    public Manager()
    {
        IntializeCards();
    }

    private void IntializeCards()
    {
        InitializeCharacters();
        InitializePlaces();
        InitializeWeapons();
    }

    #region Cards

    private void InitializeCharacters()
    {
        Characters = new List<Character>();
        Characters.Add(new Character("ADVOGADO SR. MARINHO", Enums.Characters.Advogado_Sr_Marinho));
        Characters.Add(new Character("CHEF DE COZINHA TONY GOURMET", Enums.Characters.Chef_de_Cozinha_Tony_Gourmet));
        Characters.Add(new Character("COVEIRO SÉRGIO SOTURNO", Enums.Characters.Coveiro_Sergio_Soturno));
        Characters.Add(new Character("DANÇARINA SRTA. ROSA", Enums.Characters.Dancarina_Srta_Rosa));
        Characters.Add(new Character("FLORISTA DONA BRANCA", Enums.Characters.Florista_Dona_Branca));
        Characters.Add(new Character("MÉDICA DONA VIOLETA", Enums.Characters.Medica_Dona_Violeta));
        Characters.Add(new Character("MORDOMO JAMES", Enums.Characters.Mordomo_James));
        Characters.Add(new Character("SARGENTO BIGODE", Enums.Characters.Sargento_Bicode));
    }

    private void InitializePlaces()
    {
        Places = new List<Place>();
        Places.Add(new Place("Banco", Enums.Places.Banco));
        Places.Add(new Place("Boate", Enums.Places.Boate));
        Places.Add(new Place("Cemitério", Enums.Places.Cemiterio));
        Places.Add(new Place("Estação de Trem", Enums.Places.Estacao_de_Trem));
        Places.Add(new Place("Floricultura", Enums.Places.Floricultura));
        Places.Add(new Place("Hospital", Enums.Places.Hospital));
        Places.Add(new Place("Hotel", Enums.Places.Hotel));
        Places.Add(new Place("Mansão", Enums.Places.Mansao));
        Places.Add(new Place("Praça Central", Enums.Places.Praca_Central));
        Places.Add(new Place("Prefeitura", Enums.Places.Prefeitura));
        Places.Add(new Place("Restaurante", Enums.Places.Restaurante));
    }

    private void InitializeWeapons()
    {
        Weapons = new List<Weapon>();
        Weapons.Add(new Weapon("Arma Química", Enums.Weapons.Arma_Quimica));
        Weapons.Add(new Weapon("Espingarda", Enums.Weapons.Espingarda));
        Weapons.Add(new Weapon("Faca", Enums.Weapons.Faca));
        Weapons.Add(new Weapon("Pá", Enums.Weapons.Pa));
        Weapons.Add(new Weapon("Pé de Cabra", Enums.Weapons.Pe_de_Cabra));
        Weapons.Add(new Weapon("Soco Inglês", Enums.Weapons.Soco_Ingles));
        Weapons.Add(new Weapon("Tesoura", Enums.Weapons.Tesoura));
        Weapons.Add(new Weapon("Veneno", Enums.Weapons.Veneno));
    }

    #endregion

}

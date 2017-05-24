public class Enums
{

    public enum PericiaStatus
    {
        Requested,
        InProgress,
        Result,
        Done
    }

    public enum DetectiveState
    {
        /// <summary>
        /// Read the game configuration from Xerife or whatever! 
        /// </summary>
        ReadGameConfiguration = 1,
    
        /// <summary>
        /// Starting the game - save the state and do something else
        /// </summary>
        StartGame = 2,

        /// <summary>
        /// Wainting for some player action - Investigate, hunch or notes (i dont know, dont push me, ok??)
        /// </summary>
        WaitingForAction = 3,

        /// <summary>
        /// Start investigate - do some preloads or unloads! 
        /// </summary>
        Investigate = 4,

        /// <summary>
        /// Investigating our amazing 3D models spawned with AR 
        /// </summary>
        Investigating = 5,

        /// <summary>
        /// Ok, that's enough - Finish the investigation!
        /// </summary>
        EndingInvestigation = 6,

        /// <summary>
        /// It's time!!!! Do your hunch and be ready to finish the game!
        /// </summary>
        Hunch = 7,

        /// <summary>
        /// Waiting the player move to the police station , xerife or whatever darling!
        /// </summary>
        WaitingDeploy = 8,

        /// <summary>
        /// The final screen!!!!! - Result time!!
        /// </summary>
        ResultScreen = 9,

        /// <summary>
        /// Ok, i forgot this state, dont judge me :(
        /// </summary>
        ReadingGameState = 10,
        ReadCharacter = 11
    }

    /// <summary>
    /// Possibles characters to choose
    /// Value is Qrcode ID
    /// </summary>
    public enum Characters
    {
        Advogado_Sr_Marinho = 01,
        Chef_de_Cozinha_Tony_Gourmet = 02,
        Coveiro_Sergio_Soturno = 03,
        Dancarina_Srta_Rosa = 04,
        Florista_Dona_Branca = 05,
        Medica_Dona_Violeta = 06,
        Mordomo_James = 07,
        Sargento_Bigode = 08,
    }

    /// <summary>
    /// Possibles Weapon on crime scene
    /// Value is Qrcode ID
    /// </summary>
    public enum Weapons
    {
        Arma_Quimica = 09,
        Espingarda = 10,
        Faca = 11,
        Pa = 12,
        Pe_de_Cabra = 13,
        Soco_Ingles = 14,
        Tesoura = 15,
        Veneno = 16
    }

    /// <summary>
    /// Possibles Places 
    /// Value is Qrcode ID
    /// </summary>
    public enum Places 
    {
        Banco = 17,
        Boate = 18,
        Cemiterio = 19,
        Estacao_de_Trem = 20,
        Floricultura = 21,
        Hospital = 22,
        Hotel = 23,
        Mansao = 24,
        Praca_Central = 25,
        Prefeitura = 26,
        Restaurante = 27
    }

    public enum TipsSounds
    {
        Nao_foi_Arma_Quimica_com_Ruido = 0,
        Nao_foi_Espingarda_com_Ruido = 1,
        Nao_foi_Faca_com_Ruido = 2,
        Nao_foi_Pa_com_Ruido = 3,
        Nao_foi_Pe_de_Cabra_com_Ruido = 4,
        Nao_foi_Soco_Ingles_com_Ruido = 5,
        Nao_foi_Tesoura = 6,
        Nao_foi_Veneno_com_Ruido = 7,
        Nao_foi_Advogado2_com_Ruido = 8,
        Nao_foi_Advogado_com_Ruido = 9,
        Nao_foi_Chef2_com_Ruido = 10,
        Nao_foi_Chef_com_Ruido = 11,
        Nao_foi_Coveiro2_com_Ruido = 12,
        Nao_foi_Coveiro_com_Ruido = 13,
        Nao_foi_Dancarina2_com_Ruido = 14,
        Nao_foi_Dancarina_com_Ruido = 15,
        Nao_foi_Florista2_com_Ruido = 16,
        Nao_foi_Florista_com_Ruido = 17,
        Nao_foi_Medica2_com_Ruido = 18,
        Nao_foi_Medica_com_Ruido = 19,
        Nao_foi_Mordomo2_com_Ruido = 20,
        Nao_foi_Mordomo_com_Ruido = 21,
        Nao_foi_Sargento2_com_Ruido = 22,
        Nao_foi_Sargento_com_Ruido = 23
    }
}

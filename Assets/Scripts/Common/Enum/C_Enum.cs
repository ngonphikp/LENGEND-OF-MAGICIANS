using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Enum
{
    public enum Scrolltype
    {
        VerDown = 0,
        VerUp = 1,
        HozLeft = 2,
        HozRight = 3,
    }

    public enum ReadType
    {
        SERVER,
        CONFIG,
    }

    public enum CharacterType
    {
        Hero = 0,
        Creep = 1,
        Boss = 2,
    }

    public enum CardType
    {
        CT = 0, // Star 1, 2
        DT = 1, // Star 3, 4
        TT = 2, // Star 5, 6
    }

    public enum ActionType
    {
        SKILL = 0,
        CHANGE_HP = 1,
        CHANGE_EP = 2,
        BEATEN = 3,
        DIE = 4,
        DODGE = 5,
        TURN = 6,
    }

    public enum TypeText
    {
        HP1, // Tăng HP
        HP2, // Giảm HP
        HP2c, // Giảm HP có crit
        EP1, // Tăng EP
        EP2, // Giảm EP
        DG,  // Tránh né
    }

    public enum EndGame
    {
        NOT = 0,
        WIN = 1,
        LOSE = 2,
    }

    public enum JobGuild
    {
        None = -1,
        Normal = 0,
        Master = 1,
    }

    public enum StatusAccount
    {
        None = -1,
        Off = 0,
        On = 1,
    }

    public enum CFType
    {
        None = -1,
        Global = 0,
        Guild = 1,
        Private = 2,
    }

    public enum StatusBossG
    {
        Lock = 0,
        Fight = 1,
        Reward = 2,
    }

    public enum BattleType
    {
        None = -1,
        Campain = 0,
        BossGuild = 1,
    }
}

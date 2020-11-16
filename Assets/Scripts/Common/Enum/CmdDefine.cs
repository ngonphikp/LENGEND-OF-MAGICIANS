﻿using System.Collections.Generic;

public class CmdDefine
{
    public static string CMD_ID = "cmdid";
    public static string ERROR_CODE = "ec";

    public static class CMD
    {
        public const int REGISTER = 1001;
        public const int LOGIN = 1002;
        public const int GETINFO = 1003;
        public const int SELECTION = 1004;
        public const int ARRANGE = 1005;
        public const int TAVERN = 1006;
        public const int UPLEVEL = 1007;
        public const int ENDGAME = 1008;
        public const int GETGUILDS = 1009;
        public const int CREATEGUILD = 1010;
        public const int GETGUILD = 1011;
        public const int OUTGUILD = 1012;
        public const int PLEASEGUILD = 1013;
        public const int CHANGEMASTER = 1014;
    }

    public static class ErrorCode
    {
        public const short SUCCESS = 0;
        public static readonly Dictionary<int, string> Errors
        = new Dictionary<int, string>
        {
            {1, "WRONG_USERNAME_OR_PASSWORD"},
            {2, "EXIT_ACCOUNT"},
       };
    }

    public static class Module
    {
        public const string MODULE_ACCOUNT = "account";
        public const string MODULE_CHARACTER = "character";
        public const string MODULE_TICK_MILESTONE = "tick_milestone";
        public const string MODULE_GUILD = "guild";
    }

    public static class ModuleAccount
    {
        public const string ID = "id_ac";
        public const string USERNAME = "username_ac";
        public const string PASSWORD = "password_ac";
        public const string NAME = "name_ac";

        public const string ID_GUILD = "id_guild";

        public const string ACCOUNT = "account";
        public const string LOGIN_OUT_DATA = "loginoutdata";

        public const string CHARACTERS = "characters";

        public const string TICK_MILESTONES = "tick_milestones";

        public const string TYPE_TAVERN = "type_tavern";
        public const string CHARACTER = "character";
    }

    public static class ModuleCharacter
    {
        public const string ID = "id_char";
        public const string ID_CFG = "id_cfg_char";
        public const string LV = "lv_char";
        public const string IDX = "idx_char";
    }

    public static class ModuleTickMilestone
    {
        public const string ID = "id_tms";
        public const string STAR = "star_tms";
        public const string ID_AC = "id_ac";
        public const string ID_ML = "id_ml";

        public const string IS_SAVE = "is_save";
    }

    public static class ModuleGuild
    {
        public const string ID = "id_guild";
        public const string NAME = "name_guild";
        public const string LV = "lv_guild";
        public const string NOTI = "noti_guild";

        public const string ACCOUNTS = "accounts";

        public const string MASTER = "master";

        public const string GUILD = "guild";
        public const string GUILDS = "guilds";
    }
}

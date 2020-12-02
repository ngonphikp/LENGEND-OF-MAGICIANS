using System.Collections.Generic;

public class CmdDefine
{
    public static string CMD_ID = "cmdid";
    public static string ERROR_CODE = "ec";

    public static class CMD
    {
        public const int REGISTER = 1001;
        public const int LOGIN = 1002;

        public const int GET_INFO = 1003;

        public const int SELECTION = 1004;

        public const int ARRANGE = 1005;

        public const int TAVERN = 1006;

        public const int UPLEVEL = 1007;

        public const int END_GAME_CAMPAIN = 1008;

        public const int GET_GUILDS = 1009;
        public const int CREATE_GUILD = 1010;
        public const int GET_GUILD = 1011;
        public const int OUT_GUILD = 1012;
        public const int PLEASE_GUILD = 1013;
        public const int FIX_MASTER_GUILD = 1014;
        public const int GET_NOTI_GUILD = 1015;
        public const int GET_EVENT_GUILD = 1016;
        public const int GET_MEMBER_GUID = 1017;
        public const int FIX_NOTI_GUILD = 1018;

        public const int GET_ACCOUNT_GLOBAL = 1019;
        public const int SEND_MESSAGE_GLOBAL = 1020;
        public const int GET_ACCOUNT_GUILD = 1021;
        public const int SEND_MESSAGE_GUILD = 1022;

        public const int GET_DETAILS = 1023;
        public const int MAKE_FRIEND = 1024;
        public const int REMOVE_FRIEND = 1025;
        public const int GET_ACCOUNT_FRIEND = 1026;
        public const int FIND_ACCOUNT_GLOBAL = 1027;

        public const int SEND_MESSAGE_PRIVATE = 1028;

        public const int GET_BOSSES_GUILD = 1029;
        public const int GET_TICK_BOSS_GUILD = 1030;

        public const int UNLOCK_BOSS_GUILD = 1031;

        public const int END_GAME_BOSS_GUILD = 1032;
        public const int REWARD_BOSS_GUILD = 1033;
    }

    public static class ErrorCode
    {
        public const short SUCCESS = 0;
        public const short WRONG_USERNAME_OR_PASSWORD = 1;
        public const short EXIT_ACCOUNT = 2;
        public const short ACCOUNT_LOGON = 3;

        public const short UNFRIENDED = 4;

        public static readonly Dictionary<short, string> Errors
        = new Dictionary<short, string>
        {
            {WRONG_USERNAME_OR_PASSWORD, "WRONG_USERNAME_OR_PASSWORD"},
            {EXIT_ACCOUNT, "EXIT_ACCOUNT"},
            {ACCOUNT_LOGON, "ACCOUNT_LOGON" },
            {UNFRIENDED, "UNFRINEDED" }
       };
    }

    public static class Module
    {
        public const string MODULE_ACCOUNT = "account";
        public const string MODULE_CHARACTER = "character";
        public const string MODULE_CAMPAIN = "tick_milestone";
        public const string MODULE_GUILD = "guild";
        public const string MODULE_CHAT_AND_FRIEND = "chat_and_friend";
    }

    public static class ModuleAccount
    {
        public const string ID = "id_ac";
        public const string USERNAME = "username_ac";
        public const string PASSWORD = "password_ac";

        public const string NAME = "name_ac";

        public const string LV = "lv_ac";
        public const string POWER = "power_ac";

        public const string JOB = "job_ac";
        public const string DEDITOTAL = "dedication_total_ac";
        public const string DEDIWEEK = "dedication_week_ac";

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

    public static class ModuleTickCampain
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

        public const string EVENTS = "events";

        public const string BOSSES = "bosses";
        public const string BOSS = "boss";
        public const string TICK_BOSS = "tick_boss";
    }

    public static class ModuleEventGuild
    {
        public const string ID = "id_evt_guild";
        public const string CONTENT = "content_evt_guild";
        public const string TIME = "time_evt_guild";

        public const string COUNT = "count_evt_guild";
    }

    public static class MouduleCF
    {
        public const string ACCOUNTS = "accounts";
        public const string ID_ONLINES = "id_onlines";

        public const string ACCOUNT = "account_cf";
        public const string MESSAGE = "message_cf";    
        
        public const string IS_FRIEND = "is_friend";

        public const string CONTENT = "content";
        public const string IS_CHECK_ID = "is_check_id";
    }

    public static class ModuleBossGuild
    {
        public const string ID = "id_bg";
        public const string ID_BOSS = "id_boss";
        public const string CUR_HP = "current_hp";
        public const string STATUS = "status_bg";
    }

    public static class ModuleTickBossGuild
    {
        public const string ID = "id_tbg";

        public const string CUR_TURN = "current_turn";
        public const string IS_REWARD = "is_reward";

        public const string POINT = "point_tbg";
    }
}

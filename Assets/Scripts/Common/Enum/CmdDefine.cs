using System.Collections.Generic;

public class CmdDefine
{
    public static string CMD_ID = "cmdid";
    public static string ERROR_CODE = "ec";

    public static class CMD
    {
        public const int REGISTER = 1001;
        public const int LOGIN = 1002;

        public const int GET_INFO = 2001;
        public const int SELECTION = 2002;
        public const int TAVERN = 2003;

        public const int ARRANGE = 3001;        
        public const int UPLEVEL = 3002;

        public const int GET_TICKS_CAMPAIGN = 4001;
        public const int END_GAME_CAMPAIGN = 4002;

        public const int GET_GUILDS = 5001;
        public const int CREATE_GUILD = 5002;
        public const int GET_GUILD = 5003;
        public const int OUT_GUILD = 5004;
        public const int PLEASE_GUILD = 5005;
        public const int CHANGE_MASTER_GUILD = 5006;
        public const int GET_NOTI_GUILD = 5007;
        public const int GET_EVENT_GUILD = 5008;
        public const int GET_MEMBER_GUID = 5009;
        public const int FIX_NOTI_GUILD = 5010;
        public const int GET_BOSSES_GUILD = 5011;
        public const int GET_TICK_BOSS_GUILD = 5012;
        public const int UNLOCK_BOSS_GUILD = 5013;
        public const int END_GAME_BOSS_GUILD = 5014;
        public const int REWARD_BOSS_GUILD = 5015;

        public const int GET_ACCOUNT_GLOBAL = 6001;
        public const int SEND_MESSAGE_GLOBAL = 6002;
        public const int GET_ACCOUNT_GUILD = 6003;
        public const int SEND_MESSAGE_GUILD = 6004;
        public const int GET_DETAILS = 6005;
        public const int MAKE_FRIEND = 6006;
        public const int REMOVE_FRIEND = 6007;
        public const int GET_ACCOUNT_FRIEND = 6008;
        public const int FIND_ACCOUNT_GLOBAL = 6009;
        public const int SEND_MESSAGE_PRIVATE = 6010;

        public const int START_PVP = 7001;
        public const int CANCLE_PVP = 7002;

        public const int JOIN_ROOM_GAME = 8001;
        public const int OUT_ROOM_GAME = 8002;
        public const int UN_ACTIVE_CHAR = 8003;
        public const int ACTIVE_CHAR = 8004;
        public const int CHANGE_CHAR = 8005;
        public const int LOCK_ARRANGE = 8006;
        public const int START_GAME = 8007;
        public const int SEND_SCENARIO = 8008;
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
        public const string MODULE_CAMPAIGN = "campaign";
        public const string MODULE_TICK_CAMPAIGN = "tick_campaign";
        public const string MODULE_GUILD = "guild";
        public const string MODULE_EVENT_GUILD = "event_guild";
        public const string MODULE_CHAT_AND_FRIEND = "chat_and_friend";
        public const string MODULE_BOSS_GUILD = "boss_guild";
        public const string MODULE_TICK_BOSS_GUILD = "tick_boss_guild";
        public const string MODULE_PVP = "PvP";
        public const string MODULE_GAME = "Game";
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

        public const string ACCOUNT = "account";
        public const string ACCOUNTS = "accounts";

        public const string LOGIN_OUT_DATA = "loginoutdata";
        public const string TYPE_TAVERN = "type_tavern";
    }

    public static class ModuleCharacter
    {
        public const string ID = "id_char";
        public const string ID_CFG = "id_cfg_char";
        public const string LV = "lv_char";
        public const string IDX = "idx_char";

        public const string CHARACTER = "character";
        public const string CHARACTERS = "characters";
    }

    public static class ModuleCampaign
    {
        public const string ID = "id_camp";
    }

    public static class ModuleTickCampaign
    {
        public const string ID = "id_tc";
        public const string STAR = "star_tc";

        public const string TICK_CAMPAIGN = "tick_camp";
        public const string TICKS_CAMPAIGN = "ticks_camp";
    }

    public static class ModuleGuild
    {
        public const string ID = "id_guild";
        public const string NAME = "name_guild";
        public const string LV = "lv_guild";
        public const string NOTI = "noti_guild";

        public const string GUILD = "guild";
        public const string GUILDS = "guilds";

        public const string MASTER = "master_guild";
    }

    public static class ModuleEventGuild
    {
        public const string ID = "id_evtg";
        public const string CONTENT = "content_evtg";
        public const string TIME = "time_evtg";

        public const string COUNT = "count_evtg";

        public const string EVENT_GUILD = "evt_guild";
        public const string EVENTS_GUILD = "evts_guild";
    }

    public static class ModuleCF
    {
        public const string ID_ONLINES = "id_onlines";
        public const string MESSAGE = "message_cf";
        public const string IS_FRIEND = "is_friend";

        public const string CONTENT = "content_cf";
        public const string IS_CHECK_ID = "is_check_id";
    }

    public static class ModuleBossGuild
    {
        public const string ID = "id_bg";
        public const string ID_BOSS = "id_boss";
        public const string CUR_HP = "cur_hp_bg";
        public const string STATUS = "status_bg";

        public const string BOSS_GUILD = "boss_guild";
        public const string BOSSES_GUILD = "bosses_guild";
    }

    public static class ModuleTickBossGuild
    {
        public const string ID = "id_tbg";

        public const string CUR_TURN = "cur_turn_tbg";
        public const string IS_REWARD = "is_reward_tbg";

        public const string POINT = "point_tbg";

        public const string TICK_BOSS_GUILD = "tick_bg";
        public const string TICKS_BOSS_GUILD = "ticks_bg";
    }

    public static class ModuleGame
    {
        public const string LIST = "list";

        public const string TO = "to";
        public const string FROM = "from";
    }
}

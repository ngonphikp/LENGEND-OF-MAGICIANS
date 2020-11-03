﻿public class CmdDefine
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
    }

    public static class ErrorCode
    {
        public const short SUCCESS = 0;
    }

    public static class Module
    {
        public const string MODULE_USER = "user";
        public const string MODULE_CHARACTER = "character";
        public const string MODULE_TICKMILESTONE = "tickmilestone";
        public const string MODULE_GUILD = "guild";
    }

    public static class ModuleUser
    {
        public const string ID = "id";
        public const string USERNAME = "username";
        public const string PASSWORD = "password";
        public const string NAME = "name";

        public const string TAI_KHOAN = "taikhoan";
        public const string LOGIN_OUT_DATA = "loginOutData";

        public const string NHAN_VAT_S = "nhanvats";

        public const string TICK_MILESTONES = "tick_milestones";

        public const string TYPE_TAVERN = "type_tavern";
        public const string NHAN_VAT = "nhanvat";
    }

    public static class ModuleCharacter
    {
        public const string ID_NV = "id_nv";
        public const string ID_CFG = "id_cfg";
        public const string ID_TK = "id_tk";
        public const string LV = "lv";
        public const string IDX = "idx";
    }

    public static class ModuleTickMilestone
    {
        public const string ID_TML = "id_tml";
        public const string ID_TK = "id_tk";
        public const string ID_ML = "id_ml";
        public const string STAR = "star";

        public const string IS_SAVE = "is_save";
    }
}

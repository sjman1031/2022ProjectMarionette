namespace BaseFrame
{
   
    public enum eResourceType
    {
        Table,
        Character,
        UI
    }

    public enum eUIObject
    {
        UIStartPage = 1001,
        UIMainPage = 1003,
        UIBattlePage = 1004,
        UITestPage = 1101,
        UILobbyWindow = 2001,
        UILoadingFrontPage = 3002,
        UITestPopup = 4001,
        UIErrorPopup = 9002
    }

    public enum eUIDefine
    {
        Page = 1,
        Window = 2,
        FrontPage = 3,
        Popup = 4,
        Notice = 9,
    }

    public enum eScene
    {
        ArenaStartScene = 1,
        ArenaLoadingScene = 2,
        ArenaMainScene = 3,
        ArenaBattleScene = 4
    }
}

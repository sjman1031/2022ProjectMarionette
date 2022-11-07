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
        UIStartWindow = 2001,
        UIMainWindow = 2002,
        UIStageSelectWindow = 2003,
        UIClearOverPopup =3001,
        UIExitPopup = 3002,
        UIHowToPlayPopup = 3003,
        UIOptionPopup = 3004,
        UIShopPopup = 3005,
        UIErrorPopup = 9002
    }

    public enum eUIDefine
    {
        Page = 1,
        Window = 2,
        Popup = 3,
        Notice = 9,
    }

    public enum eScene
    {
        GameStartScene = 1,

    }
}

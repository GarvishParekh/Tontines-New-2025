using System;

namespace UI.ToolKit
{
    public static class ActionHandler
    {
        #region Ui-interactionns
        public static Action<CanvasNames> OpenCanvasAction;
        public static Action<CanvasNames> CloseCavnvasAction;

        public static Action<CanvasNames> CanvasChanged;

        public static Action<PopUpNames> OpenPopup;
        public static Action<PopUpNames> ClosePopup;

        public static Action<CanvasNames> OpenCanvasWithTransitionAction;
        public static Action<string> ChangeSceneWithTransitionAction;
        #endregion
    }
}
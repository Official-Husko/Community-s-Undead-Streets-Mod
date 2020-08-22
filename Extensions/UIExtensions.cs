using GTA.Native;
using System;

namespace CWDM.Extensions
{
    public static class UIExtensions
    {
        public static bool IsAnyHelpTextOnScreen()
        {
            return Function.Call<bool>(Hash.IS_HELP_MESSAGE_ON_SCREEN);
        }

        public static void ClearAllHelpText()
        {
            Function.Call(Hash.CLEAR_ALL_HELP_MESSAGES);
        }

        public static void DisplayHelpTextThisFrame(string helpText)
        {
            if (!Main.MasterMenuPool.IsAnyMenuOpen())
            {
                Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON");
                for (int i = 0; i < helpText.Length; i += 99)
                {
                    Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, helpText.Substring(i, Math.Min(99, helpText.Length - i)));
                }
                Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, !Function.Call<bool>(Hash.IS_HELP_MESSAGE_BEING_DISPLAYED) ? 1 : 0, -1);
            }
        }
    }
}

using GTA;
using GTA.Native;

namespace CWDM.Extensions
{
    public static class ScriptExtensions
    {
        public static void TerminateScript(string script)
        {
            int scriptHash = Game.GenerateHash(script);
            if (Function.Call<bool>((Hash)0xF86AA3C56BA31381, scriptHash))
            {
                if (Function.Call<bool>((Hash)0x5F0F0C783EB16C04, scriptHash))
                {
                    Function.Call(Hash.SET_SCRIPT_AS_NO_LONGER_NEEDED, script);
                    Function.Call((Hash)0xC5BC038960E9DB27, scriptHash);
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, script);
                }
            }
        }
    }
}

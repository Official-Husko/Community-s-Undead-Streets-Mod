using System;
using System.Windows.Forms;
using GTA;

namespace CWDM.DebugExtensions
{
    public class FPSCheck : Script
    {
        private readonly string dbg_fps = "DEBUG AURORA";

        public FPSCheck()
        {
            base.Tick += new EventHandler(this.OnTick);
            base.KeyDown += new KeyEventHandler(this.OnKeyDown);
            base.KeyUp += new KeyEventHandler(this.OnKeyUp);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.U) return;
            UI.Notify(dbg_fps);
            UI.Notify("You are getting " + ((int)Game.FPS).ToString() + " FPS", false);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void OnTick(object sender, EventArgs e)
        {
        }
    }
}

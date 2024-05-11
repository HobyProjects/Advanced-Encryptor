using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Advanced_Encryptor
{
    public static class DragControl
    {
        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<Control, bool> Draggables = new Dictionary<Control, bool>();
        private static Size MouseOffset = new Size();

        public static void Draggable(this Control control, bool enable)
        {
            if (enable)
            {
                // enable drag feature
                if (Draggables.ContainsKey(control))
                {   // return if control is already draggable
                    return;
                }

                // 'false' - initial state is 'not dragging'
                Draggables.Add(control, false);

                // assign required event handlersnnn
                control.MouseDown += new MouseEventHandler(control_MouseDown);
                control.MouseUp += new MouseEventHandler(control_MouseUp);
                control.MouseMove += new MouseEventHandler(control_MouseMove);
            }
            else
            {
                // disable drag feature
                if (!Draggables.ContainsKey(control))
                {  // return if control is not draggable
                    return;
                }
                // remove event handlers
                control.MouseDown -= control_MouseDown;
                control.MouseUp -= control_MouseUp;
                control.MouseMove -= control_MouseMove;
                Draggables.Remove(control);
            }
        }

        public static void control_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOffset = new Size(e.Location);
            // turning on dragging
            Draggables[(Control)sender] = true;
        }

        public static void control_MouseUp(object sender, MouseEventArgs e)
        {
            // turning off dragging
            Draggables[(Control)sender] = false;
        }

        public static void control_MouseDown(object sender, MouseEventArgs e)
        {
            // only if dragging is turned on
            if (Draggables[(Control)sender] == true)
            {
                // calculations of control's new position
                Point newLocationOffset = e.Location - MouseOffset;
                ((Control)sender).Left += newLocationOffset.X;
                ((Control)sender).Top += newLocationOffset.Y;
            }
        }
    }
}

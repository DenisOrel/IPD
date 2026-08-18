
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.ControlUtils




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class ControlUtils
    {
      public static int GetBottomYCoordinate(this Control control)
      {
        return control.Location.Y + control.Height;
      }

      public static int IndexByName(this ComboBox comboBox, string itemName)
      {
        for (int index = 0; index < comboBox.Items.Count; ++index)
        {
          if (comboBox.Items[index].ToString() == itemName)
            return index;
        }
        return -1;
      }

      public static void SetAllVisible(this ContextMenuStrip menu)
      {
        foreach (object obj in (ArrangedElementCollection) menu.Items)
        {
          if (obj is ToolStripMenuItem toolStripMenuItem)
            toolStripMenuItem.Visible = true;
        }
      }

      public static void SetYCoordinate(this Control control, int yCoord)
      {
        control.Location = new Point(control.Location.X, yCoord);
      }

      public static void SortByName(this ToolStripItemCollection items)
      {
        List<ToolStripMenuItem> list = items.OfType<ToolStripMenuItem>().OrderBy<ToolStripMenuItem, string>((Func<ToolStripMenuItem, string>) (x => x.Text)).ToList<ToolStripMenuItem>();
        items.Clear();
        Action<ToolStripMenuItem> action = (Action<ToolStripMenuItem>) (item => items.Add((ToolStripItem) item));
        list.ForEach(action);
      }

      public static void LoadLayouts(ComboBox comboBoxLayouts, List<LayoutDescriptor> layouts)
      {
        comboBoxLayouts.Items.Clear();
        comboBoxLayouts.Items.Add((object) new LayoutAsItIs());
        foreach (LayoutDescriptor layout in layouts)
          comboBoxLayouts.Items.Add((object) layout);
      }

      public static void LoadPrinters(ComboBox comboBoxPrinters, IList<string> printersOrder)
      {
        comboBoxPrinters.Items.Clear();
        foreach (string str in (IEnumerable<string>) printersOrder)
          comboBoxPrinters.Items.Add((object) str);
      }
    }
}

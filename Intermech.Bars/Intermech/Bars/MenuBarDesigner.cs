
// Type: Intermech.Bars.MenuBarDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.Bars
{
    internal class MenuBarDesigner : ToolBarDesigner
    {
      [Obsolete]
      public override void OnSetComponentDefaults()
      {
        base.OnSetComponentDefaults();
        IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        ToolBar component = (ToolBar) this.Component;
        MenuBarItem[] items = new MenuBarItem[5]
        {
          new MenuBarItem("&File"),
          new MenuBarItem("&Edit"),
          new MenuBarItem("&View"),
          new MenuBarItem("&Window"),
          new MenuBarItem("&Help")
        };
        component.Items.AddRange((ToolbarItemBase[]) items);
        for (int index = 0; index < component.Items.Count; ++index)
          service.Container.Add((IComponent) component.Items[index]);
        (component.Items[3] as MenuBarItem).MdiWindowList = true;
      }

      protected override Type[] DesignableTypes
      {
        get
        {
          return new Type[2]
          {
            typeof (MenuBarItem),
            typeof (ContextMenuBarItem)
          };
        }
      }
    }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapContextMenu
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Map
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class MapContextMenu : ContextMenu
    {
      private MapView myView;

      public MapContextMenu(MapView view)
      {
        this.myView = (MapView) null;
        this.myView = view != null ? view : throw new ArgumentException("MapView argument to MapContextMenu constructor must not be null");
      }

      public static MapView FindView(MenuItem m)
      {
        if (m != null)
        {
          Menu parent = m.Parent;
          switch (parent)
          {
            case MenuItem _:
              return MapContextMenu.FindView((MenuItem) parent);
            case MapContextMenu _:
              return ((MapContextMenu) parent).View;
          }
        }
        return (MapView) null;
      }

      public MapView View => this.myView;
    }
}

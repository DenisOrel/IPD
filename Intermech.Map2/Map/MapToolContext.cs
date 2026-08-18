// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolContext
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolContext : MapTool
    {
      [NonSerialized]
      private ContextMenu myBackgroundContextMenu;
      private bool mySingleSelection;

      public MapToolContext(MapView v)
        : base(v)
      {
        this.myBackgroundContextMenu = (ContextMenu) null;
        this.mySingleSelection = true;
      }

      public override bool CanStart() => this.LastInput.IsContextButton;

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        this.DoSelect(this.LastInput);
        this.View.DoContextClick(this.LastInput);
        this.StopTool();
      }

      public override void DoSelect(MapInputEventArgs evt)
      {
        if (this.SingleSelection || evt.Control || evt.Shift)
        {
          base.DoSelect(evt);
        }
        else
        {
          this.CurrentObject = this.View.PickObject(true, false, evt.DocPoint, true);
          if (this.CurrentObject == null)
          {
            this.Selection.Clear();
          }
          else
          {
            if (this.Selection.Contains(this.CurrentObject))
              return;
            this.Selection.Select(this.CurrentObject);
          }
        }
      }

      public override void Start()
      {
        ContextMenu contextMenu = this.View.ContextMenu;
        if (contextMenu == null)
          return;
        this.CurrentObject = this.View.PickObject(true, false, this.LastInput.DocPoint, false);
        if (this.CurrentObject == null)
          return;
        this.myBackgroundContextMenu = contextMenu;
        this.View.ContextMenu = (ContextMenu) null;
      }

      public override void Stop()
      {
        if (this.myBackgroundContextMenu != null)
        {
          this.View.ContextMenu = this.myBackgroundContextMenu;
          this.myBackgroundContextMenu = (ContextMenu) null;
        }
        this.CurrentObject = (MapObject) null;
      }

      public ContextMenu BackgroundContextMenu => this.myBackgroundContextMenu;

      public virtual bool SingleSelection
      {
        get => this.mySingleSelection;
        set => this.mySingleSelection = value;
      }
    }
}

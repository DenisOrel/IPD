// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapControl
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapControl : MapObject
    {
      public const int ChangedControlType = 1901;
      private System.Type myControlType;
      private MapObject myEditedObject;
      [NonSerialized]
      private Hashtable myMap;

      public MapControl()
      {
        this.myControlType = (System.Type) null;
        this.myEditedObject = (MapObject) null;
        this.myMap = (Hashtable) null;
      }

      public override void Changed(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.SuspendsUpdates)
          return;
        base.Changed(subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        if (subhint != 1003)
          return;
        IDictionaryEnumerator enumerator = this.Map.GetEnumerator();
        while (enumerator.MoveNext())
        {
          DictionaryEntry entry = enumerator.Entry;
          MapView key = (MapView) entry.Key;
          Control control = (Control) entry.Value;
          if (key != null && !this.CanView() && control != null)
            control.Visible = false;
        }
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        if (e.SubHint == 1901)
          this.ControlType = (System.Type) e.GetValue(undo);
        else
          base.ChangeValue(e, undo);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapControl mapControl = (MapControl) base.CopyObject(env);
        mapControl.myEditedObject = (MapObject) env[(object) this.myEditedObject];
        mapControl.myMap = (Hashtable) null;
        return (MapObject) mapControl;
      }

      /// <summary>Событие создание Control для редактирования из указанного типа</summary>
      public event MapControl.CreateControlEdit onCreateControl;

      [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Window=\"AllWindows\"/>\r\n</PermissionSet>\r\n")]
      public virtual Control CreateControl(MapView view)
      {
        System.Type controlType = this.ControlType;
        if (controlType == (System.Type) null)
          return (Control) null;
        Control control = (Control) null;
        try
        {
          if (this.onCreateControl != null)
            control = this.onCreateControl();
          if (control == null)
            control = (Control) Activator.CreateInstance(controlType);
        }
        catch (Exception ex)
        {
        }
        RectangleF bounds = this.Bounds;
        Rectangle view1 = view.ConvertDocToView(bounds);
        control.Bounds = view1;
        if (control is IMapControlObject mapControlObject)
        {
          mapControlObject.MapView = view;
          mapControlObject.MapControl = this;
        }
        return control;
      }

      public virtual void DisposeControl(Control comp, MapView view)
      {
        if (comp == null || view == null)
          return;
        if (view.EditControl != this)
        {
          view.RemoveMapControl(this, comp);
          comp.Dispose();
        }
        else
          comp.Visible = false;
      }

      public override void DoEndEdit(MapView view) => this.EditedObject?.DoEndEdit(view);

      public virtual Control FindControl(MapView view) => (Control) this.Map[(object) view];

      public virtual Control GetControl(MapView view)
      {
        Control control = this.FindControl(view);
        if (control == null)
        {
          control = this.CreateControl(view);
          if (control != null)
          {
            this.Map[(object) view] = (object) control;
            view.AddMapControl(this, control);
          }
        }
        return control;
      }

      protected override void OnLayerChanged(MapLayer oldLayer, MapLayer newLayer, MapObject mainObj)
      {
        base.OnLayerChanged(oldLayer, newLayer, mainObj);
        if (oldLayer != null && newLayer == null && oldLayer.IsInDocument)
        {
          MapDocument document = oldLayer.Document;
          IDictionaryEnumerator enumerator = this.Map.GetEnumerator();
          while (enumerator.MoveNext())
          {
            DictionaryEntry entry = enumerator.Entry;
            MapView key = (MapView) entry.Key;
            Control comp = (Control) entry.Value;
            if (key != null && comp != null)
              this.DisposeControl(comp, key);
          }
          this.Map.Clear();
        }
        else
        {
          if (oldLayer == null || newLayer != null || !oldLayer.IsInView)
            return;
          MapView view = oldLayer.View;
          Control control = this.FindControl(view);
          if (control == null)
            return;
          this.Map.Remove((object) view);
          this.DisposeControl(control, view);
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        Control control = this.GetControl(view);
        if (control == null)
          return;
        RectangleF bounds = this.Bounds;
        Rectangle view1 = view.ConvertDocToView(bounds);
        control.Bounds = view1;
        control.Visible = true;
      }

      [Description("The Type used to specify which Control to create when first displayed in a MapView.")]
      public virtual System.Type ControlType
      {
        get => this.myControlType;
        set
        {
          System.Type controlType = this.myControlType;
          if (!(controlType != value))
            return;
          this.myControlType = value;
          this.Changed(1901, 0, (object) controlType, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The MapObject for which this control is acting as an editor.")]
      public virtual MapObject EditedObject
      {
        get => this.myEditedObject;
        set => this.myEditedObject = value;
      }

      [Description("The Hashtable that maps MapViews to Controls for this MapControl.")]
      public Hashtable Map
      {
        get
        {
          if (this.myMap == null)
            this.myMap = new Hashtable();
          return this.myMap;
        }
      }

      public delegate Control CreateControlEdit();
    }
}

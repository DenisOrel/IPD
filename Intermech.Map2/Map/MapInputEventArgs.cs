// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapInputEventArgs
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapInputEventArgs : EventArgs
    {
      private MouseButtons myButtons;
      private int myDelta;
      private PointF myDocPoint;
      private bool myDoubleClick;
      private DragEventArgs myDragEventArgs;
      private Keys myKey;
      private KeyEventArgs myKeyEventArgs;
      private Keys myModifiers;
      private MouseEventArgs myMouseEventArgs;
      private Point myViewPoint;

      public MapInputEventArgs()
      {
        this.myButtons = MouseButtons.None;
        this.myModifiers = Keys.None;
        this.myKey = Keys.None;
        this.myMouseEventArgs = (MouseEventArgs) null;
        this.myDragEventArgs = (DragEventArgs) null;
        this.myKeyEventArgs = (KeyEventArgs) null;
        this.myDoubleClick = false;
        this.myDelta = 0;
      }

      public MapInputEventArgs(MapInputEventArgs evt)
      {
        this.myButtons = MouseButtons.None;
        this.myModifiers = Keys.None;
        this.myKey = Keys.None;
        this.myMouseEventArgs = (MouseEventArgs) null;
        this.myDragEventArgs = (DragEventArgs) null;
        this.myKeyEventArgs = (KeyEventArgs) null;
        this.myDoubleClick = false;
        this.myDelta = 0;
        this.ViewPoint = evt.ViewPoint;
        this.DocPoint = evt.DocPoint;
        this.Buttons = evt.Buttons;
        this.Modifiers = evt.Modifiers;
        this.Key = evt.Key;
        this.MouseEventArgs = evt.MouseEventArgs;
        this.DragEventArgs = evt.DragEventArgs;
        this.KeyEventArgs = evt.KeyEventArgs;
        this.DoubleClick = evt.DoubleClick;
        this.Delta = evt.Delta;
      }

      public virtual bool Alt => (this.Modifiers & Keys.Alt) == Keys.Alt;

      public MouseButtons Buttons
      {
        get => this.myButtons;
        set => this.myButtons = value;
      }

      public virtual bool Control => (this.Modifiers & Keys.Control) == Keys.Control;

      public int Delta
      {
        get => this.myDelta;
        set => this.myDelta = value;
      }

      public PointF DocPoint
      {
        get => this.myDocPoint;
        set => this.myDocPoint = value;
      }

      public bool DoubleClick
      {
        get => this.myDoubleClick;
        set => this.myDoubleClick = value;
      }

      public DragEventArgs DragEventArgs
      {
        get => this.myDragEventArgs;
        set => this.myDragEventArgs = value;
      }

      public virtual bool IsContextButton => this.Buttons == MouseButtons.Right;

      public Keys Key
      {
        get => this.myKey;
        set => this.myKey = value;
      }

      public KeyEventArgs KeyEventArgs
      {
        get => this.myKeyEventArgs;
        set => this.myKeyEventArgs = value;
      }

      public Keys Modifiers
      {
        get => this.myModifiers;
        set => this.myModifiers = value;
      }

      public MouseEventArgs MouseEventArgs
      {
        get => this.myMouseEventArgs;
        set => this.myMouseEventArgs = value;
      }

      public virtual bool Shift => (this.Modifiers & Keys.Shift) == Keys.Shift;

      public Point ViewPoint
      {
        get => this.myViewPoint;
        set => this.myViewPoint = value;
      }
    }
}

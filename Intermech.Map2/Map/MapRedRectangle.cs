// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRedRectangle
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Map
{
    /// <summary>прямоугольник для RedLines с пересчёт положения объектов относительно элемента в документе</summary>
    [Serializable]
    public class MapRedRectangle : MapShape, IMapRelativePosition, IMapTime, IMapToolTipText
    {
      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private IMapRelative _relative;
      /// <summary>ID элемента базового элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string _relativeId;
      /// <summary>получить базовую точку элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private SizeF _baseOffsetId = SizeF.Empty;
      /// <summary>смещение от базовой точки элемента к базовой точки этого MapObject</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private SizeF _offset = SizeF.Empty;
      public const int ChangedModificationTime = 1616;
      /// <summary>дата создания примитива</summary>
      private DateTime _createTime = DateTime.Now;
      /// <summary>дата последнего изменения примитива</summary>
      private DateTime _modificationTime = DateTime.Now;
      public const int ChangedToolTipText = 1618;
      private string myToolTipText;

      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      public IMapRelative Relative
      {
        get => this._relative;
        set => this._relative = value;
      }

      /// <summary>ID элемента базового элемента</summary>
      public string RelativeId
      {
        get => this._relativeId;
        set
        {
          string relativeId = this._relativeId;
          this._relativeId = value;
          this._baseOffsetId = this.RelativeId == null || this.Relative == null ? SizeF.Empty : new SizeF(this.Relative.GetBasePoint(this.RelativeId));
        }
      }

      /// <summary>получить базовую точку элемента</summary>
      public PointF BasePoint => this._baseOffsetId.ToPointF();

      [Description("Whether users can see this object.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public override bool Visible
      {
        get
        {
          bool visible = true;
          if (this.Relative != null && this.Relative.CheckElementId(this.RelativeId))
            visible = this.Relative.GetVisible(this.RelativeId);
          if (visible == base.Visible)
            return visible;
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          base.Visible = visible;
          this.SkipsUndoManager = skipsUndoManager;
          return visible;
        }
        set
        {
        }
      }

      /// <summary>проверить поменялась ли базовая точка элемента</summary>
      /// <returns>true - если смещение базовой точки поменялось</returns>
      private bool CheckOffsetThis()
      {
        if (this.Relative == null || this.RelativeId == null)
          return false;
        SizeF sizeF = new SizeF(this.Relative.GetBasePoint(this.RelativeId));
        SizeF offset = sizeF - this._baseOffsetId;
        if (offset == SizeF.Empty)
          return false;
        bool skipsUndoManager = this.SkipsUndoManager;
        this.SkipsUndoManager = true;
        this._baseOffsetId = sizeF;
        this.OffsetThis(offset);
        this.SkipsUndoManager = skipsUndoManager;
        return true;
      }

      /// <summary>сместить объект в указанную сторону </summary>
      /// <param name="offset">смещение базовой точки</param>
      private void OffsetThis(SizeF offset)
      {
        RectangleF bounds = base.Bounds;
        bounds.Offset(offset.Width, offset.Height);
        base.Bounds = bounds;
      }

      public override RectangleF Bounds
      {
        get
        {
          this.CheckOffsetThis();
          return base.Bounds;
        }
        set => base.Bounds = value;
      }

      /// <summary>дата создания примитива</summary>
      public DateTime CreateTime
      {
        get => this._createTime;
        set => this._createTime = value;
      }

      /// <summary>дата последнего изменения примитива</summary>
      public DateTime ModificationTime
      {
        get => this._modificationTime;
        set
        {
          DateTime modificationTime = this._modificationTime;
          if (!(modificationTime != value))
            return;
          this._modificationTime = value;
          this.Changed(1616, 0, (object) modificationTime, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override void Dispose()
      {
        this._relativeId = (string) null;
        this._relative = (IMapRelative) null;
        base.Dispose();
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1616:
            this.ModificationTime = (DateTime) e.GetValue(undo);
            break;
          case 1618:
            this.Initializing = true;
            this.ToolTipText = (string) e.GetValue(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      /// <summary>сформировать сведения о примитиве</summary>
      /// <returns>сведения о примитиве</returns>
      public string GenerateToolTipText()
      {
        this.ToolTipText = this.Layer == null || this.Layer.Identifier == null ? (string) null : this.Layer.Identifier.ToString();
        return this.ToolTipText;
      }

      /// <summary>сведения о примитиве </summary>
      [Description("A string to be displayed in a tooltip.")]
      public string ToolTipText
      {
        get => this.myToolTipText;
        set
        {
          if (!(this.myToolTipText != value))
            return;
          this.myToolTipText = value;
        }
      }

      public override string GetToolTip(MapView view) => this.ToolTipText;

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        IMapRelative relative = this.Relative;
      }

      public override void RemoveSelectionHandles(MapSelection sel) => base.RemoveSelectionHandles(sel);

      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawRectangle(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawRectangle(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
        }
        MapShape.DrawRectangle(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
      }
    }
}

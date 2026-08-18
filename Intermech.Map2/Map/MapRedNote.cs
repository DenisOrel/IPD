// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapRedNote
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public sealed class MapRedNote : MapGroup, IMapRelativePosition, IMapTime, IMapToolTipText
    {
      public const int ChangedTopLeftMargin = 3040;
      public const int ChangedBottomRightMargin = 3041;
      public const int ChangedBaseWidth = 3042;
      public const int ChangedPlaceNote = 3043;
      public const int ChangedFacet = 3044;
      public const int ChangedFontSize = 3045;
      public const int ChangedNoteStyle = 3046;
      public const int ChangedNoteArrow = 3047;
      public const int ChangedArrowSize = 3048;
      public const int PlaceNoteHandleID = 1033;
      public const int LocationNoteHandleID = 1034;
      /// <summary>ID элемента базового элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private string _relativeId;
      /// <summary>получить базовую точку элемента</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private PointF _baseOffsetId = PointF.Empty;
      private Color myTextColor;
      private MapShape.MapPenInfo myPenInfo;
      private MapShape.MapBrushInfo myBrushInfo;
      /// <summary>размер фаски</summary>
      private float _facet = 4f;
      /// <summary>размер шрифта</summary>
      private float _fontSize = 15f;
      /// <summary>имя шрифта</summary>
      private string _fontName = "Arial";
      /// <summary>стиль фаски</summary>
      private IRedNoteStyle _noteStyle;
      /// <summary>размер стрелки</summary>
      private float _arrowSize = 4f;
      /// <summary>стиль стрелки</summary>
      private IRedArrowStyle _noteArrow;
      private MapRoundedRectangle _box;
      private MapStroke _stroke;
      private MapPolygon _background;
      /// <summary>объект содержащий текст</summary>
      private MapRedNoteText _label;
      private PointF _place;
      private SizeF _topLeftMargin;
      private SizeF _bottomRightMargin;
      private float _baseWidth;
      /// <summary>тип класса Intermech.Redline.MapAttrMemoEdit</summary>
      private static readonly Lazy<System.Type> TypeMapAttrMemoEdit = new Lazy<System.Type>((Func<System.Type>) (() => System.Type.GetType("Intermech.Redline.MapAttrMemoEdit,Intermech.Client.Core", false)));
      private SizeF noteSize = SizeF.Empty;
      public const int ChangedModificationTime = 1616;
      /// <summary>дата создания примитива</summary>
      private DateTime _createTime = DateTime.Now;
      /// <summary>дата последнего изменения примитива</summary>
      private DateTime _modificationTime = DateTime.Now;
      public const int DepthHandleID = 1033;
      public const int ChangedToolTipText = 1618;
      private string myToolTipText;

      /// <summary>цвет текста</summary>
      [Description("The color of the text.")]
      [Category("Appearance")]
      public Color TextColor
      {
        [DebuggerStepThrough] get => this.myTextColor;
        set
        {
          Color textColor = this.myTextColor;
          if (!(textColor != value))
            return;
          this.myTextColor = value;
          this.Changed(1505, 0, (object) textColor, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      internal float InternalPenWidth => this.PenInfo != null ? this.PenInfo.Width : 0.0f;

      [Category("Appearance")]
      [Description("The pen used to draw the outline of this shape.")]
      public Pen Pen
      {
        get => this.myPenInfo != null ? this.myPenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo penInfo1 = this.myPenInfo;
          MapShape.MapPenInfo penInfo2 = MapShape.GetPenInfo(value);
          if (penInfo1 == penInfo2)
            return;
          this.myPenInfo = penInfo2;
          this.Changed(1101, 0, (object) penInfo1, MapObject.NullRect, 0, (object) penInfo2, MapObject.NullRect);
          if (this.Parent != null)
            this.Parent.InvalidatePaintBounds();
          this.LayoutChildren((MapObject) null);
        }
      }

      internal MapShape.MapPenInfo PenInfo => this.myPenInfo;

      /// <summary>цвет заливки пометки</summary>
      [Category("Appearance")]
      [Description("The brush used to fill the outline of this shape.")]
      public Brush Brush
      {
        get => this.myBrushInfo != null ? this.myBrushInfo.GetBrush() : (Brush) null;
        set
        {
          MapShape.MapBrushInfo brushInfo1 = this.myBrushInfo;
          MapShape.MapBrushInfo brushInfo2 = MapShape.GetBrushInfo(value);
          if (brushInfo1 == brushInfo2)
            return;
          this.myBrushInfo = brushInfo2;
          this.Changed(1102, 0, (object) brushInfo1, MapObject.NullRect, 0, (object) brushInfo2, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>размер фаски</summary>
      public float Facet
      {
        [DebuggerStepThrough] get => this._facet;
        set
        {
          float facet = this._facet;
          if ((double) facet == (double) value || (double) value < 0.0)
            return;
          this._facet = value;
          this.Changed(3044, 0, (object) null, MapObject.MakeRect(facet), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>размер шрифта</summary>
      public float FontSize
      {
        [DebuggerStepThrough] get => this._fontSize;
        set
        {
          float fontSize = this._fontSize;
          if ((double) fontSize == (double) value || (double) value < 0.0)
            return;
          this._fontSize = value;
          this.Changed(3045, 0, (object) null, MapObject.MakeRect(fontSize), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>имя шрифта</summary>
      public string FontName
      {
        [DebuggerStepThrough] get => this._fontName;
        set
        {
          string newVal = value ?? MapText.DefaultFontFamilyName;
          string fontName = this._fontName;
          if (!(fontName != newVal))
            return;
          this._fontName = newVal;
          this.Changed(1502, 0, (object) fontName, MapObject.NullRect, 0, (object) newVal, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>стиль фаски</summary>
      public IRedNoteStyle NoteStyle
      {
        [DebuggerStepThrough] get => this._noteStyle;
        set
        {
          IRedNoteStyle noteStyle = this._noteStyle;
          if (noteStyle == value)
            return;
          this._noteStyle = value;
          this.Changed(3046, 0, (object) noteStyle, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>размер стрелки</summary>
      public float ArrowSize
      {
        [DebuggerStepThrough] get => this._arrowSize;
        set
        {
          float arrowSize = this._arrowSize;
          if ((double) arrowSize == (double) value || (double) value < 0.0)
            return;
          this._arrowSize = value;
          this.Changed(3048, 0, (object) null, MapObject.MakeRect(arrowSize), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      /// <summary>стиль стрелки</summary>
      public IRedArrowStyle NoteArrow
      {
        [DebuggerStepThrough] get => this._noteArrow;
        set
        {
          IRedArrowStyle noteArrow = this._noteArrow;
          if (noteArrow == value)
            return;
          this._noteArrow = value;
          this.Changed(3047, 0, (object) noteArrow, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      public bool FontAutoScale
      {
        get => !this._label.AutoResizes && this._label.AutoRescales;
        set
        {
          this._label.AutoResizes = !value;
          this._label.AutoRescales = value;
        }
      }

      public void UpdateFontScale() => this._label.UpdateScale();

      public bool UseMillimeters
      {
        get
        {
          MapRedNoteText label = this._label;
          return label == null || label.UseMillimeters;
        }
        set
        {
          if (this._label == null)
            return;
          this._label.UseMillimeters = value;
        }
      }

      public MapRedNote()
      {
        this.Relative = (IMapRelative) null;
        this.myPenInfo = MapShape.GetPenInfo(MapShape.Pens_LightGray);
        this.myBrushInfo = MapShape.GetBrushInfo(MapShape.Brushes_LemonChiffon);
        this.myTextColor = Color.Black;
        this._baseWidth = 10f;
        this._facet = 4f;
        this._topLeftMargin = new SizeF(4f, 2f);
        this._bottomRightMargin = new SizeF(4f, 2f);
        this.InternalFlags |= 512 /*0x0200*/;
        MapStroke mapStroke1 = new MapStroke();
        mapStroke1.Selectable = false;
        mapStroke1.Pen = this.Pen;
        mapStroke1.Visible = false;
        MapStroke mapStroke2 = mapStroke1;
        this._stroke = mapStroke1;
        this.Add((MapObject) mapStroke2);
        MapRoundedRectangle roundedRectangle1 = new MapRoundedRectangle();
        roundedRectangle1.Shadowed = true;
        roundedRectangle1.Selectable = false;
        roundedRectangle1.Pen = this.Pen;
        roundedRectangle1.Brush = this.Brush;
        roundedRectangle1.Visible = false;
        MapRoundedRectangle roundedRectangle2 = roundedRectangle1;
        this._box = roundedRectangle1;
        this.Add((MapObject) roundedRectangle2);
        MapPolygon mapPolygon1 = new MapPolygon();
        mapPolygon1.Shadowed = true;
        mapPolygon1.Selectable = false;
        mapPolygon1.Pen = this.Pen;
        mapPolygon1.Brush = this.Brush;
        MapPolygon mapPolygon2 = mapPolygon1;
        this._background = mapPolygon1;
        this.Add((MapObject) mapPolygon2);
        MapRedNoteText mapRedNoteText1 = new MapRedNoteText();
        mapRedNoteText1.Selectable = false;
        mapRedNoteText1.Multiline = true;
        mapRedNoteText1.Editable = true;
        mapRedNoteText1.DropDownList = true;
        mapRedNoteText1.EditorStyle = MapTextEditorStyle.TextBox;
        mapRedNoteText1.FontSize = 15f;
        mapRedNoteText1.TextColor = this.TextColor;
        MapRedNoteText mapRedNoteText2 = mapRedNoteText1;
        this._label = mapRedNoteText1;
        this.Add((MapObject) mapRedNoteText2);
        this.Editable = true;
        this._label.onCreateControl += new MapControl.CreateControlEdit(MapRedNote.Label_OnCreateControl);
        this.FontSize = 15f;
      }

      public static event MapControl.CreateControlEdit OnCreateControl;

      private static Control Label_OnCreateControl()
      {
        return MapRedNote.OnCreateControl == null ? (Control) null : MapRedNote.OnCreateControl();
      }

      private Control Label_onCreateControl()
      {
        System.Type type = MapRedNote.TypeMapAttrMemoEdit.Value;
        return (Control) Convert.ChangeType(Activator.CreateInstance(MapRedNote.TypeMapAttrMemoEdit.Value), MapRedNote.TypeMapAttrMemoEdit.Value);
      }

      public override void Dispose()
      {
        this._relativeId = (string) null;
        this.Relative = (IMapRelative) null;
        base.Dispose();
      }

      /// <summary>сложный объект с  IDs  состовляющеми документ</summary>
      public IMapRelative Relative { get; set; }

      /// <summary>ID элемента базового элемента</summary>
      public string RelativeId
      {
        get => this._relativeId;
        set
        {
          string relativeId = this._relativeId;
          this._relativeId = value;
          this._baseOffsetId = this._relativeId == null || this.Relative == null ? PointF.Empty : this.Relative.GetBasePoint(this._relativeId);
        }
      }

      /// <summary>получить базовую точку элемента</summary>
      public PointF BasePoint => this._baseOffsetId;

      /// <summary>проверить поменялась ли базовая точка элемента</summary>
      /// <returns>true - если смещение базовой точки поменялось</returns>
      private bool CheckOffsetThis()
      {
        if (this.Relative == null || this.RelativeId == null)
          return false;
        PointF basePoint = this.Relative.GetBasePoint(this.RelativeId);
        SizeF offset = new SizeF(basePoint) - new SizeF(this._baseOffsetId);
        if (offset == SizeF.Empty)
          return false;
        bool skipsUndoManager = this.SkipsUndoManager;
        this.SkipsUndoManager = true;
        this.Initializing = true;
        this._baseOffsetId = basePoint;
        this.OffsetThis(offset);
        this.InvalidBounds = true;
        this.Initializing = false;
        this.SkipsUndoManager = skipsUndoManager;
        return true;
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

      protected override RectangleF ComputeBounds()
      {
        RectangleF a = this.Bounds;
        bool flag = false;
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          if (mapObject.CanView())
          {
            if (!flag)
            {
              a = mapObject.Bounds;
              flag = true;
            }
            else
              a = MapObject.UnionRect(a, mapObject.Bounds);
          }
        }
        return a;
      }

      /// <summary>сместить объект в указанную сторону </summary>
      /// <param name="offset">смещение базовой точки</param>
      private void OffsetThis(SizeF offset)
      {
        this.Location = this.Location + offset;
        this._place += offset;
      }

      public override bool Editable
      {
        get => base.Editable;
        set
        {
          base.Editable = value;
          if (this._label == null)
            return;
          this._label.Editable = value;
        }
      }

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

      /// <summary></summary>
      public PointF NoteLocation
      {
        get => this._label.Location;
        set => this._label.Location = value;
      }

      /// <summary></summary>
      public SizeF NoteSize
      {
        get => !(this.noteSize == SizeF.Empty) ? this.noteSize : this._label.Size;
        set
        {
          this.noteSize = value;
          if (value == SizeF.Empty)
            this.noteSize = this._label.Size;
          this._label.Size = this.noteSize;
        }
      }

      /// <summary>текст </summary>
      public string Text
      {
        get => this._label.Text;
        set => this._label.Text = value;
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

      public override bool OnContextClick(MapInputEventArgs evt, MapView view) => false;

      /// <summary>проверка:  точка попала внутрь объекта ?</summary>
      /// <param name="p">проверяемая точка</param>
      /// <returns>true -точка попала внутрь объекта</returns>
      public override bool ContainsPoint(PointF p)
      {
        RectangleF rectangleF1 = new RectangleF(this.NoteLocation, this.NoteSize);
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        RectangleF rectangleF2 = new RectangleF(this._label.Left - topLeftMargin.Width, this._label.Top - topLeftMargin.Height, this._label.Width + topLeftMargin.Width + bottomRightMargin.Width, this._label.Height + topLeftMargin.Height + bottomRightMargin.Height);
        return (double) rectangleF2.X <= (double) p.X && (double) p.X <= (double) rectangleF2.X + (double) rectangleF2.Width && (double) rectangleF2.Y <= (double) p.Y && (double) p.Y <= (double) rectangleF2.Y + (double) rectangleF2.Height;
      }

      public override void RemoveSelectionHandles(MapSelection sel)
      {
        sel.RemoveHandles((MapObject) this);
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        this.RemoveSelectionHandles(sel);
        if (!this.CanReshape())
          return;
        RectangleF bounds1 = this.Bounds;
        PointF placeNote = this.PlaceNote;
        sel.CreateResizeHandle((MapObject) this, selectedObj, placeNote, 1033, true);
        PointF noteLocation = this.NoteLocation;
        IMapHandle resizeHandle = sel.CreateResizeHandle((MapObject) this, selectedObj, noteLocation, 1034, true);
        if (!(resizeHandle.MapObject is MapHandle))
          return;
        MapHandle mapObject = resizeHandle.MapObject as MapHandle;
        mapObject.Style = MapHandleStyle.Diamond;
        mapObject.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds2 = mapObject.Bounds;
        MapObject.InflateRect(ref bounds2, bounds2.Height * 0.3f, bounds2.Width * 0.3f);
        mapObject.Bounds = bounds2;
      }

      public override void DoResize(
        MapView view,
        RectangleF origRect,
        PointF newPoint,
        int whichHandle,
        MapInputState evttype,
        SizeF min,
        SizeF max)
      {
        if (whichHandle == 1033 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          this.PlaceNote = newPoint;
          this.LayoutChildren((MapObject) null);
        }
        else if (whichHandle == 1034 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          this.NoteLocation = newPoint;
          this.LayoutChildren((MapObject) null);
        }
        else
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1101:
            object obj1 = e.GetValue(undo);
            switch (obj1)
            {
              case Pen _:
                this.Pen = (Pen) obj1;
                if (this._background != null)
                  this._background.Pen = this.Pen;
                if (this._box != null)
                  this._box.Pen = this.Pen;
                if (this._stroke == null)
                  return;
                this._stroke.Pen = this.Pen;
                return;
              case MapShape.MapPenInfo _:
                this.Pen = ((MapShape.MapPenInfo) obj1).GetPen();
                return;
              default:
                return;
            }
          case 1102:
            object obj2 = e.GetValue(undo);
            switch (obj2)
            {
              case Brush _:
                this.Brush = (Brush) obj2;
                if (this._background != null)
                  this._background.Brush = this.Brush;
                if (this._box == null)
                  return;
                this._box.Brush = this.Brush;
                return;
              case MapShape.MapBrushInfo _:
                this.Brush = ((MapShape.MapBrushInfo) obj2).GetBrush();
                return;
              default:
                return;
            }
          case 1501:
            this.Initializing = true;
            this.Text = (string) e.GetValue(undo);
            this.LayoutChildren((MapObject) null);
            this.Initializing = false;
            break;
          case 1502:
            this.Initializing = true;
            this.FontName = (string) e.GetValue(undo);
            this.Initializing = false;
            break;
          case 1505:
            this.TextColor = (Color) e.GetValue(undo);
            if (this._label == null)
              break;
            this._label.TextColor = this.TextColor;
            break;
          case 1616:
            this.ModificationTime = (DateTime) e.GetValue(undo);
            this.GenerateToolTipText();
            break;
          case 1618:
            this.Initializing = true;
            this.ToolTipText = (string) e.GetValue(undo);
            this.Initializing = false;
            break;
          case 3040:
            this.Initializing = true;
            this.TopLeftMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 3041:
            this.Initializing = true;
            this.BottomRightMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 3042:
            this.Initializing = true;
            this.BaseWidth = e.GetFloat(undo);
            this.Initializing = false;
            break;
          case 3043:
            this.Initializing = true;
            this.PlaceNote = e.GetPoint(undo);
            this.Initializing = false;
            break;
          case 3044:
            this.Initializing = true;
            this.Facet = e.GetFloat(undo);
            this.Initializing = false;
            break;
          case 3045:
            this.Initializing = true;
            this.FontSize = e.GetFloat(undo);
            this.Initializing = false;
            break;
          case 3046:
            this.Initializing = true;
            this.NoteStyle = (IRedNoteStyle) e.GetValue(undo);
            this.Initializing = false;
            break;
          case 3047:
            this.Initializing = true;
            this.NoteArrow = (IRedArrowStyle) e.GetValue(undo);
            this.Initializing = false;
            break;
          case 3048:
            this.Initializing = true;
            this.ArrowSize = e.GetFloat(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            this.GenerateToolTipText();
            break;
        }
      }

      /// <summary>сформировать сведения о примитиве</summary>
      /// <returns>сведения о примитиве</returns>
      public string GenerateToolTipText()
      {
        this.ToolTipText = (string) null;
        if (this.Layer == null)
          return (string) null;
        return this.Layer.Identifier == null ? (string) null : (this.ToolTipText = $"{this.Layer.Identifier.ToString()}\r----------\n{this.Text}");
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

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapRedNote mapRedNote = (MapRedNote) newgroup;
        mapRedNote._stroke = (MapStroke) env[(object) this._stroke];
        mapRedNote._box = (MapRoundedRectangle) env[(object) this._box];
        mapRedNote._background = (MapPolygon) env[(object) this._background];
        mapRedNote._label = (MapRedNoteText) env[(object) this._label];
      }

      public override void DoBeginEdit(MapView view)
      {
        if (this._label == null)
          return;
        this._label.DoBeginEdit(view);
        this.GenerateToolTipText();
      }

      public override void DoEndEdit(MapView view)
      {
        if (this._label == null)
          return;
        this._label.DoEndEdit(view);
        this.GenerateToolTipText();
      }

      public override bool OnSingleClick(MapInputEventArgs evt, MapView view)
      {
        int num = base.OnSingleClick(evt, view) ? 1 : 0;
        this.GenerateToolTipText();
        return num != 0;
      }

      private RectangleF GetTextBox() => new RectangleF(this.NoteLocation, this.NoteSize);

      private RectangleF TextBox()
      {
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        return new RectangleF(this._label.Left - topLeftMargin.Width, this._label.Top - topLeftMargin.Height, this._label.Width + topLeftMargin.Width + bottomRightMargin.Width, this._label.Height + topLeftMargin.Height + bottomRightMargin.Height);
      }

      /// <summary>линия коментария</summary>
      private void ComputeStroke(RectangleF rect)
      {
        PointF p2 = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        PointF placeNote = this.PlaceNote;
        PointF result;
        MapObject.GetNearestIntersectionPoint(rect, placeNote, p2, out result);
        this._stroke.Visible = false;
        this._stroke.Pen = this.Pen;
        this._stroke.ClearPoints();
        PointF pointF1 = new PointF(result.X - p2.X, result.Y - p2.Y);
        PointF pointF2 = new PointF(placeNote.X - p2.X, placeNote.Y - p2.Y);
        if ((double) pointF1.X * (double) pointF1.X + (double) pointF1.Y * (double) pointF1.Y >= (double) pointF2.X * (double) pointF2.X + (double) pointF2.Y * (double) pointF2.Y)
          return;
        float num = 5f;
        if ((double) Math.Abs(result.X - rect.Left) < 0.1)
        {
          this._stroke.AddPoint(new PointF(result.X, p2.Y));
          this._stroke.AddPoint(new PointF(result.X - num, p2.Y));
          this._stroke.AddPoint(placeNote);
          this._stroke.Visible = true;
        }
        else if ((double) Math.Abs(result.X - rect.Right) < 0.1)
        {
          this._stroke.AddPoint(new PointF(result.X, p2.Y));
          this._stroke.AddPoint(new PointF(result.X + num, p2.Y));
          this._stroke.AddPoint(placeNote);
          this._stroke.Visible = true;
        }
        else if ((double) Math.Abs(result.Y - rect.Top) < 0.1)
        {
          this._stroke.AddPoint(new PointF(p2.X, result.Y));
          this._stroke.AddPoint(new PointF(p2.X, result.Y - num));
          this._stroke.AddPoint(placeNote);
          this._stroke.Visible = true;
        }
        else
        {
          if ((double) Math.Abs(result.Y - rect.Bottom) >= 0.1)
            return;
          this._stroke.AddPoint(new PointF(p2.X, result.Y));
          this._stroke.AddPoint(new PointF(p2.X, result.Y + num));
          this._stroke.AddPoint(placeNote);
          this._stroke.Visible = true;
        }
      }

      private void CleanPoligon()
      {
        this._background.Visible = false;
        this._background.ClearPoints();
      }

      private void CreateBoxText(RectangleStyle style, RectangleF rect)
      {
        this._box.Visible = false;
        this._box.Bounds = rect;
        this._box.Style = style;
        this._box.Corner = new SizeF(this.Facet, this.Facet);
        this._box.Pen = this.Pen;
        this._box.Brush = this.Brush;
        this._box.Visible = true;
      }

      private void OldStyle_Contur()
      {
        if (this._background == null)
          return;
        this._background.Visible = true;
        this._box.Visible = false;
        this._stroke.Visible = false;
        this._background.Pen = this.Pen;
        this._background.Brush = this.Brush;
        RectangleF rectangleF = this.TextBox();
        float num1 = Math.Min(this.Facet, rectangleF.Width / 2f);
        float num2 = Math.Min(this.Facet, rectangleF.Height / 2f);
        float x1 = rectangleF.X;
        float y1 = rectangleF.Y;
        float x2 = x1 + num1;
        float y2 = y1 + num2;
        float num3 = x1 + rectangleF.Width / 2f;
        float num4 = y1 + rectangleF.Height / 2f;
        float x3 = x1 + rectangleF.Width;
        float y3 = y1 + rectangleF.Height;
        float x4 = x3 - num1;
        float y4 = y3 - num2;
        RectangleF bounds = this._background.Bounds;
        bool suspendsUpdates = this.SuspendsUpdates;
        if (!suspendsUpdates)
          this._background.Changing(1412);
        this._background.SuspendsUpdates = true;
        this._background.ClearPoints();
        float num5 = Math.Min(rectangleF.Width - num1, this.BaseWidth);
        float num6 = Math.Min(rectangleF.Height - num2, this.BaseWidth);
        float left = this._label.Left;
        float top = this._label.Top;
        float right = this._label.Right;
        float bottom = this._label.Bottom;
        PointF center = this._label.Center;
        PointF placeNote = this.PlaceNote;
        PointF result;
        this._label.GetNearestIntersectionPoint(placeNote, center, out result);
        PointF p = placeNote;
        if ((double) result.Y <= (double) top && (double) result.X < (double) num3)
        {
          this._background.AddPoint(x1, y1);
          this._background.AddPoint(p);
          this._background.AddPoint(x1 + num5, y1);
        }
        else
          this._background.AddPoint(x2, y1);
        if ((double) result.Y <= (double) top && (double) result.X >= (double) num3)
        {
          this._background.AddPoint(x3 - num5, y1);
          this._background.AddPoint(p);
          this._background.AddPoint(x3, y1);
        }
        else
          this._background.AddPoint(x4, y1);
        if ((double) result.X >= (double) right & (double) result.Y < (double) num4)
        {
          this._background.AddPoint(x3, y1);
          this._background.AddPoint(p);
          this._background.AddPoint(x3, y1 + num6);
        }
        else
          this._background.AddPoint(x3, y2);
        if ((double) result.X >= (double) right & (double) result.Y >= (double) num4)
        {
          this._background.AddPoint(x3, y3 - num6);
          this._background.AddPoint(p);
          this._background.AddPoint(x3, y3);
        }
        else
          this._background.AddPoint(x3, y4);
        if ((double) result.Y >= (double) bottom && (double) result.X >= (double) num3)
        {
          this._background.AddPoint(x3, y3);
          this._background.AddPoint(p);
          this._background.AddPoint(x3 - num5, y3);
        }
        else
          this._background.AddPoint(x4, y3);
        if ((double) result.Y >= (double) bottom && (double) result.X < (double) num3)
        {
          this._background.AddPoint(x1 + num5, y3);
          this._background.AddPoint(p);
          this._background.AddPoint(x1, y3);
        }
        else
          this._background.AddPoint(x2, y3);
        if ((double) result.X <= (double) left && (double) result.Y >= (double) num4)
        {
          this._background.AddPoint(x1, y3);
          this._background.AddPoint(p);
          this._background.AddPoint(x1, y3 - num6);
        }
        else
          this._background.AddPoint(x1, y4);
        if ((double) result.X <= (double) left && (double) result.Y < (double) num4)
        {
          this._background.AddPoint(x1, y1 + num6);
          this._background.AddPoint(p);
          this._background.AddPoint(x1, y1);
        }
        else
          this._background.AddPoint(x1, y2);
        this._background.SuspendsUpdates = suspendsUpdates;
        if (suspendsUpdates)
          return;
        this._background.Changed(1412, 0, (object) null, bounds, 0, (object) null, this._background.Bounds);
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing || this._label == null)
          return;
        this._label.FontSize = this.FontSize;
        this._label.FamilyName = this.FontName;
        this._label.TextColor = this.TextColor;
        this.GenerateToolTipText();
        if (this._background == null || childchanged == this._background || this._box == null || childchanged == this._box || this._stroke == null || childchanged == this._stroke)
          return;
        RectangleF rect = this.TextBox();
        switch (this.NoteStyle)
        {
          case IRedNoteStyle.None:
            this.CleanPoligon();
            this._box.Visible = false;
            this._stroke.Visible = false;
            break;
          case IRedNoteStyle.Box:
            this.CleanPoligon();
            this.CreateBoxText(RectangleStyle.Box, rect);
            this.ComputeStroke(rect);
            break;
          case IRedNoteStyle.BoxFacet:
            this.CleanPoligon();
            this.CreateBoxText(RectangleStyle.BoxFacet, rect);
            this.ComputeStroke(rect);
            break;
          case IRedNoteStyle.BoxBluntPoint:
            this.CleanPoligon();
            this.CreateBoxText(RectangleStyle.BoxBluntPoint, rect);
            this.ComputeStroke(rect);
            break;
          case IRedNoteStyle.OldStyle:
            this.OldStyle_Contur();
            break;
        }
      }

      public override void Remove(MapObject obj)
      {
        if (obj == this._label)
          this._label = (MapRedNoteText) null;
        if (obj == this._background)
          this._background = (MapPolygon) null;
        if (obj == this._box)
          this._box = (MapRoundedRectangle) null;
        if (obj == this._stroke)
          this._stroke = (MapStroke) null;
        base.Remove(obj);
      }

      [Description("The margin around the text inside the background at the right side and the bottom")]
      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public SizeF BottomRightMargin
      {
        get => this._bottomRightMargin;
        set
        {
          SizeF bottomRightMargin = this._bottomRightMargin;
          if (!(bottomRightMargin != value))
            return;
          this._bottomRightMargin = value;
          this.Changed(3041, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public override bool Shadowed
      {
        get => this._background != null ? this._background.Shadowed : base.Shadowed;
        set
        {
          if (this._background != null)
            this._background.Shadowed = value;
          else
            base.Shadowed = value;
        }
      }

      [Category("Appearance")]
      [Description("The margin around the text inside the background at the left side and the top")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public SizeF TopLeftMargin
      {
        get => this._topLeftMargin;
        set
        {
          SizeF topLeftMargin = this._topLeftMargin;
          if (!(topLeftMargin != value))
            return;
          this._topLeftMargin = value;
          this.Changed(3040, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapRedNote mapRedNote = (MapRedNote) base.CopyObject(env);
        if (mapRedNote == null)
          return (MapObject) mapRedNote;
        env.Delayeds.Add((object) this);
        return (MapObject) mapRedNote;
      }

      public override void CopyObjectDelayed(MapCopyDictionary env, MapObject newobj)
      {
        base.CopyObjectDelayed(env, newobj);
        ((MapGroup) newobj).LayoutChildren((MapObject) null);
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        MapObject background = (MapObject) this._background;
        if (background != null && background.CanView())
        {
          rect = MapObject.UnionRect(rect, background.Bounds);
          rect = background.ExpandPaintBounds(rect, view);
        }
        return rect;
      }

      protected override void MoveChildren(RectangleF old)
      {
        base.MoveChildren(old);
        this.LayoutChildren((MapObject) null);
      }

      protected override void OnObservedChanged(
        MapObject observed,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        base.OnObservedChanged(observed, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        if (subhint != 1001)
          return;
        this.LayoutChildren((MapObject) null);
      }

      [Category("Appearance")]
      [Description("The margin around the text inside the background at the left side and the top")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public PointF PlaceNote
      {
        get => this._place;
        set
        {
          PointF place = this._place;
          if (!(place != value))
            return;
          this._place = value;
          this.Changed(3043, 0, (object) null, MapObject.MakeRect(place), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      [Category("Appearance")]
      [DefaultValue(10)]
      [Description("The width of the base of the balloon's pointer")]
      public float BaseWidth
      {
        get => this._baseWidth;
        set
        {
          float baseWidth = this._baseWidth;
          if ((double) baseWidth == (double) value || (double) value <= 0.0)
            return;
          this._baseWidth = value;
          this.Changed(3042, 0, (object) null, MapObject.MakeRect(baseWidth), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }
    }
}

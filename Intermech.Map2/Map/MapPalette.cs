// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPalette
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Map
{
    [ToolboxBitmap(typeof (MapPalette), "Intermech.Map.MapPalette.bmp")]
    public class MapPalette : MapView
    {
      internal static readonly IComparer AlphabeticNodeTextComparer = (IComparer) new MapPalette.AlphaComparer();
      private bool myAlignsSelectionObject;
      private bool myAutomaticLayout;
      private IComparer myComparer;
      protected Orientation myOrientation;
      private SortOrder mySorting;

      public MapPalette()
      {
        this.myOrientation = Orientation.Vertical;
        this.mySorting = SortOrder.Ascending;
        this.myComparer = MapPalette.AlphabeticNodeTextComparer;
        this.myAlignsSelectionObject = true;
        this.myAutomaticLayout = true;
        this.ShowsNegativeCoordinates = false;
        this.SetModifiable(false);
        this.AutoScrollRegion = new Size();
        if (this.InitAllowDrop(true))
          this.AllowCopy = true;
        else
          this.AllowCopy = false;
        base.GridCellSize = new SizeF(60f, 60f);
        base.GridOrigin = new PointF(20f, 5f);
        (this.FindMouseTool(typeof (MapToolDragging)) as MapToolDragging).HidesSelectionHandles = false;
      }

      public virtual void LayoutItems()
      {
        if (!this.AutomaticLayout)
          return;
        bool flag1 = this.Orientation == Orientation.Vertical;
        if (flag1)
        {
          this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
          this.ShowVerticalScrollBar = MapViewScrollBarVisibility.IfNeeded;
        }
        else
        {
          this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.IfNeeded;
          this.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
        }
        ICollection collection = (ICollection) this.Document;
        if (this.Sorting != SortOrder.None && this.Comparer != null)
        {
          MapObject[] mapObjectArray = this.Document.CopyArray();
          Array.Sort((Array) mapObjectArray, 0, mapObjectArray.Length, this.Comparer);
          if (this.Sorting == SortOrder.Descending)
            Array.Reverse((Array) mapObjectArray, 0, mapObjectArray.Length);
          collection = (ICollection) mapObjectArray;
        }
        SizeF docExtentSize = this.DocExtentSize;
        SizeF gridCellSize = this.GridCellSize;
        PointF gridOrigin = this.GridOrigin;
        bool alignsSelectionObject = this.AlignsSelectionObject;
        bool flag2 = true;
        PointF pnt = gridOrigin;
        float num1 = Math.Min(gridOrigin.X, 0.0f);
        float num2 = Math.Min(gridOrigin.Y, 0.0f);
        foreach (MapObject mapObject in (IEnumerable) collection)
        {
          MapObject selobj = mapObject;
          if (alignsSelectionObject)
            selobj = mapObject.SelectionObject ?? mapObject;
          selobj.Position = pnt;
          if (flag1)
          {
            pnt = this.ShiftRight(mapObject, selobj, num1, pnt, gridCellSize);
            if (!flag2 && (double) mapObject.Right >= (double) docExtentSize.Width)
            {
              num1 = Math.Min(gridOrigin.X, 0.0f);
              pnt.X = gridOrigin.X;
              pnt.Y = Math.Max(pnt.Y + gridCellSize.Height, num2);
              selobj.Position = pnt;
              pnt = this.ShiftRight(mapObject, selobj, num1, pnt, gridCellSize);
            }
            pnt.X += gridCellSize.Width;
          }
          else
          {
            pnt = this.ShiftDown(mapObject, selobj, num2, pnt, gridCellSize);
            if (!flag2 && (double) mapObject.Bottom >= (double) docExtentSize.Height)
            {
              num2 = Math.Min(gridOrigin.Y, 0.0f);
              pnt.Y = gridOrigin.Y;
              pnt.X += Math.Max(pnt.X + gridCellSize.Width, num1);
              selobj.Position = pnt;
              pnt = this.ShiftDown(mapObject, selobj, num2, pnt, gridCellSize);
            }
            pnt.Y += gridCellSize.Height;
          }
          num1 = Math.Max(num1, mapObject.Right);
          num2 = Math.Max(num2, mapObject.Bottom);
          flag2 = false;
        }
        RectangleF documentBounds = this.ComputeDocumentBounds();
        this.Document.Size = new SizeF(documentBounds.Width, documentBounds.Height);
        this.Document.TopLeft = new PointF(documentBounds.X, documentBounds.Y);
      }

      protected override void OnDocumentChanged(object sender, MapChangedEventArgs e)
      {
        base.OnDocumentChanged(sender, e);
        if (e.Hint != 902 && e.Hint != 903)
          return;
        this.LayoutItems();
      }

      protected override void OnPropertyChanged(PropertyChangedEventArgs evt)
      {
        base.OnPropertyChanged(evt);
        if (!(evt.PropertyName == "DocScale"))
          return;
        this.LayoutItems();
      }

      protected override void OnSizeChanged(EventArgs evt)
      {
        base.OnSizeChanged(evt);
        this.LayoutItems();
      }

      private PointF ShiftDown(
        MapObject obj,
        MapObject selobj,
        float maxrow,
        PointF pnt,
        SizeF cellsize)
      {
        while ((double) obj.Top < (double) maxrow)
        {
          pnt.Y += cellsize.Height;
          float top = obj.Top;
          selobj.Top = pnt.Y;
          if ((double) obj.Top <= (double) top)
            break;
        }
        return pnt;
      }

      private PointF ShiftRight(
        MapObject obj,
        MapObject selobj,
        float maxcol,
        PointF pnt,
        SizeF cellsize)
      {
        while ((double) obj.Left < (double) maxcol)
        {
          pnt.X += cellsize.Width;
          float left = obj.Left;
          selobj.Left = pnt.X;
          if ((double) obj.Left <= (double) left)
            break;
        }
        return pnt;
      }

      [DefaultValue(true)]
      [Description("Whether to grid-align each whole item or each item's SelectionObject")]
      [Category("Appearance")]
      public virtual bool AlignsSelectionObject
      {
        get => this.myAlignsSelectionObject;
        set
        {
          if (this.myAlignsSelectionObject == value)
            return;
          this.myAlignsSelectionObject = value;
          this.LayoutItems();
          this.RaisePropertyChangedEvent(nameof (AlignsSelectionObject));
        }
      }

      [Category("Appearance")]
      [DefaultValue(true)]
      [Description("Whether to automatically position all of the items in a grid")]
      public virtual bool AutomaticLayout
      {
        get => this.myAutomaticLayout;
        set
        {
          if (this.myAutomaticLayout == value)
            return;
          this.myAutomaticLayout = value;
          this.LayoutItems();
          this.RaisePropertyChangedEvent(nameof (AutomaticLayout));
        }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public virtual IComparer Comparer
      {
        get => this.myComparer;
        set
        {
          IComparer comparer1 = this.myComparer;
          if (value == null)
            value = MapPalette.AlphabeticNodeTextComparer;
          IComparer comparer2 = value;
          if (comparer1 == comparer2)
            return;
          this.myComparer = value;
          this.LayoutItems();
          this.RaisePropertyChangedEvent(nameof (Comparer));
        }
      }

      public override SizeF GridCellSize
      {
        get => base.GridCellSize;
        set
        {
          base.GridCellSize = value;
          this.LayoutItems();
        }
      }

      public override PointF GridOrigin
      {
        get => base.GridOrigin;
        set
        {
          base.GridOrigin = value;
          this.LayoutItems();
        }
      }

      [Description("How to fill the palette by positioning its items.")]
      [DefaultValue(1)]
      [Category("Appearance")]
      public virtual Orientation Orientation
      {
        get => this.myOrientation;
        set
        {
          if (this.myOrientation == value)
            return;
          this.myOrientation = value;
          this.LayoutItems();
          this.RaisePropertyChangedEvent(nameof (Orientation));
        }
      }

      [Category("Appearance")]
      [DefaultValue(1)]
      [Description("Whether the items in the palette are sorted before being positioned.")]
      public virtual SortOrder Sorting
      {
        get => this.mySorting;
        set
        {
          if (this.mySorting == value)
            return;
          this.mySorting = value;
          this.LayoutItems();
          this.RaisePropertyChangedEvent(nameof (Sorting));
        }
      }

      [Serializable]
      internal sealed class AlphaComparer : IComparer
      {
        private CultureInfo myCultureInfo;

        internal AlphaComparer() => this.myCultureInfo = CultureInfo.CurrentCulture;

        public int Compare(object x, object y)
        {
          IMapLabeledNode mapLabeledNode1 = x as IMapLabeledNode;
          IMapLabeledNode mapLabeledNode2 = y as IMapLabeledNode;
          return mapLabeledNode1 != null ? (mapLabeledNode2 != null ? string.Compare(mapLabeledNode1.Text, mapLabeledNode2.Text, true, this.myCultureInfo) : 1) : (mapLabeledNode2 != null ? -1 : 0);
        }

        public CultureInfo Culture
        {
          get => this.myCultureInfo;
          set => this.myCultureInfo = value;
        }
      }
    }
}

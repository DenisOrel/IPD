// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EnhListView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for EnhListView.</summary>
public class EnhListView : ListView
{
  private bool _radioGroups;
  private ImageList _subitemImages;
  private bool _customSorter;
  private bool _allowManualSorting = true;
  private int _sortColumn;
  private IntPtr hWndHeader;
  private static Dictionary<SortOrder, int> _sortFlags;
  private bool inChecked;
  protected List<Control> _subControls = new List<Control>();
  protected List<ControlListViewSubItem> _subControlItems = new List<ControlListViewSubItem>();
  private int _savedPos = -1;
  private bool? hasInvalid;

  public EnhListView()
  {
    this.OwnerDraw = true;
    this.DrawSubItem += new DrawListViewSubItemEventHandler(this.CustomDrawSubItem);
    this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.CustomDrawColumnHeader);
    this.MouseClick += new MouseEventHandler(this.EnhListView_MouseClick);
  }

  private void EnhListView_MouseClick(object sender, MouseEventArgs e)
  {
    ListViewItem itemAt = this.GetItemAt(e.X, e.Y);
    if (itemAt == null || !(itemAt.GetSubItemAt(e.X, e.Y) is IClickTarget subItemAt))
      return;
    subItemAt.MouseClick(e);
  }

  public bool RadioGroups
  {
    get => this._radioGroups;
    set
    {
      if (this._radioGroups == value)
        return;
      this._radioGroups = value;
      if (!(!this.DesignMode & value) || this.StateImageList != null)
        return;
      this.StateImageList = StateList.RadioImageList;
      this.CheckBoxes = true;
    }
  }

  private void CustomDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
  {
    e.DrawDefault = true;
  }

  public ImageList SubitemImages
  {
    get => this._subitemImages;
    set => this._subitemImages = value;
  }

  internal ImageList GetSubImages()
  {
    return this.SubitemImages != null ? this.SubitemImages : this.SmallImageList;
  }

  /// <summary>
  /// Если true, то означает, что для сортировки был присвоен кастомный IComparer, юзер не сможет изменить сортировку.
  /// </summary>
  protected bool CustomSorter => this._customSorter;

  public new IComparer ListViewItemSorter
  {
    get => base.ListViewItemSorter;
    set
    {
      if (base.ListViewItemSorter == value)
        return;
      base.ListViewItemSorter = value;
      this._customSorter = true;
      this.UpdateSortedHeaderImage();
    }
  }

  /// <summary>
  /// Разрешить или нет пользователю изменять порядок/колонку для сортировки кликом мыши на колонке.
  /// Если false, то значок сортировки не отображается.
  /// </summary>
  public bool AllowManualSorting
  {
    get => this._allowManualSorting;
    set
    {
      if (this._allowManualSorting == value)
        return;
      this._allowManualSorting = value;
      this.UpdateSortedHeaderImage();
    }
  }

  private void UpdateSorting()
  {
    if (this.CustomSorter)
      return;
    if (this.Sorting != SortOrder.None)
      base.ListViewItemSorter = (IComparer) new EnhListView.ListViewItemComparer(this);
    else
      base.ListViewItemSorter = (IComparer) null;
  }

  public new SortOrder Sorting
  {
    get => base.Sorting;
    set
    {
      base.Sorting = value;
      this.UpdateSorting();
    }
  }

  public int SortColumn
  {
    get => this._sortColumn;
    set
    {
      this._sortColumn = value;
      this.UpdateSorting();
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public HybridDictionary LayoutData
  {
    get
    {
      HybridDictionary layoutData = new HybridDictionary();
      for (int index = 0; index < this.Columns.Count; ++index)
        layoutData.Add((object) $"{this.Name}.col{index.ToString()}", (object) this.Columns[index].Width);
      return layoutData;
    }
    set
    {
      HybridDictionary hybridDictionary = value;
      for (int index = 0; index < hybridDictionary.Count && this.Columns.Count >= index; ++index)
      {
        object obj = hybridDictionary[(object) $"{this.Name}.col{index.ToString()}"];
        if (obj != null)
          this.Columns[index].Width = Convert.ToInt32(obj);
      }
    }
  }

  protected override void WndProc(ref Message message)
  {
    if (!this.DesignMode && message.Msg == 15 && this.View == View.Details && this.Columns.Count > 0)
    {
      this.Columns[this.Columns.Count - 1].Width = -2;
      this.UpdateSubControlsPos(true);
    }
    base.WndProc(ref message);
  }

  protected override void OnColumnClick(ColumnClickEventArgs e)
  {
    if (!this.CustomSorter && this.AllowManualSorting)
    {
      if (e.Column != this.SortColumn)
        this.SortColumn = e.Column;
      else
        this.Sorting = this.Sorting != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;
      this.UpdateSortedHeaderImage();
    }
    base.OnColumnClick(e);
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    this.SaveSelectedPos();
    base.OnHandleCreated(e);
    this.RestoreSelectedPos();
    this.hWndHeader = EnhListView.Win32.SendMessage(this.Handle, (IntPtr) 4127L, IntPtr.Zero, IntPtr.Zero);
    this.UpdateSortedHeaderImage();
  }

  public static Dictionary<SortOrder, int> LVHSortFlags
  {
    get
    {
      if (EnhListView._sortFlags == null)
      {
        EnhListView._sortFlags = new Dictionary<SortOrder, int>();
        EnhListView._sortFlags.Add(SortOrder.None, 0);
        EnhListView._sortFlags.Add(SortOrder.Ascending, 1024 /*0x0400*/);
        EnhListView._sortFlags.Add(SortOrder.Descending, 512 /*0x0200*/);
      }
      return EnhListView._sortFlags;
    }
  }

  protected void UpdateSortedHeaderImage()
  {
    if (!this.IsHandleCreated)
      return;
    EnhListView.Win32.HDITEM structure = new EnhListView.Win32.HDITEM();
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      structure.mask = 6U;
      structure.fmt = 16384 /*0x4000*/;
      if (!this.CustomSorter && this.AllowManualSorting && index == this.SortColumn)
        structure.fmt |= EnhListView.LVHSortFlags[this.Sorting];
      structure.fmt |= (int) this.Columns[index].TextAlign;
      structure.pszText = this.Columns[index].Text;
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf<EnhListView.Win32.HDITEM>(structure));
      Marshal.StructureToPtr<EnhListView.Win32.HDITEM>(structure, num, false);
      EnhListView.Win32.SendMessage(this.hWndHeader, (IntPtr) 4620L, (IntPtr) index, num);
      Marshal.FreeHGlobal(num);
    }
  }

  protected override void OnEnabledChanged(EventArgs e)
  {
    base.OnEnabledChanged(e);
    this.EnableSubControls(this.Enabled);
  }

  protected override void OnItemChecked(ItemCheckedEventArgs e)
  {
    base.OnItemChecked(e);
    if (!this.RadioGroups || this.inChecked)
      return;
    this.inChecked = true;
    try
    {
      foreach (ListViewItem listViewItem in this.Items)
      {
        if (listViewItem != e.Item)
          listViewItem.Checked = false;
      }
    }
    finally
    {
      this.inChecked = false;
    }
  }

  public event EnhListView.SortingRefiner RefineSorting;

  public ControlListViewSubItem AddControlSubitem(ListViewItem li, Control c, bool looseFocus)
  {
    ControlListViewSubItem controlListViewSubItem = new ControlListViewSubItem(li, c, looseFocus);
    this._subControls.Add(c);
    this._subControlItems.Add(controlListViewSubItem);
    li.SubItems.Add((ListViewItem.ListViewSubItem) controlListViewSubItem);
    controlListViewSubItem.UpdateSubControlPos(false);
    return controlListViewSubItem;
  }

  public ControlListViewSubItem AddControlSubitem(ListViewItem li, Control c)
  {
    return this.AddControlSubitem(li, c, false);
  }

  protected void EnableSubControls(bool enable)
  {
    foreach (Control subControl in this._subControls)
      subControl.Enabled = enable;
    if (enable)
      this.BackColor = SystemColors.Window;
    else
      this.BackColor = SystemColors.InactiveBorder;
  }

  protected void UpdateSubControlsPos(bool recalcPos)
  {
    foreach (ControlListViewSubItem subControlItem in this._subControlItems)
      subControlItem.UpdateSubControlPos(recalcPos);
  }

  private void CustomDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
  {
    if (e.Item.ForeColor != SystemColors.WindowText)
      e.SubItem.ForeColor = e.Item.ForeColor;
    ControlListViewSubItem subItem1 = e.SubItem as ControlListViewSubItem;
    OwnerdrawListViewSubitem subItem2 = e.SubItem as OwnerdrawListViewSubitem;
    if (subItem1 != null || subItem2 != null)
    {
      Color backColor = e.SubItem.BackColor;
      Color color = e.SubItem.ForeColor;
      Brush brush = (Brush) null;
      bool flag = false;
      if (this.Enabled)
      {
        if (e.Item.Selected && (this.Focused || !this.HideSelection))
        {
          if (this.Focused)
          {
            brush = SystemBrushes.Highlight;
            color = SystemColors.HighlightText;
          }
          else
            brush = SystemBrushes.MenuBar;
        }
      }
      else
      {
        brush = (Brush) new SolidBrush(SystemColors.ButtonFace);
        flag = true;
      }
      if (brush == null)
      {
        brush = (Brush) new SolidBrush(backColor);
        flag = true;
      }
      e.Graphics.FillRectangle(brush, e.Bounds);
      if (flag)
        brush.Dispose();
      if (subItem2 != null)
        subItem2.Draw(new DrawInfo(this, color), e);
      else
        subItem1?.UpdateSubControlColor(backColor, color);
      if (!e.DrawDefault)
        return;
      e.DrawText();
    }
    else
      e.DrawDefault = true;
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    this.UpdateSubControlsPos(true);
    if (!this.Visible || this.SelectedIndices.Count != 0)
      return;
    this.RestoreSelectedPos();
  }

  public void SaveSelectedPos()
  {
    if (this.SelectedIndices.Count > 0)
      this._savedPos = this.SelectedIndices[0];
    else
      this._savedPos = -1;
  }

  public void RestoreSelectedPos()
  {
    if (this._savedPos >= this.Items.Count)
      this._savedPos = this.Items.Count - 1;
    if (this._savedPos == -1 && this.Items.Count > 0)
      this._savedPos = 0;
    if (this._savedPos <= -1)
      return;
    this.SelectedItems.Clear();
    this.Items[this._savedPos].Selected = true;
  }

  public void HighlightInvalidItems()
  {
    if (this.StateImageList != null && this.StateImageList != Holder.ValidatedImageList)
      return;
    if (this.hasInvalid.HasValue)
      this.hasInvalid = new bool?(false);
    for (int index = 0; index < this.Items.Count; ++index)
    {
      ListViewItem listViewItem = this.Items[index];
      if (listViewItem.Tag is IValidatedItem tag)
      {
        if (tag.Invalid)
        {
          listViewItem.StateImageIndex = 0;
          listViewItem.ToolTipText = LocalizationHolder.GetString("InvalidObject");
          this.hasInvalid = new bool?(true);
        }
        else
        {
          if (listViewItem.StateImageIndex != -1)
          {
            listViewItem.StateImageIndex = -1;
            listViewItem.ToolTipText = "";
          }
          if (!this.hasInvalid.HasValue)
            this.hasInvalid = new bool?(false);
        }
      }
    }
    if (!this.hasInvalid.HasValue)
      return;
    if (this.hasInvalid.Value)
    {
      this.StateImageList = Holder.ValidatedImageList;
      this.ShowItemToolTips = true;
    }
    else
    {
      this.StateImageList = (ImageList) null;
      this.ShowItemToolTips = false;
    }
  }

  public new void EndUpdate()
  {
    this.HighlightInvalidItems();
    base.EndUpdate();
  }

  public class ListViewItemComparer : IComparer
  {
    private EnhListView _view;
    public readonly int Column;
    public readonly SortOrder Order;

    public ListViewItemComparer(EnhListView view)
    {
      this._view = view;
      this.Column = view.SortColumn;
      this.Order = view.Sorting;
    }

    public int Compare(object x, object y)
    {
      int num = 0;
      ListViewItem li1 = (ListViewItem) x;
      ListViewItem li2 = (ListViewItem) y;
      if ((li1 is NodeListViewItem ? ((NodeListViewItem) li1).Parent : (NodeListViewItem) null) != (li2 is NodeListViewItem ? ((NodeListViewItem) li2).Parent : (NodeListViewItem) null))
        return num;
      ListViewItem.ListViewSubItem si1 = (ListViewItem.ListViewSubItem) null;
      ListViewItem.ListViewSubItem si2 = (ListViewItem.ListViewSubItem) null;
      if (this.Column < li1.SubItems.Count)
        si1 = li1.SubItems[this.Column];
      if (this.Column < li2.SubItems.Count)
        si2 = li2.SubItems[this.Column];
      int sortResult;
      if (si1 is TypedObjectSubItem)
      {
        sortResult = ((TypedObjectSubItem) si1).Compare((object) si2);
      }
      else
      {
        string strA = "";
        string strB = "";
        if (si1 != null)
          strA = si1.Text;
        if (si2 != null)
          strB = si2.Text;
        sortResult = string.Compare(strA, strB);
      }
      if (this.Order == SortOrder.Descending)
        sortResult *= -1;
      EnhListView.SortingRefiner refineSorting = this._view.RefineSorting;
      if (refineSorting != null)
        refineSorting(this, li1, li2, si1, si2, ref sortResult);
      return sortResult;
    }
  }

  /// <summary>Win32 сообщения и функции</summary>
  private class Win32
  {
    public const uint HDM_FIRST = 4608;
    public const uint HDM_SETITEM = 4620;
    public const uint HDI_FORMAT = 4;
    public const uint HDI_TEXT = 2;
    public const uint HDI_BITMAP = 16 /*0x10*/;
    public const int HDF_STRING = 16384 /*0x4000*/;
    public const int HDF_BITMAP = 8192 /*0x2000*/;
    public const int HDF_BITMAP_ON_RIGHT = 4096 /*0x1000*/;
    public const int HDF_SORTDOWN = 512 /*0x0200*/;
    public const int HDF_SORTUP = 1024 /*0x0400*/;
    public const uint LVM_FIRST = 4096 /*0x1000*/;
    public const uint LVM_GETHEADER = 4127;

    [DllImport("User32.dll")]
    public static extern IntPtr SendMessage(
      IntPtr hWnd,
      IntPtr uMsg,
      IntPtr wParam,
      IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct HDITEM
    {
      public uint mask;
      public int cxy;
      public string pszText;
      public IntPtr hbm;
      public int cchTextMax;
      public int fmt;
      public int lParam;
      public int iImage;
      public int iOrder;
      public uint type;
      public IntPtr pvFilter;
    }
  }

  public delegate void SortingRefiner(
    EnhListView.ListViewItemComparer sender,
    ListViewItem li1,
    ListViewItem li2,
    ListViewItem.ListViewSubItem si1,
    ListViewItem.ListViewSubItem si2,
    ref int sortResult);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.AttributesEditPageDataProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Client.CompositionCopying.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.VirtualGrid;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class AttributesEditPageDataProvider : DataProvider
{
  private List<int> selectedIndexes;
  private List<DBObjectGraphVertex> vertices;
  private HashSet<IMSAttributeType> _allColumnsFromSource;
  private ObservableCollection<IMSAttributeType> _visibleColumns;
  private ObservableCollection<IMSAttributeType> _editableColumns;
  private readonly Dictionary<long, List<(int attributeID, string errorText)>> _errorsVertices;

  public event AttributesEditPageDataProvider.CellEditEndedHandled CellEditEvent;

  public AttributesEditPageDataProvider(IEnumerable source)
    : base(source)
  {
    this.selectedIndexes = new List<int>();
    IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"));
    IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(new Guid("cad00029-306c-11d8-b4e9-00304f19f545"));
    IMSAttributeType attributeType3 = MetaDataHelper.GetAttributeType(new Guid("cad00047-306c-11d8-b4e9-00304f19f545"));
    IMSAttributeType attributeType4 = MetaDataHelper.GetAttributeType(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    IMSAttributeType attributeType5 = MetaDataHelper.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    ObservableCollection<IMSAttributeType> observableCollection = new ObservableCollection<IMSAttributeType>();
    observableCollection.Add(attributeType1);
    observableCollection.Add(attributeType2);
    observableCollection.Add(attributeType3);
    observableCollection.Add(attributeType4);
    observableCollection.Add(attributeType5);
    this._visibleColumns = observableCollection;
    this._editableColumns = new ObservableCollection<IMSAttributeType>();
    this._editableColumns.AddRange<IMSAttributeType>(this._visibleColumns.Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (x => this.IsEditableAttribute(x.AttributeID))));
    this._errorsVertices = new Dictionary<long, List<(int, string)>>();
  }

  public List<DBObjectGraphVertex> AllVertices => this.vertices;

  public AttributesEditPageDataProvider(List<DBObjectGraphVertex> vertices)
    : this((IEnumerable) vertices)
  {
    this.selectedIndexes = new List<int>();
    this.vertices = vertices;
    List<DBObjectAttributeEntry> source = new List<DBObjectAttributeEntry>();
    foreach (DBObjectGraphVertex vertex in vertices)
      source.AddRange((IEnumerable<DBObjectAttributeEntry>) vertex.Attributes);
    this._allColumnsFromSource = new HashSet<IMSAttributeType>();
    this._allColumnsFromSource = source.GroupBy<DBObjectAttributeEntry, int>((Func<DBObjectAttributeEntry, int>) (x => x.AttributeId)).Select<IGrouping<int, DBObjectAttributeEntry>, IMSAttributeType>((Func<IGrouping<int, DBObjectAttributeEntry>, IMSAttributeType>) (x => MetaDataHelper.GetAttributeType(x.First<DBObjectAttributeEntry>().AttributeId))).ToHashSet<IMSAttributeType>();
    this._visibleColumns = new ObservableCollection<IMSAttributeType>(this._visibleColumns.Union<IMSAttributeType>(source.GroupBy<DBObjectAttributeEntry, int>((Func<DBObjectAttributeEntry, int>) (x => x.AttributeId)).Where<IGrouping<int, DBObjectAttributeEntry>>((Func<IGrouping<int, DBObjectAttributeEntry>, bool>) (y => y.ToList<DBObjectAttributeEntry>().Any<DBObjectAttributeEntry>((Func<DBObjectAttributeEntry, bool>) (x => x.IsUniqueValuesRequired && x.IsEditableAttribute)))).Select<IGrouping<int, DBObjectAttributeEntry>, DBObjectAttributeEntry>((Func<IGrouping<int, DBObjectAttributeEntry>, DBObjectAttributeEntry>) (group => group.First<DBObjectAttributeEntry>())).ToHashSet<DBObjectAttributeEntry>().Select<DBObjectAttributeEntry, IMSAttributeType>((Func<DBObjectAttributeEntry, IMSAttributeType>) (x => MetaDataHelper.GetAttributeType(x.AttributeId)))).ToList<IMSAttributeType>());
    this._editableColumns = new ObservableCollection<IMSAttributeType>(this._visibleColumns.Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (x => this.IsEditableAttribute(x.AttributeID))));
  }

  protected override void SortDescriptorPrepared(SortedEventArgs e)
  {
    List<int> list = this.ParentGrid.SelectedIndexes.ToList<int>();
    if (list.Count > 0)
      this.PersistSelection((IEnumerable<int>) list);
    base.SortDescriptorPrepared(e);
  }

  protected override void OnSortingCompleted()
  {
    base.OnSortingCompleted();
    this.UpdateSelection((IEnumerable<int>) this.selectedIndexes);
  }

  public override IList<ItemPropertyInfo> ItemProperties
  {
    get
    {
      return (IList<ItemPropertyInfo>) this.VisibleColumns.Select<IMSAttributeType, ItemPropertyInfo>((Func<IMSAttributeType, ItemPropertyInfo>) (x => new VirtualTreePropertyDescriptor(x).ToItemProperty())).ToList<ItemPropertyInfo>();
    }
  }

  public ObservableCollection<IMSAttributeType> VisibleColumns => this._visibleColumns;

  public HashSet<IMSAttributeType> AllColumns => this._allColumnsFromSource;

  protected override void OnHeaderValueNeeded(HeaderValueEventArgs e)
  {
    if (e.HeaderOrientation != VirtualGridOrientation.Horizontal)
      return;
    base.OnHeaderValueNeeded(e);
  }

  protected override void OnCellToolTipNeeded(VirtualGridCellToolTipEventArgs valueEventArgs)
  {
    if (valueEventArgs.RowIndex == -1 || valueEventArgs.ColumnIndex == -1)
      return;
    object itemAt = this.Source.GetItemAt(valueEventArgs.RowIndex);
    if (!(this.ItemProperties[valueEventArgs.ColumnIndex].Descriptor is VirtualTreePropertyDescriptor descriptor))
      return;
    bool flag = descriptor.IsEditable(itemAt);
    int num1 = descriptor.IsUnique(itemAt) ? 1 : 0;
    object obj = descriptor.GetValue(itemAt);
    int num2 = flag ? 1 : 0;
    if ((num1 & num2) != 0)
    {
      string errorText;
      if (this.IsErrorsCell(valueEventArgs.RowIndex, valueEventArgs.ColumnIndex, out errorText))
        valueEventArgs.Value = (object) errorText;
      else
        valueEventArgs.Value = (object) "Значение атрибута контролирует уникальность и обязательно к изменению";
    }
    else if (!flag)
    {
      valueEventArgs.Value = (object) "Значение атрибута изменять запрещено";
    }
    else
    {
      if (obj != null)
        return;
      valueEventArgs.Value = (object) "Атрибут отсутствует у объекта";
    }
  }

  protected override void OnCellEditEnded(CellEditEndedEventArgs e)
  {
    base.OnCellEditEnded(e);
    if (e.EditAction != VirtualGridEditAction.Commit)
      return;
    AttributesEditPageDataProvider.CellEditEndedHandled cellEditEvent = this.CellEditEvent;
    if (cellEditEvent == null)
      return;
    cellEditEvent((object) this.ParentGrid, e);
  }

  protected override void OnEditorNeeded(EditorNeededEventArgs args)
  {
    if (this.ItemProperties[args.ColumnIndex].Descriptor is VirtualTreePropertyDescriptor descriptor)
    {
      DBObjectGraphVertex vertex = this.vertices[args.RowIndex];
      if (descriptor.IsEditable((object) vertex))
      {
        if (vertex != null)
        {
          object obj = descriptor.GetValue((object) vertex);
          if (obj == null)
            return;
          TextBox textBox = new TextBox();
          args.Editor = (FrameworkElement) textBox;
          textBox.Text = obj.ToString();
          args.EditorProperty = TextBox.TextProperty;
        }
        else
          args.Editor = (FrameworkElement) null;
      }
      else
        args.Editor = (FrameworkElement) null;
    }
    else
      args.Editor = (FrameworkElement) null;
  }

  private void PersistSelection(IEnumerable<int> selectedIndexes)
  {
    foreach (int selectedIndex in selectedIndexes)
      this.selectedIndexes.Add(selectedIndex);
  }

  private void UpdateSelection(IEnumerable<int> selectedIndexes)
  {
    foreach (int selectedIndex in selectedIndexes)
    {
      if (!this.ParentGrid.SelectedIndexes.Contains<int>(selectedIndex))
        this.ParentGrid.ToggleIndexSelection(selectedIndex);
    }
  }

  public void RefreshSource()
  {
    this._editableColumns.Clear();
    this._editableColumns.AddRange<IMSAttributeType>(this._visibleColumns.Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (x => this.IsEditableAttribute(x.AttributeID))));
    this.Source.Refresh();
  }

  public ObservableCollection<IMSAttributeType> EditableColumns => this._editableColumns;

  public bool IsEditableAttribute(int attributeTypeID)
  {
    if (attributeTypeID == -1)
      return false;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeID);
    return (attributeTypeID == -50 || attributeType != null && (attributeType.FieldType == FieldTypes.ftString || attributeType.FieldType == FieldTypes.ftMemo)) && attributeType.MultiValueMode == MultiValueModes.SingleValue && attributeType.Computed == ComputeValueModes.NotComputableValue;
  }

  public object GetCellValue(int rowIndex, int cellIndex, out bool isEditable, out bool isUnique)
  {
    object itemAt = this.Source.GetItemAt(rowIndex);
    if (this.ItemProperties[cellIndex].Descriptor is VirtualTreePropertyDescriptor descriptor)
    {
      isEditable = descriptor.IsEditable(itemAt);
      isUnique = descriptor.IsUnique(itemAt);
      return descriptor.GetValue(itemAt);
    }
    isEditable = false;
    isUnique = false;
    return (object) null;
  }

  public object GetCellValueFromSelectedRow(int rowIndex, int attributeID)
  {
    object itemAt = this.Source.GetItemAt(rowIndex);
    int index = this.ItemProperties.ToList<ItemPropertyInfo>().FindIndex((Predicate<ItemPropertyInfo>) (x => x.Descriptor is VirtualTreePropertyDescriptor descriptor && descriptor.GetAttributeID() == attributeID));
    if (index == -1)
      return (object) null;
    return ((PropertyDescriptor) this.ItemProperties[index].Descriptor)?.GetValue(itemAt);
  }

  public void SelectRow(int rowIndex)
  {
    if (this.ParentGrid.SelectedIndex != -1)
      this.ParentGrid.ToggleIndexSelection(this.ParentGrid.SelectedIndex);
    this.ParentGrid.ToggleIndexSelection(rowIndex);
    this.ParentGrid.ScrollRowIndexIntoViewAsync(rowIndex, (Action) (() => { }), (Action) (() => { }));
  }

  public DBObjectGraphVertex SetValueToCell(int rowIndex, int attributeID, object value)
  {
    int index1 = this.ItemProperties.ToList<ItemPropertyInfo>().FindIndex((Predicate<ItemPropertyInfo>) (x => x.Descriptor is VirtualTreePropertyDescriptor descriptor1 && descriptor1.GetAttributeID() == attributeID));
    if (index1 != -1)
    {
      this.PushCellValueToSource(rowIndex, index1, value);
      this.ParentGrid?.PushCellValue(rowIndex, index1, value);
      if (this.Source.GetItemAt(rowIndex) is DBObjectGraphVertex itemAt && this.ErrorsVertices.ContainsKey(itemAt.ObjectId))
      {
        VirtualTreePropertyDescriptor descriptor;
        if ((descriptor = this.ItemProperties[index1].Descriptor as VirtualTreePropertyDescriptor) != null && descriptor.GetValue((object) itemAt) != null)
        {
          List<(int, string)> errorsVertex = this.ErrorsVertices[itemAt.ObjectId];
          int index2 = errorsVertex.FindIndex((Predicate<(int, string)>) (x => x.attributeID == descriptor.GetAttributeID()));
          if (index2 != -1)
            errorsVertex.RemoveAt(index2);
          if (errorsVertex.Count == 0)
            return itemAt;
        }
      }
    }
    return (DBObjectGraphVertex) null;
  }

  public Dictionary<long, List<(int attributeID, string errorText)>> ErrorsVertices
  {
    get => this._errorsVertices;
  }

  public bool IsErrorsCell(int rowIndex, int cellIndex, out string errorText)
  {
    if (this.Source.GetItemAt(rowIndex) is DBObjectGraphVertex itemAt && this.ErrorsVertices.ContainsKey(itemAt.ObjectId))
    {
      VirtualTreePropertyDescriptor descriptor;
      if ((descriptor = this.ItemProperties[cellIndex].Descriptor as VirtualTreePropertyDescriptor) != null)
      {
        List<(int attributeID, string errorText)> errorsVertex = this.ErrorsVertices[itemAt.ObjectId];
        int index = errorsVertex.FindIndex((Predicate<(int, string)>) (x => x.attributeID == descriptor.GetAttributeID()));
        if (index != -1)
        {
          errorText = errorsVertex[index].errorText;
          return true;
        }
      }
    }
    errorText = string.Empty;
    return false;
  }

  public DBObjectGraphVertex RemoveFromErrorCells(int rowIndex, int cellIndex, object value)
  {
    if (this.Source.GetItemAt(rowIndex) is DBObjectGraphVertex itemAt && this.ErrorsVertices.ContainsKey(itemAt.ObjectId))
    {
      VirtualTreePropertyDescriptor descriptor;
      if ((descriptor = this.ItemProperties[cellIndex].Descriptor as VirtualTreePropertyDescriptor) != null)
      {
        object obj = descriptor.GetValue((object) itemAt);
        if (obj != null && !obj.Equals(value))
        {
          List<(int, string)> errorsVertex = this.ErrorsVertices[itemAt.ObjectId];
          int index = errorsVertex.FindIndex((Predicate<(int, string)>) (x => x.attributeID == descriptor.GetAttributeID()));
          if (index != -1)
            errorsVertex.RemoveAt(index);
          if (errorsVertex.Count == 0)
            return itemAt;
        }
      }
    }
    return (DBObjectGraphVertex) null;
  }

  public void UpdateGridUI()
  {
    this.ParentGrid?.UpdateUI();
    this.ParentGrid?.UpdateHeadersUI();
  }

  public void CancelEdit() => this.ParentGrid?.CancelEdit();

  public void ResetGrid() => this.ParentGrid?.Reset();

  public (FontFamily FontFamily, FontStyle FontStyle, FontWeight FontWeight, FontStretch FontStretch, double FontSize, DpiScale DpiScale) GetGridSetting()
  {
    return (this.ParentGrid.FontFamily, this.ParentGrid.FontStyle, this.ParentGrid.FontWeight, this.ParentGrid.FontStretch, this.ParentGrid.FontSize, VisualTreeHelper.GetDpi((Visual) this.ParentGrid));
  }

  public object GetMaxCellValue(int cellIndex)
  {
    int index1 = 0;
    double num = 0.0;
    for (int index2 = 0; index2 < this.Source.Count; ++index2)
    {
      object itemAt = this.Source.GetItemAt(index2);
      if (this.ItemProperties[cellIndex].Descriptor is VirtualTreePropertyDescriptor descriptor)
      {
        object obj = descriptor.GetValue(itemAt);
        if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
        {
          string str = obj.ToString();
          if (num < (double) str.Length)
          {
            num = (double) str.Length;
            index1 = index2;
          }
        }
      }
    }
    object itemAt1 = this.Source.GetItemAt(index1);
    return this.ItemProperties[cellIndex].Descriptor is VirtualTreePropertyDescriptor descriptor1 ? descriptor1.GetValue(itemAt1) : (object) string.Empty;
  }

  internal delegate void CellEditEndedHandled(object sender, CellEditEndedEventArgs e);
}

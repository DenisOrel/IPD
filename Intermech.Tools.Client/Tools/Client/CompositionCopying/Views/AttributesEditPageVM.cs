// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.AttributesEditPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Model.Operations;
using Intermech.UI;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.VirtualGrid;
using Telerik.Windows.Data;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class AttributesEditPageVM : WizardPageVM
{
  private CopyingSession _session;
  private WizardPageOperationErrorsVM _pageErrors;
  private RegisterNames _registerName;
  private IEnumerable<EnumMemberViewModel> _registerNamesList;
  private readonly ContextMenuViewModel<ContextMenuUICommand<object>> _contextMenuModel;
  private int _attributeValueNameSelectedIndex;
  private int _attributeNameSelectedIndex;
  private HashSet<DBObjectGraphVertex> _allEditableObjects;
  private AttributesEditPageDataProvider _dataProvider;
  private PluggableCommand<object> _symbolSelectCommand;
  private PluggableCommand<object> _symbolCounterSelectCommand;
  private PluggableCommand _findCommand;
  private PluggableCommand _replaceCommand;
  private PluggableCommand _replaceAllCommand;
  private ICommand _virtualGridCellDecorationsNeeded;
  private ICommand _virtualGridOverlayBrushesNeeded;
  private ICommand _virtualGridLoaded;
  private DelegateCommand _virtualGridSelectionChanged;
  private ICommand _virtualGridHeaderSizeNeeded;
  private SolidColorBrush _blueBrush;
  private int _virtualGridSelectedIndex = -1;
  private PluggableCommand _checkGridDataCommand;
  private bool _canGoNext;
  private CopyingSessionProcessingStep _newProcessingStep;

  public AttributesEditPageVM()
    : base("Редактирование атрибутов")
  {
    this._pageErrors = new WizardPageOperationErrorsVM();
    this._pageErrors.PropertyChanged += new PropertyChangedEventHandler(this.OnPageErrorsChanged);
    this._pageErrors.ErrorDoubleClick += new WizardPageOperationErrorsVM.ErrorDoubleClickHandler(this.OnErrorDoubleClick);
    this._registerName = RegisterNames.Default;
    this._allEditableObjects = new HashSet<DBObjectGraphVertex>(0);
    this._virtualGridCellDecorationsNeeded = (ICommand) new DelegateCommand(new Action<object>(this.OnCellDecorationsNeeded));
    this._virtualGridOverlayBrushesNeeded = (ICommand) new DelegateCommand(new Action<object>(this.OnOverlayBrushesNeeded));
    this._virtualGridLoaded = (ICommand) new DelegateCommand(new Action<object>(this.OnGridLoaded));
    this._virtualGridSelectionChanged = new DelegateCommand(new Action<object>(this.OnVirtualGridSelectionChanged));
    this._virtualGridHeaderSizeNeeded = (ICommand) new DelegateCommand(new Action<object>(this.OnVirtualGridHeaderSizeNeeded));
    this._blueBrush = new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 140, (byte) 215));
    this._blueBrush.Freeze();
  }

  public AttributesEditPageVM(CopyingSession session)
    : this()
  {
    this._session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this._pageErrors.SetCopyingSession(session);
    this._contextMenuModel = new ContextMenuViewModel<ContextMenuUICommand<object>>();
    this._contextMenuModel.Items.Add(new ContextMenuUICommand<object>("Настройки отображения", new Action<object>(this.SelectColumns), new Predicate<object>(this.CanExecuteMenuCommand)));
    this._symbolSelectCommand = new PluggableCommand<object>(new Action<object>(this.SymbolSelector));
    this._symbolCounterSelectCommand = new PluggableCommand<object>(new Action<object>(this.SymbolCounterSelector));
    this._findCommand = new PluggableCommand(new Action(this.Find));
    this._replaceCommand = new PluggableCommand(new Action(this.Replace));
    this._replaceAllCommand = new PluggableCommand(new Action(this.ReplaceAll));
    this._checkGridDataCommand = new PluggableCommand(new Action(this.CheckGridData));
  }

  public WizardPageOperationErrorsVM PageErrors => this._pageErrors;

  private bool ValidateIsCompleted() => this._pageErrors.IsEmpty && this._canGoNext;

  private void OnPageErrorsChanged(object sender, PropertyChangedEventArgs e)
  {
    this.IsCompleted = this.ValidateIsCompleted();
  }

  private void OnErrorDoubleClick(DBObjectGraphVertex vertex)
  {
    if (vertex == null)
      return;
    int index = this.DataProvider.AllVertices.FindIndex((Predicate<DBObjectGraphVertex>) (x => x.Equals(vertex)));
    if (index == -1)
      return;
    this.VirtualGridSelectedIndex = index;
    this.DataProvider.SelectRow(index);
  }

  private HashSet<DBObjectGraphVertex> GetCurrentAllEditableObjects()
  {
    ICollection<DBObjectGraphVertex> allVertices = this._session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument() && x.CopyingSelector.IsSelected && x.IsScanned));
    HashSet<DBObjectGraphVertex> collection = new HashSet<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) allVertices);
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) allVertices)
      collection.AddRange<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) this._session.Graph.GetVerticesByInEdges(vertex, (Predicate<DBObjectGraphVertex>) (x => !x.IsDocument())));
    return collection;
  }

  public HashSet<DBObjectGraphVertex> AllEditableObjects
  {
    get => this._allEditableObjects;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._allEditableObjects.SetEquals((IEnumerable<DBObjectGraphVertex>) value))
        return;
      this._allEditableObjects = value;
      this._dataProvider = (AttributesEditPageDataProvider) null;
      this._pageErrors.Items.Clear();
      this._canGoNext = false;
      this.RaisePropertyChanged(nameof (AllEditableObjects));
    }
  }

  public AttributesEditPageDataProvider DataProvider
  {
    get
    {
      if (this._dataProvider == null)
      {
        this._dataProvider = new AttributesEditPageDataProvider(this.AllEditableObjects.ToList<DBObjectGraphVertex>());
        this._dataProvider.CellEditEvent += new AttributesEditPageDataProvider.CellEditEndedHandled(this.DataProvider_CellEditEvent);
      }
      return this._dataProvider;
    }
  }

  private void DataProvider_CellEditEvent(object sender, CellEditEndedEventArgs e)
  {
    this._canGoNext = false;
    this.IsCompleted = this.ValidateIsCompleted();
    if (!(sender is RadVirtualGrid radVirtualGrid))
      return;
    DBObjectGraphVertex errorVertex = this.DataProvider?.RemoveFromErrorCells(e.RowIndex, e.ColumnIndex, e.Value);
    if (errorVertex != null)
    {
      List<OperationError> list = this.PageErrors.Items.Where<OperationError>((Func<OperationError, bool>) (x => x.Vertex.Equals(errorVertex))).ToList<OperationError>();
      if (list.Count > 0)
      {
        foreach (OperationError operationError in list)
          this.PageErrors.Items.Remove(operationError);
      }
    }
    radVirtualGrid.UpdateUI();
  }

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this._session == null)
      return;
    this.AllEditableObjects = this.GetCurrentAllEditableObjects();
    this.IsCompleted = this.ValidateIsCompleted();
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    if (this._session == null || this._newProcessingStep == null)
      return;
    this._session.ProcessingHistory.Update(this._newProcessingStep);
    this._newProcessingStep = (CopyingSessionProcessingStep) null;
  }

  public ContextMenuViewModel<ContextMenuUICommand<object>> ContextMenuModel
  {
    get => this._contextMenuModel;
  }

  private void SelectColumns(object node)
  {
    HashSet<NodeColumn> hashSet1 = this.DataProvider.AllColumns.Select<IMSAttributeType, NodeColumn>((Func<IMSAttributeType, NodeColumn>) (x => new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) x.AttributeID, typeof (string), x.FieldType, x.Name))).ToHashSet<NodeColumn>();
    HashSet<NodeColumn> hashSet2 = this.DataProvider.VisibleColumns.Select<IMSAttributeType, NodeColumn>((Func<IMSAttributeType, NodeColumn>) (x => new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) x.AttributeID, typeof (string), x.FieldType, x.Name))).ToHashSet<NodeColumn>();
    NodeColumnCollection supportedColumns = new NodeColumnCollection((IEnumerable<NodeColumn>) hashSet1);
    NodeColumnCollection columns = new NodeColumnCollection((IEnumerable<NodeColumn>) hashSet2);
    using (AppearanceTuningForm appearanceTuningForm = new AppearanceTuningForm((INode) null, ContentType.Folders, supportedColumns, columns, Array.Empty<object>()))
    {
      appearanceTuningForm.StateStreamSuffix = (string) null;
      if (appearanceTuningForm.ShowDialog() != DialogResult.OK)
        return;
      this.DataProvider.CancelEdit();
      this.DataProvider.VisibleColumns.Clear();
      foreach (NodeColumn nodeColumn in (List<NodeColumn>) columns)
        this.DataProvider.VisibleColumns.Add(MetaDataHelper.GetAttributeType(nodeColumn.Attribute.AttributeID));
      int nameSelectedIndex1 = this.AttributeNameSelectedIndex;
      int nameSelectedIndex2 = this.AttributeValueNameSelectedIndex;
      this.DataProvider.RefreshSource();
      this.DataProvider.ResetGrid();
      this.DataProvider.UpdateGridUI();
      if (this.DataProvider.EditableColumns.Count <= 0)
        return;
      this.AttributeNameSelectedIndex = nameSelectedIndex1 < this.DataProvider.EditableColumns.Count ? nameSelectedIndex1 : 0;
      this.AttributeValueNameSelectedIndex = nameSelectedIndex2 < this.DataProvider.EditableColumns.Count ? nameSelectedIndex2 : 0;
      this.RaisePropertyChanged("AttributeNameSelectedIndex");
      this.RaisePropertyChanged("AttributeValueNameSelectedIndex");
    }
  }

  private bool CanExecuteMenuCommand(object obj) => this.DataProvider != null;

  public int AttributeNameSelectedIndex
  {
    get => this._attributeNameSelectedIndex;
    set => this._attributeNameSelectedIndex = value;
  }

  public int AttributeValueNameSelectedIndex
  {
    get => this._attributeValueNameSelectedIndex;
    set => this._attributeValueNameSelectedIndex = value;
  }

  public RegisterNames SelectedRegisterName
  {
    get => this._registerName;
    set => this._registerName = value;
  }

  public IEnumerable<EnumMemberViewModel> RegisterNamesList
  {
    get
    {
      if (this._registerNamesList == null)
        this._registerNamesList = EnumDataSource.FromType<RegisterNames>();
      return this._registerNamesList;
    }
  }

  public bool CaseSensitive { get; set; }

  public bool IsCyrillicSimilarity { get; set; }

  public string FindedAttributeValue { get; set; }

  public string ReplaceAttributeValue { get; set; }

  public bool TextReplaceSelected { get; set; }

  public PluggableCommand<object> SymbolSelectCommand => this._symbolSelectCommand;

  public PluggableCommand<object> SymbolCounterSelectCommand => this._symbolCounterSelectCommand;

  public PluggableCommand FindCommand => this._findCommand;

  public PluggableCommand ReplaceCommand => this._replaceCommand;

  public PluggableCommand ReplaceAllCommand => this._replaceAllCommand;

  public ICommand VirtualGridCellDecorationsNeeded => this._virtualGridCellDecorationsNeeded;

  public ICommand VirtualGridOverlayBrushesNeeded => this._virtualGridOverlayBrushesNeeded;

  public ICommand VirtualGridLoaded => this._virtualGridLoaded;

  public DelegateCommand VirtualGridSelectionChanged => this._virtualGridSelectionChanged;

  public ICommand VirtualGridHeaderSizeNeeded => this._virtualGridHeaderSizeNeeded;

  public int VirtualGridSelectedIndex
  {
    get => this._virtualGridSelectedIndex;
    set
    {
      this._virtualGridSelectedIndex = value;
      this.RaisePropertyChanged("IsEnableReplace");
    }
  }

  public bool IsEnableReplace
  {
    get => this.VirtualGridSelectedIndex > -1;
    set => this.RaisePropertyChanged(nameof (IsEnableReplace));
  }

  public Brush SelectionBrush => (Brush) this._blueBrush;

  public PluggableCommand CheckGridDataCommand => this._checkGridDataCommand;

  private void OnVirtualGridSelectionChanged(object obj)
  {
    if (!(obj is SelectionChangedEventArgs changedEventArgs) || !(changedEventArgs.Source is RadVirtualGrid source))
      return;
    source.UpdateUI();
  }

  private void OnCellDecorationsNeeded(object sender)
  {
    if (!(sender is CellDecorationEventArgs decorationEventArgs) || this.DataProvider == null)
      return;
    bool isEditable;
    bool isUnique;
    object cellValue = this.DataProvider.GetCellValue(decorationEventArgs.RowIndex, decorationEventArgs.ColumnIndex, out isEditable, out isUnique);
    if (decorationEventArgs.RowIndex == this.VirtualGridSelectedIndex)
    {
      decorationEventArgs.Background = (Brush) this._blueBrush;
      decorationEventArgs.Foreground = (Brush) Brushes.White;
      decorationEventArgs.CellTextAlignment = new TextAlignment?(TextAlignment.Left);
    }
    else if (cellValue == null)
      decorationEventArgs.Background = (Brush) Brushes.LightGray;
    else if (!isEditable)
    {
      decorationEventArgs.Background = (Brush) Brushes.WhiteSmoke;
      decorationEventArgs.Foreground = (Brush) Brushes.Gray;
    }
    if (((!isUnique ? 0 : (decorationEventArgs.RowIndex != this.VirtualGridSelectedIndex ? 1 : 0)) & (isEditable ? 1 : 0)) == 0)
      return;
    if (this.DataProvider.IsErrorsCell(decorationEventArgs.RowIndex, decorationEventArgs.ColumnIndex, out string _))
    {
      decorationEventArgs.Background = (Brush) Brushes.Red;
      decorationEventArgs.Foreground = (Brush) Brushes.White;
    }
    else
      decorationEventArgs.Background = (Brush) Brushes.Yellow;
  }

  private void OnOverlayBrushesNeeded(object sender)
  {
    if (!(sender is OverlayBrushesEventArgs brushesEventArgs))
      return;
    brushesEventArgs.Brushes.Add((Brush) Brushes.Red);
    brushesEventArgs.Brushes.Add((Brush) Brushes.Yellow);
    brushesEventArgs.Brushes.Add((Brush) Brushes.WhiteSmoke);
    brushesEventArgs.Brushes.Add((Brush) Brushes.LightGray);
    brushesEventArgs.Brushes.Add((Brush) this._blueBrush);
  }

  private void OnGridLoaded(object obj)
  {
    if (!(obj is RoutedEventArgs routedEventArgs) || !(routedEventArgs.Source is RadVirtualGrid source))
      return;
    source.Reset();
  }

  private void OnVirtualGridHeaderSizeNeeded(object obj)
  {
    if (!(obj is HeaderSizeEventArgs headerSizeEventArgs) || headerSizeEventArgs.HeaderOrientation != VirtualGridOrientation.Horizontal || this.DataProvider == null || headerSizeEventArgs.Size <= 0.0)
      return;
    object maxCellValue = this.DataProvider.GetMaxCellValue(headerSizeEventArgs.Index);
    string name = this.DataProvider.VisibleColumns[headerSizeEventArgs.Index].Name;
    string textToFormat = name;
    if (maxCellValue != null && maxCellValue.ToString().Length > name.Length)
      textToFormat = maxCellValue.ToString();
    (FontFamily FontFamily, FontStyle FontStyle, FontWeight FontWeight, FontStretch FontStretch, double FontSize, DpiScale DpiScale) gridSetting = this.DataProvider.GetGridSetting();
    double num = Math.Ceiling(new FormattedText(textToFormat, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface(gridSetting.FontFamily, gridSetting.FontStyle, gridSetting.FontWeight, gridSetting.FontStretch), gridSetting.FontSize, (Brush) Brushes.Black, new NumberSubstitution(), gridSetting.DpiScale.PixelsPerDip).Width + 10.0);
    headerSizeEventArgs.Size = num > 150.0 ? num : 150.0;
  }

  private void SymbolSelector(object symbol)
  {
    if (symbol == null)
      return;
    this.FindedAttributeValue += (string) symbol;
    this.RaisePropertyChanged("FindedAttributeValue");
  }

  private void SymbolCounterSelector(object symbol)
  {
    if (symbol == null)
      return;
    this.ReplaceAttributeValue += (string) symbol;
    this.RaisePropertyChanged("ReplaceAttributeValue");
  }

  private void ReplaceAll()
  {
    IMSAttributeType selectedEditableAttrName = this.DataProvider.EditableColumns[this.AttributeNameSelectedIndex];
    this.ReplaceObjectsValue(this.DataProvider.AllVertices.Select<DBObjectGraphVertex, int>((Func<DBObjectGraphVertex, int, int>) ((graphVertex, graphVertexIndex) => CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) graphVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (y => y.AttributeId == selectedEditableAttrName.AttributeID)) == -1 ? -1 : graphVertexIndex)).Where<int>((Func<int, bool>) (i => i != -1)).ToHashSet<int>());
  }

  private void Replace()
  {
    this.ReplaceObjectsValue(new HashSet<int>()
    {
      this.VirtualGridSelectedIndex
    });
  }

  private void ReplaceObjectsValue(HashSet<int> indexToReplace)
  {
    Regex result1 = new FindWhatBuilder()
    {
      MatchCase = this.CaseSensitive,
      Text = this.FindedAttributeValue,
      MatchCirillicLatinSimilarity = this.IsCyrillicSimilarity
    }.GetResult();
    ReplaceWithBuilder replaceWithBuilder = new ReplaceWithBuilder();
    switch (this.SelectedRegisterName)
    {
      case RegisterNames.Default:
        replaceWithBuilder.CharacterCaseTransformation = CharacterCaseTransformation.None;
        break;
      case RegisterNames.Lowercase:
        replaceWithBuilder.CharacterCaseTransformation = CharacterCaseTransformation.LowerCase;
        break;
      case RegisterNames.Uppercase:
        replaceWithBuilder.CharacterCaseTransformation = CharacterCaseTransformation.UpperCase;
        break;
      case RegisterNames.FirstUpper:
        replaceWithBuilder.CharacterCaseTransformation = CharacterCaseTransformation.StartWithCapital;
        break;
    }
    replaceWithBuilder.Counters = new Dictionary<int, Counter>();
    foreach (int num1 in indexToReplace)
    {
      IMSAttributeType editableColumn = this.DataProvider.EditableColumns[this.AttributeNameSelectedIndex];
      object valueFromSelectedRow = this.DataProvider.GetCellValueFromSelectedRow(num1, editableColumn.AttributeID);
      if (valueFromSelectedRow != null)
      {
        string input = valueFromSelectedRow.ToString();
        if (result1.IsMatch(input))
        {
          replaceWithBuilder.CurrentAttributeValue = input;
          if (this.TextReplaceSelected)
          {
            if (!string.IsNullOrEmpty(this.ReplaceAttributeValue))
            {
              replaceWithBuilder.ReplaceWithTemplate = this.ReplaceAttributeValue;
            }
            else
            {
              int num2 = (int) System.Windows.Forms.MessageBox.Show("Значение заменителя не может быть пустым.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
            }
          }
          else
          {
            IMSAttributeType selectedEditableAttr = this.DataProvider.EditableColumns[this.AttributeValueNameSelectedIndex];
            DBObjectGraphVertex allVertex = this.DataProvider.AllVertices[num1];
            int index = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) allVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == selectedEditableAttr.AttributeID));
            if (index != -1)
            {
              string str = allVertex.Attributes[index].NewValues[0].ToString();
              replaceWithBuilder.ReplaceWithAttributeValue = str;
            }
            else
            {
              int num3 = (int) System.Windows.Forms.MessageBox.Show($"У выделенного объекта отсутствует атрибут '{selectedEditableAttr.Name}'.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
            }
          }
          string result2 = replaceWithBuilder.GetResult();
          string str1 = result1.Replace(input, result2);
          DBObjectGraphVertex errorVertex = this.DataProvider.SetValueToCell(num1, editableColumn.AttributeID, (object) str1);
          if (errorVertex != null)
          {
            List<OperationError> list = this.PageErrors.Items.Where<OperationError>((Func<OperationError, bool>) (x => x.Vertex.Equals(errorVertex))).ToList<OperationError>();
            if (list.Count > 0)
            {
              foreach (OperationError operationError in list)
                this.PageErrors.Items.Remove(operationError);
            }
          }
        }
      }
    }
  }

  private void Find()
  {
    if (this.DataProvider == null)
      return;
    IMSAttributeType selectedEditableAttr = this.DataProvider.EditableColumns[this.AttributeNameSelectedIndex];
    int num1 = this.VirtualGridSelectedIndex + 1;
    List<DBObjectGraphVertex> objectGraphVertexList = this.DataProvider.AllVertices;
    if (num1 > 0 && num1 < objectGraphVertexList.Count)
      objectGraphVertexList = objectGraphVertexList.GetRange(num1, objectGraphVertexList.Count - num1).Union<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) objectGraphVertexList.GetRange(0, num1)).ToList<DBObjectGraphVertex>();
    for (int index1 = 0; index1 < objectGraphVertexList.Count; ++index1)
    {
      DBObjectGraphVertex currentItem = objectGraphVertexList[index1];
      int index2 = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) currentItem.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == selectedEditableAttr.AttributeID));
      if (index2 != -1)
      {
        string input = currentItem.Attributes[index2].NewValues[0].ToString();
        if (new FindWhatBuilder()
        {
          MatchCase = this.CaseSensitive,
          Text = this.FindedAttributeValue,
          MatchCirillicLatinSimilarity = this.IsCyrillicSimilarity
        }.GetResult().IsMatch(input))
        {
          int index3 = this.DataProvider.AllVertices.FindIndex((Predicate<DBObjectGraphVertex>) (x => x.Equals(currentItem)));
          if (index3 != -1)
          {
            this.VirtualGridSelectedIndex = index3;
            this.DataProvider.SelectRow(index3);
            return;
          }
        }
      }
    }
    int num2 = (int) System.Windows.Forms.MessageBox.Show("Поиск завершен. Ничего не найдено.", "Результаты поиска", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void CheckGridData()
  {
    if (this.DataProvider == null)
      return;
    this.PageErrors.Items.Clear();
    this.DataProvider.ErrorsVertices.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      CheckAttributeValueResult[] attributeValueResultArray = sessionKeeper.Session.GetObjectCollection(-1).CheckAttributesValues(this.DataProvider.AllVertices.Select(x => new
      {
        ObjectID = x.ObjectId,
        AttributeValues = x.Attributes.Where<DBObjectAttributeEntry>((Func<DBObjectAttributeEntry, bool>) (y => y.IsEditableAttribute)).Select<DBObjectAttributeEntry, AttributeValues>((Func<DBObjectAttributeEntry, AttributeValues>) (attr => new AttributeValues(attr.AttributeId, attr.NewValues[0]))).ToArray<AttributeValues>()
      }).ToDictionary(x => x.ObjectID, x => x.AttributeValues));
      if (attributeValueResultArray.Length == 0)
      {
        this._canGoNext = true;
        this.PageErrors.Items.Clear();
        if (this.ValidateIsCompleted())
        {
          this._session.DeferredEventDispatcher.RaiseAll();
          AutoRenameOperation autoRenameOperation = new AutoRenameOperation();
          autoRenameOperation.Invoke(this._session);
          if (autoRenameOperation.Errors.Count != 0)
            this.PageErrors.Items.AddRange<OperationError>((IEnumerable<OperationError>) autoRenameOperation.Errors);
          this._session.DeferredEventDispatcher.RaiseAll();
          this._newProcessingStep = new CopyingSessionProcessingStep("EditAttributes");
        }
        this.IsCompleted = this.ValidateIsCompleted();
      }
      else
      {
        this._canGoNext = false;
        this.IsCompleted = this.ValidateIsCompleted();
        foreach (CheckAttributeValueResult attributeValueResult in attributeValueResultArray)
        {
          CheckAttributeValueResult errorItem = attributeValueResult;
          if (this.DataProvider.ErrorsVertices.ContainsKey(errorItem.ObjectID))
            this.DataProvider.ErrorsVertices[errorItem.ObjectID].Add((errorItem.AttributeID, errorItem.ErrorMessage));
          else
            this.DataProvider.ErrorsVertices.Add(errorItem.ObjectID, new List<(int, string)>()
            {
              (errorItem.AttributeID, errorItem.ErrorMessage)
            });
          DBObjectGraphVertex firstVertexOrDefault = this._session.Graph.GetFirstVertexOrDefault((Predicate<DBObjectGraphVertex>) (x => x.ObjectId == errorItem.ObjectID));
          this.PageErrors.Items.Add(new OperationError(errorItem.ErrorMessage, vertex: firstVertexOrDefault));
        }
        this.DataProvider?.UpdateGridUI();
      }
    }
  }
}

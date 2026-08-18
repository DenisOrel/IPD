
// Type: Intermech.Client.Core.History.ObjectsHistory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.History;

/// <summary>
/// Форма для просмотра и выбора возможных значений атрибутов у определнного типа объекта (связи)
/// </summary>
public class ObjectsHistory : Form
{
  private long _lastKeyValue;
  private long _ID = -1;
  private int _typeID = -1;
  private int _attrID = -1;
  private int _query = -2;
  private AttributableElements _type;
  private SortOrders _sortOrders;
  private List<object> _lastOrderValue;
  private string _caption = LocalizationHolder.rm.GetString("Client.Core_223");
  private bool _nullValue;
  private bool readOnly;
  /// <summary>для чего искать историю</summary>
  private HistoryTypeEnum historyType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnOK;
  private Button _btnDelete;
  private Button _btnHistoryChanges;
  private ComboBox _cmbSort;
  private StatusBar _sBar;
  private TableLayoutPanel _layoutPanel;
  private Label _lbHistoryType;
  private ComboBox _cmbHistory;
  private TextBox _txtItems;
  private ListBox _lstItems;
  private CheckBox _chbUsers;
  private Label _lbSort;
  private Button _btnCancel;

  /// <summary>
  /// 
  /// </summary>
  public bool ReadOnly
  {
    get => this.readOnly;
    set
    {
      this.readOnly = value;
      this.UpdateComponents();
    }
  }

  /// <summary>
  /// Выбранное значение:
  /// set - устанавливает выбранным значение в истории
  /// get - возвращает введенное или выбранное значение в форме
  /// </summary>
  public object SelectedValue
  {
    get
    {
      if (this._lstItems.SelectedIndex >= 0)
      {
        SampleDescriptor selectedItem = this._lstItems.SelectedItem as SampleDescriptor;
        if (selectedItem.ToString().Equals(this._txtItems.Text))
          return selectedItem.Value;
      }
      return !this._nullValue || !(this._txtItems.Text == string.Empty) ? (object) this._txtItems.Text : (object) null;
    }
    set
    {
      this._nullValue = value == null;
      this._txtItems.Text = value != null ? value.ToString() : string.Empty;
    }
  }

  /// <summary>Констуктор.</summary>
  /// <param name="reserved"></param>
  /// <param name="ID"></param>
  /// <param name="type"></param>
  /// <param name="attrID"></param>
  private ObjectsHistory(int reserved, object ID, AttributableElements type, object attrID)
  {
    this.InitializeComponent();
    new ObjectsHistory.SelWindow(this._lstItems).AssignHandle(this._txtItems.Handle);
    this.ParseID(ID, type);
    this.ParseAttrID(attrID);
    this.UpdateComponents();
    this._nullValue = false;
    this.LoadConfiguration();
    this.FillSortOrders();
    this.FillHistoryType();
  }

  /// <summary>Конструктор создания формы истории атрибута.</summary>
  /// <param name="ID">Идентификатор объекта(связи), либо идентификатор типа объекта(связи) - Int64/Int32</param>
  /// <param name="type">Тип идентификатора (объекта/связь)</param>
  /// <param name="attrID">Идентификатор типа атрибута (Int32/Guid)</param>
  public ObjectsHistory(object ID, AttributableElements type, object attrID)
    : this(0, ID, type, attrID)
  {
  }

  /// <summary>Конструктор создания формы истории атрибута.</summary>
  /// <param name="ID">Идентификатор объекта(связи), либо идентификатор типа объекта(связи) - Int64/Int32</param>
  /// <param name="type">Тип идентификатора (объекта/связь)</param>
  /// <param name="attrID">Идентификатор типа атрибута (Int32/Guid)</param>
  /// <param name="order">Принудительная сортировка значений атрибута</param>
  public ObjectsHistory(object ID, AttributableElements type, object attrID, SortOrders order)
    : this(0, ID, type, attrID)
  {
    this._sortOrders = order;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ObjectsHistory_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveConfiguration();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ObjectsHistory_Load(object sender, EventArgs e) => this.UpdateComponents();

  private void UpdateComponents()
  {
    this._btnDelete.Enabled = !this._ID.Equals(-1L) && !this.readOnly;
    this._btnOK.Enabled = !this.readOnly;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDelete_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAHistoryCollection historyCollection = sessionKeeper.Session.GetHistoryCollection(this._attrID);
      AttributeSourceTypes st = AttributeSourceTypes.Auto;
      switch (this._type)
      {
        case AttributableElements.Object:
          st = AttributeSourceTypes.Object;
          break;
        case AttributableElements.Relation:
          st = AttributeSourceTypes.Relation;
          break;
      }
      if (this.historyType == HistoryTypeEnum.ForAllType)
        historyCollection.DeleteHistory(st);
      else if (this.historyType == HistoryTypeEnum.ForSameType)
        historyCollection.DeleteHistory4Type(this._typeID, st);
      else
        historyCollection.DeleteHistory(this._ID, st);
    }
    this.RefreshData(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnHistoryChanges_Click(object sender, EventArgs e)
  {
    using (HistoryChangesView historyChangesView = new HistoryChangesView(this._ID, this._typeID, this._type, this._attrID))
    {
      int num = (int) historyChangesView.ShowDialog();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnReadAll_Click(object sender, EventArgs e)
  {
    this._query = -1;
    this.RefreshData(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnReadMore_Click(object sender, EventArgs e) => this.RefreshData(false);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbHistory_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.historyType = (HistoryTypeEnum) Enum.GetValues(typeof (HistoryTypeEnum)).GetValue(this._cmbHistory.SelectedIndex);
    this.RefreshData(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbItems_DoubleClick(object sender, EventArgs e)
  {
    if (this.readOnly)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbItems_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._txtItems.TextChanged -= new EventHandler(this.On_txtItems_TextChanged);
    this._txtItems.Text = this._lstItems.SelectedIndex >= 0 ? this._lstItems.SelectedItem.ToString() : string.Empty;
    this._txtItems.Focus();
    this._txtItems.SelectAll();
    this._txtItems.TextChanged += new EventHandler(this.On_txtItems_TextChanged);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbSort_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._sortOrders = (SortOrders) EnumTypeHelper.GetEnumValue(typeof (SortOrders), this._cmbSort.Text, (object) SortOrders.NONE);
    this._lstItems.BeginUpdate();
    try
    {
      ArrayList arrayList = new ArrayList();
      foreach (SampleDescriptor sampleDescriptor in this._lstItems.Items)
        arrayList.Add(sampleDescriptor.Value);
      switch (this._sortOrders)
      {
        case SortOrders.ASC:
          arrayList.Sort();
          break;
        case SortOrders.DESC:
          arrayList.Reverse();
          break;
      }
      this._lstItems.Items.Clear();
      foreach (object obj in arrayList)
        this._lstItems.Items.Add((object) new SampleDescriptor(obj.ToString(), (object) obj.ToString()));
    }
    finally
    {
      this._lstItems.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbUsers_CheckedChanged(object sender, EventArgs e) => this.RefreshData(true);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtItems_KeyDown(object sender, KeyEventArgs e)
  {
    e.Handled = false;
    switch (e.KeyCode)
    {
      case Keys.Up:
        if (this._lstItems.SelectedIndex > 0 && this._lstItems.SelectedIndex <= this._lstItems.Items.Count - 1)
          --this._lstItems.SelectedIndex;
        e.Handled = true;
        break;
      case Keys.Down:
        if (this._lstItems.SelectedIndex >= 0 && this._lstItems.SelectedIndex < this._lstItems.Items.Count - 1)
          ++this._lstItems.SelectedIndex;
        e.Handled = true;
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtItems_TextChanged(object sender, EventArgs e)
  {
    this._lstItems.SelectedIndexChanged -= new EventHandler(this.On_cmbItems_SelectedIndexChanged);
    string text = this._txtItems.Text;
    bool flag = false;
    if (!text.Equals(string.Empty))
    {
      for (int index = 0; index < this._lstItems.Items.Count; ++index)
      {
        if (this._lstItems.Items[index].ToString().StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
        {
          this._lstItems.SelectedIndex = index;
          flag = true;
          break;
        }
      }
    }
    if (!flag)
      this._lstItems.SelectedIndex = -1;
    this._lstItems.SelectedIndexChanged += new EventHandler(this.On_cmbItems_SelectedIndexChanged);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbObjects_CheckedChanged(object sender, EventArgs e) => this.RefreshData(true);

  /// <summary>
  /// 
  /// </summary>
  private void LoadConfiguration()
  {
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
    {
      IConfiguration configuration = service.Open(ConfigForHistoryForm.Section);
      if (configuration != null)
      {
        if (configuration.HasProperty(ConfigForHistoryForm.SortOrder))
          this._sortOrders = (SortOrders) EnumTypeHelper.GetEnumValue(typeof (SortOrders), configuration.GetProperty(ConfigForHistoryForm.SortOrder), (object) SortOrders.NONE);
        if (configuration.HasProperty(ConfigForHistoryForm.UseUserHistory))
          this._chbUsers.Checked = Convert.ToBoolean(configuration.GetProperty(ConfigForHistoryForm.UseUserHistory));
        if (configuration.HasProperty(ConfigForHistoryForm.HistoryType))
        {
          int int32 = Convert.ToInt32(configuration.GetProperty(ConfigForHistoryForm.HistoryType));
          this.historyType = (HistoryTypeEnum) Enum.GetValues(typeof (HistoryTypeEnum)).GetValue(int32);
        }
      }
    }
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveConfiguration()
  {
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
    {
      IConfiguration configuration = service.Open(ConfigForHistoryForm.Section) ?? service.Create(ConfigForHistoryForm.Section);
      configuration.SetProperty(ConfigForHistoryForm.SortOrder, EnumTypeHelper.GetCaption((Enum) this._sortOrders));
      configuration.SetProperty(ConfigForHistoryForm.UseUserHistory, this._chbUsers.Checked.ToString());
      configuration.SetProperty(ConfigForHistoryForm.HistoryType, ((int) this.historyType).ToString());
    }
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Заполнить комбобокс с типом поиска истории.</summary>
  private void FillHistoryType()
  {
    this._cmbHistory.BeginUpdate();
    try
    {
      this._cmbHistory.Items.Clear();
      foreach (int num in (HistoryTypeEnum[]) Enum.GetValues(typeof (HistoryTypeEnum)))
        this._cmbHistory.Items.Add((object) EnumTypeHelper.GetCaption((Enum) (HistoryTypeEnum) num));
      this._cmbHistory.SelectedIndex = (int) this.historyType;
    }
    finally
    {
      this._cmbHistory.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void FillSortOrders()
  {
    this._cmbSort.BeginUpdate();
    try
    {
      this._cmbSort.Items.Clear();
      foreach (FieldInfo field in typeof (SortOrders).GetFields())
      {
        string caption = EnumTypeHelper.GetCaption((Enum) (SortOrders) field.GetValue((object) SortOrders.NONE));
        if (!this._cmbSort.Items.Contains((object) caption))
          this._cmbSort.Items.Add((object) caption);
      }
      this._cmbSort.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) this._sortOrders);
    }
    finally
    {
      this._cmbSort.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void GetValues()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAHistoryCollection historyCollection = sessionKeeper.Session.GetHistoryCollection(this._attrID);
      object[] columns = new object[2]
      {
        (object) -57,
        (object) historyCollection.TextFieldID
      };
      if (!historyCollection.TextFieldID.Equals(historyCollection.ValueFieldID))
        columns = new object[3]
        {
          (object) -57,
          (object) historyCollection.TextFieldID,
          (object) historyCollection.ValueFieldID
        };
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      conditionStructureList.Add(new ConditionStructure(-58, RelationalOperators.Equal, (object) this._attrID, LogicalOperators.AND, 0, false));
      ConditionStructure conditionStructure1 = new ConditionStructure(-7, RelationalOperators.Equal, (object) this._typeID, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2 = new ConditionStructure(-3, RelationalOperators.Equal, (object) this._ID, LogicalOperators.AND, 0, false);
      conditionStructure2.Content = ColumnContents.ID;
      switch (this._type)
      {
        case AttributableElements.None:
          return;
        case AttributableElements.Object:
          if (this.historyType == HistoryTypeEnum.ForSameType)
          {
            conditionStructureList.Add(conditionStructure1);
            break;
          }
          if (this.historyType == HistoryTypeEnum.ForObject)
          {
            conditionStructureList.Add(conditionStructure2);
            break;
          }
          break;
        case AttributableElements.Relation:
          if (this.historyType == HistoryTypeEnum.ForSameType)
          {
            conditionStructure1.Attribute = (object) -23;
            conditionStructureList.Add(conditionStructure1);
            break;
          }
          if (this.historyType == HistoryTypeEnum.ForObject)
          {
            conditionStructureList.Add(conditionStructure2);
            break;
          }
          break;
      }
      conditionStructureList.Add(new ConditionStructure(historyCollection.TextFieldID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false));
      if (!this._chbUsers.Checked)
      {
        conditionStructureList.Add(new ConditionStructure(-36, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.AND, 0, false));
        conditionStructureList.Add(new ConditionStructure(-52, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false));
      }
      if (this._query.Equals(-1))
      {
        this._lastKeyValue = 0L;
        this._lastOrderValue = (List<object>) null;
      }
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns, (object[]) null, (SortOrders[]) null);
      foreach (DataRow row in (InternalDataCollectionBase) historyCollection.Select(paramSet).Rows)
      {
        this._lastKeyValue = Convert.ToInt64(row[0]);
        this._lastOrderValue = new List<object>(1);
        this._lastOrderValue.Add(row[1]);
        SampleDescriptor sampleDescriptor;
        if (historyCollection.TextFieldID.Equals(historyCollection.ValueFieldID))
        {
          sampleDescriptor = new SampleDescriptor(Convert.ToString(row[1]), (object) Convert.ToString(row[1]));
        }
        else
        {
          object obj = row[2];
          sampleDescriptor = new SampleDescriptor(Convert.ToString(row[1]), obj);
        }
        if (!this._lstItems.Items.Contains((object) sampleDescriptor))
          this._lstItems.Items.Add((object) sampleDescriptor);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrID"></param>
  private void ParseAttrID(object attrID)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    if (attrID.GetType().Equals(typeof (int)))
      this._attrID = Convert.ToInt32(attrID);
    else if (attrID.GetType().Equals(typeof (Guid)))
    {
      this._attrID = service.GetAttributeType((Guid) attrID, true).AttributeID;
    }
    else
    {
      if (!attrID.GetType().Equals(typeof (string)))
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_224"));
      this._attrID = service.GetAttributeType(Convert.ToString(attrID), true).AttributeID;
    }
    this.Text = string.Format(this._caption, (object) service.GetAttributeType(this._attrID).Name);
    this.AcceptButton = (IButtonControl) this._btnOK;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ID"></param>
  /// <param name="type"></param>
  private void ParseID(object ID, AttributableElements type)
  {
    this._type = type;
    if (ID.GetType().Equals(typeof (int)))
    {
      this._typeID = Convert.ToInt32(ID);
      this._cmbHistory.Enabled = this._lbHistoryType.Enabled = false;
    }
    else
    {
      long num = ID.GetType().Equals(typeof (long)) ? Convert.ToInt64(ID) : throw new Exception(LocalizationHolder.rm.GetString("Client.Core_225"));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        switch (type)
        {
          case AttributableElements.Object:
            IDBObject dbObject = sessionKeeper.Session.GetObject(num);
            this._ID = dbObject.ID;
            this._typeID = dbObject.ObjectType;
            break;
          case AttributableElements.Relation:
            IDBRelation relation = sessionKeeper.Session.GetRelation(num);
            this._ID = num;
            this._typeID = relation.RelationType;
            break;
        }
      }
      this._cmbHistory.Enabled = this._lbHistoryType.Enabled = true;
    }
    this._btnHistoryChanges.Enabled = !this._ID.Equals(-1L);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="doClear"></param>
  private void RefreshData(bool doClear)
  {
    this._lstItems.BeginUpdate();
    try
    {
      if (doClear)
      {
        this._lastKeyValue = 0L;
        this._lastOrderValue = (List<object>) null;
        this._lstItems.Items.Clear();
      }
      this.GetValues();
      this._sBar.Text = LocalizationHolder.rm.GetString("Client.Core_227");
    }
    finally
    {
      this._lstItems.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void SetReadOnly() => this.ReadOnly = true;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectsHistory));
    this._sBar = new StatusBar();
    this._layoutPanel = new TableLayoutPanel();
    this._btnDelete = new Button();
    this._cmbSort = new ComboBox();
    this._btnHistoryChanges = new Button();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._lbSort = new Label();
    this._chbUsers = new CheckBox();
    this._lstItems = new ListBox();
    this._txtItems = new TextBox();
    this._cmbHistory = new ComboBox();
    this._lbHistoryType = new Label();
    this._layoutPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._sBar, "_sBar");
    this._sBar.Name = "_sBar";
    componentResourceManager.ApplyResources((object) this._layoutPanel, "_layoutPanel");
    this._layoutPanel.Controls.Add((Control) this._lbHistoryType, 0, 1);
    this._layoutPanel.Controls.Add((Control) this._cmbHistory, 0, 1);
    this._layoutPanel.Controls.Add((Control) this._txtItems, 0, 3);
    this._layoutPanel.Controls.Add((Control) this._lstItems, 0, 4);
    this._layoutPanel.Controls.Add((Control) this._chbUsers, 0, 2);
    this._layoutPanel.Controls.Add((Control) this._cmbSort, 1, 0);
    this._layoutPanel.Controls.Add((Control) this._lbSort, 0, 0);
    this._layoutPanel.Controls.Add((Control) this._btnOK, 6, 5);
    this._layoutPanel.Controls.Add((Control) this._btnCancel, 7, 5);
    this._layoutPanel.Controls.Add((Control) this._btnDelete, 0, 5);
    this._layoutPanel.Controls.Add((Control) this._btnHistoryChanges, 1, 5);
    this._layoutPanel.Name = "_layoutPanel";
    componentResourceManager.ApplyResources((object) this._btnDelete, "_btnDelete");
    this._btnDelete.Name = "_btnDelete";
    this._btnDelete.Click += new EventHandler(this.On_btnDelete_Click);
    this._layoutPanel.SetColumnSpan((Control) this._cmbSort, 7);
    componentResourceManager.ApplyResources((object) this._cmbSort, "_cmbSort");
    this._cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbSort.Name = "_cmbSort";
    this._cmbSort.SelectedIndexChanged += new EventHandler(this.On_cmbSort_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._btnHistoryChanges, "_btnHistoryChanges");
    this._btnHistoryChanges.Name = "_btnHistoryChanges";
    this._btnHistoryChanges.Click += new EventHandler(this.On_btnHistoryChanges_Click);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._lbSort, "_lbSort");
    this._lbSort.Name = "_lbSort";
    componentResourceManager.ApplyResources((object) this._chbUsers, "_chbUsers");
    this._layoutPanel.SetColumnSpan((Control) this._chbUsers, 8);
    this._chbUsers.Name = "_chbUsers";
    this._chbUsers.CheckedChanged += new EventHandler(this.On_chbUsers_CheckedChanged);
    this._layoutPanel.SetColumnSpan((Control) this._lstItems, 8);
    componentResourceManager.ApplyResources((object) this._lstItems, "_lstItems");
    this._lstItems.Name = "_lstItems";
    this._lstItems.TabStop = false;
    this._lstItems.SelectedIndexChanged += new EventHandler(this.On_cmbItems_SelectedIndexChanged);
    this._lstItems.DoubleClick += new EventHandler(this.On_cmbItems_DoubleClick);
    this._layoutPanel.SetColumnSpan((Control) this._txtItems, 8);
    componentResourceManager.ApplyResources((object) this._txtItems, "_txtItems");
    this._txtItems.Name = "_txtItems";
    this._txtItems.TextChanged += new EventHandler(this.On_txtItems_TextChanged);
    this._layoutPanel.SetColumnSpan((Control) this._cmbHistory, 7);
    componentResourceManager.ApplyResources((object) this._cmbHistory, "_cmbHistory");
    this._cmbHistory.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbHistory.Name = "_cmbHistory";
    this._cmbHistory.SelectedIndexChanged += new EventHandler(this.On_cmbHistory_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._lbHistoryType, "_lbHistoryType");
    this._lbHistoryType.Name = "_lbHistoryType";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._layoutPanel);
    this.Controls.Add((Control) this._sBar);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ObjectsHistory);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.ObjectsHistory_FormClosed);
    this.Load += new EventHandler(this.ObjectsHistory_Load);
    this._layoutPanel.ResumeLayout(false);
    this._layoutPanel.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  internal class SelWindow : NativeWindow
  {
    private const int WM_MOUSEWEEL = 522;
    private ListBox _listBox;

    /// <summary>Конструктор.</summary>
    /// <param name="listBox"></param>
    public SelWindow(ListBox listBox) => this._listBox = listBox;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m"></param>
    protected override void WndProc(ref Message m)
    {
      if (m.Msg.Equals(522))
        ObjectsHistory.SelWindow.SendMessage(new HandleRef((object) this._listBox, this._listBox.Handle), m.Msg, m.WParam, m.LParam);
      base.WndProc(ref m);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);
  }
}

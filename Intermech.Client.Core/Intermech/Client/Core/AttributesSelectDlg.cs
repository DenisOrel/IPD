
// Type: Intermech.Client.Core.AttributesSelectDlg
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// 
/// </summary>
public class AttributesSelectDlg : Form
{
  /// <summary>
  /// Хранит 10 последних введенных слов при поиске атрибута по имени
  /// </summary>
  private List<string> _lastNames = new List<string>(10);
  /// <summary>
  /// Хранит 10 последних введенных слов при поиске атрибута по короткому имени
  /// </summary>
  private List<string> _lastShortNames = new List<string>(10);
  /// <summary>
  /// Содержит список типов атрибутов, которые можно использовать
  /// </summary>
  private List<FieldTypes> _allowedAttrTypes = new List<FieldTypes>(0);
  /// <summary>
  /// Содержит список типов атрибутов, которые нельзя использовать
  /// </summary>
  private List<FieldTypes> _forbiddenAttrTypes = new List<FieldTypes>(0);
  /// <summary>
  /// Содержит список типов объектов, которые можно использовать
  /// </summary>
  private List<int> _allowedObjTypes = new List<int>();
  /// <summary>Таблица содержит все атрибуты</summary>
  private DataTable _dtAllAttrs;
  /// <summary>Таблица содержит все видимые атрибуты</summary>
  private DataTable _dtAllVisibleAttrs;
  private DataTable _dt;
  /// <summary>Список системных атрибутов</summary>
  private List<int> _obligatoryAttrs;
  /// <summary>
  /// Применяется в том случае, если необходимо дать возможность выбора из определенного списка атрибутов.
  /// Создавалось для случая добавления атрибутов, выделенным в навигаторе объектам.
  /// При этом искались атрибуты, которые можно добавить всем выделенны объектам (всем типам).
  /// При этом в верхней части диалога отображались типы выделенных объектов, а в нижней - общие для них атрибуты.
  /// </summary>
  private List<int> _commonAttrs;
  /// <summary>
  /// Список идентификаторов атрибутов, которые должны быть выделены при появлении диалога
  /// </summary>
  private int _selectOnStartupAttrID;
  /// <summary>
  /// Идентификатор объекта, если в диалоге нужно показать атрибуты для конкретного объекта
  /// </summary>
  private long _selectedObjID;
  /// <summary>
  /// Идентификатор связи, если в диалоге нужно показать атрибуты для конкретной связи
  /// </summary>
  private long _selectedRelID;
  private bool _showAutoRequiredAttrs = true;
  /// <summary>
  /// Необходимо отображать только атрибуты, принадлежащие указанному типу объектов/связи
  /// </summary>
  private bool _typeAttrsOnly;
  /// <summary>
  /// Оставлять среди обязательных атрибутов только атрибуты, относящиеся к атрибутам объектов или связей
  /// </summary>
  private AllowedAttrsSourceTypesEnum _allowedAttrSourceTypes = AllowedAttrsSourceTypesEnum.Objects | AllowedAttrsSourceTypesEnum.Relations;
  /// <summary>Индекс выбранной группы ограничений</summary>
  private int _constraintGroupIndex;
  /// <summary>
  /// Словарь с выбранными данными для каждой группы атрибутов
  /// </summary>
  private Dictionary<int, Tuple<string, Guid>> _dict;
  private bool _lockEvents;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private RadioButton _rbGroup;
  private RadioButton _rbRelTypes;
  private RadioButton _rbAll;
  private RadioButton _rbObjTypes;
  private GroupBox _grbСonstraint;
  private GroupBox _grbFind;
  private ComboBox _cmbFindCondition;
  private DataGridView _dgvFindAttr;
  private Button _btnOK;
  private Button _btnCancel;
  private RadioButton _rbShortName;
  private RadioButton _rbName;
  private Panel _pnlConstraint;
  private Button _btnConstraint;
  private TextBox _txtConstraint;
  private DataSet _ds;
  private DataTable tblAttrs;
  private DataColumn colID;
  private DataColumn colImage;
  private DataColumn colName;
  private BindingSource _bindSource;
  private DataColumn colShortName;
  private DataColumn colType;
  private Button _btnCreateAttr;
  private ImageList _imgList;
  private Panel _pnlAttributes;
  private RichTextBox _rtbDescription;
  private Splitter _splDescriptor;
  private GroupBox _grbCommonAttrs;
  private ListView _lv;
  private ColumnHeader _header;
  private CheckBox _chbViewAllAttributes;
  private CheckBox _chbSearchAccuracy;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewImageColumn dataGridViewImageColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
  private DataGridViewTextBoxColumn ID;
  private DataGridViewTextBoxColumn Image;
  private DataGridViewTextBoxColumn AttrName;
  private DataGridViewTextBoxColumn AttrShortName;
  private DataGridViewTextBoxColumn AttrType;

  /// <summary>
  /// 
  /// </summary>
  private DataTable AllAttributes
  {
    get => !this._chbViewAllAttributes.Checked ? this._dtAllVisibleAttrs : this._dtAllAttrs;
  }

  /// <summary>
  /// Определяет принадлежит атрибут типу объектов/связей или нет.
  /// </summary>
  public AllowedAttrsSourceTypesEnum AllowedAttributesSourceTypes
  {
    get => this._allowedAttrSourceTypes;
    set
    {
      if (value == AllowedAttrsSourceTypesEnum.All || (value & AllowedAttrsSourceTypesEnum.All) == AllowedAttrsSourceTypesEnum.All)
        this._allowedAttrSourceTypes = AllowedAttrsSourceTypesEnum.All;
      else
        this._allowedAttrSourceTypes = value;
    }
  }

  /// <summary>Выключить пункт выбора атрибутов объектов.</summary>
  /// <remarks>
  /// Необходимость появилась при изменении/добавлении атрибутов связей. Изменять и добавлять можно только атрибуты связей а не объектов.
  /// По аналогии с RelationGroupEnable.
  /// </remarks>
  public bool ObjectGroupEnable
  {
    get => this._rbObjTypes.Enabled;
    set => this._rbObjTypes.Enabled = value;
  }

  /// <summary>Выключить пункт выбора атрибутов связи.</summary>
  /// <remarks>
  /// Необходимость появилась при изменении/добавлении атрибутов объектов с составе. Изменять и добавлять можно только атрибуты объектов а не связей.
  /// Чтобы не вводить в заблуждение пользователя, по указанию Д.Жукова, сделал пункт выбора атрибутов связи недоступным.
  /// </remarks>
  public bool RelationGroupEnable
  {
    get => this._rbRelTypes.Enabled;
    set => this._rbRelTypes.Enabled = value;
  }

  /// <summary>Показывать кнопку создания нового атрибута.</summary>
  public bool ShowCreateAttrBtn
  {
    get => this._btnCreateAttr.Visible;
    set => this._btnCreateAttr.Visible = value;
  }

  /// <summary>
  /// Необходимо отображать только атрибуты, принадлежащие указанному типу объектов/связи.
  /// При этом убирается возможность выбора другого раздела ("Все атрибуты", "Группа атрибутов" и ).
  /// </summary>
  public bool TypeAttributesOnly
  {
    get => this._typeAttrsOnly;
    set
    {
      this._typeAttrsOnly = value;
      this._rbAll.Enabled = this._rbGroup.Enabled = this._rbRelTypes.Enabled = !value;
    }
  }

  /// <summary>Выбранные типы атрибутов по Guid.</summary>
  public List<Guid> SelectedAttributesGuid { get; private set; }

  /// <summary>Выбранные типы атрибутов по ID.</summary>
  public List<int> SelectedAttributesID { get; private set; }

  /// <summary>Возвращает Guid выделенного типа объекта.</summary>
  public Guid SelectedObjectGuid { get; private set; }

  /// <summary>
  /// Содержит список типов атрибутов, которые можно использовать.
  /// </summary>
  /// <remarks>
  /// Реализовано таким образом, что если задан этот список, то список запрещенных типов атрибутов обнуляется.
  /// Если этого не делать, то возникает неоднозначность, когда один и тотже тип атрибута содержится в обоих списках.
  /// </remarks>
  public List<FieldTypes> AllowedAttrsTypesFilter
  {
    get => this._allowedAttrTypes;
    set
    {
      this._allowedAttrTypes = value ?? new List<FieldTypes>(0);
      if (this._allowedAttrTypes.Count <= 0)
        return;
      this._forbiddenAttrTypes.Clear();
    }
  }

  /// <summary>
  /// Содержит список типов атрибутов, которые нельзя использовать.
  /// </summary>
  /// <remarks>
  /// Реализовано таким образом, что если задан этот список, то список разрешенных типов атрибутов обнуляется.
  /// Если этого не делать, то возникает неоднозначность, когда один и тотже тип атрибута содержится в обоих списках.
  /// </remarks>
  public List<FieldTypes> ForbiddenAttrsTypesFilter
  {
    get => this._forbiddenAttrTypes;
    set
    {
      this._forbiddenAttrTypes = value ?? new List<FieldTypes>(0);
      if (this._forbiddenAttrTypes.Count <= 0)
        return;
      this._allowedAttrTypes.Clear();
    }
  }

  /// <summary>
  /// Список обязательных атрибутов объектов и связей, которые необходимо добавить.
  /// </summary>
  /// <remark>Необходимость появилась при редактировании атрибутов у группы объектов (для атрибута "Владелец объекта")</remark>
  public List<int> ObligatoryAttrsList
  {
    get => this._obligatoryAttrs;
    set => this._obligatoryAttrs = value ?? new List<int>(0);
  }

  /// <summary>Фильтр отображаемых атрибутов.</summary>
  public ISelectorFilter SelectorFilter { get; set; }

  /// <summary>
  /// Конструктор. Применяется для общего случая с выбором более одного атрибута.
  /// </summary>
  /// <param name="bMultiSelect">Множественный выбор атрибутов</param>
  public AttributesSelectDlg(bool bMultiSelect)
  {
    this.InitializeComponent();
    this.SelectedAttributesGuid = new List<Guid>();
    this.SelectedAttributesID = new List<int>();
    this.SelectedObjectGuid = Guid.Empty;
    this._imgList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._dgvFindAttr.MultiSelect = bMultiSelect;
    this._dt = this._ds.Tables[nameof (tblAttrs)];
    this._dtAllAttrs = this._dt.Clone();
    this._dtAllVisibleAttrs = this._dt.Clone();
    this._grbСonstraint.Width = this._grbFind.Width;
    this._dict = new Dictionary<int, Tuple<string, Guid>>()
    {
      {
        0,
        Tuple.Create<string, Guid>(string.Empty, Guid.Empty)
      },
      {
        1,
        Tuple.Create<string, Guid>(string.Empty, Guid.Empty)
      },
      {
        2,
        Tuple.Create<string, Guid>(string.Empty, Guid.Empty)
      },
      {
        3,
        Tuple.Create<string, Guid>(string.Empty, Guid.Empty)
      }
    };
  }

  /// <summary>Конструктор.</summary>
  /// <param name="bMultiSelect">Множественный выбор атрибутов</param>
  /// <param name="arrAttrID"></param>
  public AttributesSelectDlg(bool bMultiSelect, int[] arrAttrID)
    : this(bMultiSelect)
  {
    foreach (int attrTypeID in arrAttrID)
    {
      if (attrTypeID >= 0 && !this.SelectedAttributesID.Contains(attrTypeID))
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
        if (!(attributeTypeGuid == Guid.Empty))
        {
          this.SelectedAttributesID.Add(attrTypeID);
          this.SelectedAttributesGuid.Add(attributeTypeGuid);
        }
      }
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="bMultiSelect">Множественный выбор атрибутов</param>
  /// <param name="arrAttrGuid"></param>
  public AttributesSelectDlg(bool bMultiSelect, Guid[] arrAttrGuid)
    : this(bMultiSelect)
  {
    foreach (Guid attrTypeGuid in arrAttrGuid)
    {
      if (!(attrTypeGuid == Guid.Empty))
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
        if (attributeTypeId >= 0 && !this.SelectedAttributesID.Contains(attributeTypeId))
        {
          this.SelectedAttributesID.Add(attributeTypeId);
          this.SelectedAttributesGuid.Add(attrTypeGuid);
        }
      }
    }
  }

  /// <summary>
  /// По просьбе А.Куприянчика написано событие, которое проверяет выбранные атрибуты перед закрытием формы.
  /// </summary>
  /// <remarks>Нужно не дать выбирать атрибуты, которые запрещено редактировать вручную</remarks>
  public event EventHandler<CancelEventArgs> BeforeClosing;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnConstraint_Click(object sender, EventArgs e)
  {
    Tuple<string, Guid> tuple = this._dict[this._constraintGroupIndex];
    using (TreeViewForm treeViewForm = new TreeViewForm(this._constraintGroupIndex, this._allowedObjTypes))
    {
      if (tuple.Item2 != Guid.Empty)
        treeViewForm.SelectedNodeData = new Hashtable(1)
        {
          {
            (object) tuple.Item2,
            (object) tuple.Item1
          }
        };
      if (treeViewForm.ShowDialog((IWin32Window) this) != DialogResult.OK || treeViewForm.SelectedNodeData.Keys.Count <= 0)
        return;
      IEnumerator enumerator = treeViewForm.SelectedNodeData.Keys.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          Guid guid = this._rbGroup.Checked ? MetaDataHelper.GetAttributeGroupGuid(Convert.ToInt32(current)) : new Guid(Convert.ToString(current));
          if (tuple.Item2 == guid)
            return;
          this._dict[this._constraintGroupIndex] = Tuple.Create<string, Guid>(Convert.ToString(treeViewForm.SelectedNodeData[current]), guid);
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      this.SelectedChenged(this._constraintGroupIndex);
    }
  }

  /// <summary>Создать новый атрибут.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCreateAttr_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service))
      return;
    int attrTypeID = service.AddAttribute(LocalizationHolder.rm.GetString("CreateAttribute_DialogCaption"), (int[]) null);
    if (attrTypeID == 0)
      return;
    this._dtAllAttrs.Rows.Clear();
    this._lockEvents = true;
    this._rbAll.Checked = this._rbName.Checked = true;
    this._lockEvents = false;
    this.LoadAttributesCollection();
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
    this._cmbFindCondition.Text = attributeType != null ? attributeType.Name : string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbSearchAccuracy_CheckedChanged(object sender, EventArgs e)
  {
    this.SearchAttribute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbViewAllAttributes_CheckedChanged(object sender, EventArgs e)
  {
    this.LoadAttributesCollection();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbFindCondition_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.EndSelectionAttribute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbFindCondition_Leave(object sender, EventArgs e)
  {
    this.FillcmbFindConditionItems(true);
  }

  /// <summary>Изменение текста в контроле поиска.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbFindCondition_TextChanged(object sender, EventArgs e)
  {
    this.SearchAttribute();
  }

  /// <summary>Сортировка по типу атрибута.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <remarks>Создавалось для сортировки иконок.</remarks>
  private void On_dgvFindAttr_ColumnHeaderMouseClick(
    object sender,
    DataGridViewCellMouseEventArgs e)
  {
    DataGridView dataGridView = sender as DataGridView;
    if (dataGridView.Columns["Image"].Index != e.ColumnIndex)
      return;
    dataGridView.Sort(dataGridView.Columns["AttrType"], ListSortDirection.Ascending);
  }

  /// <summary>Выбор атрибута по двойному клику.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgvFindAttr_DoubleClick(object sender, EventArgs e)
  {
    this.EndSelectionAttribute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgvFindAttr_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.Return)
      return;
    this.EndSelectionAttribute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgvFindAttr_SelectionChanged(object sender, EventArgs e)
  {
    this._rtbDescription.Text = string.Empty;
    if (this._dgvFindAttr.SelectedRows.Count != 1)
      return;
    int result = 0;
    if (!int.TryParse(Convert.ToString(this._dgvFindAttr.SelectedRows[0].Cells["ID"].Value), out result))
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(result);
    this._rtbDescription.Text = attributeType != null ? attributeType.Note : string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lv_SizeChanged(object sender, EventArgs e)
  {
    if (this._lv == null || this._lv.Columns.Count <= 0 || this._lv.Columns[0] == null)
      return;
    this._lv.Columns[0].Width = -2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnСonstraintRadion_CheckedChanged(object sender, EventArgs e)
  {
    RadioButton radioButton = sender as RadioButton;
    if (!radioButton.Checked || this._lockEvents)
      return;
    this._constraintGroupIndex = (int) Convert.ToInt16(radioButton.Tag);
    this.SelectedChenged(this._constraintGroupIndex);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnFindRadio_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender as RadioButton).Checked)
      return;
    this._cmbFindCondition.Text = string.Empty;
    this.FillcmbFindConditionItems(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private void OnBeforeClosing(CancelEventArgs e)
  {
    EventHandler<CancelEventArgs> beforeClosing = this.BeforeClosing;
    if (beforeClosing == null)
      return;
    beforeClosing((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    HybridDictionary hybridDictionary = new HybridDictionary(0, true);
    FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary);
    int result = 0;
    if (int.TryParse(Convert.ToString(hybridDictionary[(object) "ColumnNameWidth"]), out result))
      this._dgvFindAttr.Columns["AttrName"].Width = result;
    if ((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
      this._chbViewAllAttributes.Visible = true;
    else
      this._chbViewAllAttributes.Visible = this._chbViewAllAttributes.Checked = false;
    if (this._imgList != null)
    {
      this._rbAll.Image = this._imgList.Images[Statics.IconSrv.IndexOf(3, 0)];
      this._rbGroup.Image = this._imgList.Images[Statics.IconSrv.IndexOf(12, 0)];
      this._rbObjTypes.Image = this._imgList.Images[Statics.IconSrv.IndexOf(4, 0)];
      this._rbRelTypes.Image = this._imgList.Images[Statics.IconSrv.IndexOf(6, 0)];
    }
    this.LoadInfo();
    this.LoadAttributesCollection();
    this._selectOnStartupAttrID = 0;
    this._cmbFindCondition.Select();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    this.SelectedAttributesID.Clear();
    this.SelectedAttributesGuid.Clear();
    foreach (DataGridViewRow selectedRow in (BaseCollection) this._dgvFindAttr.SelectedRows)
    {
      int int32 = Convert.ToInt32(selectedRow.Cells["ID"].EditedFormattedValue);
      this.SelectedAttributesID.Add(int32);
      this.SelectedAttributesGuid.Add(MetaDataHelper.GetAttributeTypeGuid(int32));
    }
    this.SaveInfo();
    this.OnBeforeClosing(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this, (IDictionary) new HybridDictionary(0, true)
    {
      {
        (object) "ColumnNameWidth",
        (object) this._dgvFindAttr.Columns["AttrName"].Width
      }
    });
  }

  /// <summary>
  /// Проверка, принадлежит ли тип атрибута к разрешенным типам атрибутов.
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <returns>Результат</returns>
  private bool IsAllowedAttributeSourceType(int attrID)
  {
    bool flag = true;
    if (this._allowedAttrSourceTypes != AllowedAttrsSourceTypesEnum.All && attrID <= 0)
    {
      switch (ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attrID))
      {
        case AttributeSourceTypes.Object:
          flag = (this._allowedAttrSourceTypes & AllowedAttrsSourceTypesEnum.Objects) == AllowedAttrsSourceTypesEnum.Objects;
          break;
        case AttributeSourceTypes.Relation:
          flag = (this._allowedAttrSourceTypes & AllowedAttrsSourceTypesEnum.Relations) == AllowedAttrsSourceTypesEnum.Relations;
          break;
        default:
          flag = false;
          break;
      }
    }
    return flag;
  }

  /// <summary>Проверка на попадание в фильтр.</summary>
  /// <param name="attrID">ID атрибута</param>
  /// <returns>Результат</returns>
  private bool IsInFilter(int attrID)
  {
    return this.SelectorFilter != null && this.SelectorFilter.IsInFilter(3, (object) attrID);
  }

  /// <summary>Загружается список атрибутов.</summary>
  private void LoadAttributesCollection()
  {
    if (this._selectedObjID != 0L)
    {
      this._bindSource.DataSource = (object) this._dt;
      this._dt.Rows.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._selectedObjID, false);
        if (objectActualCopy != null)
        {
          GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.CheckWriteAccess;
          if (!this._chbViewAllAttributes.Checked)
            modes |= GetAttributeValuesModes.CheckVisibility;
          AttributeValues[] attributesValues = objectActualCopy.GetAttributesValues(modes);
          if (attributesValues != null)
          {
            if (!this._showAutoRequiredAttrs)
            {
              foreach (AttributeValues attributeValues in attributesValues)
              {
                IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objectActualCopy.ObjectType, attributeValues.AttributeID);
                if (attribute4ObjectType == null || attribute4ObjectType.Required != RequiredModes.AutoRequired)
                  this.AddAttrToTable(MetaDataHelper.GetAttributeType(attributeValues.AttributeID), this._dt);
              }
            }
            else
            {
              foreach (AttributeValues attributeValues in attributesValues)
                this.AddAttrToTable(MetaDataHelper.GetAttributeType(attributeValues.AttributeID), this._dt);
            }
          }
          this.LoadObligatoryAttrs();
        }
      }
    }
    else if (this._selectedRelID != 0L)
    {
      this._bindSource.DataSource = (object) this._dt;
      this._dt.Rows.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._selectedRelID, false);
        if (relation != null)
        {
          GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.CheckWriteAccess;
          if (!this._chbViewAllAttributes.Checked)
            modes |= GetAttributeValuesModes.CheckVisibility;
          AttributeValues[] attributesValues = relation.GetAttributesValues(modes);
          if (attributesValues != null)
          {
            if (!this._showAutoRequiredAttrs)
            {
              foreach (AttributeValues attributeValues in attributesValues)
              {
                IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relation.RelationType, attributeValues.AttributeID);
                if (attribute4RelationType == null || attribute4RelationType.Required != RequiredModes.AutoRequired)
                  this.AddAttrToTable(MetaDataHelper.GetAttributeType(attributeValues.AttributeID), this._dt);
              }
            }
            else
            {
              foreach (AttributeValues attributeValues in attributesValues)
                this.AddAttrToTable(MetaDataHelper.GetAttributeType(attributeValues.AttributeID), this._dt);
            }
          }
          this.LoadObligatoryAttrs();
        }
      }
    }
    else if (this._commonAttrs == null)
    {
      DataTable allAttributes = this.AllAttributes;
      if (this._rbAll.Checked)
      {
        this._bindSource.DataSource = (object) allAttributes;
        if (allAttributes.Rows.Count == 0)
          this.LoadAllAttributes(allAttributes);
      }
      else
      {
        this._bindSource.DataSource = (object) this._dt;
        this._dt.Rows.Clear();
        Tuple<string, Guid> tuple = this._dict[this._constraintGroupIndex];
        if (tuple.Item2 != Guid.Empty)
        {
          if (this._rbGroup.Checked)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              int attributeGroupId = MetaDataHelper.GetAttributeGroupID(tuple.Item2);
              IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(attributeGroupId, !this._chbViewAllAttributes.Checked);
              if (attributeTypeCollection != null)
                this.AddAttrToTable(attributeTypeCollection.Select(""), this._dt);
            }
            this.LoadObligatoryAttrs();
          }
          else
          {
            IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
            IDBAttributableTypeInfo attributableTypeInfo = (IDBAttributableTypeInfo) null;
            if (this._rbObjTypes.Checked)
              attributableTypeInfo = (IDBAttributableTypeInfo) service.GetObjectType(tuple.Item2, false);
            else if (this._rbRelTypes.Checked)
              attributableTypeInfo = (IDBAttributableTypeInfo) service.GetRelationType(tuple.Item2, false);
            if (attributableTypeInfo != null)
            {
              IDBAttribute4TypeInfoCollection typeInfoCollection = this._chbViewAllAttributes.Checked ? attributableTypeInfo.Attributes : attributableTypeInfo.VisibleAttributes;
              if (typeInfoCollection != null)
              {
                foreach (DataRow row in (InternalDataCollectionBase) typeInfoCollection.Select(string.Empty).Rows)
                {
                  if (this._showAutoRequiredAttrs || (RequiredModes) Enum.Parse(typeof (RequiredModes), Convert.ToString(row["F_REQUIRED"])) != RequiredModes.AutoRequired)
                    this.AddAttrToTable(MetaDataHelper.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"])), this._dt);
                }
              }
            }
            this.LoadObligatoryAttrs();
          }
        }
      }
    }
    else
    {
      this._bindSource.DataSource = (object) this._dt;
      this._dt.Rows.Clear();
      foreach (int commonAttr in this._commonAttrs)
        this.AddAttrToTable(MetaDataHelper.GetAttributeType(commonAttr), this._dt);
      this.LoadObligatoryAttrs();
    }
    this.On_cmbFindCondition_TextChanged((object) this._cmbFindCondition, EventArgs.Empty);
    if (this._dgvFindAttr.Rows.Count <= 0)
      return;
    this._dgvFindAttr.Sort(this._dgvFindAttr.Columns["AttrName"], ListSortDirection.Ascending);
    this._dgvFindAttr.ClearSelection();
    if (this._selectOnStartupAttrID != 0)
    {
      foreach (DataGridViewRow row in (IEnumerable) this._dgvFindAttr.Rows)
      {
        if (Convert.ToInt32(row.Cells["ID"].Value) == this._selectOnStartupAttrID)
        {
          row.Selected = true;
          this._dgvFindAttr.FirstDisplayedScrollingRowIndex = row.Index;
          this._selectOnStartupAttrID = 0;
          break;
        }
      }
    }
    else
    {
      this._dgvFindAttr.Rows[0].Selected = true;
      this._dgvFindAttr.FirstDisplayedScrollingRowIndex = 0;
    }
  }

  /// <summary>Загрузка всех допустимых атрибутов.</summary>
  /// <param name="sourceDT"></param>
  private void LoadAllAttributes(DataTable sourceDT)
  {
    this.AddAttrToTable((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeTypeCollection(-1, !this._chbViewAllAttributes.Checked).Select(""), sourceDT);
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadObligatoryAttrs()
  {
    if (this._obligatoryAttrs == null)
      return;
    foreach (int obligatoryAttr in this._obligatoryAttrs)
      this.AddAttrToTable(MetaDataHelper.GetAttributeType(obligatoryAttr), this._dt);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imsAttrType"></param>
  /// <param name="dt"></param>
  private void AddAttrToTable(IMSAttributeType imsAttrType, DataTable dt)
  {
    if (imsAttrType == null)
      return;
    if (dt == null)
      return;
    try
    {
      dt.BeginLoadData();
      FieldTypes fieldType = imsAttrType.FieldType;
      int attributeId = imsAttrType.AttributeID;
      if (!this.IsAllowedAttributeSourceType(attributeId) || this._forbiddenAttrTypes.Count > 0 && this._forbiddenAttrTypes.Contains(fieldType) || this._allowedAttrTypes.Count > 0 && !this._allowedAttrTypes.Contains(fieldType) || this.IsInFilter(attributeId))
        return;
      int num = Statics.IconSrv.IndexOf(3, -1, (object) fieldType);
      dt.Rows.Add((object) attributeId, (object) num, (object) imsAttrType.Name, (object) imsAttrType.ShortName, (object) fieldType);
    }
    finally
    {
      dt.EndLoadData();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sourceTable"></param>
  /// <param name="dt"></param>
  private void AddAttrToTable(DataTable sourceTable, DataTable dt)
  {
    if (sourceTable == null)
      return;
    if (dt == null)
      return;
    try
    {
      dt.BeginLoadData();
      foreach (DataRow row in (InternalDataCollectionBase) sourceTable.Rows)
      {
        int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        if (this.IsAllowedAttributeSourceType(int32_1) && (this._forbiddenAttrTypes.Count <= 0 || !this._forbiddenAttrTypes.Contains(int32_2)) && (this._allowedAttrTypes.Count <= 0 || this._allowedAttrTypes.Contains(int32_2)) && !this.IsInFilter(int32_1))
        {
          int num = Statics.IconSrv.IndexOf(3, -1, (object) int32_2);
          dt.Rows.Add((object) int32_1, (object) num, row["F_NAME"], row["F_SHORT_NAME"], (object) int32_2);
        }
      }
    }
    finally
    {
      dt.EndLoadData();
    }
  }

  /// <summary>Загрузить сохраненную информацию.</summary>
  private void LoadInfo()
  {
    if (!(ServicesManager.ServiceContainer.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Configurations["UserSettings"];
    if (configuration1 == null)
    {
      IConfiguration configuration2 = service.Create("UserSettings").Add("AttributeSelection");
      configuration2.Add("FindAttributeName");
      configuration2.Add("FindAttributeShortName");
    }
    else
    {
      IConfiguration configuration3 = configuration1.Configurations["AttributeSelection"];
      if (configuration3.HasProperty("SearchAccuracy"))
      {
        bool result = false;
        if (bool.TryParse(configuration3.GetProperty("SearchAccuracy"), out result))
          this._chbSearchAccuracy.Checked = result;
      }
      IConfiguration configuration4 = configuration3.Configurations["FindAttributeName"];
      this._lastNames.Clear();
      foreach (IConfigurationProperty property in configuration4.Properties)
        this._lastNames.Add(property.Value);
      IConfiguration configuration5 = configuration3.Configurations["FindAttributeShortName"];
      this._lastShortNames.Clear();
      foreach (IConfigurationProperty property in configuration5.Properties)
        this._lastShortNames.Add(property.Value);
      this.FillcmbFindConditionItems(false);
    }
  }

  /// <summary>Сохранить информацию поиска.</summary>
  private void SaveInfo()
  {
    if (!(ServicesManager.ServiceContainer.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Configurations["UserSettings"].Configurations["AttributeSelection"];
    configuration1.SetProperty("SearchAccuracy", Convert.ToString(this._chbSearchAccuracy.Checked));
    IConfiguration configuration2 = configuration1.Configurations["FindAttributeName"];
    if (configuration2 != null)
    {
      for (int index = 1; index <= this._lastNames.Count; ++index)
        configuration2.SetProperty($"p{index}", this._lastNames[index - 1]);
    }
    IConfiguration configuration3 = configuration1.Configurations["FindAttributeShortName"];
    if (configuration3 == null)
      return;
    for (int index = 1; index <= this._lastShortNames.Count; ++index)
      configuration3.SetProperty($"p{index}", this._lastShortNames[index - 1]);
  }

  /// <summary>
  /// Заполняется комбобокс, который хранит 10 последних введенных слов для поиска атрибута.
  /// </summary>
  /// <remarks>Поиск может вестись либо по имени атрибута, либо по его короткому имени.</remarks>
  /// <param name="bAddNewItem">Флаг, который указывает необходимость добавления слова в список последних введенных слов</param>
  private void FillcmbFindConditionItems(bool bAddNewItem)
  {
    this._cmbFindCondition.Items.Clear();
    List<string> stringList = this._rbName.Checked ? this._lastNames : this._lastShortNames;
    if (bAddNewItem && !string.IsNullOrEmpty(this._cmbFindCondition.Text) && !stringList.Contains(this._cmbFindCondition.Text))
    {
      stringList.Insert(0, this._cmbFindCondition.Text);
      if (stringList.Count > 10)
        stringList.RemoveRange(10, stringList.Count - 10);
    }
    this._cmbFindCondition.Items.AddRange((object[]) stringList.ToArray());
  }

  /// <summary>
  /// 
  /// </summary>
  private void SearchAttribute()
  {
    string str = this._rbName.Checked ? "colName" : "colShortName";
    string data = this._cmbFindCondition.Text.Replace("'", "''");
    this._bindSource.Filter = $"{SQLStringHelper.QuoteLikeString($"{str} LIKE '")}{(this._chbSearchAccuracy.Checked ? (object) "*" : (object) string.Empty)}{SQLStringHelper.QuoteLikeString(data)}*'";
  }

  /// <summary>Изменение выделенной группы.</summary>
  /// <param name="group">Выделенная группа</param>
  private void SelectedChenged(int index)
  {
    Tuple<string, Guid> tuple = this._dict[index];
    this.SelectedObjectGuid = index == 2 ? tuple.Item2 : Guid.Empty;
    this._txtConstraint.Text = tuple.Item1;
    this._pnlConstraint.Enabled = index != 0;
    this.LoadAttributesCollection();
  }

  /// <summary>
  /// 
  /// </summary>
  private void EndSelectionAttribute()
  {
    if (this._dgvFindAttr.SelectedRows.Count <= 0)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>
  /// Отображение диалога с определенным списком атрибутов, общим для указанных типов объектов.
  /// </summary>
  /// <remarks>
  /// Создавалось для работы с группой объектов.
  /// В гриде выделено несколько разнотипных объектов и при добавлении/удалении/редактировании атрибутов производится поиск атрибутов,
  /// которые присутствуют у всех типов выделенных объектов (пересечение атрибутов).
  /// Вот их и нужно отобразить.
  /// </remarks>
  /// <param name="typeIDs">Список идентификаторов типов объектов</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов</param>
  public void LoadAttrDialogForCommonAttrs(
    List<int> typeIDs,
    List<int> attrIDs,
    AttributableElements kind)
  {
    this._grbCommonAttrs.Location = this._grbСonstraint.Location;
    this._grbCommonAttrs.Width = this._grbFind.Width;
    this._grbСonstraint.Visible = false;
    this._grbCommonAttrs.Visible = true;
    if (this._lv.Columns.Count > 0)
      this._lv.Columns[0].Width = -2;
    this._commonAttrs = attrIDs;
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    if (typeIDs == null)
      return;
    switch (kind)
    {
      case AttributableElements.Object:
        using (List<int>.Enumerator enumerator = typeIDs.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            IMSObjectType objectType = MetaDataHelper.GetObjectType(current);
            if (objectType != null)
            {
              int imageIndex = Statics.IconSrv.IndexOf(4, current);
              this._lv.Items.Add(new ListViewItem(objectType.ObjectName, imageIndex));
            }
          }
          break;
        }
      case AttributableElements.Relation:
        using (List<int>.Enumerator enumerator = typeIDs.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            IMSRelationType relationType = MetaDataHelper.GetRelationType(current);
            if (relationType != null)
            {
              int imageIndex = Statics.IconSrv.IndexOf(6, current);
              this._lv.Items.Add(new ListViewItem(relationType.Description, imageIndex));
            }
          }
          break;
        }
    }
  }

  /// <summary>
  /// Диалог необходимо отобразить со списком атрибутов, назначенных конкретному объекту.
  /// </summary>
  /// <remarks>
  /// Необходимость появилась, когда диалог отображается для удаления атрибутов.
  /// Если в гриде выделен один объект. то необходимо показать только те атрибуты, которые назначены объекту.
  /// </remarks>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты. При удалении отображать ненужно, т.к. удалить такие атрибуты нельзя</param>
  public void LoadAttrDialogForObject(long objID, bool showAutoRequiredAttrs)
  {
    if (objID == 0L)
      return;
    this._grbCommonAttrs.Location = this._grbСonstraint.Location;
    this._grbCommonAttrs.Width = this._grbFind.Width;
    this._grbCommonAttrs.Text = LocalizationHolder.rm.GetString("Client.Core_1583");
    this._grbСonstraint.Visible = false;
    this._grbCommonAttrs.Visible = true;
    if (this._lv.Columns.Count > 0)
      this._lv.Columns[0].Width = -2;
    this._selectedObjID = objID;
    this._showAutoRequiredAttrs = showAutoRequiredAttrs;
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(objID);
    if (objectInfo.Empty)
      return;
    int imageIndex = Statics.IconSrv.IndexOf(4, objectInfo.ObjectTypeID);
    this._lv.Items.Add(new ListViewItem(objectInfo.Caption, imageIndex));
  }

  /// <summary>Устанавливает фильтр для доступных типов обектов.</summary>
  /// <param name="objType">Список идентификаторов типов объектов</param>
  public void LoadAttrDialogForObjectsTypes(List<int> objType)
  {
    this._allowedObjTypes = objType;
    if (objType.Count != 1)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objType[0]);
    if (objectType != null)
      this._dict[2] = Tuple.Create<string, Guid>(objectType.ObjectTypeName, objectType.Guid);
    this._rbObjTypes.Checked = true;
  }

  /// <summary>Устанавливает фильтр для доступных типов обектов.</summary>
  /// <param name="objType">Список идентификаторов типов объектов</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты</param>
  /// <remarks>При удалении отображать ненужно, т.к. удалить такие атрибуты нельзя</remarks>
  public void LoadAttrDialogForObjectsTypes(List<int> objType, bool showAutoRequiredAttrs)
  {
    this.LoadAttrDialogForObjectsTypes(objType);
    this._showAutoRequiredAttrs = showAutoRequiredAttrs;
  }

  /// <summary>Устанавливает фильтр для доступных типов обектов.</summary>
  /// <param name="objectTypeGuid">Глобальный идентификатор типа объекта.</param>
  public void LoadAttrDialogForObjectsTypes(Guid objectTypeGuid)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeGuid);
    if (objectType != null)
      this._dict[2] = Tuple.Create<string, Guid>(objectType.ObjectTypeName, objectType.Guid);
    this._rbObjTypes.Checked = true;
  }

  /// <summary>
  /// Диалог необходимо отобразить со списком атрибутов, назначенных конкретной связи.
  /// </summary>
  /// <remarks>Если в гриде выделен один объект, то необходимо показать только те атрибуты, которые назначены объекту</remarks>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты. При удалении отображать ненужно, т.к. удалить такие атрибуты нельзя</param>
  public void LoadAttrDialogForRelation(long relID, bool showAutoRequiredAttrs)
  {
    if (relID == 0L)
      return;
    this._grbCommonAttrs.Location = this._grbСonstraint.Location;
    this._grbCommonAttrs.Width = this._grbFind.Width;
    this._grbCommonAttrs.Text = LocalizationHolder.rm.GetString("Client.Core_317");
    this._grbСonstraint.Visible = false;
    this._grbCommonAttrs.Visible = true;
    if (this._lv.Columns.Count > 0)
      this._lv.Columns[0].Width = -2;
    this._selectedRelID = relID;
    this._showAutoRequiredAttrs = showAutoRequiredAttrs;
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relID);
      int imageIndex = Statics.IconSrv.IndexOf(6, relation.RelationType);
      this._lv.Items.Add(new ListViewItem(MetaDataHelper.GetRelationTypeName(relation.RelationType), imageIndex));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relTypes">Список идентификаторов типов связей</param>
  public void LoadAttrDialogForRelationsTypes(List<int> relTypes)
  {
    if (relTypes.Count == 1)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(relTypes[0]);
      if (relationType != null)
        this._dict[3] = Tuple.Create<string, Guid>(relationType.Description, relationType.Guid);
    }
    this._rbRelTypes.Checked = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relationTypeGuid">Глобальный идентификатор типа связи</param>
  public void LoadAttrDialogForRelationsTypes(Guid relationTypeGuid)
  {
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relationTypeGuid);
    if (relationType != null)
      this._dict[3] = Tuple.Create<string, Guid>(relationType.Description, relationType.Guid);
    this._rbRelTypes.Checked = true;
  }

  /// <summary>
  /// Запомнить список идентификаторов атрибутов, которые должны быть выделены при появлении диалога.
  /// </summary>
  /// <param name="selectedAttrIDOnStartup">Список идентификаторов атрибутов, которые должны быть выделены при появлении диалога</param>
  public void SelectedAttributeIDOnStartup(int selectedAttrIDOnStartup)
  {
    this._selectOnStartupAttrID = selectedAttrIDOnStartup;
  }

  /// <summary>прорисовка картинки в ячейке по индексу</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgvFindAttr_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
  {
    if (e.ColumnIndex != this.Image.Index || e.RowIndex < 0)
      return;
    using (Brush brush1 = (Brush) new SolidBrush(this._dgvFindAttr.GridColor))
    {
      using (Brush brush2 = (Brush) new SolidBrush(e.CellStyle.BackColor))
      {
        using (new Pen(brush1))
        {
          e.Graphics.FillRectangle(brush2, e.CellBounds);
          int int32 = Convert.ToInt32(this._dgvFindAttr.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
          if (int32 != -1)
            this._imgList.Draw(e.Graphics, e.CellBounds.Left + 1, e.CellBounds.Top + 1, int32);
          e.Handled = true;
        }
      }
    }
  }

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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributesSelectDlg));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    this._grbСonstraint = new GroupBox();
    this._pnlConstraint = new Panel();
    this._txtConstraint = new TextBox();
    this._btnConstraint = new Button();
    this._rbRelTypes = new RadioButton();
    this._rbAll = new RadioButton();
    this._rbObjTypes = new RadioButton();
    this._rbGroup = new RadioButton();
    this._grbFind = new GroupBox();
    this._chbSearchAccuracy = new CheckBox();
    this._pnlAttributes = new Panel();
    this._dgvFindAttr = new DataGridView();
    this.ID = new DataGridViewTextBoxColumn();
    this.Image = new DataGridViewTextBoxColumn();
    this.AttrName = new DataGridViewTextBoxColumn();
    this.AttrShortName = new DataGridViewTextBoxColumn();
    this.AttrType = new DataGridViewTextBoxColumn();
    this._bindSource = new BindingSource(this.components);
    this._ds = new DataSet();
    this.tblAttrs = new DataTable();
    this.colID = new DataColumn();
    this.colImage = new DataColumn();
    this.colName = new DataColumn();
    this.colShortName = new DataColumn();
    this.colType = new DataColumn();
    this._splDescriptor = new Splitter();
    this._rtbDescription = new RichTextBox();
    this._chbViewAllAttributes = new CheckBox();
    this._rbShortName = new RadioButton();
    this._rbName = new RadioButton();
    this._cmbFindCondition = new ComboBox();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._btnCreateAttr = new Button();
    this._imgList = new ImageList(this.components);
    this._grbCommonAttrs = new GroupBox();
    this._lv = new ListView();
    this._header = new ColumnHeader();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
    this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
    this._grbСonstraint.SuspendLayout();
    this._pnlConstraint.SuspendLayout();
    this._grbFind.SuspendLayout();
    this._pnlAttributes.SuspendLayout();
    ((ISupportInitialize) this._dgvFindAttr).BeginInit();
    ((ISupportInitialize) this._bindSource).BeginInit();
    this._ds.BeginInit();
    this.tblAttrs.BeginInit();
    this._grbCommonAttrs.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._grbСonstraint, "_grbСonstraint");
    this._grbСonstraint.Controls.Add((Control) this._pnlConstraint);
    this._grbСonstraint.Controls.Add((Control) this._rbRelTypes);
    this._grbСonstraint.Controls.Add((Control) this._rbAll);
    this._grbСonstraint.Controls.Add((Control) this._rbObjTypes);
    this._grbСonstraint.Controls.Add((Control) this._rbGroup);
    this._grbСonstraint.ForeColor = SystemColors.ControlText;
    this._grbСonstraint.Name = "_grbСonstraint";
    this._grbСonstraint.TabStop = false;
    componentResourceManager.ApplyResources((object) this._pnlConstraint, "_pnlConstraint");
    this._pnlConstraint.Controls.Add((Control) this._txtConstraint);
    this._pnlConstraint.Controls.Add((Control) this._btnConstraint);
    this._pnlConstraint.Name = "_pnlConstraint";
    componentResourceManager.ApplyResources((object) this._txtConstraint, "_txtConstraint");
    this._txtConstraint.BackColor = SystemColors.Window;
    this._txtConstraint.Name = "_txtConstraint";
    this._txtConstraint.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._btnConstraint, "_btnConstraint");
    this._btnConstraint.Name = "_btnConstraint";
    this._btnConstraint.UseVisualStyleBackColor = true;
    this._btnConstraint.Click += new EventHandler(this.On_btnConstraint_Click);
    componentResourceManager.ApplyResources((object) this._rbRelTypes, "_rbRelTypes");
    this._rbRelTypes.Name = "_rbRelTypes";
    this._rbRelTypes.Tag = (object) "3";
    this._rbRelTypes.CheckedChanged += new EventHandler(this.OnСonstraintRadion_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rbAll, "_rbAll");
    this._rbAll.Checked = true;
    this._rbAll.Name = "_rbAll";
    this._rbAll.TabStop = true;
    this._rbAll.Tag = (object) "0";
    this._rbAll.CheckedChanged += new EventHandler(this.OnСonstraintRadion_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rbObjTypes, "_rbObjTypes");
    this._rbObjTypes.Name = "_rbObjTypes";
    this._rbObjTypes.Tag = (object) "2";
    this._rbObjTypes.CheckedChanged += new EventHandler(this.OnСonstraintRadion_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rbGroup, "_rbGroup");
    this._rbGroup.Name = "_rbGroup";
    this._rbGroup.Tag = (object) "1";
    this._rbGroup.CheckedChanged += new EventHandler(this.OnСonstraintRadion_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._grbFind, "_grbFind");
    this._grbFind.Controls.Add((Control) this._chbSearchAccuracy);
    this._grbFind.Controls.Add((Control) this._pnlAttributes);
    this._grbFind.Controls.Add((Control) this._rbShortName);
    this._grbFind.Controls.Add((Control) this._rbName);
    this._grbFind.Controls.Add((Control) this._cmbFindCondition);
    this._grbFind.ForeColor = SystemColors.ControlText;
    this._grbFind.Name = "_grbFind";
    this._grbFind.TabStop = false;
    componentResourceManager.ApplyResources((object) this._chbSearchAccuracy, "_chbSearchAccuracy");
    this._chbSearchAccuracy.Checked = true;
    this._chbSearchAccuracy.CheckState = CheckState.Checked;
    this._chbSearchAccuracy.Name = "_chbSearchAccuracy";
    this._chbSearchAccuracy.UseVisualStyleBackColor = true;
    this._chbSearchAccuracy.CheckedChanged += new EventHandler(this.On_chbSearchAccuracy_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._pnlAttributes, "_pnlAttributes");
    this._pnlAttributes.Controls.Add((Control) this._dgvFindAttr);
    this._pnlAttributes.Controls.Add((Control) this._splDescriptor);
    this._pnlAttributes.Controls.Add((Control) this._rtbDescription);
    this._pnlAttributes.Controls.Add((Control) this._chbViewAllAttributes);
    this._pnlAttributes.Name = "_pnlAttributes";
    this._dgvFindAttr.AllowUserToAddRows = false;
    this._dgvFindAttr.AllowUserToDeleteRows = false;
    this._dgvFindAttr.AllowUserToResizeRows = false;
    this._dgvFindAttr.AutoGenerateColumns = false;
    this._dgvFindAttr.BackgroundColor = SystemColors.Window;
    this._dgvFindAttr.BorderStyle = BorderStyle.None;
    this._dgvFindAttr.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._dgvFindAttr.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this._dgvFindAttr.Columns.AddRange((DataGridViewColumn) this.ID, (DataGridViewColumn) this.Image, (DataGridViewColumn) this.AttrName, (DataGridViewColumn) this.AttrShortName, (DataGridViewColumn) this.AttrType);
    this._dgvFindAttr.DataSource = (object) this._bindSource;
    componentResourceManager.ApplyResources((object) this._dgvFindAttr, "_dgvFindAttr");
    this._dgvFindAttr.Name = "_dgvFindAttr";
    this._dgvFindAttr.RowHeadersVisible = false;
    this._dgvFindAttr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._dgvFindAttr.CellPainting += new DataGridViewCellPaintingEventHandler(this.On_dgvFindAttr_CellPainting);
    this._dgvFindAttr.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.On_dgvFindAttr_ColumnHeaderMouseClick);
    this._dgvFindAttr.SelectionChanged += new EventHandler(this.On_dgvFindAttr_SelectionChanged);
    this._dgvFindAttr.DoubleClick += new EventHandler(this.On_dgvFindAttr_DoubleClick);
    this._dgvFindAttr.KeyDown += new KeyEventHandler(this.On_dgvFindAttr_KeyDown);
    this.ID.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.ID, "ID");
    this.ID.Name = "ID";
    this.ID.ReadOnly = true;
    this.ID.Resizable = DataGridViewTriState.False;
    this.ID.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Image.DataPropertyName = "colImage";
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.Padding = new Padding(5, 0, 0, 0);
    this.Image.DefaultCellStyle = gridViewCellStyle1;
    componentResourceManager.ApplyResources((object) this.Image, "Image");
    this.Image.Name = "Image";
    this.Image.ReadOnly = true;
    this.Image.Resizable = DataGridViewTriState.False;
    this.Image.SortMode = DataGridViewColumnSortMode.Programmatic;
    this.AttrName.DataPropertyName = "colName";
    componentResourceManager.ApplyResources((object) this.AttrName, "AttrName");
    this.AttrName.Name = "AttrName";
    this.AttrName.ReadOnly = true;
    this.AttrShortName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.AttrShortName.DataPropertyName = "colShortName";
    componentResourceManager.ApplyResources((object) this.AttrShortName, "AttrShortName");
    this.AttrShortName.Name = "AttrShortName";
    this.AttrShortName.ReadOnly = true;
    this.AttrType.DataPropertyName = "colType";
    componentResourceManager.ApplyResources((object) this.AttrType, "AttrType");
    this.AttrType.Name = "AttrType";
    this.AttrType.ReadOnly = true;
    this.AttrType.Resizable = DataGridViewTriState.False;
    this._bindSource.DataMember = "tblAttrs";
    this._bindSource.DataSource = (object) this._ds;
    this._ds.DataSetName = "NewDataSet";
    this._ds.Tables.AddRange(new DataTable[1]
    {
      this.tblAttrs
    });
    this.tblAttrs.Columns.AddRange(new DataColumn[5]
    {
      this.colID,
      this.colImage,
      this.colName,
      this.colShortName,
      this.colType
    });
    this.tblAttrs.TableName = "tblAttrs";
    this.colID.AllowDBNull = false;
    this.colID.ColumnName = "colID";
    this.colImage.ColumnName = "colImage";
    this.colImage.DataType = typeof (object);
    this.colName.ColumnName = "colName";
    this.colShortName.ColumnName = "colShortName";
    this.colType.AllowDBNull = false;
    this.colType.Caption = "";
    this.colType.ColumnName = "colType";
    this.colType.ReadOnly = true;
    this._splDescriptor.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._splDescriptor, "_splDescriptor");
    this._splDescriptor.Name = "_splDescriptor";
    this._splDescriptor.TabStop = false;
    this._rtbDescription.BackColor = SystemColors.Control;
    this._rtbDescription.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._rtbDescription, "_rtbDescription");
    this._rtbDescription.Name = "_rtbDescription";
    this._rtbDescription.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._chbViewAllAttributes, "_chbViewAllAttributes");
    this._chbViewAllAttributes.Name = "_chbViewAllAttributes";
    this._chbViewAllAttributes.UseVisualStyleBackColor = true;
    this._chbViewAllAttributes.CheckedChanged += new EventHandler(this.On_chbViewAllAttributes_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rbShortName, "_rbShortName");
    this._rbShortName.Name = "_rbShortName";
    this._rbShortName.Tag = (object) "1";
    this._rbShortName.UseVisualStyleBackColor = true;
    this._rbShortName.CheckedChanged += new EventHandler(this.OnFindRadio_CheckedChanged);
    this._rbName.Checked = true;
    componentResourceManager.ApplyResources((object) this._rbName, "_rbName");
    this._rbName.Name = "_rbName";
    this._rbName.TabStop = true;
    this._rbName.Tag = (object) "0";
    this._rbName.UseVisualStyleBackColor = true;
    this._rbName.CheckedChanged += new EventHandler(this.OnFindRadio_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._cmbFindCondition, "_cmbFindCondition");
    this._cmbFindCondition.FormattingEnabled = true;
    this._cmbFindCondition.Name = "_cmbFindCondition";
    this._cmbFindCondition.TextChanged += new EventHandler(this.On_cmbFindCondition_TextChanged);
    this._cmbFindCondition.KeyPress += new KeyPressEventHandler(this.On_cmbFindCondition_KeyPress);
    this._cmbFindCondition.Leave += new EventHandler(this.On_cmbFindCondition_Leave);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCreateAttr, "_btnCreateAttr");
    this._btnCreateAttr.Name = "_btnCreateAttr";
    this._btnCreateAttr.UseVisualStyleBackColor = true;
    this._btnCreateAttr.Click += new EventHandler(this.On_btnCreateAttr_Click);
    this._imgList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this._imgList, "_imgList");
    this._imgList.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this._grbCommonAttrs, "_grbCommonAttrs");
    this._grbCommonAttrs.Controls.Add((Control) this._lv);
    this._grbCommonAttrs.Name = "_grbCommonAttrs";
    this._grbCommonAttrs.TabStop = false;
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this._header
    });
    this._lv.FullRowSelect = true;
    this._lv.HeaderStyle = ColumnHeaderStyle.None;
    this._lv.HideSelection = false;
    this._lv.Name = "_lv";
    this._lv.Sorting = SortOrder.Ascending;
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this._header, "_header");
    this.dataGridViewTextBoxColumn1.DataPropertyName = "colID";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn2.DataPropertyName = "colName";
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.Padding = new Padding(5, 0, 0, 0);
    this.dataGridViewTextBoxColumn2.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Programmatic;
    this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn3.DataPropertyName = "colShortName";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn4.DataPropertyName = "colType";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
    this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
    this.dataGridViewTextBoxColumn4.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn5.DataPropertyName = "colType";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
    this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
    this.dataGridViewTextBoxColumn5.ReadOnly = true;
    this.dataGridViewTextBoxColumn5.Resizable = DataGridViewTriState.False;
    this.dataGridViewImageColumn1.DataPropertyName = "colImage";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle3.NullValue");
    gridViewCellStyle3.Padding = new Padding(5, 0, 0, 0);
    this.dataGridViewImageColumn1.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
    this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
    this.dataGridViewImageColumn1.ReadOnly = true;
    this.dataGridViewImageColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewImageColumn1.SortMode = DataGridViewColumnSortMode.Programmatic;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._grbCommonAttrs);
    this.Controls.Add((Control) this._btnCreateAttr);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._grbFind);
    this.Controls.Add((Control) this._grbСonstraint);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (AttributesSelectDlg);
    this.ShowInTaskbar = false;
    this._grbСonstraint.ResumeLayout(false);
    this._pnlConstraint.ResumeLayout(false);
    this._pnlConstraint.PerformLayout();
    this._grbFind.ResumeLayout(false);
    this._grbFind.PerformLayout();
    this._pnlAttributes.ResumeLayout(false);
    this._pnlAttributes.PerformLayout();
    ((ISupportInitialize) this._dgvFindAttr).EndInit();
    ((ISupportInitialize) this._bindSource).EndInit();
    this._ds.EndInit();
    this.tblAttrs.EndInit();
    this._grbCommonAttrs.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}

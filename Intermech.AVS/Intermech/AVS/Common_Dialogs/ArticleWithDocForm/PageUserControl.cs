// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.PageUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraTreeList;
using Intermech.Client.Core;
using Intermech.Client.Core.History;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Базовая закладка</summary>
internal class PageUserControl : UserControl, IPageControl
{
  protected AVSRow selectedSpecRow;
  /// <summary>
  /// Идентификатор объекта/связи закладки (для AttributeProcessor)
  /// </summary>
  protected long attributableElementID;
  /// <summary>Тип хранителя атрибутов (для AttributeProcessor)</summary>
  protected AttributableElements attributableElement;
  /// <summary>AttributeProcessor</summary>
  protected AttributeProcessor aProcessor;
  /// <summary>Ссылка на общие данные для всех закладок</summary>
  protected IFormCommonData commonData;
  /// <summary>
  /// Флаг того, что были изменены атрибуты у объекта/связи закладки
  /// </summary>
  protected bool changed;
  protected CommonDataType disableControls;
  /// <summary>
  /// Классификатор, которым проклассифицирован объект attributableElementID
  /// </summary>
  protected long classifierID;
  private FormType formType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Конструктор</summary>
  public PageUserControl()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="id">Идентификатор объекта/связи закладки (для AttributeProcessor)</param>
  /// <param name="aElement">Тип хранителя атрибутов (для AttributeProcessor)</param>
  /// <param name="dControls">Общие данные, которые закрыты для редактирования</param>
  public PageUserControl(long id, AttributableElements aElement, CommonDataType dControls)
  {
    this.Init(id, aElement, dControls);
  }

  protected void Init(long id, AttributableElements aElement, CommonDataType dControls)
  {
    this.attributableElementID = id;
    this.attributableElement = aElement;
    this.disableControls = dControls;
    if (this.attributableElementID == -1L)
      return;
    this.aProcessor = new AttributeProcessor(this.attributableElementID, this.attributableElement, true);
  }

  protected bool IsReadOnly(int attributeId)
  {
    if (attributeId == FormHelper.AttributeNameID)
      return this.commonData.GetReadOnly("Name");
    if (attributeId == FormHelper.AttributeDesignationID)
      return this.commonData.GetReadOnly("Designation");
    return this.aProcessor != null && this.aProcessor.GetReadOnly(attributeId);
  }

  /// <summary>
  /// Событие об том, что изменились атрибуты у объекта/связи закладки
  /// </summary>
  public event EventHandler Changed;

  /// <summary>Событие об том, что необходимо перечитать контролы</summary>
  public event EventHandler ReloadData;

  /// <summary>Событие об том, что произошла классификация</summary>
  public event ClassificatedEventHandler ClassificatedEvent;

  /// <summary>
  /// Событие об том, что был вызван редактор атрибута (только для первой закладки)
  /// </summary>
  public event GetEditorDelegate GetEditorEvent;

  public IFormCommonData CommonData
  {
    set => this.commonData = value;
  }

  /// <summary>Тип формы</summary>
  internal FormType FormType
  {
    get => this.formType;
    set => this.formType = value;
  }

  public virtual void Save(IUserSession session, OpenModes mode, CreatedPair pair)
  {
    if (mode == OpenModes.InView && !this.changed)
      return;
    if (this.aProcessor.Id != -1L)
      this.aProcessor.StartTransaction();
    try
    {
      this.OnSave(session, mode, pair);
      if (this.aProcessor.Id != -1L)
        this.aProcessor.CommitTransaction();
      this.changed = false;
    }
    catch
    {
      if (this.aProcessor.Id != -1L)
        this.aProcessor.RollbackTransaction();
      throw;
    }
  }

  public void Reload(IUserSession session, OpenModes mode)
  {
    if (this.aProcessor != null && this.aProcessor.Id != -1L)
      this.aProcessor.Load(this.attributableElementID, this.attributableElement, ClientConsts.GetAttributeValuesModes, false);
    this.OnReload(session, mode);
    this.changed = false;
  }

  public void CommonDataChanged(CommonDataType type) => this.OnCommonDataChanged(type);

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.Dock = DockStyle.None;
      this.Visible = false;
    }
    else
    {
      this.Dock = DockStyle.Fill;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  public bool AutoNotifications
  {
    set
    {
      if (this.aProcessor == null)
        return;
      this.aProcessor.InvokeNotifications = true;
    }
  }

  protected virtual void OnSave(IUserSession session, OpenModes mode, CreatedPair pair)
  {
  }

  protected virtual void OnReload(IUserSession session, OpenModes mode)
  {
  }

  protected void SetAllReadOnly(Control c)
  {
    c.Enabled = false;
    foreach (Control control in (ArrangedElementCollection) c.Controls)
      this.SetAllReadOnly(control);
  }

  protected virtual void OnCommonDataChanged(CommonDataType type)
  {
  }

  public virtual void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID)
  {
  }

  protected void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed != null)
      changed((object) this, new EventArgs());
    this.changed = true;
  }

  protected void OnReloadData()
  {
    EventHandler reloadData = this.ReloadData;
    if (reloadData != null)
      reloadData((object) this, new EventArgs());
    this.changed = false;
  }

  /// <summary>Изменить значение атрибута в едиторе</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="oldValue">Старое значение атрибута</param>
  /// <param name="id">Идентификатор объекта/связи для которого покажем историю изменения значений</param>
  /// <param name="element">Тип</param>
  /// <returns>Новое значение, либо старое при неудаче/отмене</returns>
  protected object ChangeInEditor(
    int attrID,
    object oldValue,
    long id,
    AttributableElements element)
  {
    if (this.GetEditorEvent != null)
    {
      GetEditorEventArgs args = new GetEditorEventArgs(attrID, oldValue);
      GetEditorDelegate getEditorEvent = this.GetEditorEvent;
      object obj = getEditorEvent != null ? getEditorEvent((object) this, args) : (object) null;
      if (args.Handled)
        return obj;
    }
    IAttributePropertyDescriber describer = (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(attrID);
    UITypeEditor uiTypeEditor = (UITypeEditor) null;
    if (describer != null)
      uiTypeEditor = describer.GetPropDescriptorEditor(attrID) as UITypeEditor;
    if (uiTypeEditor != null)
    {
      object obj = oldValue;
      object propertyValue = uiTypeEditor.EditValue((System.IServiceProvider) null, obj);
      Intermech.Client.Core.FormDesigner.Controls.ElementInfo elementInfo = new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(id, element);
      return describer.GetAttributeValue((IElementInfo) elementInfo, attrID, propertyValue);
    }
    AVSRow row = (AVSRow) null;
    AttributeProcessor processorForValue = this.GetAttributeProcessorForValue(out row);
    if (row == null)
      row = this.selectedSpecRow;
    if (attrID == AvsIDCache.Attr_Count && row != null)
    {
      object attrValue = oldValue;
      if (oldValue is MeasuredValue)
      {
        string caption = (oldValue as MeasuredValue).Caption;
        if (caption != null && caption != "")
          attrValue = (object) AVSRow.ConvertCountToMeasuredValue((object) caption, false);
      }
      object res = (object) null;
      bool allProducts = false;
      if (row.CallCountDocCellEditor(-1, attrValue, out res, ref allProducts))
        return res;
    }
    else
    {
      processorForValue.Load(processorForValue.Id, element, GetAttributeValuesModes.None, false);
      IAttributeEditorControl editorControl = processorForValue.GetEditorControl(attrID, new int?(0), UITypeEditorEditStyle.Modal);
      if (editorControl != null && editorControl is Form)
      {
        AttributeValuesList list = new AttributeValuesList();
        list.Add(new AttributeValues(attrID, oldValue));
        processorForValue.SetAttributeValuesArray(list);
        editorControl.RefreshControl();
        if (((Form) editorControl).ShowDialog() == DialogResult.OK)
          return processorForValue.GetValue(attrID);
      }
      else if (id != 0L)
      {
        ObjectsHistory objectsHistory = new ObjectsHistory((object) id, element, (object) attrID);
        objectsHistory.SelectedValue = oldValue;
        if (objectsHistory.ShowDialog() == DialogResult.OK)
          return objectsHistory.SelectedValue;
      }
    }
    return oldValue;
  }

  protected virtual AttributeProcessor GetAttributeProcessorForValue(out AVSRow row)
  {
    row = (AVSRow) null;
    return this.aProcessor;
  }

  /// <summary>
  /// Изменить значение атрибута в едиторе для объекта/связи по умолчанию для этой закладки
  /// </summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="oldValue">Старое значение атрибута</param>
  /// <returns>Новое значение, либо старое при неудаче/отмене</returns>
  protected object ChangeInEditor(int attrID, object oldValue)
  {
    return this.ChangeInEditor(attrID, oldValue, this.attributableElementID, this.attributableElement);
  }

  protected IObjectClassificator GetClassificator(
    IUserSession session,
    ClassificatedObjects classif,
    ref long clasifID)
  {
    long[] collection = (long[]) null;
    long[] numArray = (long[]) null;
    ISelectionsService customService = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
    if (classif.articleID != 0L)
      collection = customService.GetClassifierForObjType((object) session.SessionGUID, classif.articleType);
    if (classif.documentID != 0L)
      numArray = customService.GetClassifierForObjType((object) session.SessionGUID, classif.documentType);
    List<long> longList = new List<long>();
    if (collection != null && collection.Length != 0 && numArray != null && numArray.Length != 0)
    {
      for (int index = 0; index < collection.Length; ++index)
      {
        if (Array.IndexOf<long>(numArray, collection[index]) >= 0)
          longList.Add(collection[index]);
      }
    }
    else if (collection != null && collection.Length != 0)
      longList.AddRange((IEnumerable<long>) collection);
    else if (numArray != null && numArray.Length != 0)
      longList.AddRange((IEnumerable<long>) numArray);
    using (ClassifySelectionForm classifySelectionForm = new ClassifySelectionForm(longList.ToArray()))
    {
      if (classifySelectionForm.ShowDialog().Equals((object) DialogResult.OK))
      {
        if (classifySelectionForm.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
          clasifID = itemData.Value;
      }
    }
    return clasifID != 0L ? customService.GetObjectClassificator((object) session.SessionGUID, clasifID) : (IObjectClassificator) null;
  }

  /// <summary>
  /// Отобразить окно выбора классификатора и при выборе оного дать рассчитанные атрибуты
  /// Общий случай для первой закладки (для закладки с изделием и документов переопределить)
  /// </summary>
  /// <returns></returns>
  protected virtual void OnClassifier(ClassificatedObjects classif)
  {
    long clasifID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IObjectClassificator classificator = this.GetClassificator(sessionKeeper.Session, classif, ref clasifID);
      this.commonData.ClassifierID = clasifID;
      if (classificator == null)
        return;
      try
      {
        this.OnSetClassifyAttributes(classificator, clasifID);
        ClassificatedEventHandler classificatedEvent = this.ClassificatedEvent;
        if (classificatedEvent == null)
          return;
        classificatedEvent((object) this, new ClassificatedEventArgs(classificator, clasifID));
      }
      catch
      {
        EventHandler reloadData = this.ReloadData;
        if (reloadData != null)
          reloadData((object) this, new EventArgs());
        throw;
      }
    }
  }

  /// <summary>
  /// Обработчик на выход из поля для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="textBox">TextBox</param>
  /// <param name="oldValue">Старое значение</param>
  /// <returns>Новое значение или старое значение при ощибке</returns>
  protected MeasuredValue OnLeaveCount(TextBox textBox, MeasuredValue oldValue)
  {
    MeasuredValue measuredValue = (MeasuredValue) null;
    try
    {
      if (textBox.Text != string.Empty)
        measuredValue = AVSRow.ConvertCountToMeasuredValue((object) textBox.Text);
    }
    catch (Exception ex)
    {
      double result = 0.0;
      bool flag = false;
      string text = ex.Message;
      if (double.TryParse(textBox.Text, out result) && this.attributableElement == AttributableElements.Relation)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this.attributableElementID);
          IDBAttributeType4Relation attributeById = sessionKeeper.Session.GetRelationType(relation.RelationType).Attributes.GetAttributeByID(FormHelper.AttributeCountID) as IDBAttributeType4Relation;
          if ((attributeById as IDBMeasureAttributeType).DefaultMeasureID != 0L)
          {
            measuredValue = new MeasuredValue(result, (attributeById as IDBMeasureAttributeType).DefaultMeasureID);
            flag = true;
          }
        }
      }
      else
        text = $"Невозможно преобразовать \"{textBox.Text}\" в вещественное значение";
      if (!flag)
      {
        int num = (int) MessageBox.Show(text, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBox.Text = oldValue != null ? oldValue.ToString() : string.Empty;
        return oldValue;
      }
    }
    return measuredValue;
  }

  /// <summary>
  /// Обработчик на выход из поля для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="textBox">TextBox</param>
  /// <param name="oldValue">Старое значение</param>
  /// <returns>Новое значение или старое значение при ощибке</returns>
  protected MeasuredValue OnLeaveCount(ConvertEditValueEventArgs e, MeasuredValue oldValue)
  {
    if (Convert.ToString(e.Value) == "-")
      return oldValue;
    MeasuredValue measuredValue = (MeasuredValue) null;
    string s = Convert.ToString(e.Value);
    try
    {
      if (s != string.Empty)
        measuredValue = AVSRow.ConvertCountToMeasuredValue((object) s);
    }
    catch (Exception ex)
    {
      double result = 0.0;
      bool flag = false;
      string text = ex.Message;
      if (double.TryParse(s, out result) && this.attributableElement == AttributableElements.Relation)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this.attributableElementID);
          IDBAttributeType4Relation attributeById = sessionKeeper.Session.GetRelationType(relation.RelationType).Attributes.GetAttributeByID(FormHelper.AttributeCountID) as IDBAttributeType4Relation;
          if ((attributeById as IDBMeasureAttributeType).DefaultMeasureID != 0L)
          {
            measuredValue = new MeasuredValue(result, (attributeById as IDBMeasureAttributeType).DefaultMeasureID);
            flag = true;
          }
        }
      }
      else
        text = $"Невозможно преобразовать \"{s}\" в вещественное значение";
      if (!flag)
      {
        int num = (int) MessageBox.Show(text, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        e.Value = oldValue != null ? (object) oldValue.ToString() : (object) string.Empty;
        return oldValue;
      }
    }
    return measuredValue;
  }

  /// <summary>
  /// Обработчик на нажатие кнопки вызова редактора для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="textBox">TextBox</param>
  /// <param name="oldValue">Старое значение</param>
  /// <returns>Новое значение или старое значение при ощибке</returns>
  protected MeasuredValue OnEditCount(TreeList treeList, MeasuredValue oldValue)
  {
    object obj = this.ChangeInEditor(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), (object) oldValue);
    MeasuredValue measuredValue = (MeasuredValue) null;
    if (obj is MeasuredValue)
    {
      measuredValue = (MeasuredValue) obj;
    }
    else
    {
      string str = Convert.ToString(obj);
      try
      {
        if (str != string.Empty)
          measuredValue = AVSRow.ConvertCountToMeasuredValue((object) str);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        object val = oldValue != null ? (object) oldValue.ToString() : (object) string.Empty;
        if (treeList.FocusedNode != null && treeList.FocusedColumn != null)
          treeList.FocusedNode.SetValue((object) treeList.FocusedColumn, val);
        return oldValue;
      }
    }
    return measuredValue;
  }

  /// <summary>
  /// Обработчик на нажатие кнопки вызова редактора для редактирования атрибута "Количество"
  /// </summary>
  /// <param name="textBox">TextBox</param>
  /// <param name="oldValue">Старое значение</param>
  /// <returns>Новое значение или старое значение при ощибке</returns>
  protected MeasuredValue OnEditCount(TextBox textBox, MeasuredValue oldValue)
  {
    object obj = this.ChangeInEditor(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), (object) oldValue);
    MeasuredValue measuredValue = (MeasuredValue) null;
    if (obj is MeasuredValue)
    {
      measuredValue = (MeasuredValue) obj;
    }
    else
    {
      string str = Convert.ToString(obj);
      try
      {
        if (str != string.Empty)
          measuredValue = AVSRow.ConvertCountToMeasuredValue((object) str);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        textBox.Text = oldValue != null ? oldValue.ToString() : string.Empty;
        return oldValue;
      }
    }
    return measuredValue;
  }

  /// <summary>
  /// Проверка на допустимость добавления атрибута для объекта
  /// с добавлением в атрибуте процессор, если можно...
  /// </summary>
  /// <param name="session"></param>
  /// <param name="_objType"></param>
  /// <param name="av"></param>
  protected void CheckEnableAddAttribute(
    IUserSession session,
    IObjectClassificator oc,
    int objType,
    AttributeValues av)
  {
    if ((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(objType).AnyAttributes)
    {
      this.aProcessor.ActualAttributeValues.Add(av);
    }
    else
    {
      if (!oc.ObligatoryCalculated)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.attributableElementID);
        throw new Exception($"Нельзя добавить атрибут \"{sessionKeeper.Session.GetAttributeType(av.AttributeID).Name}\" объекту \"{dbObject.NameInMessages}\"");
      }
    }
  }

  /// <summary>Изменить значение атрибута "Количество"</summary>
  protected bool ChangeCount(TextBox textBox, MeasuredValue newCount, ref MeasuredValue oldCount)
  {
    if (oldCount == null && newCount == null || (oldCount != null || newCount == null) && (oldCount == null || newCount != null) && MeasureHelper.Compare(oldCount, newCount) == CompareResult.Equal && oldCount.MeasureID == newCount.MeasureID)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetAttributeType(FormHelper.AttributeCountID) is IDBMeasureAttributeType attributeType)
      {
        long[] validPhysicalValues = attributeType.GetValidPhysicalValues();
        if (newCount != null)
        {
          if (validPhysicalValues != null)
          {
            if (validPhysicalValues.Length != 0)
            {
              List<long> longList = new List<long>((IEnumerable<long>) validPhysicalValues);
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(newCount);
              if (descriptor.Empty)
              {
                int num = (int) MessageBox.Show("Не найден описатель для введенной единицы измерения", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textBox.Text = oldCount != null ? oldCount.ToString() : string.Empty;
                return false;
              }
              if (!longList.Contains(descriptor.PhysicalQuantityID))
              {
                int num = (int) MessageBox.Show("Неверная физическая величина", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textBox.Text = oldCount != null ? oldCount.ToString() : string.Empty;
                return false;
              }
            }
          }
        }
      }
    }
    oldCount = newCount;
    if (this.selectedSpecRow != null)
      this.selectedSpecRow.GetAttributeReadOnly(this.selectedSpecRow.avsDocument.Field_Count, 0, this.selectedSpecRow.Relations);
    this.SetValue(FormHelper.AttributeCountID, (object) newCount);
    textBox.Text = oldCount != null ? oldCount.ToString() : string.Empty;
    this.OnChanged();
    return true;
  }

  protected virtual void SaveCount(int productIndex, object value)
  {
    if (this.selectedSpecRow == null)
      return;
    if (!this.selectedSpecRow.GetReadOnlyCount(productIndex))
      this.selectedSpecRow.SetCount(productIndex, value, true);
    else
      this.selectedSpecRow.SetCountMeasure(productIndex, value, true);
  }

  protected virtual void SetValue(int attributeID, object newValue)
  {
    this.aProcessor.SetValue(attributeID, newValue);
  }

  /// <summary>
  /// Обработчик на выход из поля для редактирования атрибута "Обозначение"
  /// </summary>
  /// <param name="value"></param>
  protected void OnDesignationLeave(string value)
  {
    if (!(this.commonData.Designation != value))
      return;
    this.commonData.Designation = value;
    this.OnChanged();
  }

  protected void OnPodborChanged(bool value) => this.OnChanged();

  /// <summary>
  /// Обработчик на выход из поля для редактирования атрибута "Наименование"
  /// </summary>
  /// <param name="value"></param>
  protected void OnNameLeave(string value)
  {
    if (!(this.commonData.Name != value))
      return;
    this.commonData.Name = value;
    this.OnChanged();
  }

  /// <summary>
  /// Обработчик на выход из поля для редактирования атрибута "Формат"
  /// </summary>
  /// <param name="value"></param>
  protected void OnFormatLeave(string value)
  {
    if (!(this.commonData.Format != value))
      return;
    this.commonData.Format = value;
    this.OnChanged();
  }

  /// <summary>
  /// Обработчик на нажатие кнопки вызова редактора для редактирования атрибута "Наименование"
  /// </summary>
  /// <param name="objectID">Идентификатор объекта для которого будет показана история</param>
  protected void OnEditName(long objectID)
  {
    if (objectID == 0L)
      return;
    string str = Convert.ToString(this.ChangeInEditor(FormHelper.AttributeNameID, (object) this.commonData.Name, objectID, AttributableElements.Object));
    if (str.Equals(this.commonData.Name))
      return;
    this.commonData.Name = str;
    this.OnChanged();
  }

  /// <summary>
  /// Обработчик на нажатие кнопки вызова редактора для редактирования атрибута "Обозначение"
  /// </summary>
  /// <param name="objectID">Идентификатор объекта для которого будет показана история</param>
  protected void OnEditDesignation(long objectID)
  {
    if (objectID == 0L)
      return;
    string str = Convert.ToString(this.ChangeInEditor(FormHelper.AttributeDesignationID, (object) this.commonData.Designation, objectID, AttributableElements.Object));
    if (str.Equals(this.commonData.Designation))
      return;
    this.commonData.Designation = str;
    this.OnChanged();
  }

  protected void OnEditMaterial(long objectID)
  {
    if (objectID == 0L)
      return;
    string str = Convert.ToString(this.ChangeInEditor(FormHelper.AttributeMaterialID, (object) this.commonData.Designation, objectID, AttributableElements.Object));
    if (str.Equals(this.commonData.Designation))
      return;
    this.commonData.Designation = str;
    this.OnChanged();
  }

  /// <summary>Получить структуру по идентификатору</summary>
  /// <param name="materialID"></param>
  /// <returns></returns>
  protected MaterialInfo GetMaterial(long materialID)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(materialID, true);
        return new MaterialInfo(dbObject.ObjectID, dbObject.Caption);
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return new MaterialInfo(0L, string.Empty);
    }
  }

  /// <summary>Получить структуру по идентификатору</summary>
  /// <param name="newValue"></param>
  /// <returns></returns>
  protected MaterialInfo GetMaterial(object newValue)
  {
    try
    {
      return this.GetMaterial(Convert.ToInt64(newValue));
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return new MaterialInfo(0L, string.Empty);
    }
  }

  /// <summary>
  /// Обработчик на нажатие кнопки вызова редактора для редактирования атрибута "Формат"
  /// </summary>
  /// <param name="objectID">Идентификатор объекта для которого будет показана история</param>
  protected void OnEditFormat(long objectID)
  {
    string str = Convert.ToString(this.ChangeInEditor(FormHelper.AttributeFormatID, (object) this.commonData.Format, objectID, AttributableElements.Object));
    if (!(str != this.commonData.Format))
      return;
    this.commonData.Format = str;
    this.OnChanged();
  }

  /// <summary>Получить значения для комбобокса "Формат"</summary>
  /// <param name="documentID"></param>
  /// <returns></returns>
  protected void SetFormatValues(long documentID, ComboBox formatBox)
  {
    formatBox.Items.AddRange(new object[6]
    {
      (object) "A0",
      (object) "A1",
      (object) "A2",
      (object) "A3",
      (object) "A4",
      (object) "A5"
    });
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (PageUserControl);
    this.ResumeLayout(false);
  }
}

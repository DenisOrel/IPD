
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeProcessor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>Класс для централизованной работы с атрибутами</summary>
public class AttributeProcessor
{
  private bool loaded;
  private long id;
  private int elementType;
  private bool anyAttribute;
  private AttributableElements elementKind;
  private bool _invokeNotifications = true;
  private GetAttributeValuesModes getAttributeValuesModes;
  private AttributeValuesList originalAttributeValues;
  private AttributeValuesList actualAttributeValues;
  private static readonly GetAttributeValuesModes defaultGetAttributeValuesModes = GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption;
  private AttributeValuesList attributeValuesBackup;
  private bool inTransaction;
  private AttributeValuesList lastDeltaBackup;
  private AttributeValuesList lastDelta;
  private bool modified;
  private bool multiValuesAsOneEdit = true;
  private ObjectPropDescriptorHolder holder;
  private AttributeProcessorControlsContext context;

  /// <summary>инициализировано или нет</summary>
  public bool Loaded => this.loaded;

  /// <summary>
  /// назначенный id объекта-связи; 0 - не инициализировано
  /// </summary>
  public long Id => this.id;

  /// <summary>id типа объекта-связи</summary>
  public int ElementType => this.elementType;

  /// <summary>
  /// флаг от типа объекта-связи: можно ли к объекту-связи типа elementType назначать любой атрибут
  /// </summary>
  public bool AnyAttribute => this.anyAttribute;

  /// <summary>назначенный attributeValuesModes</summary>
  public AttributableElements ElementKind => this.elementKind;

  /// <summary>
  /// Рассылать сообщения навигатору,
  /// если нет то нужно делат это самому
  /// </summary>
  public bool InvokeNotifications
  {
    get => this._invokeNotifications;
    set => this._invokeNotifications = value;
  }

  public GetAttributeValuesModes GetAttributeValuesModes => this.getAttributeValuesModes;

  /// <summary>список после load</summary>
  public AttributeValuesList OriginalAttributeValues => this.originalAttributeValues;

  /// <summary>измененные в процессе редактирования значения</summary>
  public AttributeValuesList ActualAttributeValues => this.actualAttributeValues;

  /// <summary>флаг нахождения в транзакции</summary>
  public bool InTransaction => this.inTransaction;

  /// <summary>
  /// список AttributeValues, измененных во время последней команды CommitTransaction
  /// </summary>
  public AttributeValuesList LastDelta => this.lastDelta;

  /// <summary>стартовать транзакцию</summary>
  public void StartTransaction()
  {
    if (this.inTransaction)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_887"));
    this.attributeValuesBackup = (AttributeValuesList) this.actualAttributeValues.Clone();
    this.lastDeltaBackup = this.lastDelta;
    this.inTransaction = true;
  }

  public void CommitTransaction()
  {
    if (!this.inTransaction)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_888"));
    this.lastDelta = this.actualAttributeValues.ReturnDelta(this.attributeValuesBackup);
    this.inTransaction = false;
    this.attributeValuesBackup = (AttributeValuesList) null;
  }

  public void RollbackTransaction()
  {
    if (!this.inTransaction)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_889"));
    this.actualAttributeValues = this.attributeValuesBackup;
    this.lastDelta = this.lastDeltaBackup;
    this.inTransaction = false;
    this.attributeValuesBackup = (AttributeValuesList) null;
  }

  /// <summary>Было ли изменение внутреннего списка атрибутов,</summary>
  public bool Modified => this.modified;

  /// <summary>
  /// изменить флаг модификации на true и отправить сообщение AttributeValuesChanged без подробностей в виде EventArgs
  /// </summary>
  public void SetModified()
  {
    this.modified = true;
    if (this.AttributeValuesChanged == null)
      return;
    this.AttributeValuesChanged((object) this, (AttributeValuesChangedEventArgs) null);
  }

  /// <summary>
  /// происходит при изменении атрибутов через функции AttributeProcessor.
  /// следует отличать от почти одноименного event AttributeValueChanged,
  /// которое присутствует у контролов-редакторов.
  /// </summary>
  public event AttributeValuesChangedHandler AttributeValuesChanged;

  /// <summary>
  /// Вызывается после сохранения, оповещая об возможном изменении значений атрибутов после сохранения на сервере
  /// </summary>
  public event AfterSaveEventHandler AfterSave;

  /// <summary>
  /// Вызывается на валидации значений при вызове AttributeProcessor.ValidateAttributeValues
  /// </summary>
  public event AttributeValuesValidationHandler AttributeValuesValidation;

  /// <summary>Конструктор</summary>
  public AttributeProcessor()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="invokeNotifications">вызвать рассылку сообщений для навигатора</param>
  public AttributeProcessor(bool invokeNotifications)
  {
    this._invokeNotifications = invokeNotifications;
  }

  /// <summary>
  /// инициализация без загрузки данных, хорошо для получения и вызова конверторов на свои данные
  /// </summary>
  /// <param name="aId">Идентификатор объекта или связи</param>
  /// <param name="aAttributableElement">Тип владелеца атрибута - объект или связь</param>
  public AttributeProcessor(long aId, AttributableElements aAttributableElement)
  {
    this.id = aId;
    this.elementKind = aAttributableElement;
  }

  /// <summary>
  /// инициализация без загрузки данных, хорошо для получения и вызова конверторов на свои данные
  /// </summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="invokeNotifications">вызывать рассылку сообщений для навигатора</param>
  public AttributeProcessor(
    long aId,
    AttributableElements aAttributableElement,
    bool invokeNotifications)
    : this(aId, aAttributableElement)
  {
    this._invokeNotifications = invokeNotifications;
  }

  /// <summary>
  /// инициализация без загрузки данных, хорошо для получения и вызова конверторов на свои данные
  /// </summary>
  /// <param name="aId"></param>
  /// <param name="elementType"></param>
  /// <param name="aAttributableElement"></param>
  public AttributeProcessor(long aId, int elementType, AttributableElements aAttributableElement)
    : this(aId, aAttributableElement)
  {
    this.elementType = elementType;
  }

  /// <summary>
  /// инициализация без загрузки данных, хорошо для получения и вызова конверторов на свои данные
  /// </summary>
  /// <param name="aId"></param>
  /// <param name="elementType"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="invokeNotifications">вызывать рассылку сообщений для навигатора</param>
  public AttributeProcessor(
    long aId,
    int elementType,
    AttributableElements aAttributableElement,
    bool invokeNotifications)
    : this(aId, aAttributableElement, invokeNotifications)
  {
    this.elementType = elementType;
  }

  /// <summary>читает напрямую с сервера список значений</summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="aGetAttributeValuesModes"></param>
  /// <returns></returns>
  public static AttributeValues[] GetAttributeValues(
    long aId,
    AttributableElements aAttributableElement,
    GetAttributeValuesModes aGetAttributeValuesModes)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ClientCommons.GetAttributable(aId, aAttributableElement, sessionKeeper.Session)?.GetAttributesValues(aGetAttributeValuesModes);
  }

  public static GetAttributeValuesModes CheckAndFill(GetAttributeValuesModes modes)
  {
    GetAttributeValuesModes attributeValuesModes = AttributeProcessor.defaultGetAttributeValuesModes;
    if ((modes & GetAttributeValuesModes.IncludeAlias) != GetAttributeValuesModes.None)
      attributeValuesModes |= GetAttributeValuesModes.IncludeAlias;
    if ((modes & GetAttributeValuesModes.IncludeName) != GetAttributeValuesModes.None)
      attributeValuesModes |= GetAttributeValuesModes.IncludeName;
    if ((modes & GetAttributeValuesModes.IncludeGuid) != GetAttributeValuesModes.None)
      attributeValuesModes |= GetAttributeValuesModes.IncludeGuid;
    if ((modes & GetAttributeValuesModes.IncludeGroupName) != GetAttributeValuesModes.None)
      attributeValuesModes |= GetAttributeValuesModes.IncludeGroupName;
    if ((modes & GetAttributeValuesModes.IncludeCaption) != GetAttributeValuesModes.None)
      attributeValuesModes |= GetAttributeValuesModes.IncludeCaption;
    return attributeValuesModes;
  }

  /// <summary>Инициализация-чтение значений атрибутов</summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="aGetAttributeValuesModes"></param>
  /// <param name="checkVisibility"></param>
  /// <returns>null -&gt; ошибка</returns>
  public AttributeValues[] Load(
    long aId,
    AttributableElements aAttributableElement,
    GetAttributeValuesModes aGetAttributeValuesModes,
    bool checkVisibility = true)
  {
    aGetAttributeValuesModes = AttributeProcessor.CheckAndFill(aGetAttributeValuesModes);
    if (!checkVisibility)
      aGetAttributeValuesModes &= ~GetAttributeValuesModes.CheckVisibility;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int aType = 0;
      IDBAttributable attributable = ClientCommons.GetAttributable(aId, aAttributableElement, out aType, sessionKeeper.Session);
      if (attributable == null)
        return (AttributeValues[]) null;
      AttributeValuesList aOriginalAttributeValues = new AttributeValuesList((IEnumerable<AttributeValues>) attributable.GetAttributesValues(aGetAttributeValuesModes));
      bool anyAttributesFlag = ClientCommons.GetAnyAttributesFlag(aType, aAttributableElement);
      return this.MemLoad(aId, aAttributableElement, aGetAttributeValuesModes, aType, anyAttributesFlag, aOriginalAttributeValues);
    }
  }

  /// <summary>Инициализация-чтение значений атрибутов</summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="aGetAttributeValuesModes"></param>
  /// <returns>null -&gt; ошибка</returns>
  public AttributeValues[] Load(
    object value,
    AttributableElements attributableElement,
    GetAttributeValuesModes modes)
  {
    AttributeValues[] attributeValuesArray = (AttributeValues[]) null;
    if (value != null && value != DBNull.Value)
    {
      long result = attributableElement == AttributableElements.Object ? 0L : 0L;
      int aType = 0;
      if (!(value is IDBAttributable dbAttributable))
      {
        if (long.TryParse(value.ToString(), out result))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            dbAttributable = ClientCommons.GetAttributable(result, attributableElement, out aType, sessionKeeper.Session);
        }
      }
      else
      {
        result = attributableElement == AttributableElements.Object ? (dbAttributable as IDBObject).ObjectID : (dbAttributable as IDBRelation).RelationID;
        aType = dbAttributable.TypeID;
      }
      if (dbAttributable != null)
      {
        AttributeValuesList aOriginalAttributeValues = new AttributeValuesList((IEnumerable<AttributeValues>) dbAttributable.GetAttributesValues(modes));
        bool anyAttributesFlag = ClientCommons.GetAnyAttributesFlag(aType, attributableElement);
        attributeValuesArray = this.MemLoad(result, attributableElement, modes, aType, anyAttributesFlag, aOriginalAttributeValues);
      }
    }
    return attributeValuesArray;
  }

  public AttributeValues[] MemLoad(
    long aId,
    AttributableElements aAttributableElement,
    GetAttributeValuesModes aGetAttributeValuesModes,
    int aElementType,
    bool aAnyAttribute,
    AttributeValuesList aOriginalAttributeValues)
  {
    this.id = aId;
    this.elementKind = aAttributableElement;
    this.getAttributeValuesModes = aGetAttributeValuesModes;
    this.elementType = aElementType;
    this.originalAttributeValues = (AttributeValuesList) aOriginalAttributeValues.Clone();
    for (int index = 0; index < this.originalAttributeValues.Count; ++index)
    {
      if (!this.originalAttributeValues[index].ReadOnly && Statics.CheckAttributeReadonlyBlacklist(this.originalAttributeValues[index].AttributeID))
        this.originalAttributeValues[index].ReadOnly = true;
    }
    ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true).LockAttributeValues(aAttributableElement, aId, aElementType, (IList<AttributeValues>) aOriginalAttributeValues);
    this.actualAttributeValues = (AttributeValuesList) this.originalAttributeValues.Clone();
    this.anyAttribute = aAnyAttribute;
    this.modified = false;
    this.loaded = true;
    return aOriginalAttributeValues.ToArray();
  }

  /// <summary>
  /// сохраняет изменения на сервер.
  /// возвращается список значений атрибутов, измененных на стороне сервера
  /// </summary>
  /// <returns></returns>
  public AttributeValues[] Save()
  {
    if (!this.loaded)
      return (AttributeValues[]) null;
    AttributeValues[] delta = (AttributeValues[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int aType = -1;
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.elementKind, out aType, sessionKeeper.Session);
      if (attributable == null)
        throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingAttributable);
      AttributeValuesList attributeValuesList1 = new AttributeValuesList((IEnumerable<AttributeValues>) this.actualAttributeValues);
      for (int index = 0; index < this.originalAttributeValues.Count; ++index)
      {
        if (AttributeProcessor.FindAttributeValuesIndex(this.originalAttributeValues[index].AttributeID, this.actualAttributeValues) == -1)
          attributeValuesList1.Add(new AttributeValues(this.originalAttributeValues[index].AttributeID, FieldTypes.ftUnknown, MultiValueModes.SingleValue, ComputeValueModes.NotComputableValue)
          {
            Values = new object[1]
            {
              (object) DeleteModesEnum.None
            }
          });
      }
      int index1 = 0;
      while (index1 < attributeValuesList1.Count && attributeValuesList1[index1].AttributeType != FieldTypes.ftUnknown)
      {
        if (attributeValuesList1[index1].AttributeType == FieldTypes.ftSystem)
        {
          attributeValuesList1.RemoveAt(index1);
        }
        else
        {
          int attributeValuesIndex = AttributeProcessor.FindAttributeValuesIndex(attributeValuesList1[index1].AttributeID, this.originalAttributeValues);
          if (attributeValuesIndex != -1)
          {
            if (attributeValuesList1[index1].Equals(this.originalAttributeValues[attributeValuesIndex]))
            {
              attributeValuesList1.RemoveAt(index1);
              continue;
            }
          }
          else if (attributeValuesList1[index1].Values == null || attributeValuesList1[index1].Values == DBNull.Value || attributeValuesList1[index1].Values.Length == 0 || attributeValuesList1[index1].Values.Length == 1 && (attributeValuesList1[index1].Values[0] == DBNull.Value || attributeValuesList1[index1].Values[0] == null))
          {
            attributeValuesList1.RemoveAt(index1);
            continue;
          }
          ++index1;
        }
      }
      if (this.elementKind == AttributableElements.Object)
      {
        IDocumentTypeSettingsService customService = sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService;
        if (customService.InheritedFromDocuments(sessionKeeper.Session.SessionGUID, aType))
        {
          int attributeId = sessionKeeper.Session.IdentHelper.GetAttributeID("cad0001f-306c-11d8-b4e9-00304f19f545");
          AttributeValues byAttributeId = attributeValuesList1.FindByAttributeID(attributeId);
          if (byAttributeId != null)
          {
            DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, aType);
            if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
            {
              string designation = Convert.ToString(byAttributeId.Values[0]);
              byAttributeId.Values[0] = (object) DocumentsHelper.AppendDocCode(sessionKeeper.Session, designation, settings.DocumentTypeCode);
              if (!designation.Equals(Convert.ToString(byAttributeId.Values[0])))
              {
                AttributeValues attributeValues = (AttributeValues) byAttributeId.Clone();
              }
            }
          }
        }
      }
      AttributeValuesList attributeValuesList2 = new AttributeValuesList(0);
      AttributeValues[] array = attributeValuesList1.ToArray();
      if (array.Length != 0)
      {
        AttributeProcessor.ReplacePasswordString(array);
        foreach (AttributeValues attributeValues in array)
          attributeValues.ReadOnly = false;
        delta = attributable.SetAttributesValues(array, false, true, true, this.getAttributeValuesModes);
        if (delta != null)
        {
          for (int index2 = 0; index2 < delta.Length; ++index2)
          {
            AttributeValues attributeValues1 = AttributeProcessor.FindAttributeValues(delta[index2].AttributeID, this.actualAttributeValues);
            if (attributeValues1 != null)
            {
              AttributeValues attributeValues2 = (AttributeValues) delta[index2].Clone();
              attributeValues1.Values = attributeValues2.Values;
              attributeValues1.Descriptions = attributeValues2.Descriptions;
            }
          }
        }
        attributeValuesList2 = (AttributeValuesList) attributeValuesList1.Clone();
        if (delta != null)
        {
          for (int index3 = 0; index3 < delta.Length; ++index3)
          {
            int indexByAttributeId = attributeValuesList2.FindIndexByAttributeID(delta[index3].AttributeID);
            if (indexByAttributeId == -1)
              attributeValuesList2.Add(delta[index3]);
            else
              attributeValuesList2[indexByAttributeId] = (AttributeValues) delta[index3].Clone();
          }
        }
      }
      AttributeValuesList originalAttributeValues = this.originalAttributeValues;
      this.originalAttributeValues = (AttributeValuesList) this.actualAttributeValues.Clone();
      this.modified = false;
      if (this.AfterSave != null)
        this.AfterSave((object) this, new AfterSaveEventEventArgs(delta));
      if (this._invokeNotifications)
      {
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        {
          switch (this.elementKind)
          {
            case AttributableElements.Object:
              service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", this.id, aType, originalAttributeValues.ToArray(), attributeValuesList2?.ToArray()));
              break;
            case AttributableElements.Relation:
              service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsExtendedEventArgs("RelationsChanged", this.id, aType, originalAttributeValues.ToArray(), attributeValuesList2?.ToArray()));
              break;
          }
        }
      }
    }
    return delta;
  }

  /// <summary>
  /// Метод шифрует значения типа Пароль перед передачей их на сервер
  /// </summary>
  /// <param name="attributeValuesNew">Массив значений, отправляемый на сервер</param>
  public static void ReplacePasswordString(AttributeValues[] attributeValuesNew)
  {
    for (int index = 0; index < attributeValuesNew.Length; ++index)
    {
      if (attributeValuesNew[index].AttributeType == FieldTypes.ftPassword && attributeValuesNew[index].Values.Length != 0)
      {
        IMServerService service = ServicesManager.GetService(typeof (IMServerService)) as IMServerService;
        string str = attributeValuesNew[index].Values[0].ToString();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          EncryptedAttributeHelper.ValidateComplexPassword(sessionKeeper.Session, str);
        attributeValuesNew[index].Values = new object[1]
        {
          (object) new PswPackage(str, service.ServerObject.CryptMethod)
        };
      }
    }
  }

  public bool MultiValuesAsOneEdit
  {
    get => this.multiValuesAsOneEdit;
    set => this.multiValuesAsOneEdit = value;
  }

  public bool EditGranted(int attributeId) => false;

  public bool EditByPlaceGranted(int attributeId)
  {
    bool flag = false;
    TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
    if (singleValueConverter != null)
      flag = singleValueConverter.CanConvertFrom(typeof (string));
    return flag;
  }

  public bool CheckValueByMask(int attributeId) => true;

  public AttributeValuesList EditAttributeValue(int attributeId, UITypeEditorEditStyle editorStyle)
  {
    int? index = MultiValueModesHelper.IsMultipleValued(AttributeProcessorProcs.GetMultiValueModes(attributeId)) ? new int?() : new int?(0);
    IAttributeEditorControl editorControl = this.GetEditorControl(attributeId, index, UITypeEditorEditStyle.Modal);
    if (editorControl == null || !(editorControl is Form) || ((Form) editorControl).ShowDialog() != DialogResult.OK)
      return (AttributeValuesList) null;
    return this.LastDelta == null ? (AttributeValuesList) null : (AttributeValuesList) this.LastDelta.Clone();
  }

  /// <summary>Получить редактор для атрибута</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="index">Индекс редактируемого значения атрибута. При null вернуть редактор множественного значения</param>
  /// <param name="style">Стиль редактора. При DropDown выдается Control, при Modal выдается форма, в которую помещен Control</param>
  /// <param name="wrapEditorControlForForm">Создавать форму если есть только DropDown редактор</param>
  /// <returns>Control или форма, содержащая Control. И Control и форма поддерживают интерфейс взаимодействия IAttributeEditorControl </returns>
  public IAttributeEditorControl GetEditorControl(
    int attributeId,
    int? index,
    UITypeEditorEditStyle style,
    bool wrapEditorControlForForm)
  {
    IAttributeEditorControl iAttributeEditorControl = (IAttributeEditorControl) null;
    if (style == UITypeEditorEditStyle.None)
      return (IAttributeEditorControl) null;
    TypeConverter typeConverter = !index.HasValue ? this.GetMultipleValuesConverter(attributeId) : this.GetSingleValueConverter(attributeId);
    if (typeConverter != null)
    {
      iAttributeEditorControl = typeConverter is CommonTypeConverter ? ((CommonTypeConverter) typeConverter).GetEditorControl(style) : throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_890"));
      if (iAttributeEditorControl != null && style == UITypeEditorEditStyle.Modal && !(iAttributeEditorControl is Form))
        iAttributeEditorControl = (IAttributeEditorControl) null;
      if (iAttributeEditorControl != null && style == UITypeEditorEditStyle.DropDown && !(iAttributeEditorControl is Control))
        iAttributeEditorControl = (IAttributeEditorControl) null;
      if (style == UITypeEditorEditStyle.Modal)
      {
        if (iAttributeEditorControl != null)
        {
          if (iAttributeEditorControl is Control && !(iAttributeEditorControl is Form))
          {
            EditorControlForm editorControlForm = new EditorControlForm();
            editorControlForm.AssignControl(iAttributeEditorControl);
            iAttributeEditorControl = (IAttributeEditorControl) editorControlForm;
          }
        }
        else
        {
          iAttributeEditorControl = ((CommonTypeConverter) typeConverter).GetEditorControl(UITypeEditorEditStyle.DropDown);
          if (iAttributeEditorControl != null)
          {
            EditorControlForm editorControlForm = new EditorControlForm();
            editorControlForm.AssignControl(iAttributeEditorControl);
            iAttributeEditorControl = (IAttributeEditorControl) editorControlForm;
          }
        }
      }
      iAttributeEditorControl?.InitControl(attributeId, (object) this, index);
    }
    return iAttributeEditorControl;
  }

  /// <summary>Выдается редактор для атрибута</summary>
  /// <param name="attributeId"></param>
  /// <param name="index">Индекс редактируемого значения атрибута. При null вернуть редактор множественного значения</param>
  /// <param name="style">Стиль редактора. При DropDown выдается Control, при Modal выдается форма, в которую помещен Control</param>
  /// <returns>Control или форма, содержащая Control. И Control и форма поддерживают интерфейс взаимодействия IAttributeEditorControl </returns>
  public IAttributeEditorControl GetEditorControl(
    int attributeId,
    int? index,
    UITypeEditorEditStyle style)
  {
    return this.GetEditorControl(attributeId, index, style, false);
  }

  /// <summary>
  /// Получить дескриптор атрибута : одиночного или многозначного
  /// </summary>
  /// <param name="attributeId">id атрибута</param>
  /// <param name="attrs">доп. атрибуты</param>
  /// <returns></returns>
  public CommonPropertyDescriptor GetPropertyDescriptor(int attributeId, Attribute[] attrs)
  {
    CommonPropertyDescriptor propertyDescriptor = (CommonPropertyDescriptor) null;
    AttributeValues attributeValues = this.FindAttributeValues(attributeId);
    return attributeValues == null ? propertyDescriptor : (!MultiValueModesHelper.IsMultipleValued(attributeValues.MultipleValued) ? (CommonPropertyDescriptor) new SinglePropertyDescriptor(attributeId, this, (string) null, attrs, (System.Type) null, (TypeConverter) null, new bool?(), new bool?()) : (CommonPropertyDescriptor) new MultipleValuesPropertyDescriptor(attributeId, this, (string) null, attrs, new bool?(), new bool?()));
  }

  /// <summary>
  /// Производит валидацию значений атрибутов с использование валидаторов конверторов,
  /// затем результаты прогоняются еще и через event AttributeValuesValidation
  /// </summary>
  /// <param name="attributeValuesList"></param>
  /// <returns>null -&gt; валидация прошла успешно, иначе список невалидированных элементов</returns>
  public List<ValidationResult> ValidateAttributeValues(AttributeValuesList attributeValuesList)
  {
    List<ValidationResult> results = new List<ValidationResult>();
    for (int index = 0; index < attributeValuesList.Count; ++index)
    {
      TypeConverter converter = this.GetConverter(attributeValuesList[index].AttributeID);
      if (converter != null && converter is CommonTypeConverter)
      {
        if (AttributeProcessorProcs.IsMultipleValued(attributeValuesList[index].AttributeID))
          ((CommonTypeConverter) converter).IsValidExt((ITypeDescriptorContext) null, (object) attributeValuesList[index].Values, ref results);
        else
          ((CommonTypeConverter) converter).IsValidExt((ITypeDescriptorContext) null, attributeValuesList[index].Values != null ? attributeValuesList[index].Values[0] : (object) null, ref results);
      }
    }
    if (this.AttributeValuesValidation != null)
      this.AttributeValuesValidation((object) this, new AttributeValuesValidationEventArgs((AttributeValuesList) attributeValuesList.Clone(), results));
    return results.Count <= 0 ? (List<ValidationResult>) null : results;
  }

  private int FindAttributeValuesIndex(int attributeId)
  {
    return this.FindAttributeValuesIndex(attributeId, true);
  }

  private int FindAttributeValuesIndex(int attributeId, bool inActualValues)
  {
    AttributeValuesList ava = inActualValues ? this.actualAttributeValues : this.originalAttributeValues;
    return AttributeProcessor.FindAttributeValuesIndex(attributeId, ava);
  }

  private static int FindAttributeValuesIndex(int attributeId, AttributeValuesList ava)
  {
    return ava.FindIndexByAttributeID(attributeId);
  }

  public AttributeValues FindAttributeValues(int attributeId)
  {
    return this.FindAttributeValues(attributeId, true);
  }

  public AttributeValues FindAttributeValues(int attributeId, bool inActualValues)
  {
    AttributeValuesList ava = inActualValues ? this.actualAttributeValues : this.originalAttributeValues;
    return AttributeProcessor.FindAttributeValues(attributeId, ava);
  }

  public static AttributeValues FindAttributeValues(int attributeId, AttributeValuesList ava)
  {
    return ava.FindByAttributeID(attributeId);
  }

  /// <summary>
  /// возвращает AttributeValues, инициализированный всеми нужными значениями по умолчанию
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="elementId"></param>
  /// <param name="elementKind"></param>
  /// <returns></returns>
  public static AttributeValues CreateAttributeValues(
    int attributeId,
    long elementId,
    AttributableElements elementKind)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    if (attributeType == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_891") + attributeId.ToString());
    AttributeValues attributeValues = new AttributeValues(attributeType.AttributeID, attributeType.AttributeType, attributeType.MultipleValued, attributeType.Computed);
    attributeValues.AttributeName = attributeType.Name;
    object[] objArray1 = new object[1];
    object[] objArray2 = new object[1];
    bool flag = true;
    string str = string.Empty;
    ArrayList groupById = DataHolders.AttributesHolder.GetGroupByID(attributeType.AttributeID);
    if (groupById != null && groupById.Count > 0)
      str = DataHolders.AttributeGroupsHolder.GetNamebyID((int) groupById[0]);
    if (attributeType.AttributeType != FieldTypes.ftMemo && attributeType.AttributeType != FieldTypes.ftAutoInc)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable attributable = ClientCommons.GetAttributable(elementId, elementKind, sessionKeeper.Session);
        if (attributable != null)
        {
          IDBAttribute dbAttribute = attributable.Attributes.AddTemporaryAttribute(attributeType.AttributeID, false);
          if (dbAttribute != null)
          {
            objArray1 = new object[dbAttribute.Values.Length];
            dbAttribute.Values.CopyTo((Array) objArray1, 0);
            objArray2 = new object[dbAttribute.Values.Length];
            flag = dbAttribute.ReadOnly;
            if (!flag && Statics.CheckAttributeReadonlyBlacklist(attributeId))
              flag = true;
            if (str == string.Empty)
              str = dbAttribute.GroupName;
          }
        }
      }
    }
    attributeValues.Values = objArray1;
    attributeValues.ReadOnly = flag;
    attributeValues.GroupName = str;
    attributeValues.Descriptions = objArray2;
    return attributeValues;
  }

  /// <summary>вернуть тип атрибута</summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public FieldTypes GetFieldType(int attributeId)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    return attributeType != null ? attributeType.AttributeType : FieldTypes.ftUnknown;
  }

  /// <summary>поиск типа свойства (он же TypeOfAttributeValue).</summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public System.Type GetPropertyType(int attributeId)
  {
    return AttributesTypeHelper.GetTypeOfAttributeValue(this.GetFieldType(attributeId));
  }

  public bool GetReadOnly(int attributeId)
  {
    return ((this.FindAttributeValues(attributeId, false) ?? this.FindAttributeValues(attributeId, true)) ?? throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_892"))).ReadOnly;
  }

  public bool GetCanReset(int attributeId)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    if (attributeType == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_893"));
    return (attributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None;
  }

  /// <summary>
  /// вернуть конвертер в зависимости от того, множественный атрибут или одиночный
  /// * можно вызывать без инициализации this класса
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public TypeConverter GetConverter(int attributeId)
  {
    return AttributeProcessorProcs.IsMultipleValued(attributeId) ? this.GetMultipleValuesConverter(attributeId) : this.GetSingleValueConverter(attributeId);
  }

  public TypeConverter GetOriginalConverter(int attributeId)
  {
    return AttributeProcessorProcs.IsMultipleValued(attributeId) ? this.GetOriginalMultipleValuesConverter(attributeId) : this.GetOriginalSingleValueConverter(attributeId);
  }

  public TypeConverter GetOriginalSingleValueConverter(int attributeId)
  {
    TypeConverter singleValueConverter = (TypeConverter) null;
    IDBAttributeTypeInfo attributeType1 = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    if (attributeType1 == null)
      return singleValueConverter;
    FieldTypes attributeType2 = attributeType1.AttributeType;
    switch (attributeType2)
    {
      case FieldTypes.ftString:
        singleValueConverter = (TypeConverter) new StringConverter(attributeId, this);
        break;
      case FieldTypes.ftInteger:
        singleValueConverter = (TypeConverter) new Int64Converter(attributeId, this);
        break;
      case FieldTypes.ftDouble:
        singleValueConverter = (TypeConverter) new DoubleConverter(attributeId, this);
        break;
      case FieldTypes.ftDateTime:
        singleValueConverter = (TypeConverter) new DateTimeAttributeConverter(attributeId, this);
        break;
      case FieldTypes.ftFile:
        singleValueConverter = (TypeConverter) new FileConverter(attributeId, this);
        break;
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        singleValueConverter = (TypeConverter) new ObjectLinkConverter(attributeId, this, attributeType2 == FieldTypes.ftObjectLink);
        break;
      case FieldTypes.ftMemo:
        singleValueConverter = (TypeConverter) new MemoConverter(attributeId, this);
        break;
      case FieldTypes.ftBoolean:
        singleValueConverter = (TypeConverter) new MemoConverter(attributeId, this);
        break;
      case FieldTypes.ftMeasured:
        singleValueConverter = (TypeConverter) new MeasuredValueConverter(attributeId, this);
        break;
      case FieldTypes.ftAutoInc:
        singleValueConverter = (TypeConverter) new AutoIncConverter(attributeId, this);
        break;
      case FieldTypes.ftSystem:
        singleValueConverter = (TypeConverter) new SystemConverter(attributeId, this);
        break;
      case FieldTypes.ftGuid:
        singleValueConverter = (TypeConverter) new GuidConverter(attributeId, this);
        break;
    }
    if (singleValueConverter == null)
      singleValueConverter = (TypeConverter) new CommonTypeConverter(attributeId, this);
    return singleValueConverter;
  }

  /// <summary>
  /// Получить стиль редактирования для атрибута сответствующий propertygridу для атрибута
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns>Стиль редактирования</returns>
  public UITypeEditorEditStyle GetEditorStyle(AttributeValues attrValues)
  {
    PropDescriptor propDescriptor = this.GetPropDescriptor(attrValues);
    if (propDescriptor == null)
      return UITypeEditorEditStyle.None;
    UITypeEditor editor = this.GetEditor(propDescriptor, attrValues);
    if (editor == null)
      return UITypeEditorEditStyle.None;
    ITypeDescriptorContext attributeContext = this.GetAttributeContext(attrValues);
    return editor.GetEditStyle(attributeContext);
  }

  /// <summary>Получить конвертер propertygridу для атрибута</summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns>Конвертер</returns>
  public TypeConverter GetTypeConverter(AttributeValues attrValues)
  {
    return this.GetPropDescriptor(attrValues)?.Converter;
  }

  /// <summary>Получить редактор propertygrid для атрибута</summary>
  /// <param name="pd">Дескриптор атрибута</param>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns></returns>
  private UITypeEditor GetEditor(PropDescriptor pd, AttributeValues attrValues)
  {
    if (!(pd.GetEditor(typeof (UITypeEditor)) is UITypeEditor editor))
    {
      if (pd.Converter != null && pd.Converter.GetStandardValuesSupported())
        return (UITypeEditor) new StandartValuesEditor(pd.Converter);
      if (this.ElementKind == AttributableElements.Object)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.ElementType, attrValues.AttributeID);
        if (attribute4ObjectType != null)
          editor = attribute4ObjectType.PossibleValues == null || attribute4ObjectType.PossibleValues.Count <= 0 ? (UITypeEditor) new HistoryEditor(this.Id, this.ElementKind, attrValues.AttributeID) : (UITypeEditor) null;
      }
    }
    return editor;
  }

  /// <summary>
  /// Получить отображаемое в propertygrid значение атрибута
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns>Строковое отображение атрибута</returns>
  public string GetViewValue(AttributeValues attrValues)
  {
    this.CheckAttributeValues(attrValues);
    return Convert.ToString(this.GetPropDescriptor(attrValues).GetValue((object) null));
  }

  /// <summary>Получить дескриптор для атрибута для propertygrid</summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns>Дескриптор</returns>
  public PropDescriptor GetPropDescriptor(AttributeValues attrValues)
  {
    this.CheckAttributeValues(attrValues);
    if (this.holder == null)
    {
      this.holder = new ObjectPropDescriptorHolder();
      this.holder.AssignData(this.Id, this.ElementKind, GetAttributeValuesModes.None, (ObjectPropertyGrid) null, false, (System.Type[]) null);
    }
    return this.holder.AttributeValuesToPropDescriptor(attrValues);
  }

  /// <summary>
  /// Получить значение как в БД по значению для propertygrid
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее propertygrid</param>
  /// <returns>Значение как в БД</returns>
  public object GetAVValue(AttributeValues attrValues)
  {
    this.CheckAttributeValues(attrValues);
    return AttributeValuesEditor.GetAVValue(this.GetPropDescriptor(attrValues), attrValues, (object) this.holder);
  }

  /// <summary>
  /// Получить значение как в propertygrid по значению для БД
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns>Значение атрибута соответствующее propertygrid</returns>
  public object GetPDValue(AttributeValues attrValues)
  {
    this.CheckAttributeValues(attrValues);
    return this.GetPropDescriptor(attrValues).GetValue((object) null);
  }

  /// <summary>
  /// Получить контекст редактирования для атрибута в propertygrid
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <returns></returns>
  public ITypeDescriptorContext GetAttributeContext(AttributeValues attrValues)
  {
    PropDescriptor propDescriptor = this.GetPropDescriptor(attrValues);
    return (ITypeDescriptorContext) new AttributeProcessorControlsContext(attrValues, propDescriptor, (IElementInfo) null);
  }

  /// <summary>
  /// Редактирование значения атрибута через методы propertygrid
  /// </summary>
  /// <param name="attrValues">Значение атрибута соответствующее БД</param>
  /// <param name="sp">Сервис комманд, который используется propertygrid, если не задавать будет создан новый </param>
  /// <param name="editorControl">Элемент управления для которого будет использован dropdown стиль, в случае если заданы controlBounds, то можно указать основной элемент управления, например если вызывается для грида, то если нет координат, то должна быть ячейка грида, иначе можно сам грид </param>
  /// <param name="controlBounds">Границы элемента управления на краях которого будет показ выпадающий редактор</param>
  /// <returns>Значение атрибута соответствующее БД</returns>
  public object EditValue(
    AttributeValues attrValues,
    System.IServiceProvider sp = null,
    Control editorControl = null,
    Rectangle? controlBounds = null)
  {
    PropDescriptor propDescriptor = this.GetPropDescriptor(attrValues);
    Intermech.Client.Core.FormDesigner.Controls.ElementInfo parentInfo = new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(this.Id, this.ElementKind);
    object obj1 = (object) null;
    if (attrValues.Values != null && attrValues.Values.Length != 0)
      obj1 = attrValues.Values[0];
    UITypeEditor editor = this.GetEditor(propDescriptor, attrValues);
    if (editor != null)
    {
      AttributeProcessorDropDownEditorForm serviceInstance = (AttributeProcessorDropDownEditorForm) null;
      if (sp == null)
      {
        sp = (System.IServiceProvider) new ServiceContainer();
        serviceInstance = new AttributeProcessorDropDownEditorForm(editorControl, controlBounds);
        (sp as ServiceContainer).AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
      }
      try
      {
        ITypeDescriptorContext context = (ITypeDescriptorContext) new AttributeProcessorControlsContext(attrValues, propDescriptor, (IElementInfo) parentInfo);
        switch (editor.GetEditStyle(context))
        {
          case UITypeEditorEditStyle.Modal:
          case UITypeEditorEditStyle.DropDown:
            object objB = propDescriptor.GetValue((object) null);
            object obj2 = editor.EditValue(context, sp, objB);
            if (!object.Equals(obj2, objB))
            {
              propDescriptor.SetValue((object) null, obj2);
              AttributeValues attributeValues = new AttributeValues(attrValues.AttributeID, obj2);
              this.CheckAttributeValues(attributeValues);
              obj1 = AttributeValuesEditor.GetAVValue(propDescriptor, attributeValues, (object) this.holder);
              break;
            }
            break;
        }
      }
      finally
      {
        if (serviceInstance != null)
        {
          serviceInstance.Dispose();
          (sp as ServiceContainer).Dispose();
        }
      }
    }
    return obj1;
  }

  /// <summary>Проверка значения на заполненность всех полей</summary>
  /// <param name="attrValues"></param>
  private void CheckAttributeValues(AttributeValues attrValues)
  {
    IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
    if (this.ElementKind == AttributableElements.Object)
      imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(this.ElementType, attrValues.AttributeID);
    if (this.ElementKind == AttributableElements.Relation)
      imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(this.ElementType, attrValues.AttributeID);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrValues.AttributeID);
    if (attributeType == null)
      return;
    if (imsAttribute4 != null)
    {
      attrValues.AttributeType = imsAttribute4.FieldType;
      attrValues.ComputeMode = imsAttribute4.Computed;
    }
    else
    {
      attrValues.AttributeType = attributeType.FieldType;
      attrValues.ComputeMode = attributeType.Computed;
    }
    attrValues.AttributeName = attributeType.Name;
    attrValues.MultipleValued = attributeType.MultiValueMode;
  }

  public TypeConverter GetSingleValueConverter(int attributeId)
  {
    TypeConverter singleValueConverter = (TypeConverter) null;
    IAttributePropertyDescriber propertyDescriber = AttributeValuesEditor.GetAttributePropertyDescriber(attributeId);
    if (propertyDescriber != null)
      singleValueConverter = propertyDescriber.GetConverter(attributeId, (object) this);
    if (singleValueConverter == null)
      singleValueConverter = this.GetOriginalSingleValueConverter(attributeId);
    return singleValueConverter;
  }

  public TypeConverter GetOriginalMultipleValuesConverter(int attributeId)
  {
    return (TypeConverter) new MultipleValuesTypeConverter(attributeId, this);
  }

  /// <summary>
  /// Вернуть конвертор множественного атрибута
  /// * можно вызывать без инициализации this класса
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public TypeConverter GetMultipleValuesConverter(int attributeId)
  {
    return this.GetOriginalMultipleValuesConverter(attributeId);
  }

  /// <summary>
  /// добавляет значение в многозначный атрибут
  /// AttributeValue должен присутствовать.
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public AttributeValues AddValue(int attributeId, object value)
  {
    return !this.loaded ? (AttributeValues) null : this.InsertValue(attributeId, -1, value);
  }

  /// <summary>
  /// вставляет значение в многозначный AttributeValue.
  /// AttributeValue должен присутствовать.
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="index">-1 для добавления</param>
  /// <param name="value"></param>
  /// <returns></returns>
  public AttributeValues InsertValue(int attributeId, int index, object value)
  {
    if (!this.loaded)
      return (AttributeValues) null;
    AttributeValues attributeValues = this.FindAttributeValues(attributeId);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_894"));
    if (attributeValues.Values == null || attributeValues.Values == DBNull.Value)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_895"));
    if (!MultiValueModesHelper.IsMultipleValued(attributeValues.MultipleValued))
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_896"));
    if (index >= attributeValues.Values.Length)
      index = -1;
    System.Type propertyType = this.GetPropertyType(attributeId);
    TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
    if (singleValueConverter == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_898"));
    if (value != null && value != DBNull.Value && !value.GetType().Equals(propertyType))
      value = singleValueConverter.CanConvertFrom(value.GetType()) ? singleValueConverter.ConvertFrom(value) : throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_897") + value.GetType().ToString());
    ArrayList arrayList = new ArrayList((ICollection) attributeValues.Values);
    if (index == -1)
      arrayList.Add(value);
    else
      arrayList.Insert(index, value);
    attributeValues.Values = (object[]) arrayList.ToArray(typeof (object));
    this.modified = true;
    if (this.AttributeValuesChanged != null)
      this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs(attributeId, AttributeValuesAction.InsertValue, (object) new object[2]
      {
        (object) index,
        value
      }));
    return attributeValues;
  }

  /// <summary>удаляет значение из многозначного атрибута</summary>
  /// <param name="attributeId"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public void RemoveValue(int attributeId, int index)
  {
    if (!this.loaded)
      return;
    AttributeValues attributeValues = this.FindAttributeValues(attributeId);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_894"));
    if (attributeValues.Values == null || attributeValues.Values == DBNull.Value || index >= attributeValues.Values.Length)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_899"));
    if (attributeValues.Values.Length <= 1)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_900"));
    ArrayList arrayList = new ArrayList((ICollection) attributeValues.Values);
    object obj = arrayList[index];
    arrayList.RemoveAt(index);
    attributeValues.Values = (object[]) arrayList.ToArray(typeof (object));
    this.modified = true;
    if (this.AttributeValuesChanged == null)
      return;
    this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs(attributeId, AttributeValuesAction.RemoveValue, (object) new object[2]
    {
      (object) index,
      obj
    }));
  }

  /// <summary>
  /// Назначает массив AttributeValues
  /// 
  /// Список может содержать недоинициализированые AttributeValues
  /// (то есть не взятые от сервера, а созданные вручную).
  /// В каждом AttributeValues должен быть обязательно назначен AttributeID и Values.
  /// 
  /// Детектирует master-атрибуты, которые назначаются в первую очередь
  /// с рассовыванием по атрибутам источникам, за ними назначаются остальные атрибуты.
  /// 
  /// При отсутствии атрибутов AttributeValues во внутреннем списке они создаются.
  /// </summary>
  /// <param name="list"></param>
  public void SetAttributeValuesArray(AttributeValuesList list)
  {
    if (!this.loaded)
      return;
    AttributeValuesList attributeValuesList1 = new AttributeValuesList();
    AttributeValuesList attributeValuesList2 = new AttributeValuesList();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].AttributeType == FieldTypes.ftUnknown)
      {
        IDBAttributeTypeInfo attributeType = service.GetAttributeType(list[index].AttributeID);
        list[index].AttributeType = attributeType != null ? attributeType.AttributeType : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingAttribute);
        list[index].MultipleValued = attributeType.MultipleValued;
      }
      if (list[index].AttributeType == FieldTypes.ftObjectLink || list[index].AttributeType == FieldTypes.ftObjectLinkByID)
      {
        int[] source4Attribute = this.GetMasterAndSource4Attribute(list[index].AttributeID);
        if (source4Attribute == null)
          throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_901") + list[index].AttributeID.ToString());
        if (source4Attribute[0] == 0 && list[index].AttributeType == FieldTypes.ftObjectLink)
        {
          List<SlaveAttributeInfo> forMasterAttribute = this.GetSlaveForMasterAttribute(list[index].AttributeID);
          if (forMasterAttribute != null && forMasterAttribute.Count > 0)
          {
            attributeValuesList1.Add(list[index]);
            continue;
          }
        }
      }
      attributeValuesList2.Add(list[index]);
    }
    for (int index = 0; index < attributeValuesList1.Count; ++index)
      this.AssignMasterAttributePrim(attributeValuesList1[index].AttributeID, attributeValuesList1[index].Values[0], this.actualAttributeValues, true);
    for (int index = 0; index < attributeValuesList2.Count; ++index)
    {
      if (AttributeProcessor.FindAttributeValues(attributeValuesList2[index].AttributeID, this.actualAttributeValues) == null)
      {
        AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(attributeValuesList2[index].AttributeID, this.id, this.elementKind);
        attributeValues.ReadOnly = attributeValuesList2[index].ReadOnly;
        this.actualAttributeValues.Add(attributeValues);
      }
      this.SetValues(attributeValuesList2[index].AttributeID, attributeValuesList2[index].Values, this.actualAttributeValues, true);
    }
  }

  /// <summary>убирает AttributeValues по списку</summary>
  /// <param name="attributeIdList"></param>
  public void RemoveAttributeValuesArray(int[] attributeIdList)
  {
    if (!this.loaded)
      return;
    for (int index = 0; index < attributeIdList.Length; ++index)
    {
      int attributeValuesIndex = AttributeProcessor.FindAttributeValuesIndex(attributeIdList[index], this.actualAttributeValues);
      if (attributeValuesIndex != -1)
      {
        AttributeValues actualAttributeValue = this.actualAttributeValues[attributeValuesIndex];
        this.actualAttributeValues.RemoveAt(attributeValuesIndex);
        this.modified = true;
        if (this.AttributeValuesChanged != null)
          this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs(attributeIdList[index], AttributeValuesAction.Remove, (object) actualAttributeValue));
      }
    }
  }

  /// <summary>
  /// назначает SingleValued атрибут.
  /// не проверяет на master атрибут.
  /// AttributeValue должен присутствовать
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="value"></param>
  public AttributeValues SetValue(int attributeId, object value)
  {
    return !this.loaded ? (AttributeValues) null : this.SetValue(attributeId, value, this.actualAttributeValues, true);
  }

  protected AttributeValues SetValue(
    int attributeId,
    object value,
    AttributeValuesList avList,
    bool throwChangeEvent)
  {
    return !this.loaded ? (AttributeValues) null : this.SetValue(attributeId, 0, value, avList, throwChangeEvent);
  }

  /// <summary>
  /// инициализировать values[ index ] = value для атрибута attributeId.
  /// при выходе за границы index выдается exception.
  /// не проверяет на master атрибут.
  /// AttributeValue должен присутствовать
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="index"></param>
  /// <param name="value"></param>
  public AttributeValues SetValue(int attributeId, int index, object value)
  {
    return !this.loaded ? (AttributeValues) null : this.SetValue(attributeId, index, value, this.actualAttributeValues, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="index"></param>
  /// <param name="value"></param>
  /// <param name="avList"></param>
  /// <param name="throwChangeEvent"></param>
  /// <returns></returns>
  protected AttributeValues SetValue(
    int attributeId,
    int index,
    object value,
    AttributeValuesList avList,
    bool throwChangeEvent)
  {
    if (!this.loaded)
      return (AttributeValues) null;
    AttributeValues attributeValues = AttributeProcessor.FindAttributeValues(attributeId, avList);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_894"));
    if (attributeValues.Values == null || attributeValues.Values == DBNull.Value)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_903"));
    if (index >= attributeValues.Values.Length || !MultiValueModesHelper.IsMultipleValued(attributeValues.MultipleValued) && index > 0)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_899"));
    System.Type propertyType = this.GetPropertyType(attributeId);
    TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
    if (singleValueConverter == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_898"));
    if (value != null && value != DBNull.Value && !value.GetType().Equals(propertyType) && !(value is DeleteModesEnum))
      value = singleValueConverter.CanConvertFrom(value.GetType()) ? singleValueConverter.ConvertFrom(value) : throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_897") + value.GetType().ToString());
    if (!AttributeValues.ValueEquals(attributeValues.Values[index], value))
    {
      attributeValues.Values[index] = value;
      this.modified = true;
      if (this.AttributeValuesChanged != null & throwChangeEvent)
        this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs(attributeId, AttributeValuesAction.ModifyValue, (object) new object[2]
        {
          (object) index,
          value
        }));
    }
    return attributeValues;
  }

  /// <summary>
  /// проинициализировать атрибут со множественными значениями.
  /// должен присутствовать в списке.
  /// не проверяет на master атрибут.
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="values"></param>
  public AttributeValues SetValues(int attributeId, object[] values)
  {
    return !this.loaded ? (AttributeValues) null : this.SetValues(attributeId, values, this.actualAttributeValues, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="values"></param>
  /// <param name="avList"></param>
  /// <param name="throwChangeEvent"></param>
  /// <returns></returns>
  protected AttributeValues SetValues(
    int attributeId,
    object[] values,
    AttributeValuesList avList,
    bool throwChangeEvent)
  {
    if (!this.loaded)
      return (AttributeValues) null;
    AttributeValues attributeValues = AttributeProcessor.FindAttributeValues(attributeId, avList);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_894"));
    if ((attributeValues.Values == null || attributeValues.Values == DBNull.Value) && values != null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_904"));
    if (values != null && values != DBNull.Value)
    {
      ArrayList arrayList = new ArrayList((ICollection) values);
      System.Type propertyType = this.GetPropertyType(attributeId);
      TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
      if (singleValueConverter == null)
        throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_906"));
      for (int index = 0; index < arrayList.Count; ++index)
      {
        if (arrayList[index] != DBNull.Value && arrayList[index] != null && !arrayList[index].GetType().Equals(propertyType) && !(arrayList[index] is DeleteModesEnum))
        {
          if (!singleValueConverter.CanConvertFrom(arrayList[index].GetType()))
            throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_905") + arrayList[index].GetType().ToString())
            {
              AddiotionelMsg = string.Format(LocalizationHolder.rm.GetString("AttributeProcessor_CastValueExeption_Msg"), (object) arrayList[index].ToString(), (object) attributeValues.AttributeName)
            };
          arrayList[index] = singleValueConverter.ConvertFrom(arrayList[index]);
        }
      }
      attributeValues.Values = (object[]) arrayList.ToArray(typeof (object));
    }
    else
      attributeValues.Values = values;
    this.modified = true;
    if (this.AttributeValuesChanged != null & throwChangeEvent)
      this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs(attributeId, AttributeValuesAction.ModifyValue, (object) new object[2]
      {
        (object) -1,
        (object) values
      }));
    return attributeValues;
  }

  /// <summary>вернуть значение [0] для атрибута</summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public object GetValue(int attributeId)
  {
    return !this.loaded ? (object) null : this.GetValue(attributeId, 0, (System.Type) null);
  }

  /// <summary>
  /// вернуть значение [0] для атрибута с приведением к типу
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public object GetValue(int attributeId, System.Type type)
  {
    return !this.loaded ? (object) null : this.GetValue(attributeId, 0, type);
  }

  /// <summary>вернуть значение [index] для атрибута</summary>
  /// <param name="attributeId"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public object GetValue(int attributeId, int index)
  {
    return !this.loaded ? (object) null : this.GetValue(attributeId, index, (System.Type) null);
  }

  /// <summary>
  /// вернуть значение [index] для атрибута с приведением к типу
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="index"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public object GetValue(int attributeId, int index, System.Type type)
  {
    if (!this.loaded)
      return (object) null;
    AttributeValues attributeValues = this.FindAttributeValues(attributeId);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_907"));
    if (attributeValues.Values == null || attributeValues.Values == DBNull.Value)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_908"));
    if (attributeValues.Values[index] == null || attributeValues.Values[index] == DBNull.Value)
      return attributeValues.Values[index];
    object obj = attributeValues.Values[index];
    if (type == (System.Type) null)
      return obj;
    TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
    if (singleValueConverter == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_910"));
    if (!singleValueConverter.CanConvertTo(type))
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_909") + type.ToString());
    if (obj != null && obj != DBNull.Value && !obj.GetType().Equals(type))
      obj = singleValueConverter.ConvertTo(obj, type);
    return obj;
  }

  /// <summary>вернуть массив значений для атрибута</summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public object[] GetValues(int attributeId)
  {
    return !this.loaded ? (object[]) null : this.GetValues(attributeId, (System.Type) null);
  }

  /// <summary>
  /// вернуть массив значений для атрибута с приведением к типу
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public object[] GetValues(int attributeId, System.Type type)
  {
    if (!this.loaded)
      return (object[]) null;
    AttributeValues attributeValues = this.FindAttributeValues(attributeId);
    if (attributeValues == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_911"));
    if (attributeValues.Values == null || attributeValues.Values == DBNull.Value)
      return attributeValues.Values;
    object[] values = ((AttributeValues) attributeValues.Clone()).Values;
    if (type == (System.Type) null)
      return values;
    TypeConverter singleValueConverter = this.GetSingleValueConverter(attributeId);
    if (singleValueConverter == null)
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_913"));
    if (!singleValueConverter.CanConvertTo(type))
      throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_912") + type.ToString());
    ArrayList arrayList = new ArrayList((ICollection) values);
    for (int index = 0; index < arrayList.Count; ++index)
    {
      if (arrayList[index] != null && arrayList[index] != DBNull.Value && !arrayList[index].GetType().Equals(type))
        arrayList[index] = singleValueConverter.ConvertTo(arrayList[index], type);
    }
    return (object[]) arrayList.ToArray(typeof (object));
  }

  public int[] GetMasterAndSource4Attribute(int attributeId)
  {
    if (this.id == 0L)
      return (int[]) null;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributableTypeInfo attributableType = ClientCommons.GetAttributableType(this.elementType, this.elementKind);
    if (attributableType == null)
      return (int[]) null;
    IDBAttributeTypeInfo4 attributeById = attributableType.Attributes.GetAttributeByID(attributeId);
    if (attributeById != null)
      return new int[2]
      {
        attributeById.MasterAttributeID,
        attributeById.SourceAttributeID
      };
    IDBAttributeTypeInfo attributeType = service.GetAttributeType(attributeId);
    if (attributeType == null)
      return (int[]) null;
    return new int[2]
    {
      attributeType.MasterAttributeID,
      attributeType.SourceAttributeID
    };
  }

  protected internal bool AssignMasterAttributePrim(int attributeId, object attrValue)
  {
    return this.AssignMasterAttributePrim(attributeId, attrValue, this.actualAttributeValues, true);
  }

  /// <summary>
  /// назначает значение мастер атрибуту attributeId с учетом распихивания атрибутов.
  /// не выполняет каких либо проверок - все проверки должны быть сделаны предварительно.
  /// currentAVList - текущий список значений
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attrValue"></param>
  /// <param name="currentAVList"></param>
  /// <param name="throwChangeEvent"></param>
  /// <returns></returns>
  protected bool AssignMasterAttributePrim(
    int attributeId,
    object attrValue,
    AttributeValuesList currentAVList,
    bool throwChangeEvent)
  {
    return this.AssignMasterAttributePrim(attributeId, attrValue, currentAVList, throwChangeEvent, out AttributeValuesList _);
  }

  public bool AssignMasterAttributePrim(
    int attributeId,
    object attrValue,
    AttributeValuesList currentAVList,
    bool throwChangeEvent,
    out AttributeValuesList deltaList)
  {
    deltaList = (AttributeValuesList) null;
    if (!this.loaded)
      return false;
    List<object[]> objArrayList1 = new List<object[]>();
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    if (attributeType == null || attributeType.AttributeType != FieldTypes.ftObjectLink || attributeType.MultipleValued != MultiValueModes.SingleValue && attributeType.MultipleValued != MultiValueModes.SingleValueFromList)
      return false;
    deltaList = new AttributeValuesList();
    AttributeValues attributeValues1 = AttributeProcessor.FindAttributeValues(attributeId, currentAVList);
    AttributeValues attributeValues2 = attributeValues1 != null ? (AttributeValues) attributeValues1.Clone() : AttributeProcessor.CreateAttributeValues(attributeId, this.id, this.elementKind);
    deltaList.Add(attributeValues2);
    this.SetValue(attributeId, attrValue, deltaList, false);
    AttributeValues byAttributeId = deltaList.FindByAttributeID(attributeId);
    objArrayList1.Add(new object[3]
    {
      (object) attributeId,
      (object) AttributeValuesAction.ModifyValue,
      (object) new object[2]
      {
        (object) -1,
        (object) byAttributeId.Values
      }
    });
    List<SlaveAttributeInfo> forMasterAttribute = this.GetSlaveForMasterAttribute(attributeId);
    if (attrValue != null && attrValue != DBNull.Value)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable attributable = ClientCommons.GetAttributable(Convert.ToInt64(attrValue), AttributableElements.Object, sessionKeeper.Session);
        if (attributable != null)
        {
          for (int index = 0; index < forMasterAttribute.Count; ++index)
          {
            IDBAttributable dbAttributable1 = attributable;
            SlaveAttributeInfo slaveAttributeInfo = forMasterAttribute[index];
            int sourceId1 = slaveAttributeInfo.SourceId;
            object[] valuesById = dbAttributable1.GetValuesByID(sourceId1, false);
            IDBAttributable dbAttributable2 = attributable;
            slaveAttributeInfo = forMasterAttribute[index];
            int sourceId2 = slaveAttributeInfo.SourceId;
            string[] descriptionsById = dbAttributable2.GetDescriptionsByID(sourceId2, false);
            if (valuesById != null)
            {
              slaveAttributeInfo = forMasterAttribute[index];
              AttributeValues attributeValues3 = AttributeProcessor.FindAttributeValues(slaveAttributeInfo.SlaveId, currentAVList);
              AttributeValues attributeValues4;
              if (attributeValues3 == null)
              {
                slaveAttributeInfo = forMasterAttribute[index];
                attributeValues4 = AttributeProcessor.CreateAttributeValues(slaveAttributeInfo.SlaveId, this.id, this.elementKind);
              }
              else
                attributeValues4 = (AttributeValues) attributeValues3.Clone();
              AttributeValues attributeValues5 = attributeValues4;
              slaveAttributeInfo = forMasterAttribute[index];
              Guid attributeGuidById = DBHelper.GetAttributeGuidByID(slaveAttributeInfo.SlaveId);
              attributeValues5.AttributeGuid = attributeGuidById;
              attributeValues4.Values = valuesById;
              attributeValues4.Descriptions = (object[]) descriptionsById;
              List<object[]> objArrayList2 = objArrayList1;
              object[] objArray = new object[3];
              slaveAttributeInfo = forMasterAttribute[index];
              objArray[0] = (object) slaveAttributeInfo.SlaveId;
              objArray[1] = (object) AttributeValuesAction.ModifyValue;
              objArray[2] = (object) new object[2]
              {
                (object) -1,
                (object) valuesById
              };
              objArrayList2.Add(objArray);
              deltaList.Add(attributeValues4);
            }
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < forMasterAttribute.Count; ++index)
      {
        SlaveAttributeInfo slaveAttributeInfo = forMasterAttribute[index];
        AttributeValues attributeValues6 = AttributeProcessor.FindAttributeValues(slaveAttributeInfo.SlaveId, currentAVList);
        AttributeValues attributeValues7;
        if (attributeValues6 == null)
        {
          slaveAttributeInfo = forMasterAttribute[index];
          attributeValues7 = AttributeProcessor.CreateAttributeValues(slaveAttributeInfo.SlaveId, this.id, this.elementKind);
        }
        else
          attributeValues7 = (AttributeValues) attributeValues6.Clone();
        AttributeValues attributeValues8 = attributeValues7;
        slaveAttributeInfo = forMasterAttribute[index];
        Guid attributeGuidById = DBHelper.GetAttributeGuidByID(slaveAttributeInfo.SlaveId);
        attributeValues8.AttributeGuid = attributeGuidById;
        attributeValues7.Values = new object[1]
        {
          (object) DBNull.Value
        };
        attributeValues7.Descriptions = (object[]) new string[1]
        {
          string.Empty
        };
        List<object[]> objArrayList3 = objArrayList1;
        object[] objArray = new object[3];
        slaveAttributeInfo = forMasterAttribute[index];
        objArray[0] = (object) slaveAttributeInfo.SlaveId;
        objArray[1] = (object) AttributeValuesAction.ModifyValue;
        objArray[2] = (object) new object[2]
        {
          (object) -1,
          (object) attributeValues7.Values
        };
        objArrayList3.Add(objArray);
        deltaList.Add(attributeValues7);
      }
    }
    currentAVList.SyncronizeWith(deltaList);
    this.modified = true;
    if (this.AttributeValuesChanged != null & throwChangeEvent)
    {
      for (int index = 0; index < objArrayList1.Count; ++index)
        this.AttributeValuesChanged((object) this, new AttributeValuesChangedEventArgs((int) objArrayList1[index][0], (AttributeValuesAction) objArrayList1[index][1], objArrayList1[index][2]));
    }
    return true;
  }

  protected List<SlaveAttributeInfo> GetSlaveForMasterAttribute(int masterId)
  {
    List<SlaveAttributeInfo> forMasterAttribute = new List<SlaveAttributeInfo>();
    IDBAttributableTypeInfo attributableType = ClientCommons.GetAttributableType(this.elementType, this.elementKind);
    if (attributableType == null)
      return (List<SlaveAttributeInfo>) null;
    if (attributableType.Attributes.GetAttributeByID(masterId) != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) attributableType.Attributes.Select("").Rows)
      {
        if (Convert.ToInt32(row["F_MASTER_ID"]) == masterId)
          forMasterAttribute.Add(new SlaveAttributeInfo(masterId, Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_SOURCE_ID"])));
      }
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(-1);
        if (attributeTypeCollection != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) attributeTypeCollection.Select("").Rows)
          {
            if (Convert.ToInt32(row["F_MASTER_ID"]) == masterId)
              forMasterAttribute.Add(new SlaveAttributeInfo(masterId, Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_SOURCE_ID"])));
          }
        }
      }
    }
    return forMasterAttribute;
  }

  public bool IsMasterAttribute(int masterId)
  {
    IDBAttributableTypeInfo attributableType = ClientCommons.GetAttributableType(this.elementType, this.elementKind);
    if (attributableType == null)
      return false;
    if (attributableType.Attributes.GetAttributeByID(masterId) != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) attributableType.Attributes.Select("").Rows)
      {
        if (Convert.ToInt32(row["F_MASTER_ID"]) == masterId)
          return true;
      }
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(-1);
        if (attributeTypeCollection != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) attributeTypeCollection.Select("").Rows)
          {
            if (Convert.ToInt32(row["F_MASTER_ID"]) == masterId)
              return true;
          }
        }
      }
    }
    return false;
  }
}

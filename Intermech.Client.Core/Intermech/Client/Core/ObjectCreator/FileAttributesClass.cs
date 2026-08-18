
// Type: Intermech.Client.Core.ObjectCreator.FileAttributesClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Класс для работы с файловыми атрибутами.</summary>
public class FileAttributesClass
{
  private CreatedObjectItem _owner;
  public Hashtable fileAttributesHT = new Hashtable();
  protected List<int> asgdFileAttrsIds = new List<int>();

  /// <summary>Содержит ли создаваемый объект файловые атрибуты.</summary>
  public bool Contains => this.fileAttributesHT.Count > 0;

  /// <summary>Конструктор.</summary>
  /// <param name="owner">Ссылка на объект в контексте которого должен работать данный</param>
  public FileAttributesClass(CreatedObjectItem owner) => this._owner = owner;

  /// <summary>
  /// Добавление файловых атрибутов в список, допустимых для создаваемого объекта.
  /// </summary>
  /// <param name="idbCollection">Коллекция атрибутов, которые надо добавить к списку</param>
  private void AddFileAttributes(IDBCollection idbCollection)
  {
    if (idbCollection == null)
      return;
    DataTable dataTable = idbCollection.Select(string.Empty, (object) FieldTypes.ftFile, (object) "ALL_FIELDS");
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      string name = Convert.ToString(row["F_NAME"]);
      MultiValueModes int16 = (MultiValueModes) Convert.ToInt16(row["F_MULTIPLE_VALUED"]);
      if (!this.fileAttributesHT.ContainsKey((object) int32))
        this.fileAttributesHT.Add((object) int32, (object) new FileAttributesClass.FileAttributeClass(int32, name, int16));
    }
  }

  /// <summary>
  /// Добавление идентификатора атрибута к списку инициализированных по прототипу.
  /// </summary>
  /// <param name="fileAttrId">Идентификатор атрибута</param>
  public void AssignedAdd(int fileAttrId) => this.asgdFileAttrsIds.Add(fileAttrId);

  /// <summary>
  /// Удаление идентификатора атрибута из списка инициализированных по прототипу.
  /// </summary>
  /// <param name="fileAttrId">Идентификатор атрибута</param>
  public void AssignedRemove(int fileAttrId) => this.asgdFileAttrsIds.Remove(fileAttrId);

  /// <summary>
  /// Получить список файловых атрибутов, доступных для добавления.
  /// </summary>
  /// <param name="notInclIds">Перечень атрибутов, которые не нужно предлагать для добавления</param>
  /// <returns>Список атрибутов</returns>
  public ArrayList GetPosibleAttrSelObject(params int[] notInclIds)
  {
    ArrayList posibleAttrSelObject = new ArrayList();
    ArrayList arrayList = new ArrayList((ICollection) notInclIds);
    foreach (DictionaryEntry dictionaryEntry in this.fileAttributesHT)
    {
      FileAttributesClass.FileAttributeClass fileAttributeClass = (FileAttributesClass.FileAttributeClass) dictionaryEntry.Value;
      if (arrayList.IndexOf((object) fileAttributeClass.ID) == -1)
        posibleAttrSelObject.Add((object) new AttrSelObject(fileAttributeClass.ID, FieldTypes.ftFile, fileAttributeClass.Name));
    }
    return posibleAttrSelObject;
  }

  /// <summary>
  /// Получение файловых атрибутов, назначенных для типа создаваемого объекта.
  /// </summary>
  /// <param name="session">Пользовательская сессия, в контексте которой производится работа</param>
  public void Initialize(IUserSession session)
  {
    IDBObject dbObject1 = session.GetObject(this._owner.ObjectID);
    IDBObjectType objectType = session.GetObjectType(dbObject1.ObjectType);
    this.AddFileAttributes((IDBCollection) objectType.Attributes);
    if (this.fileAttributesHT.Count > 0 && objectType.AnyAttributes)
      this.AddFileAttributes((IDBCollection) session.GetAttributesGroup(-1).Attributes);
    if (this._owner.PrototypeID == -1L)
      return;
    IDBObject dbObject2 = session.GetObject(this._owner.PrototypeID);
    if (dbObject2.ObjectType != dbObject1.ObjectType)
      return;
    foreach (IDBAttribute dbAttribute in dbObject2.Attributes.GetAttributesByType(FieldTypes.ftFile))
    {
      if (!this.IsAssigned(dbAttribute.AttributeID))
        this.AssignedAdd(dbAttribute.AttributeID);
    }
  }

  /// <summary>
  /// Проверка - есть ли идентификатор атрибута в списке инициализированных по прототипу.
  /// </summary>
  /// <param name="fileAttrId">Идентификатор атрибута</param>
  public bool IsAssigned(int fileAttrId) => this.asgdFileAttrsIds.IndexOf(fileAttrId) > -1;

  public void Unassign() => this.asgdFileAttrsIds.Clear();

  /// <summary>
  /// Проверка наличия файловых атрибутов, для которых не была проведена инициализация по прототипу.
  /// </summary>
  /// <returns></returns>
  public bool IsExistsUnassigned()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IDBAttribute dbAttribute in sessionKeeper.Session.GetObject(this._owner.ObjectID).Attributes.GetAttributesByType(FieldTypes.ftFile))
      {
        if (this.asgdFileAttrsIds.IndexOf(dbAttribute.AttributeID) == -1)
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Вспомогательная функция для проверки - может ли указанный атрибут содержать несколько значений.
  /// </summary>
  /// <param name="attrId">Идентификатор атрибута</param>
  /// <returns>Результат - может ли атрибут содержать несколько значений</returns>
  public bool IsMultiValue(int attrId)
  {
    return this.fileAttributesHT.ContainsKey((object) attrId) && ((FileAttributesClass.FileAttributeClass) this.fileAttributesHT[(object) attrId]).Mode != 0;
  }

  /// <summary>Локальный класс для представления файлового атрибута.</summary>
  private class FileAttributeClass
  {
    public int ID;
    public string Name;
    public MultiValueModes Mode;

    /// <summary>Конструктор.</summary>
    /// <param name="id">Идентификатор атрибута</param>
    /// <param name="name">Наименование атрибута</param>
    /// <param name="mode">Режим работы со списком значений атрибута</param>
    public FileAttributeClass(int id, string name, MultiValueModes mode)
    {
      this.ID = id;
      this.Name = name;
      this.Mode = mode;
    }
  }
}

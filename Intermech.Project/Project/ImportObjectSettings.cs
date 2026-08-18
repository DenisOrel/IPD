// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ImportObjectSettings
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Project;

/// <summary>Настройки импорта объектов в новом окне импорта</summary>
[Serializable]
public class ImportObjectSettings : ImportObjectSettingsBase, ICloneable
{
  /// <summary>Импортировать как подзадачи</summary>
  private bool _importAsSubTasks;
  /// <summary>Импортировать ли корневой элемент</summary>
  private bool _importRootObjects = true;
  /// <summary>Ограничить ли глубину импорта</summary>
  private bool _limitMaxLevels = true;
  /// <summary>Число импортируемых уровней структуры (если глубина импорта ограничена)</summary>
  private int _limitMaxLevelsCount = 3;
  /// <summary>Создавать вложенные копии суммарных задач</summary>
  private bool _copySummaries;
  /// <summary>Создавать задачи на одном уровне, игнорируя иерархию</summary>
  private bool _linearImport;
  /// <summary>Создавать ли итерации импортируемых объектов</summary>
  private bool _createIteration;
  /// <summary>Наименование итерации</summary>
  [NotNull]
  [NonSerialized]
  private string _iterationName = string.Empty;
  /// <summary>Словарь "тип объекта =&gt; настройки для него"</summary>
  [NotNull]
  private Dictionary<int, ImportObjectSettingsForObjType> _settingsForObjTypesDictionary;
  [CanBeNull]
  [NonSerialized]
  private SettingsForObjTypeLink _settingsForObjTypeLink;
  private long _finalScriptID;

  /// <summary>Событие вызывается после изменения значения </summary>
  public event ImportObjectSettingsBase.AfterValueChangedDelegate<bool> AfterImportAsSubTasksChanged;

  /// <summary>Импортировать как подзадачи</summary>
  public override bool ImportAsSubTasks
  {
    [DebuggerStepThrough] get => this._importAsSubTasks;
    set
    {
      if (value == this._importAsSubTasks)
        return;
      bool importAsSubTasks = this._importAsSubTasks;
      this._importAsSubTasks = value;
      if (this.AfterImportAsSubTasksChanged == null)
        return;
      this.AfterImportAsSubTasksChanged(importAsSubTasks);
    }
  }

  /// <summary>Событие вызывается после изменения значения </summary>
  public event ImportObjectSettingsBase.AfterValueChangedDelegate<bool> AfterImportRootObjectsChanged;

  /// <summary>Импортировать ли корневой элемент</summary>
  public override bool ImportRootObjects
  {
    [DebuggerStepThrough] get => this._importRootObjects;
    set
    {
      bool importRootObjects = this._importRootObjects;
      this._importRootObjects = value;
      ImportObjectSettingsBase.AfterValueChangedDelegate<bool> rootObjectsChanged = this.AfterImportRootObjectsChanged;
      if (rootObjectsChanged == null)
        return;
      rootObjectsChanged(importRootObjects);
    }
  }

  /// <summary>Событие вызывается после изменения значения </summary>
  public event Action AfterLimitMaxLevelsChanged;

  /// <summary>Ограничить ли глубину импорта</summary>
  public override bool LimitMaxLevels
  {
    [DebuggerStepThrough] get => this._limitMaxLevels;
    set
    {
      if (this._limitMaxLevels == value)
        return;
      this._limitMaxLevels = value;
      Action maxLevelsChanged = this.AfterLimitMaxLevelsChanged;
      if (maxLevelsChanged == null)
        return;
      maxLevelsChanged();
    }
  }

  /// <summary>Число импортируемых уровней структуры (если глубина импорта ограничена)</summary>
  public override int LimitMaxLevelsCount
  {
    [DebuggerStepThrough] get => this._limitMaxLevelsCount;
    set
    {
      if (this._limitMaxLevelsCount == value)
        return;
      this._limitMaxLevelsCount = value;
      Action maxLevelsChanged = this.AfterLimitMaxLevelsChanged;
      if (maxLevelsChanged == null)
        return;
      maxLevelsChanged();
    }
  }

  /// <summary>Создавать вложенные копии суммарных задач</summary>
  public override bool CopySummaries
  {
    [DebuggerStepThrough] get => this._copySummaries;
    set => this._copySummaries = value;
  }

  /// <summary>Создавать задачи на одном уровне, игнорируя иерархию</summary>
  public override bool LinearImport
  {
    [DebuggerStepThrough] get => this._linearImport;
    set => this._linearImport = value;
  }

  /// <summary>Создавать ли итерации импортируемых объектов</summary>
  public override bool CreateIteration
  {
    [DebuggerStepThrough] get => this._createIteration;
    set => this._createIteration = value;
  }

  /// <summary>Наименование итерации</summary>
  public override string IterationName
  {
    [DebuggerStepThrough] get => this._iterationName;
    set => this._iterationName = value;
  }

  /// <summary>Настройки по-умолчанию</summary>
  [NotNull]
  public override ImportObjectSettings DefaultSettings
  {
    [DebuggerStepThrough] get => this;
  }

  /// <summary>Перечисление типов объектов, которым назначены специальные настройки</summary>
  [NotNull]
  public override IReadOnlyCollection<int> ObjTypesWithSpecialTypes
  {
    [DebuggerStepThrough] get
    {
      return (IReadOnlyCollection<int>) this._settingsForObjTypesDictionary.Keys;
    }
  }

  /// <summary>Интерфейс "тип объекта =&gt; настройки для него"</summary>
  [NotNull]
  public override SettingsForObjTypeLink SettingsForObjType
  {
    [DebuggerStepThrough] get
    {
      this._settingsForObjTypeLink = this._settingsForObjTypeLink ?? new SettingsForObjTypeLink(this, this._settingsForObjTypesDictionary);
      return this._settingsForObjTypeLink;
    }
  }

  /// <summary>Скрипт пост-обработки импортированных задач</summary>
  public override long FinalScriptID
  {
    [DebuggerStepThrough] get => this._finalScriptID;
    set
    {
      long finalScriptId = this._finalScriptID;
      this._finalScriptID = value;
      Action<long> finalScriptChanged = this.AfterFinalScriptChanged;
      if (finalScriptChanged == null)
        return;
      finalScriptChanged(finalScriptId);
    }
  }

  public event Action<long> AfterFinalScriptChanged;

  public ImportObjectSettings(
    bool importAsSubTasks = false,
    bool importRootObjects = true,
    bool limitMaxLevels = false,
    int limitMaxLevelsCount = 3,
    bool copySummaries = false,
    bool linearImport = false,
    long initTaskScriptID = 0,
    long prototypeObjectVersionID = 0,
    bool createIteration = true,
    [CanBeNull] string iterationName = null,
    [CanBeNull] Task initTaskSettings = null)
    : base(initTaskScriptID, prototypeObjectVersionID, initTaskSettings)
  {
    this.ImportAsSubTasks = importAsSubTasks;
    this.ImportRootObjects = importRootObjects;
    this.LimitMaxLevels = limitMaxLevels;
    this.LimitMaxLevelsCount = limitMaxLevelsCount;
    this.CopySummaries = copySummaries;
    this._linearImport = linearImport;
    this.CreateIteration = createIteration;
    this.IterationName = iterationName ?? "Импорт в IMProject";
    this._settingsForObjTypesDictionary = new Dictionary<int, ImportObjectSettingsForObjType>();
  }

  public ImportObjectSettings([NotNull] ImportObjectSettingsBase prototype)
    : base(prototype)
  {
    this.ImportAsSubTasks = prototype.ImportAsSubTasks;
    this.ImportRootObjects = prototype.ImportRootObjects;
    this.LimitMaxLevels = prototype.LimitMaxLevels;
    this.LimitMaxLevelsCount = prototype.LimitMaxLevelsCount;
    this.CopySummaries = prototype.CopySummaries;
    this._linearImport = prototype.LinearImport;
    this.CreateIteration = prototype.CreateIteration;
    this.IterationName = prototype.IterationName;
    this._settingsForObjTypesDictionary = new Dictionary<int, ImportObjectSettingsForObjType>();
  }

  public override void LoadFormDictionary(Dictionary<string, object> dic)
  {
    base.LoadFormDictionary(dic);
    object obj;
    if (dic.TryGetValue("ImportAsSubTasks", out obj))
      this.ImportAsSubTasks = (bool) obj;
    if (dic.TryGetValue("ImportRootObjects", out obj))
      this.ImportRootObjects = (bool) obj;
    if (dic.TryGetValue("LimitMaxLevels", out obj))
      this._limitMaxLevels = (bool) obj;
    if (dic.TryGetValue("LimitMaxLevelsCount", out obj))
      this._limitMaxLevelsCount = (int) obj;
    Action maxLevelsChanged = this.AfterLimitMaxLevelsChanged;
    if (maxLevelsChanged != null)
      maxLevelsChanged();
    if (dic.TryGetValue("CopySummaries", out obj))
      this.CopySummaries = (bool) obj;
    if (dic.TryGetValue("LinearImport", out obj))
      this._linearImport = (bool) obj;
    if (dic.TryGetValue("CreateIteration", out obj))
      this.CreateIteration = (bool) obj;
    if (dic.TryGetValue("IterationName", out obj))
      this.IterationName = (string) obj;
    if (dic.TryGetValue("IterationName", out obj))
      this.IterationName = (string) obj;
    if (!dic.TryGetValue("SettingsForObjTypesDictionary", out obj) || obj == null)
      return;
    this._settingsForObjTypesDictionary = (Dictionary<int, ImportObjectSettingsForObjType>) obj;
  }

  public override void SaveToDictionary(Dictionary<string, object> dic, bool withLinks = true)
  {
    base.SaveToDictionary(dic, withLinks);
    dic["ImportAsSubTasks"] = (object) this.ImportAsSubTasks;
    dic["ImportRootObjects"] = (object) this.ImportRootObjects;
    dic["LimitMaxLevels"] = (object) this.LimitMaxLevels;
    dic["LimitMaxLevelsCount"] = (object) this.LimitMaxLevelsCount;
    dic["CopySummaries"] = (object) this.CopySummaries;
    dic["LinearImport"] = (object) this.LinearImport;
    dic["CreateIteration"] = (object) this.CreateIteration;
    dic["IterationName"] = (object) this.IterationName;
    dic["SettingsForObjTypesDictionary"] = (object) this._settingsForObjTypesDictionary;
  }

  /// <summary>Вызывается после десериализации. Позволяет восстановить несериализуемые поля</summary>
  [OnDeserialized]
  protected void AfterLoad(StreamingContext context) => this.AfterLoad();

  /// <summary>Вызывается после десериализации. Позволяет восстановить несериализуемые поля</summary>
  protected void AfterLoad()
  {
    foreach (ImportObjectSettingsForObjType settingsForObjType in this._settingsForObjTypesDictionary.Values)
      settingsForObjType.LinkToDefaultSettings(this);
  }

  public object Clone() => (object) new ImportObjectSettings((ImportObjectSettingsBase) this);

  /// <summary>Сохранить в связь</summary>
  public void SaveToDbRelation([NotNull] IDBRelation relation)
  {
    this.PrototypeObjectVersionID = relation.UpdateLinkToObjectWithCheck((int) (IpsMetadataEntityBase<int>) Attributes.Prototype, this.PrototypeObjectVersionID);
    this.InitTaskScriptID = relation.UpdateLinkToObjectWithCheck((int) (IpsMetadataEntityBase<int>) Attributes.InitScript, this.InitTaskScriptID);
    IDBAttribute dbAttribute = relation.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data, false);
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    this.SaveToDictionary(dictionary, false);
    using (MemoryStream serializationStream = new MemoryStream())
    {
      try
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) dictionary);
        long length = serializationStream.Length;
        IBlobWriter blobWriter = Intermech.Diagnostics.Check.Is<IBlobWriter>((object) dbAttribute);
        BlobInformation blobInfo = new BlobInformation(length, length, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty);
        if (!blobWriter.OpenBlob(blobInfo, false))
          return;
        blobWriter.WriteDataBlock(serializationStream.ToArray());
      }
      finally
      {
        serializationStream.Close();
      }
    }
  }

  /// <summary>Создать из связи</summary>
  [ContractAnnotation("failNotFound:false => CanBeNull; => NotNull")]
  public static ImportObjectSettings CreateFromDB(
    [NotNull] IUserSession session,
    [NotEmpty] long relationID,
    bool failNotFound = true)
  {
    IDBRelation relation = session.GetRelation(relationID, failNotFound);
    return relation != null ? ImportObjectSettings.CreateFromRelation(relation) : (ImportObjectSettings) null;
  }

  /// <summary>Создать из связи</summary>
  [NotNull]
  public static ImportObjectSettings CreateFromRelation([NotNull] IDBRelation iDbRelation)
  {
    ImportObjectSettings fromRelation = new ImportObjectSettings();
    fromRelation.LoadFromRelation(iDbRelation);
    return fromRelation;
  }

  /// <summary>Загрузить все свойства из связи в БД</summary>
  private void LoadFromRelation([NotNull] IDBRelation iDbRelation)
  {
    IBlobReader blobReader = Intermech.Diagnostics.Check.Is<IBlobReader>((object) iDbRelation.Attributes.FindByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data));
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    byte[] buffer = blobReader.ReadDataBlock((int) blobInformation.RealFileSize);
    MemoryStream memoryStream = new MemoryStream(buffer);
    try
    {
      memoryStream.Seek(0L, SeekOrigin.Begin);
      memoryStream.Write(buffer, 0, buffer.Length);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
      {
        MemoryStream outStream = new MemoryStream();
        ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = outStream;
        memoryStream.Seek(0L, SeekOrigin.Begin);
      }
      this.LoadFormDictionary((Dictionary<string, object>) new BinaryFormatter().Deserialize((Stream) memoryStream));
    }
    finally
    {
      memoryStream.Close();
      memoryStream.Dispose();
      blobReader.CloseBlob();
    }
    IDBAttribute byId1 = iDbRelation.Attributes.FindByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data);
    this.PrototypeObjectVersionID = byId1 == null || byId1.IsNull ? 0L : byId1.AsInteger;
    IDBAttribute byId2 = iDbRelation.Attributes.FindByID((int) (IpsMetadataEntityBase<int>) Attributes.InitScript);
    this.InitTaskScriptID = byId2 == null || byId2.IsNull ? 0L : byId2.AsInteger;
    this.AfterLoad();
  }

  /// <summary>Вызывается после того, как пользователь добавляет новый тип объекта в список типов объектов, у которых специальные настройки импорта</summary>
  public void AddNewSpecialSettings([NotEmpty] int objTypeID)
  {
    ImportObjectSettingsBase prototype = this.SettingsForObjType[objTypeID];
    ImportObjectSettingsForObjType settingsForObjType = new ImportObjectSettingsForObjType(this, objTypeID, prototype);
    this._settingsForObjTypesDictionary[objTypeID] = settingsForObjType;
    this.SettingsForObjType.ClearCache();
  }

  /// <summary>Вызывается после того, как пользователь добавляет новый тип объекта в список типов объектов, у которых специальные настройки импорта</summary>
  public void DeleteSpecialSettings([NotEmpty] int objTypeID)
  {
    this._settingsForObjTypesDictionary.Remove(objTypeID);
    this.SettingsForObjType.ClearCache();
  }
}

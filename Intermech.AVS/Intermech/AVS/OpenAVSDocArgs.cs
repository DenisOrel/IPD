// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OpenAVSDocArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс для передачи аргументов и контекста при открытии документов AVS</summary>
public class OpenAVSDocArgs
{
  /// <summary>Возвращаемое открытое окно документа</summary>
  public AVSWindow AvsWindow;
  /// <summary>Показать окно</summary>
  public bool Show;
  /// <summary>Идентификатор сборки или спецификации</summary>
  public long ObjectId;
  /// <summary>Идентификатор сборки или спецификации</summary>
  public Guid ObjectGuid;
  /// <summary>Тип объекта</summary>
  public int ObjectType;
  /// <summary>Режим только для чтения</summary>
  public bool ReadOnly;
  /// <summary>Список ранее сохранённых параметров для восстановления окна</summary>
  public HybridDictionary RestoreParams;
  /// <summary>Создавать данные для восстановления</summary>
  public bool? CreateUndo;
  /// <summary>Необходимо обновить документ перед открытием</summary>
  public bool NeedUpdate;
  /// <summary>Сохранить файл документа после обновления при открытии</summary>
  public bool SaveIfUpdatedForLoad;
  /// <summary>Массив внешних команд, которые можно вызвать из окна</summary>
  public ExternalAVSCommand[] ExternalCommands;
  public bool ForceReload;
  public string ErrorMessage;

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Идентификатор сборки или спецификации</param>
  /// <param name="readOnly">Режим только для чтения</param>
  public OpenAVSDocArgs(long objectId, bool readOnly)
    : this(objectId, -1, true, readOnly, (HybridDictionary) null, new bool?(), true, (ExternalAVSCommand[]) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objId">Идентификатор сборки или спецификации</param>
  /// <param name="objType">Тип объекта</param>
  /// <param name="show">Показать окно</param>
  /// <param name="readOnly">Режим только для чтения</param>
  /// <param name="restoreParams">Список ранее сохранённых параметров для восстановления окна</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  /// <param name="saveIfUpdatedForLoad">Сохранить файл документа после обновления при открытии</param>
  /// <param name="externalCommands">Массив внешних команд, которые можно вызвать из окна</param>
  public OpenAVSDocArgs(
    long objId,
    int objType = -1,
    bool show = true,
    bool readOnly = false,
    HybridDictionary restoreParams = null,
    bool? createUndo = null,
    bool saveIfUpdatedForLoad = true,
    ExternalAVSCommand[] externalCommands = null)
  {
    if (Consts.IsUndefinedObjectId((long) objType) && !Consts.IsUndefinedObjectId(objId))
      objType = DBHelper.GetObjTypeID(objId);
    this.ObjectId = objId;
    this.ObjectType = objType;
    this.Show = show;
    this.ReadOnly = readOnly;
    this.RestoreParams = restoreParams;
    this.CreateUndo = createUndo;
    this.SaveIfUpdatedForLoad = saveIfUpdatedForLoad;
    this.ExternalCommands = externalCommands;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.LaunchParams
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using System;

#nullable disable
namespace Intermech.Tools.LaunchActions;

[Serializable]
public class LaunchParams
{
  private readonly long originalObjectId;
  private readonly int originalObjectTypeId;
  private long objectId;
  private int objectTypeId;
  private string objectFileName;
  private bool needCheckout;
  private readonly VersionsRulePackage versionsRule;
  private readonly LaunchType launchType;
  private readonly PropertyContainer launchContext;
  private IFileArea fileArea;
  private string resultFilePath;

  public LaunchParams(
    LaunchType launchType,
    long objectId,
    int objectTypeId,
    VersionsRulePackage versionsRule)
    : this(launchType, objectId, objectTypeId, versionsRule, launchType == LaunchType.Edit)
  {
  }

  public LaunchParams(
    LaunchType launchType,
    long objectId,
    int objectTypeId,
    VersionsRulePackage versionsRule,
    bool needCheckout)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (objectTypeId));
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    this.originalObjectId = objectId;
    this.originalObjectTypeId = objectTypeId;
    this.objectId = objectId;
    this.objectTypeId = objectTypeId;
    this.versionsRule = versionsRule;
    this.launchType = launchType;
    this.needCheckout = needCheckout;
    this.launchContext = new PropertyContainer();
  }

  public long OriginalObjectId => this.originalObjectId;

  public int OriginalObjectTypeId => this.originalObjectTypeId;

  public long ObjectId => this.objectId;

  public int ObjectTypeId => this.objectTypeId;

  /// <summary>
  /// Возвращает или задает имя файла объекта, который должен быть передан приложению. Если это свойство
  /// не задано или пусто, то обработчик сам выберет файл объекта. По умолчанию всегда используется мастер файл.
  /// </summary>
  public string ObjectFileName
  {
    get => this.objectFileName;
    set => this.objectFileName = value;
  }

  public bool NeedCheckout
  {
    get => this.needCheckout;
    set => this.needCheckout = value;
  }

  public VersionsRulePackage VersionsRule => this.versionsRule;

  public LaunchType LaunchType => this.launchType;

  public IFileArea FileArea
  {
    get => this.fileArea;
    set => this.fileArea = value;
  }

  public string ResultFilePath
  {
    get => this.resultFilePath;
    set => this.resultFilePath = value;
  }

  /// <summary>
  /// Возвращает контекст, в котором была вызвана команда запуска приложения и открытия в нем файла объекта. Контекст применяется
  /// в специализированных вариантах команды "открыть" и используется для дополнительного конфигурирования приложения, а также
  /// для позиционирования в открытом файле объекта. По умолчанию контекст пуст.
  /// </summary>
  public PropertyContainer LaunchContext => this.launchContext;

  public void ChangeObject(long objectId, int objectTypeId)
  {
    if (Consts.IsUndefinedObjectId(objectId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (objectTypeId));
    this.objectId = objectId;
    this.objectTypeId = objectTypeId;
  }
}

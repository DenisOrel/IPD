// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.AfterObjectCommandArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Аргументы для события, рассылаемого после применения команды к объекту IPS.
/// </summary>
public class AfterObjectCommandArgs : EventArgs
{
  private long objectId;
  private long oldObjectId;

  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор версии объекта IPS после применения команды. Значение может быть не задано, если объект IPS был удален</param>
  /// <param name="oldObjectId">Идентификатор версии объекта IPS до применения команды. Значение должно быть задано</param>
  public AfterObjectCommandArgs(long objectId, long oldObjectId)
  {
    this.objectId = objectId;
    this.oldObjectId = oldObjectId;
  }

  /// <summary>
  /// Возвращает идентификатор версии объекта IPS после применения команды.
  /// Значение может быть не задано, если объект IPS был удален.
  /// </summary>
  public long ObjectId
  {
    [DebuggerStepThrough] get => this.objectId;
  }

  /// <summary>
  /// Возвращает идентификатор версии объекта IPS до применения команды.
  /// </summary>
  public long OldObjectId
  {
    [DebuggerStepThrough] get => this.oldObjectId;
  }

  /// <summary>Возвращает признак, что объект IPS был удален.</summary>
  public bool IsObjectRemoved
  {
    [DebuggerStepThrough] get => Consts.IsUndefinedObjectId(this.objectId);
  }

  /// <summary>
  /// Возвращает признак, что рабочая копия объекта была заменена архивной (или наоборот).
  /// </summary>
  public bool IsObjectCopyReplaced
  {
    [DebuggerStepThrough] get
    {
      return !Consts.IsUndefinedObjectId(this.objectId) && this.objectId != this.oldObjectId;
    }
  }
}

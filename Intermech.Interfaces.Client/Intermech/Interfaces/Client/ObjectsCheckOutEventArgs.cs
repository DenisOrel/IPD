// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectsCheckOutEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Аргументы события "ObjectsCheckOutEventHandler"</summary>
[Serializable]
public sealed class ObjectsCheckOutEventArgs : EventArgs
{
  /// <summary>
  /// Список исходных версий объектов, которые требуется взять на редактирование
  /// </summary>
  private List<ObjectCheckOutVersionDescription> _sourceVersions;
  /// <summary>Список версий объектов для редактирования</summary>
  private List<ObjectCheckOutVersionDescription> _resultVersions;
  /// <summary>Выполнена ли обработка события</summary>
  public bool Handled;
  /// <summary>
  /// Если значение флага установить в true, служба попытается отменить все изменения
  /// </summary>
  public bool Rollback;
  /// <summary>
  /// Пользователь хочет отменить создание версии. При этом НЕ НАДО выдавать исключение
  /// </summary>
  public bool Cancel;

  /// <summary>
  /// Список исходных версий объектов, которые требуется взять на редактирование
  /// </summary>
  public List<ObjectCheckOutVersionDescription> SourceVersions
  {
    [DebuggerStepThrough] get => this._sourceVersions;
  }

  /// <summary>Список версий объектов для редактирования</summary>
  public List<ObjectCheckOutVersionDescription> ResultVersions
  {
    [DebuggerStepThrough] get => this._resultVersions;
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ObjectsCheckOutEventArgs()
  {
  }

  /// <summary>
  /// Создать заполненные аргументы события "ObjectsCheckOutEventHandler"
  /// </summary>
  /// <param name="sourceVersions">Список исходных версий объектов, которые требуется взять на редактирование</param>
  /// <param name="resultVersions">Список версий объектов для редактирования</param>
  public ObjectsCheckOutEventArgs(
    List<ObjectCheckOutVersionDescription> sourceVersions,
    List<ObjectCheckOutVersionDescription> resultVersions)
  {
    if (sourceVersions == null || sourceVersions.Count == 0)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Client_147"));
    if (resultVersions == null || resultVersions.Count == 0)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Client_148"));
    if (sourceVersions.Count != resultVersions.Count)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Client_149"));
    this._sourceVersions = sourceVersions;
    this._resultVersions = resultVersions;
    this.Handled = false;
  }
}

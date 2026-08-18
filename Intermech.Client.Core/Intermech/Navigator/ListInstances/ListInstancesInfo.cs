
// Type: Intermech.Navigator.ListInstances.ListInstancesInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Pdm;
using System;


namespace Intermech.Navigator.ListInstances;

/// <summary>Информация для построения списка исполнений</summary>
[Serializable]
public class ListInstancesInfo : IListInstancesInfo
{
  private Guid _numGroupInstance;
  private Guid _initInstanceGUID;

  /// <summary>Значение атрибута "Идентификатор группового изделия"</summary>
  public Guid NumGroupInstance
  {
    get => this._numGroupInstance;
    set => this._numGroupInstance = value;
  }

  /// <summary>
  /// GUID исполнения с которого захотели открыть список исполнений
  /// </summary>
  public Guid InitInstanceGUID => this._initInstanceGUID;

  public ListInstancesInfo(Guid numGroupInstance, Guid instanceGUID)
  {
    this._numGroupInstance = numGroupInstance;
    this._initInstanceGUID = instanceGUID;
  }
}

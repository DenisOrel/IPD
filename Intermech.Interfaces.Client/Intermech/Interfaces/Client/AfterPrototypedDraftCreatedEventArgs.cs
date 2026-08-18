// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AfterPrototypedDraftCreatedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события, возникающего после создания заготовки нового объекта по прототипу
/// </summary>
public sealed class AfterPrototypedDraftCreatedEventArgs : AfterDraftCreatedEventArgs
{
  public long PrototypeID { get; } = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectTypeID">Тип объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  public AfterPrototypedDraftCreatedEventArgs(int objectTypeID, long objectID, long prototypeID)
    : base(objectTypeID, objectID)
  {
    this.PrototypeID = prototypeID;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectCreatedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события, возникающего с создаваемым объектом
/// </summary>
public class ObjectCreatedEventArgs
{
  /// <summary>Идентификатор типа создаваемого объекта</summary>
  public int ObjectTypeID { get; private set; }

  /// <summary>Идентификатор версии созданной заготовки.</summary>
  public long ObjectID { get; set; }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectTypeID">Тип объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  public ObjectCreatedEventArgs(int objectTypeID, long objectID)
  {
    this.ObjectTypeID = objectTypeID;
    this.ObjectID = objectID;
  }
}

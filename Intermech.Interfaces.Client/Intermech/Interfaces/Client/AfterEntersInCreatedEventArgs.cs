// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AfterEntersInCreatedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события, возникающего при успешном включении в какой-либо состав создаваемого объекта
/// </summary>
public sealed class AfterEntersInCreatedEventArgs
{
  /// <summary>Тип создаваемого объекта</summary>
  public int ObjectType { get; private set; }

  /// <summary>Идентификатор создаваемого объекта</summary>
  public long ObjectID { get; private set; }

  /// <summary>Тип связи</summary>
  public int RelationType { get; private set; }

  /// <summary>Идентификатор связи</summary>
  public long PrjLinkID { get; private set; }

  /// <summary>
  /// Идентификатор объекта в состав которого включили создаваемый объект
  /// </summary>
  public long ProjectID { get; private set; }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectType">Тип дочернего объекта</param>
  /// <param name="objectID">Идентификатор дочернего объекта</param>
  /// <param name="projectID">Идентификатор родительского объекта</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  public AfterEntersInCreatedEventArgs(
    int objectType,
    long objectID,
    long projectID,
    int relationType,
    long prjLinkID)
  {
    this.ObjectID = objectID;
    this.ObjectType = objectType;
    this.PrjLinkID = prjLinkID;
    this.RelationType = relationType;
    this.ProjectID = projectID;
  }
}

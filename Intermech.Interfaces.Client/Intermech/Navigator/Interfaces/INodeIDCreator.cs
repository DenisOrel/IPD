// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeIDCreator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, позволяющий создавать частично заполненные описания узлов (INodeID) по идентификаторам связей
/// </summary>
public interface INodeIDCreator
{
  /// <summary>
  /// Создать частично заполненное описание узла по идентификатору связи
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>Частично заполненное описание узла</returns>
  INodeID Create(long prjLinkID);
}

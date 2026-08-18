// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodesFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс сервиса для регистрации расширений навигатора, а также для
/// создания зарегистрированных объектов-расширений.
/// </summary>
public interface INodesFactory
{
  /// <summary>
  /// Возвращает элемент из пространства навигации указанной категории и типа.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  INode GetNode(int categoryID, int typeID);

  /// <summary>
  /// Возвращает элемент из пространства навигации указанной категории и типа.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор элемента.</param>
  /// <param name="args">Массив параметров, которые будут переданы конструктору элемента.</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  INode GetNode(INodeID nodeID, params object[] args);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ICategoryInheritance
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс для получения идентификаторов родительских типов для указанного
/// типа в рамках одной категории. Этот интерфейс используется сервисом
/// IFactory для поиска различных провайдеров для элемента из пространства
/// навигации.
/// </summary>
public interface ICategoryInheritance
{
  /// <summary>
  /// Возвраращает массив идентификаторов родительских типов для указанного
  /// типа.
  /// </summary>
  /// <param name="typeID">Идентификатор типа элемента навигации</param>
  /// <returns>Массив идентификаторов родительских типов</returns>
  int[] GetParentTypes(int typeID);
}

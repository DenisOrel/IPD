// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IUpdatePlan
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс плана по обновлению элементов пространства навигации
/// </summary>
public interface IUpdatePlan
{
  /// <summary>
  /// Требуется добавить дочерний элемент пространства навигации
  /// </summary>
  /// <param name="partialNodeID">Частично заполненный идентификатор элемента пространства навигации</param>
  void Append(INodeID partialNodeID);

  /// <summary>Требуется обновить информацию о текущем элементе</summary>
  void Update();

  /// <summary>Заменить текущий элемент указанным</summary>
  /// <param name="replacementNodeID">Идентификатор элемента пространства навигации</param>
  void Replace(INodeID replacementNodeID);

  /// <summary>Удалить текущий элемент</summary>
  void Remove();
}

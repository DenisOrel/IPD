// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.INodeSelectorFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Фильтр используется для проверки, можно ли выбирать указанный узел в окне
/// </summary>
public interface INodeSelectorFilter
{
  /// <summary>Можно ли выбирать указанный узел</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <param name="errorMessage">Если значение не равно String.Empty, то оно будет отображено в статусной строке окна</param>
  /// <returns>true, если выбор узла разрешён</returns>
  bool CanSelectNode(int category, object id, out string errorMessage);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsNodeSelectorFilter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Фильтр используется для проверки, можно ли выбирать указанный узел в окне
/// </summary>
public class AvsNodeSelectorFilter : INodeSelectorFilter
{
  public string ErrorMessageText { get; }

  public AvsNodeSelectorFilter()
  {
  }

  public AvsNodeSelectorFilter(string errMessageText) => this.ErrorMessageText = errMessageText;

  /// <summary>Можно ли выбирать указанный узел</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <param name="errorMessage">Если значение не равно String.Empty, то оно будет отображено в статусной строке окна</param>
  /// <returns>true, если выбор узла разрешён</returns>
  public bool CanSelectNode(int category, object id, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (category != 4)
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(id != null ? (int) id : -1);
    if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
      return true;
    errorMessage = this.ErrorMessageText ?? "Абстрактный тип объекта выбирать нельзя";
    return false;
  }
}

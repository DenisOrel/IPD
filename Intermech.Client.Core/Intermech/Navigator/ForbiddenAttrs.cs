
// Type: Intermech.Navigator.ForbiddenAttrs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>Фильтрует многозначные атрибуты</summary>
internal class ForbiddenAttrs : ISelectorFilter
{
  internal List<int> _attrsIDs;

  /// <summary>Конструктор.</summary>
  /// <param name="attrsIDs">Список идентификаторов запрещенных атрибутов</param>
  internal ForbiddenAttrs(List<int> attrsIDs) => this._attrsIDs = attrsIDs;

  /// <summary>Попадание в фильтр.</summary>
  /// <param name="category">Катугория</param>
  /// <param name="id">Идентификатор</param>
  /// <returns></returns>
  public bool IsInFilter(int category, object id) => this._attrsIDs.Contains(Convert.ToInt32(id));
}

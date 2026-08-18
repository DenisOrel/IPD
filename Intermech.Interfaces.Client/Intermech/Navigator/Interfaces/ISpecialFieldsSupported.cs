// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISpecialFieldsSupported
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

public interface ISpecialFieldsSupported
{
  /// <summary>
  /// Возвращает список идентификаторов полей источника данных, значения
  /// которых обязательно должны быть получены в результате выполнения
  /// запроса.
  /// </summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  List<object> GetSpecialFields();
}

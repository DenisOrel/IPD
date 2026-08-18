// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionEditorAttribute
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс на обработчик спец. атрибута</summary>
public interface IConditionEditorAttribute
{
  /// <summary>Тип данных для параметров в узлах условий выборки</summary>
  SelectionParameterTypes NodeValueType { get; }

  /// <summary>Допустимые операторы отношений</summary>
  RelationalOperators[] Operators { get; }
}

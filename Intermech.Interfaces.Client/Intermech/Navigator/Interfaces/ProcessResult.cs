// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ProcessResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Код реагрирования узла на событие</summary>
public enum ProcessResult
{
  /// <summary>Никаких действий не предпринимать</summary>
  None,
  /// <summary>
  /// Узел должен перечитать только своё содержимое, список дочерних узлов остаётся неизменным
  /// </summary>
  RefreshNodeFields,
  /// <summary>
  /// Узел должен перечитать своё содержимое, а также список своих дочерних узлов
  /// </summary>
  RefreshNode,
  /// <summary>
  /// Узел должен перестроить свои колонки, перечитать своё содержимое, а также список своих дочерних узлов
  /// </summary>
  RefreshNodeAndColumns,
}

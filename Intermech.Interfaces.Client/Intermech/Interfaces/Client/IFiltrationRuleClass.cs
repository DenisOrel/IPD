// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IFiltrationRuleClass
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для класса (формы), который позволяет менять настройки фильтрации извне, какой-либо службой
/// </summary>
public interface IFiltrationRuleClass
{
  /// <summary>
  /// Назначив этому свойству значение, можно уведомить реализующий данный интерфейс класс о том,
  /// что при его активации следует использовать именно это правило подбора версий
  /// </summary>
  VersionsRule NewRule { set; }
}

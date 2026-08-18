
// Type: Intermech.Interfaces.Client.IVersionsRuleSource
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Interfaces.Client;

/// <summary>Позволяет реализовать источний правил подбора версий.</summary>
public interface IVersionsRuleSource
{
  /// <summary>Возвращает доступное правило.</summary>
  /// <returns>Пакет с правилом подбора</returns>
  /// <exception cref="T:Intermech.Interfaces.Client.RuleNotAvailableException">Правило не доступно</exception>
  /// <exception cref="T:System.Exception">Возникла ошибка при получении правила</exception>
  VersionsRulePackage GetRule();
}

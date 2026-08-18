
// Type: Intermech.Interfaces.Client.EditorRuleSource
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует источник правил подбора версий, предоставляющий правило для редактирования, указанное
/// администратором IPS.
/// </summary>
public sealed class EditorRuleSource : IVersionsRuleSource
{
  /// <summary>
  /// Возвращает правило подбора версий, выбранного пользователем.
  /// </summary>
  /// <returns>Пакет с правилом подбора</returns>
  public VersionsRulePackage GetRule()
  {
    return new VersionsRulePackage("cad005aa-306c-11d8-b4e9-00304f19f545");
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentRootsCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Data;
using Intermech.Tools.Settings;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Проверяет, чтобы типы документов были унасленованы от нужного базового типа документов.
/// </summary>
public sealed class DocumentRootsCheck : AbstractDocumentRootsCheck
{
  /// <summary>Выполняет проверку в настройках интегратора.</summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    string str = this.CheckDocumentGroupsRoots(settings.FileDocumentGroups);
    return !string.IsNullOrEmpty(str) ? str : (string) null;
  }

  /// <summary>
  /// Проверяем чтобы объекты заданные в настройках типов объектов для интегратора соответствовали своему типу данных
  /// </summary>
  /// <param name="docGroups"></param>
  /// <returns></returns>
  private string CheckDocumentGroupsRoots(DocumentGroupCollection docGroups)
  {
    foreach (DocumentGroup docGroup in (Collection<DocumentGroup>) docGroups)
    {
      switch (docGroup.Name)
      {
        case "Assembly":
          string str1 = this.CheckDocumentGroupIsBasedOnType(docGroup, IDCache.Default.AllDocuments);
          if (!string.IsNullOrEmpty(str1))
            return str1;
          continue;
        case "Part":
          string str2 = this.CheckDocumentGroupIsBasedOnType(docGroup, IDCache.Default.AllDocuments);
          if (!string.IsNullOrEmpty(str2))
            return str2;
          continue;
        case "AssemblyDrawing":
        case "PartDrawing":
          string str3 = this.CheckDocumentGroupIsBasedOnType(docGroup, IDCache.Default.MechanicalDocuments);
          if (!string.IsNullOrEmpty(str3))
            return str3;
          continue;
        default:
          continue;
      }
    }
    return (string) null;
  }
}

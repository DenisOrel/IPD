// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentGroupsCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует валидатор для групп документов в настройках интегратора.
/// </summary>
public class DocumentGroupsCheck : CADSettingsCheck
{
  private readonly IEnumerable<string> groupNames;

  /// <summary>Создает объект.</summary>
  /// <param name="groupNames">Имена групп документов, которые должны быть проверены</param>
  /// <exception cref="T:ArgumentNullException">groupNames</exception>
  public DocumentGroupsCheck(IEnumerable<string> groupNames)
  {
    this.groupNames = groupNames != null ? groupNames : throw new ArgumentNullException(nameof (groupNames));
  }

  /// <summary>
  /// Выполняет проверку групп документов в настройках интегратора.
  /// </summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    string str1 = this.EliminateEmptyGroups(settings.FileDocumentGroups);
    if (!string.IsNullOrEmpty(str1))
      return str1;
    string str2 = this.EliminateDuplicatedObjectTypes(settings.FileDocumentGroups);
    if (!string.IsNullOrEmpty(str2))
      return str2;
    string str3 = this.EliminateCrossUsedObjectTypes(settings.FileDocumentGroups);
    return !string.IsNullOrEmpty(str3) ? str3 : (string) null;
  }

  private string EliminateEmptyGroups(DocumentGroupCollection docGroups)
  {
    foreach (string groupName in this.groupNames)
    {
      DocumentGroup byName = docGroups.FindByName(groupName, true);
      if (byName.DocumentTypes.Count == 0)
        return string.Format(LocalizationHolder.rm.GetString("Tools.Components_355"), (object) byName.Caption);
    }
    return (string) null;
  }

  private string EliminateDuplicatedObjectTypes(DocumentGroupCollection docGroups)
  {
    foreach (string groupName in this.groupNames)
    {
      DocumentGroup byName = docGroups.FindByName(groupName, true);
      for (int index = 0; index < byName.DocumentTypes.Count; ++index)
      {
        GlobalId<int> docType = byName.DocumentTypes[index];
        if (byName.DocumentTypes.FindIndex(index + 1, (Predicate<GlobalId<int>>) (other => other.Equals((LocalId<int>) docType))) >= 0)
          return string.Format(LocalizationHolder.rm.GetString("Tools.Components_356"), (object) docType, (object) byName.Caption);
      }
    }
    return (string) null;
  }

  private string EliminateCrossUsedObjectTypes(DocumentGroupCollection docGroups)
  {
    foreach (string groupName in this.groupNames)
    {
      DocumentGroup byName = docGroups.FindByName(groupName, true);
      foreach (GlobalId<int> documentType in byName.DocumentTypes)
      {
        foreach (DocumentGroup docGroup in (Collection<DocumentGroup>) docGroups)
        {
          if (!(docGroup.Name == byName.Name) && docGroup.DocumentTypes.Contains(documentType))
          {
            string caption1 = byName.Caption;
            string caption2 = docGroup.Caption;
            return string.Format(LocalizationHolder.rm.GetString("Tools.Components_357"), (object) documentType, (object) caption1, (object) caption2);
          }
        }
      }
    }
    return (string) null;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ObjectTypesRootCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Проверяет, чтобы типы изделий, выпускаемых по документам CAD-системы, были унасленованы от нужного базового типа изделий.
/// </summary>
internal sealed class ObjectTypesRootCheck : CADSettingsCheck
{
  /// <summary>Выполняет проверку в настройках интегратора.</summary>
  /// <param name="settings">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    if (context == SettingsValidatorContext.Generic)
    {
      string str = this.CheckArticleRoots((IList<DocumentGroup>) settings.FileDocumentGroups);
      if (!string.IsNullOrEmpty(str))
        return str;
    }
    return (string) null;
  }

  /// <summary>
  /// Если контекст настроек Generic проверим правильно ли заданы типы создаваемых объектов в конфигураторе БД
  /// </summary>
  /// <param name="documentGroups">Список групп документов</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  private string CheckArticleRoots(IList<DocumentGroup> documentGroups)
  {
    foreach (DocumentGroup documentGroup in (IEnumerable<DocumentGroup>) documentGroups)
    {
      if (documentGroup.Name == "Assembly")
      {
        string str = this.CheckArticleRoots(documentGroup);
        if (!string.IsNullOrEmpty(str))
          return str;
      }
      if (documentGroup.Name == "Part")
      {
        string str = this.CheckArticleRoots(documentGroup);
        if (!string.IsNullOrEmpty(str))
          return str;
      }
    }
    return (string) null;
  }

  private string CheckArticleRoots(DocumentGroup documentGroup)
  {
    int num = 0;
    foreach (GlobalId<int> documentType in documentGroup.DocumentTypes)
    {
      string[] outputObjectTypes = DocumentTypeSettingsCache.GetSettings(documentType.Id).OutputObjectTypes.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      if (outputObjectTypes.Length != 0)
      {
        string str = this.CheckArticleRoots(documentType, outputObjectTypes);
        if (str != null)
          return str;
        ++num;
      }
    }
    return num == 0 ? $"Хотя бы один тип документов в группе '{documentGroup.Caption}', должен иметь настроенные типы объектов, выпускаемые по документам этого типа." : (string) null;
  }

  private string CheckArticleRoots(GlobalId<int> docType, string[] outputObjectTypes)
  {
    foreach (string outputObjectType in outputObjectTypes)
    {
      GlobalId<int> objectTypeGid = DBHelper.CreateObjectTypeGID(new Guid(outputObjectType), false);
      if (objectTypeGid != null && !DBHelper.IsBasedOnType(objectTypeGid.Id, IDCache.Default.AllArticles.Id) && !DBHelper.IsBasedOnType(objectTypeGid.Id, IDCache.Default.AllMaterials.Id))
        return string.Format("Тип объектов '{1}', выпускаемых по документам '{0}', должен быть унаследован от типа '{2}' или '{3}'.", (object) docType.Name, (object) objectTypeGid.Name, (object) IDCache.Default.AllArticles.Text, (object) IDCache.Default.AllMaterials.Text);
    }
    return (string) null;
  }
}

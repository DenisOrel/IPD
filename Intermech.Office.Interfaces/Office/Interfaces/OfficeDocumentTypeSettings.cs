// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeDocumentTypeSettings
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Канцелярские настройки для документа.</summary>
[Serializable]
public class OfficeDocumentTypeSettings
{
  /// <summary>Допустимые виды канцелярского документа.</summary>
  [CanBeNull]
  public OfficeDocumentTypes[] EnableTypes;
  /// <summary>Шаблоны автоматической генерации регистрационного номера.</summary>
  [CanBeNull]
  public Dictionary<OfficeDocumentTypes, RegNumberSettings> Templates;
  /// <summary>Флаги определяющие возможность пустого значения в регистрационном номере.</summary>
  [CanBeNull]
  public Dictionary<OfficeDocumentTypes, bool> EnableEmptyRegNumbers;
  /// <summary>Шаблоны поручений.</summary>
  [NotNull]
  public OrderProcessTemplates ProcessTemplates;

  /// <summary>Конструктор.</summary>
  /// <param name="enableTypes">Допустимые виды канцелярского документа.</param>
  /// <param name="templates">Шаблоны автоматической генерации регистрационного номера.</param>
  /// <param name="enableEmptyRegNumbers">Флаги определяющие возможность пустого значения в регистрационном номере.</param>
  /// <param name="processTemplates">Шаблоны поручений.</param>
  public OfficeDocumentTypeSettings(
    [CanBeNull] OfficeDocumentTypes[] enableTypes,
    [CanBeNull] Dictionary<OfficeDocumentTypes, RegNumberSettings> templates,
    [CanBeNull] Dictionary<OfficeDocumentTypes, bool> enableEmptyRegNumbers,
    [NotNull] OrderProcessTemplates processTemplates)
  {
    this.EnableTypes = enableTypes;
    this.Templates = templates;
    this.ProcessTemplates = processTemplates;
    this.EnableEmptyRegNumbers = enableEmptyRegNumbers;
  }

  [NotNull]
  public static OfficeDocumentTypeSettings CreateDefault()
  {
    return new OfficeDocumentTypeSettings((OfficeDocumentTypes[]) null, (Dictionary<OfficeDocumentTypes, RegNumberSettings>) null, (Dictionary<OfficeDocumentTypes, bool>) null, new OrderProcessTemplates());
  }

  public override bool Equals(object obj)
  {
    if (!(obj is OfficeDocumentTypeSettings documentTypeSettings) || documentTypeSettings.Templates == null && this.Templates != null || documentTypeSettings.Templates != null && this.Templates == null)
      return false;
    if (documentTypeSettings.Templates != null && this.Templates != null)
    {
      foreach (KeyValuePair<OfficeDocumentTypes, RegNumberSettings> template in documentTypeSettings.Templates)
      {
        if (!template.Value.Equals((object) this.Templates[template.Key]))
          return false;
      }
    }
    if (documentTypeSettings.EnableEmptyRegNumbers == null && this.EnableEmptyRegNumbers != null || documentTypeSettings.EnableEmptyRegNumbers != null && this.EnableEmptyRegNumbers == null)
      return false;
    if (documentTypeSettings.EnableEmptyRegNumbers != null && this.EnableEmptyRegNumbers != null)
    {
      foreach (KeyValuePair<OfficeDocumentTypes, bool> enableEmptyRegNumber in documentTypeSettings.EnableEmptyRegNumbers)
      {
        if (!enableEmptyRegNumber.Value.Equals(this.EnableEmptyRegNumbers[enableEmptyRegNumber.Key]))
          return false;
      }
    }
    return (documentTypeSettings.EnableTypes != null || this.EnableTypes == null) && (documentTypeSettings.EnableTypes == null || this.EnableTypes != null) && (documentTypeSettings.EnableTypes == null || this.EnableTypes == null || documentTypeSettings.EnableTypes.Length == this.EnableTypes.Length) && documentTypeSettings.ProcessTemplates.Equals((object) this.ProcessTemplates);
  }
}

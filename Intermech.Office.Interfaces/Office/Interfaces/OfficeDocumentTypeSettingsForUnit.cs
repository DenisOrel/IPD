// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeDocumentTypeSettingsForUnit
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Настройки типа канцелярского документа для конкретного подразделения.</summary>
[Serializable]
public sealed class OfficeDocumentTypeSettingsForUnit
{
  /// <summary>Шаблоны автоматической генерации регистрационного номера.</summary>
  [NotNull]
  public Dictionary<OfficeDocumentTypes, RegNumberSettings> Templates;

  /// <summary>Создать объект.</summary>
  /// <param name="templates">Шаблоны автоматической генерации регистрационного номера.</param>
  public OfficeDocumentTypeSettingsForUnit(
    [NotNull] Dictionary<OfficeDocumentTypes, RegNumberSettings> templates)
  {
    this.Templates = templates;
  }

  /// <summary>Создать объект.</summary>
  /// <param name="templates">Шаблоны автоматической генерации регистрационного номера.</param>
  public OfficeDocumentTypeSettingsForUnit(
    [NotNull] params (OfficeDocumentTypes type, RegNumberSettings settings)[] templates)
  {
    this.Templates = new Dictionary<OfficeDocumentTypes, RegNumberSettings>(templates.Length);
    this.Templates.AddRange<OfficeDocumentTypes, RegNumberSettings>(templates);
  }

  [NotNull]
  public OfficeDocumentTypeSettingsForUnit Clone()
  {
    return new OfficeDocumentTypeSettingsForUnit(DictionaryFactory.Create<OfficeDocumentTypes, RegNumberSettings>(this.Templates.Select<KeyValuePair<OfficeDocumentTypes, RegNumberSettings>, KeyValuePair<OfficeDocumentTypes, RegNumberSettings>>((Func<KeyValuePair<OfficeDocumentTypes, RegNumberSettings>, KeyValuePair<OfficeDocumentTypes, RegNumberSettings>>) (keyValue => new KeyValuePair<OfficeDocumentTypes, RegNumberSettings>(keyValue.Key, keyValue.Value.Clone()))), this.Templates.Comparer, this.Templates.Count));
  }
}

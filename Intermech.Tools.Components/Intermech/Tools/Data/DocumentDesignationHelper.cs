// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.DocumentDesignationHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Data;

public static class DocumentDesignationHelper
{
  public static string GetDocCode(int objectType)
  {
    DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(objectType);
    return !settings.DocumentTypeCodeInDesignation || string.IsNullOrEmpty(settings.DocumentTypeCode) ? (string) null : settings.DocumentTypeCode;
  }

  public static string AppendDocCode(string origDesignation, int objectType)
  {
    DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(objectType);
    if (!settings.DocumentTypeCodeInDesignation || string.IsNullOrEmpty(settings.DocumentTypeCode))
      return origDesignation;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentsHelper.AppendDocCode(sessionKeeper.Session, origDesignation, settings.DocumentTypeCode);
  }

  public static string RemoveDocCode(string origDesignation, int objectType)
  {
    DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(objectType);
    if (!settings.DocumentTypeCodeInDesignation || string.IsNullOrEmpty(settings.DocumentTypeCode))
      return origDesignation;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return DocumentsHelper.RemoveDocCode(sessionKeeper.Session, origDesignation, settings.DocumentTypeCode);
  }

  internal static List<string> GetLegacyDocCodes()
  {
    List<string> stringList = new List<string>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string separatorInDesignation = DocumentsHelper.GetSeparatorInDesignation(sessionKeeper.Session);
      if (!string.IsNullOrEmpty(separatorInDesignation))
        stringList.Add(separatorInDesignation);
    }
    stringList.Add("-");
    stringList.Add(" ");
    List<string> legacyDocCodes = new List<string>(stringList.Count);
    foreach (string str in stringList)
      legacyDocCodes.Add(str + CADDocumentResources.EMB_DocumentBuggySuffix);
    return legacyDocCodes;
  }
}

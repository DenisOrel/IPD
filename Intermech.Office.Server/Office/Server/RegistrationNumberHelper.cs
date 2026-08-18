// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.RegistrationNumberHelper
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Server;

internal class RegistrationNumberHelper
{
  public const string ClassifierSign = "{C}";

  [CanBeNull]
  public static RegNumberSettings GetTemplate(
    [NotNull] IUserSession session,
    int docTypeID,
    OfficeDocumentTypes type)
  {
    return RegistrationNumberHelper.GetTemplate(session, docTypeID, type, 0L);
  }

  [CanBeNull]
  public static RegNumberSettings GetTemplate(
    [NotNull] IUserSession session,
    int docTypeID,
    OfficeDocumentTypes type,
    long unitID)
  {
    IOfficeDocumentTypeService customService = session.GetCustomService<IOfficeDocumentTypeService>();
    if (unitID != 0L)
    {
      OfficeDocumentTypeSettingsForUnit settings = customService.GetSettings(unitID, docTypeID);
      if (settings == null)
        return (RegNumberSettings) null;
      RegNumberSettings regNumberSettings;
      return !settings.Templates.TryGetValue(type, out regNumberSettings) ? (RegNumberSettings) null : regNumberSettings;
    }
    OfficeDocumentTypeSettings settings1 = customService.GetSettings(session.SessionGUID, docTypeID);
    if (settings1.EnableTypes == null || Array.IndexOf<OfficeDocumentTypes>(settings1.EnableTypes, type) < 0 || settings1.Templates == null)
      return (RegNumberSettings) null;
    RegNumberSettings regNumberSettings1;
    return !settings1.Templates.TryGetValue(type, out regNumberSettings1) ? (RegNumberSettings) null : regNumberSettings1;
  }
}

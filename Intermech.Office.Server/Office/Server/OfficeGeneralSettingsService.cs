// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.OfficeGeneralSettingsService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class OfficeGeneralSettingsService : LongLifeObject, IOfficeGeneralSettingsService
{
  public OfficeGeneralSettings Settings { get; private set; }

  public long[] SupervisorObjVerIDs { get; private set; }

  public void Reload(Guid sessionGuid)
  {
    IUserSession session = OfficeGeneralSettingsService.GetSession(sessionGuid);
    Dictionary<OfficeDocumentTypes, CountResetTypes> ownResetModes = session.GetCustomService<IOfficeDocumentTypeService>().GetOwnResetModes(sessionGuid);
    IDBConfigurations configurations = session.Configurations;
    this.Settings = new OfficeGeneralSettings(ownResetModes[OfficeDocumentTypes.Incoming], ownResetModes[OfficeDocumentTypes.Outgoing], ownResetModes[OfficeDocumentTypes.Internal], configurations.ReadInteger("Intermech.Office", "General", "AutoSendTemplateID", 0L, DBConfigMode.GlobalOnly), configurations.ReadString("Intermech.Office", "General", "AutoSendEmail", string.Empty, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "AutoSendUserID", 0L, DBConfigMode.GlobalOnly), configurations.ReadBool("Intermech.Office", "General", "PrivateOffice", false, DBConfigMode.GlobalOnly), configurations.ReadBool("Intermech.Office", "General", "FilterResolutions", true, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "SendAddresseeTemplateID", 0L, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "ConsistentCtrlResolTemplID", 0L, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "ConsistentNCtrlResolTemplID", 0L, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "ParallelCtrlResolTemplID", 0L, DBConfigMode.GlobalOnly), configurations.ReadInteger("Intermech.Office", "General", "ParallelNCtrlResolTemplID", 0L, DBConfigMode.GlobalOnly), configurations.ReadBool("Intermech.Office", "General", "IncomingPrivateFolderEnable", true, DBConfigMode.GlobalOnly), (int) configurations.ReadInteger("Intermech.Office", "General", OfficeConsts.CaptionAttributeForEmailMessagesParamName, 0L, DBConfigMode.GlobalOnly));
    this.SupervisorObjVerIDs = this.CommaTextToObjVerIDs(configurations.ReadString("Intermech.Office", "General", "SupervisorObjVerIDs", (string) null, DBConfigMode.GlobalOnly), session, new long[1]
    {
      OfficeConsts.ObjectAdminRoleID
    });
  }

  public void Save(Guid sessionGuid, OfficeGeneralSettings settings)
  {
    IUserSession session = OfficeGeneralSettingsService.GetSession(sessionGuid);
    Dictionary<OfficeDocumentTypes, CountResetTypes> resetTypes = new Dictionary<OfficeDocumentTypes, CountResetTypes>(3)
    {
      {
        OfficeDocumentTypes.Incoming,
        settings.IncomingDocResetType
      },
      {
        OfficeDocumentTypes.Outgoing,
        settings.OutgoingDocResetType
      },
      {
        OfficeDocumentTypes.Internal,
        settings.InternalDocResetType
      }
    };
    session.GetCustomService<IOfficeDocumentTypeService>().SetOwnResetModes(sessionGuid, resetTypes);
    IDBConfigurations configurations = session.Configurations;
    configurations.WriteInteger("Intermech.Office", "General", "AutoSendTemplateID", settings.TemplateID, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "AutoSendUserID", settings.UserID, 0L);
    configurations.WriteString("Intermech.Office", "General", "AutoSendEmail", settings.AutoSendEmail, 0L);
    configurations.WriteBool("Intermech.Office", "General", "PrivateOffice", settings.PrivateOffice, 0L);
    configurations.WriteBool("Intermech.Office", "General", "FilterResolutions", settings.FilterResolutions, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "SendAddresseeTemplateID", settings.AddresseeTemplateID, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "ConsistentCtrlResolTemplID", settings.ConsistentControlResolutionTemplateID, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "ConsistentNCtrlResolTemplID", settings.ConsistentNonControlResolutionTemplateID, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "ParallelCtrlResolTemplID", settings.ParallelControlResolutionTemplateID, 0L);
    configurations.WriteInteger("Intermech.Office", "General", "ParallelNCtrlResolTemplID", settings.ParallelNonControlResolutionTemplateID, 0L);
    configurations.WriteBool("Intermech.Office", "General", "IncomingPrivateFolderEnable", settings.IncomingPrivateFolderEnable, 0L);
    this.Settings = settings;
  }

  public void WriteSupervisorsList(Guid sessionGuid, long[] supervisorsList)
  {
    IUserSession session = OfficeGeneralSettingsService.GetSession(sessionGuid);
    this.SupervisorObjVerIDs = supervisorsList;
    session.Configurations.WriteString("Intermech.Office", "General", "SupervisorObjVerIDs", this.ObjVerIDsToCommaText(this.SupervisorObjVerIDs, session), 0L);
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  private long[] CommaTextToObjVerIDs([CanBeNull] string commaText, [NotNull] IUserSession session, [CanBeNull] long[] defaultValue = null)
  {
    long[] numArray;
    if (commaText == null)
    {
      numArray = (long[]) null;
    }
    else
    {
      long result;
      numArray = ((IEnumerable<string>) commaText.Split(',')).Select<string, long>((Func<string, long>) (str => !long.TryParse(str, out result) ? 0L : result)).Distinct<long>().Where<long>((Func<long, bool>) (objVerID => objVerID != 0L && !session.GetObjectInfo(objVerID).Empty)).Distinct<long>().ToArray<long>();
    }
    return numArray ?? defaultValue;
  }

  [NotNull]
  private string ObjVerIDsToCommaText([CanBeNull] long[] objVerIDs, [NotNull] IUserSession session)
  {
    return objVerIDs == null || objVerIDs.Length == 0 ? string.Empty : string.Join<long>(",", ((IEnumerable<long>) objVerIDs).Where<long>((Func<long, bool>) (objVerID => objVerID != 0L && !session.GetObjectInfo(objVerID).Empty)));
  }

  [NotNull]
  private static IUserSession GetSession(Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    return (sessionById != null ? (!sessionById.IsAdmin ? 1 : 0) : 1) == 0 ? sessionById : throw new ArgumentException(string.Empty, nameof (sessionGuid));
  }
}

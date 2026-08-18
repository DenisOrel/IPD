// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Params.ImbaseParamsService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Params;

internal class ImbaseParamsService : LongLifeObject, IImbaseParamsService
{
  private const string Imbase = "IMBASE";
  private const string ImbaseViewParamsSectionID = "VIEW";
  private const string ImbaseCommonParamsSectionID = "ImbaseCommon";
  private const string EditorSectionID = "EDITOR";
  private const string AnalizeHiddenRecordsParamName = "ANALIZEHIDDENRECORDS";
  private const string UseExtendedSecurityCheckForIndexesParamName = "UseExtendedSecurityCheck";
  private const string DenyFewLinksForSameTableParamName = "DenyFewLinksForSameTable";
  private const string HideEmptyColumnsParamName = "HIDEEMPTYCOLUMNS";
  private const string FreezeFirstColumnParamName = "FREEZEFIRSTCOLUMN";
  private const string DeleteusedrecordsParamName = "DELETEUSEDRECORDS";
  private const string CheckApplicabilityBeforeCreateCompositionParamName = "CheckApplicability";
  private const string RecordColorParamName = "RecordColorParamName";
  private const string FolderImagesParamsName = "FolderImagesParamsName";
  private Dictionary<long, ImbaseUserParams> _userParamsDict = new Dictionary<long, ImbaseUserParams>();
  private ImbaseCommonParams _commonParams;
  private ParamsServerSynchronizer _paramsServerSynchronizer;

  public ImbaseParamsService()
  {
    this._paramsServerSynchronizer = new ParamsServerSynchronizer((IImbaseParamsService) this);
    ApplicationServices.Container.GetService<IServerSynchronizersManager>().RegisterSynchronizer((IServerSynchronizer) this._paramsServerSynchronizer);
    ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true).AfterLogoutEvent += new LoginHandler(this.EventLogHelper_AfterLogoutEvent);
  }

  public ImbaseCommonParams CommonParams
  {
    get => this._commonParams ?? (this._commonParams = this.LoadCommonParams());
  }

  public ImbaseUserParams GetUserParams(Guid sessionGuid)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    ImbaseUserParams imbaseUserParams;
    return !this._userParamsDict.TryGetValue(session.UserID, out imbaseUserParams) ? this.LoadUserParams(session) : imbaseUserParams;
  }

  public void SetUserParams(Guid sessionGuid, ImbaseUserParams userParams)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    this.SaveUserParams(session, userParams);
    this._paramsServerSynchronizer?.AddEvent(session.UserID.ToString(), ((UserSession) session).DataManager);
  }

  public void SetCommonParams(Guid sessionGuid, ImbaseCommonParams commonParams)
  {
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (!session.IsAdmin)
      return;
    this._commonParams = commonParams;
    this.SaveCommonParams(session, this._commonParams);
    this._paramsServerSynchronizer?.AddEvent("common", ((UserSession) session).DataManager);
  }

  public void ResetSettings(IUserSession session, string info)
  {
    if (info.Equals("common"))
    {
      this._commonParams = (ImbaseCommonParams) null;
    }
    else
    {
      long result;
      if (!long.TryParse(info, out result))
        return;
      this._userParamsDict.Remove(result);
    }
  }

  private ImbaseCommonParams LoadCommonParams()
  {
    IUserSession session = (IUserSession) null;
    try
    {
      session = ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone("imbase.startup");
      return session != null ? this.LoadCommonParamsInternal(session) : throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullSession"));
    }
    finally
    {
      session?.Logout("imbase.startup");
    }
  }

  private ImbaseCommonParams LoadCommonParamsInternal(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    ImbaseCommonParams commonParams = new ImbaseCommonParams();
    IDBConfigurations configurations = session.Configurations;
    commonParams.AnalizeHiddenRecords = configurations.ReadBool("IMBASE", "VIEW", "ANALIZEHIDDENRECORDS", commonParams.AnalizeHiddenRecords, DBConfigMode.GlobalOnly);
    commonParams.UseExtendedSecurityCheckForIndexes = Convert.ToInt32(configurations.ReadInteger("IMBASE", "ImbaseCommon", "UseExtendedSecurityCheck", commonParams.UseExtendedSecurityCheckForIndexes ? 1L : 0L, DBConfigMode.GlobalOnly)) > 0;
    commonParams.DenyFewLinksForSameTable = configurations.ReadBool("IMBASE", "ImbaseCommon", "DenyFewLinksForSameTable", commonParams.DenyFewLinksForSameTable, DBConfigMode.GlobalOnly);
    commonParams.DeleteRecordMode = (DeleteRecordMode) configurations.ReadInteger("IMBASE", "EDITOR", "DELETEUSEDRECORDS", 0L, DBConfigMode.GlobalOnly);
    commonParams.CheckApplicabilityBeforeCreateComposition = configurations.ReadBool("IMBASE", "ImbaseCommon", "CheckApplicability", commonParams.CheckApplicabilityBeforeCreateComposition, DBConfigMode.GlobalOnly);
    List<AttributeForObjectTypeInfo> objectTypeFromString1 = this.GetAttributeForObjectTypeFromString(configurations.ReadString("IMBASE", "ImbaseCommon", "NotExpandableAttributes", string.Empty, DBConfigMode.GlobalOnly));
    commonParams.NotExpandableAttributes.AddRange((IEnumerable<AttributeForObjectTypeInfo>) objectTypeFromString1);
    List<AttributeForObjectTypeInfo> objectTypeFromString2 = this.GetAttributeForObjectTypeFromString(configurations.ReadString("IMBASE", "ImbaseCommon", "SkipAttributes", string.Empty, DBConfigMode.GlobalOnly));
    commonParams.SkipAttributes.AddRange((IEnumerable<AttributeForObjectTypeInfo>) objectTypeFromString2);
    byte[] config_file;
    configurations.LoadConfigData("FolderImagesParamsName", out BlobInformation _, out config_file, 0L);
    commonParams.FolderApplicabilityIcons.SavedData = config_file;
    string str = configurations.ReadString("IMBASE", "ImbaseCommon", "ImbaseSyncParams", string.Empty, DBConfigMode.GlobalOnly);
    commonParams.ImbaseSyncParams.SetData(str, session.EventLog);
    ImbaseFolderStatusesProvider.SetStatusValues(commonParams);
    return commonParams;
  }

  private ImbaseUserParams LoadUserParams(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    ImbaseUserParams imbaseUserParams = new ImbaseUserParams();
    IDBConfigurations configurations = session.Configurations;
    imbaseUserParams.HideEmptyColumns = configurations.ReadBool("IMBASE", "VIEW", "HIDEEMPTYCOLUMNS", false, DBConfigMode.UserAndGlobal);
    imbaseUserParams.FreezeFirstColumn = configurations.ReadBool("IMBASE", "VIEW", "FREEZEFIRSTCOLUMN", false, DBConfigMode.UserAndGlobal);
    imbaseUserParams.UseIMHSelector = configurations.ReadBool("IMBASE", "VIEW", "UseIMHSelector", true, DBConfigMode.UserAndGlobal);
    imbaseUserParams.SaveColumnsState = configurations.ReadBool("IMBASE", "VIEW", "SaveColumnsState", true, DBConfigMode.UserAndGlobal);
    imbaseUserParams.SaveFilterState = configurations.ReadBool("IMBASE", "VIEW", "SaveFilterState", true, DBConfigMode.UserAndGlobal);
    imbaseUserParams.SaveUserFilterState = configurations.ReadBool("IMBASE", "VIEW", "SaveUserFilterState", true, DBConfigMode.UserAndGlobal);
    imbaseUserParams.UseExtendedLog = configurations.ReadBool("IMBASE", "ImbaseCommon", "UseExtendedLog", false, DBConfigMode.UserAndGlobal);
    byte[] config_file;
    configurations.LoadConfigData("RecordColorParamName", out BlobInformation _, out config_file, session.UserID);
    imbaseUserParams.TableRecordsApplicabilityColors.SavedData = config_file;
    this._userParamsDict[session.UserID] = imbaseUserParams;
    return imbaseUserParams;
  }

  private void SaveCommonParams(IUserSession session, ImbaseCommonParams commonParams)
  {
    IDBConfigurations configurations = session.Configurations;
    if (!session.IsAdmin)
      return;
    configurations.WriteBool("IMBASE", "VIEW", "ANALIZEHIDDENRECORDS", commonParams.AnalizeHiddenRecords, 0L);
    configurations.WriteInteger("IMBASE", "ImbaseCommon", "UseExtendedSecurityCheck", commonParams.UseExtendedSecurityCheckForIndexes ? 1L : 0L, 0L);
    configurations.WriteBool("IMBASE", "ImbaseCommon", "DenyFewLinksForSameTable", commonParams.DenyFewLinksForSameTable, 0L);
    configurations.WriteInteger("IMBASE", "EDITOR", "DELETEUSEDRECORDS", (long) commonParams.DeleteRecordMode, 0L);
    configurations.WriteBool("IMBASE", "ImbaseCommon", "CheckApplicability", commonParams.CheckApplicabilityBeforeCreateComposition, 0L);
    string attributeForObjectType1 = this.GetStringFromAttributeForObjectType(commonParams.NotExpandableAttributes);
    configurations.WriteString("IMBASE", "ImbaseCommon", "NotExpandableAttributes", attributeForObjectType1, 0L);
    string attributeForObjectType2 = this.GetStringFromAttributeForObjectType(commonParams.SkipAttributes);
    configurations.WriteString("IMBASE", "ImbaseCommon", "SkipAttributes", attributeForObjectType2, 0L);
    byte[] savedData = commonParams.FolderApplicabilityIcons.SavedData;
    BlobInformation config_info = new BlobInformation((long) savedData.Length, (long) savedData.Length, DateTime.Now, "FolderImagesParamsName", ArcMethods.NotPacked, string.Empty);
    configurations.WriteConfigData(config_info, savedData, 0L);
    configurations.WriteString("IMBASE", "ImbaseCommon", "ImbaseSyncParams", commonParams.ImbaseSyncParams.GetData(), 0L);
    ImbaseFolderStatusesProvider.SetStatusValues(commonParams);
  }

  private void SaveUserParams(IUserSession session, ImbaseUserParams userParams)
  {
    IDBConfigurations configurations = session.Configurations;
    configurations.WriteBool("IMBASE", "VIEW", "HIDEEMPTYCOLUMNS", userParams.HideEmptyColumns, session.UserID);
    configurations.WriteBool("IMBASE", "VIEW", "FREEZEFIRSTCOLUMN", userParams.FreezeFirstColumn, session.UserID);
    configurations.WriteBool("IMBASE", "VIEW", "SaveColumnsState", userParams.SaveColumnsState, session.UserID);
    configurations.WriteBool("IMBASE", "VIEW", "SaveFilterState", userParams.SaveFilterState, session.UserID);
    configurations.WriteBool("IMBASE", "VIEW", "SaveUserFilterState", userParams.SaveUserFilterState, session.UserID);
    configurations.WriteBool("IMBASE", "ImbaseCommon", "UseExtendedLog", userParams.UseExtendedLog, session.UserID);
    byte[] savedData = userParams.TableRecordsApplicabilityColors.SavedData;
    configurations.WriteConfigData(new BlobInformation((long) savedData.Length, (long) savedData.Length, DateTime.Now, "RecordColorParamName", ArcMethods.NotPacked, string.Empty), savedData, session.UserID);
    this._userParamsDict[session.UserID] = userParams;
  }

  private void EventLogHelper_AfterLogoutEvent(IUserSession session)
  {
    this._userParamsDict.Remove(session.UserID);
  }

  private List<AttributeForObjectTypeInfo> GetAttributeForObjectTypeFromString(string sourceStr)
  {
    List<AttributeForObjectTypeInfo> objectTypeFromString = new List<AttributeForObjectTypeInfo>();
    if (string.IsNullOrEmpty(sourceStr))
      return objectTypeFromString;
    string str1 = sourceStr;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '=' };
      string[] strArray = str2.Split(chArray2);
      if (strArray.Length == 2)
      {
        string input = strArray[0];
        Guid result1;
        if (Guid.TryParse(strArray[1], out result1))
        {
          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(result1);
          if (attributeTypeId != -10000)
          {
            int objectTypeId = -1;
            Guid result2;
            if (input != string.Empty && Guid.TryParse(input, out result2))
              objectTypeId = MetaDataHelper.GetObjectTypeID(result2);
            objectTypeFromString.Add(new AttributeForObjectTypeInfo(objectTypeId, attributeTypeId));
          }
        }
      }
    }
    return objectTypeFromString;
  }

  private string GetStringFromAttributeForObjectType(List<AttributeForObjectTypeInfo> sourceList)
  {
    List<string> values = new List<string>();
    sourceList = sourceList.Distinct<AttributeForObjectTypeInfo>().ToList<AttributeForObjectTypeInfo>();
    foreach (AttributeForObjectTypeInfo source in sourceList)
    {
      if (source.AttrTypeId != 0 && !(MetaDataHelper.GetAttributeTypeGuid(source.AttrTypeId) == Guid.Empty) && (source.ObjectTypeId == -1 || !(MetaDataHelper.GetObjectTypeGuid(source.ObjectTypeId) == Guid.Empty)))
      {
        Guid guid;
        string empty;
        if (source.ObjectTypeId != -1)
        {
          guid = MetaDataHelper.GetObjectTypeGuid(source.ObjectTypeId);
          empty = guid.ToString();
        }
        else
          empty = string.Empty;
        string str1 = empty;
        guid = MetaDataHelper.GetAttributeTypeGuid(source.AttrTypeId);
        string str2 = guid.ToString();
        values.Add(string.Join("=", str1, str2));
      }
    }
    return string.Join(",", (IEnumerable<string>) values);
  }
}

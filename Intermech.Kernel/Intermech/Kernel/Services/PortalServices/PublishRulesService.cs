// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishRulesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Services.PortalServices;

public sealed class PublishRulesService : 
  RulesService,
  IPublishRulesService,
  ITransferSettingsService
{
  public static Guid[] ForbiddenAttributes = new Guid[14]
  {
    PortalConsts.attributeEnabledSites,
    PortalConsts.attributeCopyKeepers,
    PortalConsts.attributePublishOptions,
    PortalConsts.attributeFirstPublishSite,
    PortalConsts.attributeOwner,
    PortalConsts.attributeCompositionOwner,
    PortalConsts.attributeParentSites,
    PortalConsts.attributeCompositionParentSites,
    PortalConsts.attributePublishInComposition,
    PortalConsts.attributePublishLinksGuid,
    PortalConsts.attributePublishObjectGUID,
    PortalConsts.attributeRootTypePublishObject,
    PortalConsts.attributePublicationNecessary,
    new Guid("cad001c2-306c-11d8-b4e9-00304f19f545")
  };
  public static string[] ForbiddenAttributeNames = PublishRulesService.GetAttributeNames(PublishRulesService.ForbiddenAttributes);
  public static string AttributeContentModifyDateName = MetaDataHelper.GetAttributeTypeName(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
  private readonly Dictionary<int, List<Guid>> _forbiddenAttributes;

  private static string[] GetAttributeNames(Guid[] guids)
  {
    return ((IEnumerable<Guid>) guids).ToList<Guid>().ConvertAll<string>((Converter<Guid, string>) (x => MetaDataHelper.GetAttributeTypeName(x))).ToArray();
  }

  public PublishRulesService(IUserSession session)
    : base(session, "GENERAL_SETTINGS")
  {
    this._forbiddenAttributes = new Dictionary<int, List<Guid>>();
    for (int index = 0; index < PublishRulesService.ForbiddenAttributes.Length; ++index)
    {
      IDBAttributeType attributeType = session.GetAttributeType(PublishRulesService.ForbiddenAttributes[index], false);
      if (attributeType != null)
        this._forbiddenAttributes.Add(attributeType.AttributeID, new List<Guid>()
        {
          Guid.Empty
        });
    }
  }

  public void RegisterForbiddenAttribute(Guid typeGuid, int attributeID)
  {
    List<Guid> guidList1;
    if (this._forbiddenAttributes.TryGetValue(attributeID, out guidList1))
    {
      if (guidList1.Contains(Guid.Empty) || guidList1.Contains(typeGuid))
        return;
      guidList1.Add(typeGuid);
    }
    else
    {
      List<Guid> guidList2 = new List<Guid>() { typeGuid };
      this._forbiddenAttributes.Add(attributeID, guidList2);
    }
  }

  public void RegisterForbiddenAttribute(int attributeID)
  {
    List<Guid> guidList1;
    if (this._forbiddenAttributes.TryGetValue(attributeID, out guidList1))
    {
      guidList1 = new List<Guid>() { Guid.Empty };
    }
    else
    {
      List<Guid> guidList2 = new List<Guid>() { Guid.Empty };
      this._forbiddenAttributes.Add(attributeID, guidList2);
    }
  }

  public bool IsForbiddenAttribute(Guid typeGuid, int attributeID)
  {
    List<Guid> guidList;
    return this._forbiddenAttributes.TryGetValue(attributeID, out guidList) && (guidList.Contains(Guid.Empty) || guidList.Contains(typeGuid));
  }

  public int MaxAccessLevel
  {
    get
    {
      return (int) this.Config.ReadInteger(this.moduleName, this.sectionName, "MAX_ACCESS", 0L, DBConfigMode.GlobalOnly);
    }
    set
    {
      this.Config.WriteInteger(this.moduleName, this.sectionName, "MAX_ACCESS", (long) value, 0L);
    }
  }

  public bool OTDFiltering
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "OTD_FILTER", false, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "OTD_FILTER", value, 0L);
  }

  public long BlobStorageID
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "STORAGE_ID", 0L, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteInteger(this.moduleName, this.sectionName, "STORAGE_ID", value, 0L);
  }

  public List<long> BeSurePublishForSites
  {
    get => this.GetListSitiesFromConfig("BESURE_PUBLISH");
    set => this.SetListSitiesToConfig("BESURE_PUBLISH", value);
  }

  public List<long> EnableTrueTaskForSites
  {
    get => this.GetListSitiesFromConfig("ENABLE_TASKS_SITIES");
    set => this.SetListSitiesToConfig("ENABLE_TASKS_SITIES", value);
  }

  private void SetListSitiesToConfig(string paramName, List<long> sities)
  {
    string str = string.Empty;
    if (sities != null && sities.Count > 0)
    {
      foreach (long sity in sities)
        str += $"{sity};";
      str = str.TrimEnd(';');
    }
    this.Config.WriteString(this.moduleName, this.sectionName, paramName, str, 0L);
  }

  private List<long> GetListSitiesFromConfig(string paramName)
  {
    List<long> sitiesFromConfig = new List<long>();
    string str1 = this.Config.ReadString(this.moduleName, this.sectionName, paramName, string.Empty, DBConfigMode.GlobalOnly);
    if (!string.IsNullOrEmpty(str1))
    {
      string str2 = str1;
      char[] chArray = new char[1]{ ';' };
      foreach (string str3 in str2.Split(chArray))
        sitiesFromConfig.Add(Convert.ToInt64(str3));
    }
    return sitiesFromConfig;
  }

  public bool IsEnableTrueTaskForSites(string sities, bool defaultValue)
  {
    List<long> trueTaskForSites = this.EnableTrueTaskForSites;
    if (trueTaskForSites == null || trueTaskForSites.Count <= 0)
      return defaultValue;
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    bool flag = true;
    foreach (char sity in sities)
    {
      SiteInfo site = customService.GetSite(sity);
      if (!trueTaskForSites.Contains(site.ID))
      {
        flag = false;
        break;
      }
    }
    return flag | defaultValue;
  }

  public List<Tuple<int, int>> InseparableObjectTypes
  {
    get
    {
      List<Tuple<int, int>> inseparableObjectTypes = (List<Tuple<int, int>>) null;
      BlobInformation config_info;
      byte[] config_file;
      this.Config.LoadConfigData(nameof (InseparableObjectTypes), out config_info, out config_file);
      if (config_info.RealFileSize > 0L && config_file != null)
      {
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          serializationStream.Position = 0L;
          inseparableObjectTypes = (List<Tuple<int, int>>) new BinaryFormatter().Deserialize((Stream) serializationStream);
        }
      }
      return inseparableObjectTypes;
    }
    set
    {
      byte[] config_file = (byte[]) null;
      if (value == null)
        value = new List<Tuple<int, int>>(0);
      using (ImChunkedStream serializationStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) value);
        serializationStream.Position = 0L;
        config_file = serializationStream.ToArray();
      }
      this.Config.WriteConfigData(new BlobInformation((long) config_file.Length, (long) config_file.Length, DateTime.Now, nameof (InseparableObjectTypes), ArcMethods.NotPacked, string.Empty), config_file, 0L);
      ((LinkedObjectsService) ServiceUtils.GetService<ILinkedObjectsService>((object) ServerServices.ServiceContainer, false)).ForceReloadTypes(this.session);
    }
  }

  public TaskPriority Receipt4packetTaskPriority
  {
    get
    {
      return (TaskPriority) this.Config.ReadInteger(this.moduleName, this.sectionName, "RECEIPT_PRIORITY", 1L, DBConfigMode.GlobalOnly);
    }
    set
    {
      this.Config.WriteInteger(this.moduleName, this.sectionName, "RECEIPT_PRIORITY", (long) value, 0L);
    }
  }

  public TaskPriority AnswerTaskPriority
  {
    get
    {
      return (TaskPriority) this.Config.ReadInteger(this.moduleName, this.sectionName, "ANSWER_PRIORITY", 1L, DBConfigMode.GlobalOnly);
    }
    set
    {
      this.Config.WriteInteger(this.moduleName, this.sectionName, "ANSWER_PRIORITY", (long) value, 0L);
    }
  }

  public bool CreateDetailTaskLog
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "CREATE_DETAIL_LOG", false, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "CREATE_DETAIL_LOG", value, 0L);
  }

  public List<int> LoggingTransferObjectTypesWithChildTypes
  {
    get
    {
      List<int> transferObjectTypes = this.LoggingTransferObjectTypes;
      return transferObjectTypes != null ? MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) transferObjectTypes) : (List<int>) null;
    }
  }

  public List<int> LoggingTransferObjectTypes
  {
    get
    {
      List<int> transferObjectTypes = (List<int>) null;
      BlobInformation config_info;
      byte[] config_file;
      this.Config.LoadConfigData("LOGGING_TYPES", out config_info, out config_file);
      if (config_info.RealFileSize > 0L && config_file != null)
      {
        using (MemoryStream serializationStream = new MemoryStream(config_file))
        {
          serializationStream.Position = 0L;
          transferObjectTypes = (List<int>) new BinaryFormatter().Deserialize((Stream) serializationStream);
        }
      }
      return transferObjectTypes;
    }
    set
    {
      byte[] config_file = (byte[]) null;
      if (value == null)
        value = new List<int>(0);
      using (ImChunkedStream serializationStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) value);
        serializationStream.Position = 0L;
        config_file = serializationStream.ToArray();
      }
      this.Config.WriteConfigData(new BlobInformation((long) config_file.Length, (long) config_file.Length, DateTime.Now, "LOGGING_TYPES", ArcMethods.NotPacked, string.Empty), config_file, 0L);
    }
  }
}

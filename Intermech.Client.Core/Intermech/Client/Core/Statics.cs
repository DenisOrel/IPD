
// Type: Intermech.Client.Core.Statics
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client.Core;

/// <summary>Статические свойства</summary>
public class Statics
{
  public static readonly Guid CategoryAttributesGUID = new Guid("{23F49204-EE94-4124-B7D2-06CB9E722C6D}");
  public static int CategoryAttributes = 0;
  public static readonly Guid CategorySubjectAreasGUID = new Guid("{6A5C204F-967D-429f-A914-D949F5D8863E}");
  public static int CategorySubjectAreas = 0;
  public static readonly Guid CategoryObjectTypesGUID = new Guid("{C68DB4BC-A5F2-40a0-95D7-9E6B9BF8D1DA}");
  public static int CategoryObjectTypes = 0;
  public static readonly Guid CategoryRelationTypesGUID = new Guid("{E02B1E4F-694F-4573-81AE-0B9EB1CF383F}");
  public static int CategoryRelationTypes = 0;
  public static readonly Guid CategoryLCLevelsGUID = new Guid("{37EE13D8-270F-47a4-8472-93C21EE8E8FD}");
  public static int CategoryLCLevels = 0;
  public static readonly Guid CategoryLanguagesGUID = new Guid("{F8AFAF79-1822-4b70-9597-F23AFAD79F0E}");
  public static int CategoryLanguages = 0;
  public static readonly Guid CategoryLCSchemasGUID = new Guid("{25A723B3-2161-4a51-B5C0-79694E81ECD6}");
  public static int CategoryLCSchemas = 0;
  public static readonly Guid CategoryStatisticsGUID = new Guid("{AB75755D-BCCD-4d15-BDDE-866862BDBEF5}");
  public static int CategoryStatistics = 0;
  public static readonly Guid[] AttributeReadonlyBlacklistGUID = new Guid[4]
  {
    new Guid("cad001c0-306c-11d8-b4e9-00304f19f545"),
    new Guid("cad001c1-306c-11d8-b4e9-00304f19f545"),
    new Guid("cad00817-306c-11d8-b4e9-00304f19f545"),
    new Guid("cad00818-306c-11d8-b4e9-00304f19f545")
  };
  public static int[] AttributeReadonlyBlacklist = new int[2];
  private static ICategoryTypeIconService iCategoryTypeIconService = (ICategoryTypeIconService) null;
  /// <summary>Признак того, что приложение закрывается</summary>
  public static bool IsApplicationClosing = false;

  /// <summary>
  /// проверить атрибут на вхождение в черный список редактирования по ID
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public static bool CheckAttributeReadonlyBlacklist(int attributeId)
  {
    for (int index = 0; index < Statics.AttributeReadonlyBlacklist.Length; ++index)
    {
      if (Statics.AttributeReadonlyBlacklist[index] == attributeId)
        return true;
    }
    return false;
  }

  /// <summary>
  /// проверить атрибут на вхождение в черный список редактирования по GUID
  /// </summary>
  /// <param name="attributeGuid"></param>
  /// <returns></returns>
  public static bool CheckAttributeReadonlyBlacklist(Guid attributeGuid)
  {
    for (int index = 0; index < Statics.AttributeReadonlyBlacklistGUID.Length; ++index)
    {
      if (Statics.AttributeReadonlyBlacklistGUID[index] == attributeGuid)
        return true;
    }
    return false;
  }

  /// <summary>вызывается при старте клиента</summary>
  public static void InitAttributeReadonlyBlacklist()
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    for (int index = 0; index < Statics.AttributeReadonlyBlacklist.Length; ++index)
    {
      if (Statics.AttributeReadonlyBlacklist[index] == 0)
      {
        IDBAttributeTypeInfo attributeType = service.GetAttributeType(Statics.AttributeReadonlyBlacklistGUID[index], false);
        if (attributeType != null)
          Statics.AttributeReadonlyBlacklist[index] = attributeType.AttributeID;
      }
    }
  }

  public static ICategoryTypeIconService ICategoryTypeIconService
  {
    get
    {
      if (Statics.iCategoryTypeIconService == null)
        Statics.iCategoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      return Statics.iCategoryTypeIconService;
    }
  }

  public static ICategoryTypeIconService IconSrv => Statics.ICategoryTypeIconService;
}

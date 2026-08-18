// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Const
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class Const
{
  public static readonly string FullPluginName = "Серверная часть модуля интеграции с внешними системами";
  public static readonly string ConfigName = "ExternalSystemIntegerationConfig";
  public static readonly string LogFileName = "extintegeration.log";
  public static readonly string CompareLogFileName = "extintegerationCompareXml.log";
  public static readonly Guid RequestTaskGuid = new Guid("3D790FFF-EE63-4229-ADE7-FC78A618AF47");
  public static readonly string RequestTaskName = "Обработка исходящих запросов";
  public static readonly Guid ResponceTaskGuid = new Guid("414F6342-147F-4420-8A92-0C7EE95AAD61");
  public static readonly string ResponceTaskName = "Обработка входящих запросов";
  public static int TransfSchemeAttrTypeID = 0;
  public static readonly Guid TransfSchemeAttrTypeGuid = new Guid("cadd956c-306c-11d8-b4e9-00304f19f545");
  public static int NameAttrTypeID = 0;
  public static readonly Guid NameAttrTypeGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static int LinkObjectAttrTypeID = 0;
  public static readonly Guid LinkObjectAttrTypeGuid = new Guid("cad0156a-306c-11d8-b4e9-00304f19f545");
  public static int ObjectTypeIDAttrTypeID = 0;
  public static readonly Guid ObjectTypeIDAttrTypeGUID = new Guid("cad001a0-306c-11d8-b4e9-00304f19f545");
  public static int AttributeComprasionAttrTypeID = 0;
  public static readonly Guid AttributeComprasionAttrTypeGUID = new Guid("cadd958c-306c-11d8-b4e9-00304f19f545");
  public static int RequestSchemeLinkAttrTypeID = 0;
  public static readonly Guid RequestSchemeLinkAttrTypeGUID = new Guid("cadd95b1-306c-11d8-b4e9-00304f19f545");
  public static int ResponceSchemeLinkAttrTypeID = 0;
  public static readonly Guid ResponceSchemeLinkAttrTypeGUID = new Guid("cadd95b0-306c-11d8-b4e9-00304f19f545");
  public static int RequestIDAttrTypeID = 0;
  public static readonly Guid RequestIDAttrTypeGUID = new Guid("cadd959c-306c-11d8-b4e9-00304f19f545");
  public static int ResponceIDAttrTypeID = 0;
  public static readonly Guid ResponceIDAttrTypeGUID = new Guid("cadd95a8-306c-11d8-b4e9-00304f19f545");
  public static int ConfigElementLinkTypeID = 0;
  public static readonly Guid ConfigElementLinkTypeGUID = new Guid("cadd958d-306c-11d8-b4e9-00304f19f545");
  public static int SourceObjectLinkAttrID = 0;
  public static readonly Guid SourceObjectLinkAttrGUID = new Guid("cadd95b4-306c-11d8-b4e9-00304f19f545");
  public static int DestinationObjectLinkAttrID = 0;
  public static readonly Guid DestinationObjectLinkAttrGUID = new Guid("cadd95ba-306c-11d8-b4e9-00304f19f545");
  public static int StatusAttrTypeID = 0;
  public static readonly Guid StatusIDAttrTypeGUID = new Guid("cadd959e-306c-11d8-b4e9-00304f19f545");
  public static int ErrorTextAttrTypeID = 0;
  public static readonly Guid ErrorTextAttrTypeGUID = new Guid("cadd95b8-306c-11d8-b4e9-00304f19f545");
  public static int ShowCardAttrTypeID = 0;
  public static readonly Guid ShowCardAttrTypeGUID = new Guid("cadd95b6-306c-11d8-b4e9-00304f19f545");
  public static int RequestFileNameAttrTypeID = 0;
  public static readonly Guid RequestFileNameAttrTypeGUID = new Guid("cadd95b7-306c-11d8-b4e9-00304f19f545");
  public static int FinderIDTypeID = 0;
  public static readonly Guid FinderIDTypeGUID = new Guid("cadd95b9-306c-11d8-b4e9-00304f19f545");
  public static int ResponceObjTypeID = -1;
  public static readonly Guid ResponceObjTypeGuid = new Guid("cadd9536-306c-11d8-b4e9-00304f19f545");
  public static int RequestObjTypeID = -1;
  public static readonly Guid RequestObjTypeGuid = new Guid("cadd9534-306c-11d8-b4e9-00304f19f545");
  public static int ConfigElementObjTypeID = -1;
  public static readonly Guid ConfigElementObjTypeGuid = new Guid("cadd958a-306c-11d8-b4e9-00304f19f545");
  public static int ResponceConfigObjTypeID = -1;
  public static readonly Guid ResponceConfigObjTypeGuid = new Guid("cadd958f-306c-11d8-b4e9-00304f19f545");
  public static int RequestConfigObjTypeID = -1;
  public static readonly Guid RequestConfigObjTypeGuid = new Guid("cadd9590-306c-11d8-b4e9-00304f19f545");
  public static int TypeSettingItemObjTypeID = -1;
  public static readonly Guid TypeSettingItemObjTypeGuid = new Guid("cadd958e-306c-11d8-b4e9-00304f19f545");
  public static int ResponceSchemeObjTypeID = -1;
  public static readonly Guid ResponceSchemeObjTypeGuid = new Guid("cadd956a-306c-11d8-b4e9-00304f19f545");
  public static int RequestSchemeObjTypeID = -1;
  public static readonly Guid RequestSchemeObjTypeGuid = new Guid("cadd956b-306c-11d8-b4e9-00304f19f545");

  static Const()
  {
    Const.TransfSchemeAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.TransfSchemeAttrTypeGuid);
    Const.NameAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.NameAttrTypeGuid);
    Const.LinkObjectAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.LinkObjectAttrTypeGuid);
    Const.ObjectTypeIDAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.ObjectTypeIDAttrTypeGUID);
    Const.AttributeComprasionAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.AttributeComprasionAttrTypeGUID);
    Const.RequestSchemeLinkAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.RequestSchemeLinkAttrTypeGUID);
    Const.ResponceSchemeLinkAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.ResponceSchemeLinkAttrTypeGUID);
    Const.RequestIDAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.RequestIDAttrTypeGUID);
    Const.ResponceIDAttrTypeID = MetaDataHelper.GetAttributeTypeID(Const.ResponceIDAttrTypeGUID);
    Const.StatusAttrTypeID = MetaDataHelper.GetAttributeID((object) Const.StatusIDAttrTypeGUID);
    Const.ConfigElementLinkTypeID = MetaDataHelper.GetAttributeID((object) Const.ConfigElementLinkTypeGUID);
    Const.ShowCardAttrTypeID = MetaDataHelper.GetAttributeID((object) Const.ShowCardAttrTypeGUID);
    Const.SourceObjectLinkAttrID = MetaDataHelper.GetAttributeID((object) Const.SourceObjectLinkAttrGUID);
    Const.DestinationObjectLinkAttrID = MetaDataHelper.GetAttributeID((object) Const.DestinationObjectLinkAttrGUID);
    Const.RequestFileNameAttrTypeID = MetaDataHelper.GetAttributeID((object) Const.RequestFileNameAttrTypeGUID);
    Const.ErrorTextAttrTypeID = MetaDataHelper.GetAttributeID((object) Const.ErrorTextAttrTypeGUID);
    Const.FinderIDTypeID = MetaDataHelper.GetAttributeID((object) Const.FinderIDTypeGUID);
    Const.ResponceObjTypeID = MetaDataHelper.GetObjectTypeID(Const.ResponceObjTypeGuid);
    Const.RequestObjTypeID = MetaDataHelper.GetObjectTypeID(Const.RequestObjTypeGuid);
    Const.ConfigElementObjTypeID = MetaDataHelper.GetObjectTypeID(Const.ConfigElementObjTypeGuid);
    Const.ResponceConfigObjTypeID = MetaDataHelper.GetObjectTypeID(Const.ResponceConfigObjTypeGuid);
    Const.RequestConfigObjTypeID = MetaDataHelper.GetObjectTypeID(Const.RequestConfigObjTypeGuid);
    Const.TypeSettingItemObjTypeID = MetaDataHelper.GetObjectTypeID(Const.TypeSettingItemObjTypeGuid);
    Const.ResponceSchemeObjTypeID = MetaDataHelper.GetObjectTypeID(Const.ResponceSchemeObjTypeGuid);
    Const.RequestSchemeObjTypeID = MetaDataHelper.GetObjectTypeID(Const.RequestSchemeObjTypeGuid);
  }
}

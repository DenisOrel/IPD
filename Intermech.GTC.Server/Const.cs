// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Const
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.GTC.Server;

public class Const
{
  public static string PluginName = "Серверная часть GTC";
  public static string SettingsName = "GTCSettings";
  public static string ClassifFolderKey = "*6";
  public static Guid CatalogClassifObjGuid = new Guid("cadd98a7-306c-11d8-b4e9-00304f19f545");
  public static int NameAttributeTypeId;
  public static readonly Guid NameAttributeTypeGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static int BsuAttributeTypeId;
  public static readonly Guid BsuAttributeTypeGuid = new Guid("cadd96fa-306c-11d8-b4e9-00304f19f545");
  public static int ClassifFolderKeyAttributeTypeId;
  public static readonly Guid ClassifFolderKeyAttributeTypeGuid = new Guid("cad0014d-306c-11d8-b4e9-00304f19f545");
  public static int LibraryTypeAttributeTypeId;
  public static readonly Guid LibraryTypeAttributeTypeGuid = new Guid("cadd98a1-306c-11d8-b4e9-00304f19f545");
  public static int CoatingTypeAttributeTypeId;
  public static readonly Guid CoatingTypeAttributeTypeGuid = new Guid("cadd98a0-306c-11d8-b4e9-00304f19f545");
  public static int EffectivityTypeAttributeTypeId;
  public static readonly Guid EffectivityTypeAttributeTypeGuid = new Guid("cadd98a2-306c-11d8-b4e9-00304f19f545");
  public static int AttrsRelationshipTypeAttributeTypeId;
  public static readonly Guid AttrsRelationshipTypeAttributeTypeGuid = new Guid("cadd989e-306c-11d8-b4e9-00304f19f545");
  public static int ClassAttrTypeAttributeTypeId;
  public static readonly Guid ClassAttrTypeAttributeTypeGuid = new Guid("cadd989d-306c-11d8-b4e9-00304f19f545");
  public static int GtcLinkAttributeTypeId;
  public static readonly Guid GtcLinkAttributeTypeGuid = new Guid("cadd989f-306c-11d8-b4e9-00304f19f545");
  public static int GtcIdAttributeTypeId;
  public static readonly Guid GtcIdAttributeTypeGuid = new Guid("cadd989c-306c-11d8-b4e9-00304f19f545");
  public static int DateModifAttributeTypeId;
  public static readonly Guid DateModifAttributeTypeGuid = new Guid("cad00702-306c-11d8-b4e9-00304f19f545");
  public static int SortAttributeTypeId;
  public static readonly Guid SortAttributeTypeGuid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
  public static int GtcVersionIdAttributeTypeId;
  public static readonly Guid GtcVersionIdAttributeTypeGuid = new Guid("cadd96ea-306c-11d8-b4e9-00304f19f545");
  public static int GtcOrganizationAttributeTypeId;
  public static readonly Guid GtcOrganizationAttributeTypeGuid = new Guid("cadd96eb-306c-11d8-b4e9-00304f19f545");
  public static int AlternativeIdentiificationAttributeTypeId;
  public static readonly Guid AlternativeIdentiificationAttributeTypeGuid = new Guid("cadd96ec-306c-11d8-b4e9-00304f19f545");
  public static int DescriptionAttributeTypeId;
  public static readonly Guid DescriptionAttributeTypeGuid = new Guid("cad0001c-306c-11d8-b4e9-00304f19f545");
  public static int RepairDateAttributeTypeId;
  public static readonly Guid RepairDateAttributeTypeGuid = new Guid("cad00702-306c-11d8-b4e9-00304f19f545");
  public static int ClassificatorKeyAttributeTypeId;
  public static readonly Guid ClassificatorKeyAttributeTypeGuid = new Guid("cad0014d-306c-11d8-b4e9-00304f19f545");
  public static int ImbaseFolderObjectTypeId;
  public static readonly Guid ImbaseFolderObjectTypeGuid = new Guid("cad00222-306c-11d8-b4e9-00304f19f545");
  public static int BaseItemObjectTypeId;
  public static readonly Guid BaseItemObjectTypeGuid = new Guid("cadd96ca-306c-11d8-b4e9-00304f19f545");
  public static int AdaptiveItemObjectTypeId;
  public static readonly Guid AdaptiveItemTypeGuid = new Guid("cadd96e6-306c-11d8-b4e9-00304f19f545");
  public static int ToolItemObjectTypeId;
  public static readonly Guid ToolItemTypeGuid = new Guid("cadd96e7-306c-11d8-b4e9-00304f19f545");
  public static int CuttingItemObjectTypeId;
  public static readonly Guid CuttingItemTypeGuid = new Guid("cadd96e9-306c-11d8-b4e9-00304f19f545");
  public static int AssemblyItemObjectTypeId;
  public static readonly Guid AssemblyItemTypeGuid = new Guid("cadd96e8-306c-11d8-b4e9-00304f19f545");
  public static int GtcToolObjectTypeId;
  public static readonly Guid GtcToolObjectTypeGuid = new Guid("cadd9722-306c-11d8-b4e9-00304f19f545");
  public static int SimpleWithSortRelationTypeId;
  public static readonly Guid SimpleWithSortRelationTypeGuid = new Guid("cad00151-306c-11d8-b4e9-00304f19f545");

  static Const()
  {
    Const.BsuAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.BsuAttributeTypeGuid);
    Const.ClassifFolderKeyAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.ClassifFolderKeyAttributeTypeGuid);
    Const.LibraryTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.LibraryTypeAttributeTypeGuid);
    Const.NameAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.NameAttributeTypeGuid);
    Const.CoatingTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.CoatingTypeAttributeTypeGuid);
    Const.EffectivityTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.EffectivityTypeAttributeTypeGuid);
    Const.AttrsRelationshipTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.AttrsRelationshipTypeAttributeTypeGuid);
    Const.ClassAttrTypeAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.ClassAttrTypeAttributeTypeGuid);
    Const.GtcLinkAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.GtcLinkAttributeTypeGuid);
    Const.GtcIdAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.GtcIdAttributeTypeGuid);
    Const.SortAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.SortAttributeTypeGuid);
    Const.DateModifAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.DateModifAttributeTypeGuid);
    Const.GtcVersionIdAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.GtcVersionIdAttributeTypeGuid);
    Const.GtcOrganizationAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.GtcOrganizationAttributeTypeGuid);
    Const.AlternativeIdentiificationAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.AlternativeIdentiificationAttributeTypeGuid);
    Const.DescriptionAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.DescriptionAttributeTypeGuid);
    Const.RepairDateAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.RepairDateAttributeTypeGuid);
    Const.ClassificatorKeyAttributeTypeId = MetaDataHelper.GetAttributeTypeID(Const.ClassificatorKeyAttributeTypeGuid);
    Const.ImbaseFolderObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.ImbaseFolderObjectTypeGuid);
    Const.BaseItemObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.BaseItemObjectTypeGuid);
    Const.AdaptiveItemObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.AdaptiveItemTypeGuid);
    Const.ToolItemObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.ToolItemTypeGuid);
    Const.CuttingItemObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.CuttingItemTypeGuid);
    Const.AssemblyItemObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.AssemblyItemTypeGuid);
    Const.GtcToolObjectTypeId = MetaDataHelper.GetObjectTypeID(Const.GtcToolObjectTypeGuid);
    Const.SimpleWithSortRelationTypeId = MetaDataHelper.GetRelationTypeID(Const.SimpleWithSortRelationTypeGuid);
  }
}

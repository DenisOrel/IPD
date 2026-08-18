// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IPSAttributeLocalizer
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Локализатор ресурсов PDM-системы для CAD-интерфейса.</summary>
public sealed class IPSAttributeLocalizer : IAttributeLocalizer
{
  private readonly Lazy<Dictionary<string, EAttributeID>> reverseTable;

  public IPSAttributeLocalizer()
  {
    this.reverseTable = new Lazy<Dictionary<string, EAttributeID>>(new Func<Dictionary<string, EAttributeID>>(this.InitializeReverseTable));
  }

  private Dictionary<string, EAttributeID> InitializeReverseTable()
  {
    Array values = Enum.GetValues(typeof (EAttributeID));
    Dictionary<string, EAttributeID> dictionary = new Dictionary<string, EAttributeID>(values.Length, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase);
    foreach (EAttributeID ID in values)
    {
      string attributeNameById = this.GetAttributeNameByID(ID);
      dictionary.Add(attributeNameById, ID);
    }
    return dictionary;
  }

  public string ATTR_DocumentType => CADDocumentResources.EMB_DocumentTypeAttribute;

  public string GetAttributeNameByID(EAttributeID ID)
  {
    switch (ID)
    {
      case EAttributeID.ATTR_Name:
        return IDCache.Default.Name.Text;
      case EAttributeID.ATTR_Designation:
        return IDCache.Default.Designation.Text;
      case EAttributeID.ATTR_OKPCode:
        return IDCache.Default.OKPCode.Text;
      case EAttributeID.ATTR_SPSection:
        return CADDocumentResources.EMB_ArticleTypeAttribute;
      case EAttributeID.ATTR_IMBaseKey:
        return IDCache.Default.ImbaseKey.Text;
      case EAttributeID.ATTR_Material:
        return IDCache.Default.Material.Text;
      case EAttributeID.ATTR_Mass:
        return IDCache.Default.Mass.Text;
      case EAttributeID.ATTR_Position:
        return IDCache.Default.Position.Text;
      case EAttributeID.ATTR_Comment:
        return IDCache.Default.Note.Text;
      case EAttributeID.ATTR_Format:
        return IDCache.Default.Format.Text;
      case EAttributeID.ATTR_Quantity:
        return IDCache.Default.Count.Text;
      case EAttributeID.ATTR_SubstitutesGroupNumber:
        return "Subst_Group_Num";
      case EAttributeID.ATTR_SubstituteNumber:
        return "Subst_Num";
      case EAttributeID.ATTR_Modification:
        return CADDocumentResources.EMB_ModificationAttribute;
      case EAttributeID.ATTR_Zone:
        return IDCache.Default.Zone.Text;
      case EAttributeID.ATTR_GUID:
        return CADDocumentResources.EMB_OccurenceGuidAttribute;
      case EAttributeID.ATTR_PDMFlag:
        return CADDocumentResources.EMB_PDMFlagAttribute;
      case EAttributeID.ATTR_DocumentCode:
        return CADDocumentResources.EMB_DocumentCode;
      case EAttributeID.ATTR_PrimaryApplication:
        return CADDocumentResources.EMB_PrimaryApplication;
      case EAttributeID.ATTR_NotForAVS:
        return CADDocumentResources.EMB_IgnoreConfiguration;
      case EAttributeID.ATTR_MaterialID:
        return CADDocumentResources.EMB_MaterialID;
      case EAttributeID.ATTR_VersionNumber:
        return CADDocumentResources.EMB_VersionNumber;
      case EAttributeID.ATTR_IsVirtual:
        return CADDocumentResources.EMB_IsVirtualObject;
      case EAttributeID.ATTR_ArticleBillet:
        return CADDocumentResources.EMB_ArticleBillet;
      case EAttributeID.ATTR_UnitMass:
        return CADDocumentResources.EMB_UnitMass;
      case EAttributeID.ATTR_AppConditionsGUID:
        return CADDocumentResources.EMB_AppConditionsGUID;
      case EAttributeID.ATTR_ReplaceByConfiguration:
        return CADDocumentResources.EMB_ReplaceWithAttribute;
      case EAttributeID.ATTR_AttrsCount:
        return CADDocumentResources.EMB_AttributesCount;
      default:
        throw new NotSupportedEnumException((Enum) ID, string.Format(LocalizationHolder.rm.GetString("Tools.Components_340"), (object) ID));
    }
  }

  public EAttributeID? GetAttributeIDByName(string name)
  {
    if (string.IsNullOrEmpty(name))
      return new EAttributeID?();
    EAttributeID eattributeId;
    return !this.reverseTable.Value.TryGetValue(name, out eattributeId) ? new EAttributeID?() : new EAttributeID?(eattributeId);
  }
}

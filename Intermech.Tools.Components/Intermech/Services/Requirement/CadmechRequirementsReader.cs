// Decompiled with JetBrains decompiler
// Type: Intermech.Services.Requirement.CadmechRequirementsReader
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Services.Requirement;

internal sealed class CadmechRequirementsReader
{
  public List<RequirementExternalData> ReadRequirementData(IIMTextDocumentProvider cadDocument)
  {
    if (cadDocument == null)
      throw new ArgumentNullException(nameof (cadDocument));
    List<RequirementExternalData> requirementExternalDataList = new List<RequirementExternalData>();
    this.ReadRequirementsList(cadDocument, requirementExternalDataList);
    return requirementExternalDataList;
  }

  /// <summary>Прочитать требования из документа</summary>
  /// <param name="imtextDocumentProvider"></param>
  /// <param name="requirementExternalDataList"></param>
  private void ReadRequirementsList(
    IIMTextDocumentProvider imtextDocumentProvider,
    List<RequirementExternalData> requirementExternalDataList)
  {
    IMTextDocumentProxy textDocumentProxy = (IMTextDocumentProxy) null;
    IMTextAttributeManagerProxy attributeManagerProxy = (IMTextAttributeManagerProxy) null;
    try
    {
      textDocumentProxy = imtextDocumentProvider.GetIMTextDocument(true);
      attributeManagerProxy = textDocumentProxy.GetAttrManager();
      foreach (IMTextFaceAttributeProxy imtextAttribute in attributeManagerProxy.GetAllFaceAttrsByType(IMTextFaceAttributeType.Tt))
      {
        Guid guid = this.ParseGuid((object) imtextAttribute.GUID, Guid.Empty);
        RequirementExternalData requirementExternalData = this.ReadRequirementsData(imtextAttribute);
        requirementExternalData.AnchorGuid = guid;
        requirementExternalDataList.Add(requirementExternalData);
      }
    }
    finally
    {
      if (attributeManagerProxy != null)
        Marshal.FinalReleaseComObject((object) attributeManagerProxy.RawObject);
      if (textDocumentProxy != null)
        Marshal.FinalReleaseComObject((object) textDocumentProxy.RawObject);
    }
  }

  /// <summary>Прочитать данные тех требоаний из атрибута</summary>
  /// <param name="imtextAttribute"></param>
  /// <returns></returns>
  private RequirementExternalData ReadRequirementsData(IMTextFaceAttributeProxy imtextAttribute)
  {
    TechnicalRequirementsAttributeAdapter attributeAdapter = imtextAttribute.AsTechnicalRequirements();
    string[] items = attributeAdapter.GetItems();
    RequirementExternalData requirementExternalData = new RequirementExternalData();
    if (items != null)
    {
      foreach (string str in items)
      {
        Intermech.Services.Requirement.Requirement requirement = new Intermech.Services.Requirement.Requirement()
        {
          Guid = str,
          Text = attributeAdapter.GetItemText(str)
        };
        requirement.Refs = attributeAdapter.GetExtRefs(requirement.Text);
        requirement.Index = attributeAdapter.GetItemIndex(str);
        requirementExternalData.Requirements.Add(requirement);
      }
    }
    return requirementExternalData;
  }

  private Guid ParseGuid(object value, Guid defaultValue)
  {
    return Guid.Parse(TextServices.Trim((string) value));
  }
}

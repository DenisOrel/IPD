// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.CheckBaseMaterialAttributeState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class CheckBaseMaterialAttributeState : IAttributeAnalyzerState
{
  public void Handle(SynchronizationAttributesAnalyzer context)
  {
    this.CheckBaseMaterialAttribute(context);
  }

  private void CheckBaseMaterialAttribute(SynchronizationAttributesAnalyzer context)
  {
    if (context.DifferentAttributeValues.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD)) != null)
      return;
    AttributeValues baseMaterialAttr = context.DifferentAttributeValues.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID));
    if (baseMaterialAttr == null)
      return;
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(context.SourceObject.ObjectType);
    AttributeValues attributeValues = context.SourceAttributeValues.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD));
    if (attributeValues != null)
    {
      long result;
      if (attributeValues.Values.Length != 0 && (attributeValues.Values[0] == null || attributeValues.Values[0] == DBNull.Value || !long.TryParse(Convert.ToString(attributeValues.Values[0]), out result) || result == 0L))
        this.AddMaterialGradeValue(context, baseMaterialAttr);
    }
    else if (attribute4ObjectTypeList.Any<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD)))
      this.AddMaterialGradeValue(context, baseMaterialAttr);
    if (!context.DifferentAttributeValues.Contains(baseMaterialAttr) || !attribute4ObjectTypeList.All<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID != Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID)))
      return;
    context.DifferentAttributeValues.Remove(baseMaterialAttr);
  }

  private void AddMaterialGradeValue(
    SynchronizationAttributesAnalyzer context,
    AttributeValues baseMaterialAttr)
  {
    long materailGradeValue = this.GetMaterailGradeValue(context.Session, baseMaterialAttr);
    if (materailGradeValue == 0L)
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD);
    AttributeValues attributeValues = new AttributeValues(attributeType.AttributeID, (object) materailGradeValue)
    {
      AttributeName = attributeType.Name,
      AttributeGuid = attributeType.AttributeGuid
    };
    context.DifferentAttributeValues.Add(attributeValues);
    context.Log.AddMessage(MessageType.Extended, $"{Environment.NewLine}Обработка атрибута 'Основной материал': добавлен атрибут 'Марка материала' = '{materailGradeValue}'");
  }

  private long GetMaterailGradeValue(IUserSession session, AttributeValues baseMaterialAttr)
  {
    long materailGradeValue = 0;
    long linkId;
    long recordId;
    if (ImbaseHelper.TryParseRecordReference(session, Convert.ToString(baseMaterialAttr.Values[0]), out linkId, out recordId))
      materailGradeValue = ServiceUtils.GetService<IImbaseServer>((object) ServerServices.ServiceContainer, true).CreateObject(session.SessionGUID, -1L, linkId, recordId, true, -1);
    return materailGradeValue;
  }
}

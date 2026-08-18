// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.SynchronizationAttributesAnalyzer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class SynchronizationAttributesAnalyzer
{
  private HashSet<int> _systemAttributeIds;
  private List<int> _skipAttributes;
  public HashSet<AttributeValues> SourceAttributeValues;

  public IDBObject SourceObject { get; }

  public IUserSession Session { get; }

  public long ImbaseObjectId { get; }

  public long ImbaseRecordId { get; }

  public HashSet<IMSAttribute4ObjectType> ComparedAttributes { get; }

  public HashSet<AttributeValues> DifferentAttributeValues { get; }

  public IAttributeAnalyzerState State { get; set; }

  public bool FinishAnalyze => !this.ComparedAttributes.Any<IMSAttribute4ObjectType>();

  public GetAttributeValuesModes AttributeValuesModes { get; } = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess;

  public ILogSupport Log { get; }

  public List<string> ProcessedLinksToRecs { get; } = new List<string>();

  public List<long> ProcessedLinksToObj { get; } = new List<long>();

  public List<int> NotExpandableAttributes { get; } = new List<int>();

  public SynchronizationAttributesAnalyzer(
    IUserSession session,
    HashSet<int> systemAttributeIds,
    IDBObject sourceObject,
    long imbaseObjId,
    long recId,
    ILogSupport log)
  {
    this._systemAttributeIds = systemAttributeIds;
    IImbaseParamsService service = ApplicationServices.Container.GetService<IImbaseParamsService>();
    this._skipAttributes = this.GetAttributeList(service.CommonParams.SkipAttributes, sourceObject.ObjectType);
    this.NotExpandableAttributes = this.GetAttributeList(service.CommonParams.NotExpandableAttributes, sourceObject.ObjectType);
    this.Log = log;
    this.Session = session;
    this.SourceObject = sourceObject;
    this.ImbaseObjectId = imbaseObjId;
    this.ImbaseRecordId = recId;
    this.SourceAttributeValues = new HashSet<AttributeValues>((IEnumerable<AttributeValues>) this.SourceObject.GetAttributesValues(this.AttributeValuesModes));
    this.ComparedAttributes = this.GetAttributesForObjectType(this.SourceObject.ObjectType);
    this.DifferentAttributeValues = new HashSet<AttributeValues>();
    if (session.GetObjectInfo(imbaseObjId).ObjectTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID)
      this.State = (IAttributeAnalyzerState) new HierarchyCompareState();
    else
      this.State = (IAttributeAnalyzerState) new RowCompareState();
  }

  public void Analyze() => this.State.Handle(this);

  private List<int> GetAttributeList(
    List<AttributeForObjectTypeInfo> attributeForObjectTypes,
    int objTypeId)
  {
    List<int> attributeList = new List<int>();
    foreach (AttributeForObjectTypeInfo attributeForObjectType in attributeForObjectTypes)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objTypeId, attributeForObjectType.ObjectTypeId) || attributeForObjectType.ObjectTypeId == -1)
        attributeList.Add(attributeForObjectType.AttrTypeId);
    }
    return attributeList;
  }

  private HashSet<IMSAttribute4ObjectType> GetAttributesForObjectType(int objTypeID)
  {
    HashSet<IMSAttribute4ObjectType> source = (HashSet<IMSAttribute4ObjectType>) null;
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objTypeID);
    if (attribute4ObjectTypeList != null && attribute4ObjectTypeList.Count > 0)
    {
      source = new HashSet<IMSAttribute4ObjectType>();
      foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
      {
        int attributeId = attribute4ObjectType.AttributeID;
        if (attributeId >= 0 && attribute4ObjectType.Computed == ComputeValueModes.NotComputableValue && !this.IsSystemAttribute(attributeId) && !this.IsSkipAttribute(attributeId))
          source.Add(attribute4ObjectType);
      }
      IMSAttribute4ObjectType attribute4ObjectType1 = source.FirstOrDefault<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD));
      AttributeValues attributeValues = this.SourceAttributeValues.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD));
      if ((attribute4ObjectType1 != null || attributeValues != null) && source.FirstOrDefault<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID)) == null)
      {
        HashSet<IMSAttribute4ObjectType> attribute4ObjectTypeSet = source;
        IMSAttribute4ObjectType attribute4ObjectType2 = new IMSAttribute4ObjectType();
        attribute4ObjectType2.AttributeID = Intermech.Imbase.Consts.ImbaseBaseMaterialAttrID;
        attribute4ObjectType2.ObjectTypeID = objTypeID;
        attribute4ObjectType2.IsContent = attribute4ObjectType1 != null ? attribute4ObjectType1.IsContent : MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.ImbaseMaterialGradeAttrD).IsContent;
        attribute4ObjectTypeSet.Add(attribute4ObjectType2);
      }
    }
    return source != null && source.Count != 0 ? source : throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Synch_ObjType_Attrs_Empty"));
  }

  private bool IsSystemAttribute(int attId) => this._systemAttributeIds.Contains(attId);

  private bool IsSkipAttribute(int attId)
  {
    if (!this._skipAttributes.Contains(attId))
      return false;
    this.Log.AddMessage(MessageType.Extended, $"Атрибут {MetaDataHelper.GetAttributeTypeName(attId)} [{attId}] не обрабатывается при синхронизации, т.к. находится в списке игнорируемых атрибутов");
    return true;
  }
}

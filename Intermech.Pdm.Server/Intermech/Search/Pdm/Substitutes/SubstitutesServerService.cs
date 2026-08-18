// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutesServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

public sealed class SubstitutesServerService : LongLifeObject, ISubstitutesServerService
{
  private LazyService<IAttributeTypeForRelationRepository> _attributeTypeForRelationRepository = new LazyService<IAttributeTypeForRelationRepository>();
  private LazyService<IRelationRepository> _relationRepository = new LazyService<IRelationRepository>();
  private LazyService<IObjectRepository> _objectRepository = new LazyService<IObjectRepository>();
  private LazyService<IObjectTypeApplicabilityRepository> _objectTypeApplicabilityRepository = new LazyService<IObjectTypeApplicabilityRepository>();

  public SubstitutePack FindSubstitutes(
    Guid userSessionGuid,
    long projectVersionID,
    int relationTypeID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(projectVersionID))
      throw new ArgumentException();
    if (relationTypeID == -1 || !SubstitutesHelper.IsSuitableForSubstitutesRelationType(relationTypeID))
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.FindSubstitutesInternal(projectVersionID, relationTypeID);
  }

  public void ActualizeSubstitute(Guid userSessionGuid, long relationID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (RelationHelper.IsUnknownRelationID(relationID))
        throw new ArgumentException();
      this.ActualizeSubstituteInternal(relationID);
    }
  }

  public AnalyzeSaveSubsitutesResult AnalyzeSaveSubstitutes(
    Guid userSessionGuid,
    SaveSubstitutesParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (@params == null)
      throw new ArgumentNullException("@params");
    if (!SaveSubstitutesParams.Check(@params))
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.AnalyzeSaveSubstitutesInternal(@params);
  }

  public void SaveSubstitutes(Guid userSessionGuid, SaveSubstitutesParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (@params == null)
      throw new ArgumentNullException("@params");
    if (!SaveSubstitutesParams.Check(@params))
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      this.SaveSubstitutes(@params);
  }

  public void RemoveSubstitutes(Guid userSessionGuid, RemoveSubstitutesParams @params)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (@params == null)
      throw new ArgumentNullException("@params");
    if (!RemoveSubstitutesParams.Check(@params))
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      this.RemoveSubstitutesInternal(@params);
  }

  public long[] GetExistsSubstituteGroupNumbersFromOtherInstances(
    Guid userSessionGuid,
    long objectVersionID,
    int relationTypeID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (RelationTypeHelper.IsUnknownRelationTypeID(relationTypeID))
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetExistsSubstituteGroupNumbersFromOtherInstances(objectVersionID, relationTypeID);
  }

  private SubstitutePack FindSubstitutesInternal(long projectVersionID, int relationTypeID)
  {
    return SubstitutesHelper.CreatePackFromRelations((IEnumerable<Relation>) this.FindRelations(projectVersionID, relationTypeID));
  }

  private void ActualizeSubstituteInternal(long relationID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation1 = sessionKeeper.Session.GetRelation(relationID);
      AttributeValues[] attributesValues = relation1.GetAttributesValues(GetAttributeValuesModes.None);
      long int64_1 = Convert.ToInt64(((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((System.Func<AttributeValues, bool>) (o => o.AttributeID == SubstitutesConstants.SubstituteGroupNumberAttributeTypeID)).Select<AttributeValues, object>((System.Func<AttributeValues, object>) (o => o.Values == null || o.Values.Length == 0 ? (object) null : o.Values[0])).FirstOrDefault<object>() ?? throw new Exception("Не удается актуализировать заменитель, на связи не найден идентификатор группы заменителей"));
      long int64_2 = Convert.ToInt64(((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((System.Func<AttributeValues, bool>) (o => o.AttributeID == SubstitutesConstants.SubstituteNumberAttributeTypeID)).Select<AttributeValues, object>((System.Func<AttributeValues, object>) (o => o.Values == null || o.Values.Length == 0 ? (object) null : o.Values[0])).FirstOrDefault<object>() ?? throw new Exception("Не удается актуализировать заменитель, на связи не найден идентификатор заменителя"));
      if (int64_2 == 0L)
        throw new Exception("Не удается актуализировать заменитель, он уже является актуальным");
      SubstituteGroup group = this.FindSubstitutesInternal(relation1.ProjID, relation1.RelationType).Groups[int64_1];
      if (group == null)
        throw new Exception();
      List<Relation> relationList = new List<Relation>();
      foreach (SubstitutePosition position in group.Substitutes[0L].Positions)
      {
        Relation relation2 = new Relation();
        relation2.ID = position.RelationID;
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) int64_2));
        relationList.Add(relation2);
      }
      foreach (SubstitutePosition position in group.Substitutes[int64_2].Positions)
      {
        Relation relation3 = new Relation();
        relation3.ID = position.RelationID;
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) 0L));
        relationList.Add(relation3);
      }
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      customService.StartTransaction();
      try
      {
        foreach (Relation relation4 in relationList)
          this._relationRepository.Value.AddOrUpdate(relation4);
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private AnalyzeSaveSubsitutesResult AnalyzeSaveSubstitutesInternal(SaveSubstitutesParams @params)
  {
    AnalyzeSaveSubsitutesResult subsitutesResult = new AnalyzeSaveSubsitutesResult();
    List<Relation> relations = this.FindRelations(@params);
    AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack subsitutesChangesPack1 = this.AnalyzeSaveSubstitutesInternal(@params, (IEnumerable<Relation>) relations);
    subsitutesResult.ChangesPackDictionary[@params.ProjectVersionID] = subsitutesChangesPack1;
    if (@params.InstanceVersionIds != null)
    {
      foreach (long num in ((IEnumerable<long>) @params.InstanceVersionIds).Where<long>((System.Func<long, bool>) (o => o != @params.ProjectVersionID)).Distinct<long>())
      {
        AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack subsitutesChangesPack2 = this.AnalyzeSaveSubstitutesInternal(@params, num, (IEnumerable<Relation>) relations);
        subsitutesResult.ChangesPackDictionary[num] = subsitutesChangesPack2;
      }
    }
    return subsitutesResult;
  }

  private void SaveSubstitutes(SaveSubstitutesParams @params)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      customService.StartTransaction();
      try
      {
        List<Relation> relations = this.FindRelations(@params);
        AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack saveSubstitutesChangesPack1 = this.AnalyzeSaveSubstitutesInternal(@params, (IEnumerable<Relation>) relations);
        this.SaveSubstitutesInternal(@params.ProjectVersionID, saveSubstitutesChangesPack1);
        if (@params.InstanceVersionIds != null)
        {
          foreach (long projectVersionID in ((IEnumerable<long>) @params.InstanceVersionIds).Where<long>((System.Func<long, bool>) (o => o != @params.ProjectVersionID)).Distinct<long>())
          {
            AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack saveSubstitutesChangesPack2 = this.AnalyzeSaveSubstitutesInternal(@params, projectVersionID, (IEnumerable<Relation>) relations);
            this.SaveSubstitutesInternal(projectVersionID, saveSubstitutesChangesPack2);
          }
        }
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
      foreach (SubstituteGroup group in (Collection<SubstituteGroup>) @params.Pack.Groups)
      {
        foreach (Substitute substitute in (Collection<Substitute>) group.Substitutes)
        {
          foreach (SubstitutePosition position in substitute.Positions)
          {
            if (!RelationHelper.IsUnknownRelationID(position.RelationID))
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(position.RelationID, false);
              if (relation != null)
              {
                ObjectsApplicabilitiesCriterionsCollection criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection();
                criterionsCollection.LoadFromObject((IDBAttributable) relation);
                if (criterionsCollection.Count > 0)
                {
                  criterionsCollection.SaveToObject((IDBAttributable) relation);
                  break;
                }
              }
            }
          }
        }
      }
    }
  }

  private void RemoveSubstitutesInternal(RemoveSubstitutesParams @params)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      customService.StartTransaction();
      try
      {
        long num = @params.ProjectVersionID;
        bool flag = false;
        try
        {
          if (this.NeedCheckout(num, @params.RelationTypeID))
          {
            num = this.Checkout(num);
            flag = true;
          }
          foreach (long instansesVersionId in this.FindInstansesVersionIds(num))
            this.RemoveSubstitutesInternal(instansesVersionId, @params.RelationTypeID, @params.DeleteAuxiliaryPositionRelations);
        }
        finally
        {
          if (flag)
            this.Checkin(num);
        }
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack AnalyzeSaveSubstitutesInternal(
    SaveSubstitutesParams @params,
    IEnumerable<Relation> relations)
  {
    List<SubstitutePosition> substitutePositions = this.GetValidSubstitutePositions(@params.Pack, relations);
    return this.AnalyzeSaveSubstitutesInternal(@params.ProjectVersionID, @params.RelationTypeID, (IEnumerable<SubstitutePosition>) substitutePositions, relations, @params.GroupsAffected);
  }

  private AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack AnalyzeSaveSubstitutesInternal(
    SaveSubstitutesParams @params,
    long projectVersionID,
    IEnumerable<Relation> mainInstanceRelations)
  {
    List<Relation> relations = this.FindRelations(projectVersionID, @params);
    List<Relation> relationList1 = new List<Relation>();
    List<Relation> relationList2 = new List<Relation>();
    foreach (Relation relation1 in relations)
    {
      Relation relation = relation1;
      if (mainInstanceRelations.Where<Relation>((System.Func<Relation, bool>) (o => Math.Abs(o.PartID) == Math.Abs(relation.PartID))).Count<Relation>() > 0)
        relationList1.Add(relation);
      else
        relationList2.Add(relation);
    }
    List<SubstitutePosition> substitutePositions1 = this.GetValidSubstitutePositions(@params.Pack, (IEnumerable<Relation>) relationList1);
    SubstitutePack packFromRelations = SubstitutesHelper.CreatePackFromRelations((IEnumerable<Relation>) relationList2);
    if (packFromRelations != null)
    {
      List<SubstitutePosition> substitutePositions2 = this.GetValidSubstitutePositions(packFromRelations, (IEnumerable<Relation>) relationList2);
      substitutePositions1.AddRange((IEnumerable<SubstitutePosition>) substitutePositions2);
    }
    List<SubstitutePosition> list = this.FindSubstitutesInternal(projectVersionID, @params.RelationTypeID).GetPositions().Where<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (o => @params.Pack.Groups.Any<SubstituteGroup>((System.Func<SubstituteGroup, bool>) (oo => oo.Number != o.Substitute.Group.Number)))).ToList<SubstitutePosition>();
    foreach (SubstitutePosition substitutePosition1 in list)
    {
      SubstitutePosition seldSubstitutesPosition = substitutePosition1;
      SubstitutePosition substitutePosition2 = substitutePositions1.FirstOrDefault<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (o => o.ObjectID == seldSubstitutesPosition.ObjectID && o.Substitute.Number == seldSubstitutesPosition.Substitute.Number && o.Substitute.Group.Number == seldSubstitutesPosition.Substitute.Group.Number));
      if (substitutePosition2 != null)
        substitutePositions1.Remove(substitutePosition2);
    }
    IEnumerable<SubstitutePosition> collection = list.Where<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (sp => @params.Pack.Groups.Any<SubstituteGroup>((System.Func<SubstituteGroup, bool>) (g => g.Number == sp.Substitute.Group.Number))));
    substitutePositions1.AddRange(collection);
    return @params.Pack.Groups.Count == 0 || substitutePositions1.Count > 0 ? this.AnalyzeSaveSubstitutesInternal(projectVersionID, @params.RelationTypeID, (IEnumerable<SubstitutePosition>) substitutePositions1, (IEnumerable<Relation>) relations, @params.GroupsAffected) : new AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack();
  }

  private AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack AnalyzeSaveSubstitutesInternal(
    long projectVersionID,
    int relationTypeID,
    IEnumerable<SubstitutePosition> substitutePositions,
    IEnumerable<Relation> relations,
    Dictionary<long, string> groupsAffected = null)
  {
    AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack saveSubstitutesChangesPack = new AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack();
    List<SubstitutePosition> preparedSubstitutePositions = new List<SubstitutePosition>();
    foreach (Relation relation1 in relations.ToList<Relation>())
    {
      Relation relation = relation1;
      object attributeValue1 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID);
      string attributeValue2 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNameAttributeTypeID) as string;
      object attributeValue3 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNumberAttributeTypeID);
      string attributeValue4 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNameAttributeTypeID) as string;
      object attributeValue5 = relation.Attributes.GetAttributeValue(SubstitutesConstants.DesignActualVariantAttributeTypeID);
      object.Equals(attributeValue5, (object) 1L);
      bool flag = relation.Attributes.HasAttribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID);
      SubstitutePosition substitutePosition = substitutePositions.FirstOrDefault<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (o => o.RelationID == relation.ID && !preparedSubstitutePositions.Contains(o))) ?? substitutePositions.FirstOrDefault<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (o => o.ObjectID == relation.PartID && !relations.Any<Relation>((System.Func<Relation, bool>) (oo => oo.ID == o.RelationID)) && !preparedSubstitutePositions.Contains(o)));
      if (substitutePosition == null)
      {
        if (((attributeValue1 != null || attributeValue2 != null || attributeValue3 != null || attributeValue4 != null ? 1 : (attributeValue5 != null ? 1 : 0)) | (flag ? 1 : 0)) != 0)
        {
          if (attributeValue1 != null && attributeValue3 != null)
          {
            long int64 = Convert.ToInt64(attributeValue1);
            if (groupsAffected == null || groupsAffected.Keys.Contains<long>(int64) || groupsAffected.Values.Contains<string>(attributeValue2))
            {
              if (relations.Any<Relation>((System.Func<Relation, bool>) (o => o.PartID == relation.PartID && o.ID != relation.ID && !saveSubstitutesChangesPack.ToRemoveRelationIds.Contains(o.ID))))
                saveSubstitutesChangesPack.ToRemoveRelationIds.Add(relation.ID);
              else
                saveSubstitutesChangesPack.ToClearRelationIds.Add(relation.ID);
            }
          }
          else
            saveSubstitutesChangesPack.ToClearRelationIds.Add(relation.ID);
        }
      }
      else
      {
        Relation relation2 = new Relation();
        relation2.ID = relation.ID;
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) substitutePosition.Substitute.Group.Number));
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) substitutePosition.Substitute.Group.Name));
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) substitutePosition.Substitute.Number));
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) substitutePosition.Substitute.Name));
        relation2.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) substitutePosition.Number));
        if (substitutePosition.IsAuxiliary)
          relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) 3L));
        else if (substitutePosition.IsEqual)
          relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) 4L));
        else
          relation2.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) null));
        if (substitutePosition.Substitute.IsDesignerActualVariant)
          relation2.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) 1L));
        else
          relation2.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
        saveSubstitutesChangesPack.ToChangeRelations.Add(relation2);
        preparedSubstitutePositions.Add(substitutePosition);
      }
    }
    foreach (SubstitutePosition substitutePosition1 in substitutePositions)
    {
      SubstitutePosition substitutePosition = substitutePosition1;
      if (!preparedSubstitutePositions.Contains(substitutePosition) && relations.Where<Relation>((System.Func<Relation, bool>) (o => o.PartID == substitutePosition.ObjectID)).Count<Relation>() != 0)
      {
        Relation relation3 = new Relation();
        relation3.TypeID = relationTypeID;
        relation3.ProjectVersionID = projectVersionID;
        relation3.PartID = substitutePosition.ObjectID;
        if (!ObjectHelper.IsUnknownObjectVersionID(substitutePosition.ObjectVersionID))
          relation3.PartVersionID = substitutePosition.ObjectVersionID;
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) substitutePosition.Substitute.Group.Number));
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) substitutePosition.Substitute.Group.Name));
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) substitutePosition.Substitute.Number));
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) substitutePosition.Substitute.Name));
        relation3.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) substitutePosition.Number));
        if (substitutePosition.IsAuxiliary)
          relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) 3L));
        else if (substitutePosition.IsEqual)
          relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) 4L));
        else
          relation3.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) null));
        if (substitutePosition.Substitute.IsDesignerActualVariant)
          relation3.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) 1L));
        else
          relation3.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
        if (!RelationHelper.IsUnknownRelationID(substitutePosition.RelationID) && this.IsExistingRelation(substitutePosition.RelationID))
        {
          relation3.Attributes.Add(new _Attribute(ObligatoryObjectAttributes.F_INTEGER_VALUE, (object) substitutePosition.RelationID));
        }
        else
        {
          Relation relation4 = relations.FirstOrDefault<Relation>((System.Func<Relation, bool>) (o => Math.Abs(o.PartID) == Math.Abs(substitutePosition.ObjectID)));
          if (relation4 != null)
            relation3.Attributes.Add(new _Attribute(ObligatoryObjectAttributes.F_INTEGER_VALUE, (object) relation4.ID));
        }
        saveSubstitutesChangesPack.ToAddRelations.Add(relation3);
      }
    }
    return saveSubstitutesChangesPack;
  }

  private List<SubstitutePosition> GetValidSubstitutePositions(
    SubstitutePack substitutePack,
    IEnumerable<Relation> relations)
  {
    List<SubstitutePosition> substitutePositions = new List<SubstitutePosition>();
    foreach (SubstituteGroup group in (Collection<SubstituteGroup>) substitutePack.Groups)
    {
      if (this.IsValidSubstituteGroup(relations, group))
        substitutePositions.AddRange(group.GetPositions());
      else if (group.Substitutes.Where<Substitute>((System.Func<Substitute, bool>) (o => this.IsValidSubstitute(relations, o))).Count<Substitute>() >= 2 && group.Substitutes.Where<Substitute>((System.Func<Substitute, bool>) (o => o.Type == SubstituteType.Actual && this.IsValidSubstitute(relations, o))).Count<Substitute>() == 1)
      {
        foreach (Substitute substitute in group.Substitutes.Where<Substitute>((System.Func<Substitute, bool>) (o => this.IsValidSubstitute(relations, o))))
          substitutePositions.AddRange((IEnumerable<SubstitutePosition>) substitute.Positions);
      }
    }
    return substitutePositions;
  }

  private bool IsExistingRelation(long relationID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelation(relationID, false) != null;
  }

  private bool IsValidSubstitutePosition(
    IEnumerable<Relation> relations,
    SubstitutePosition position)
  {
    return relations.Where<Relation>((System.Func<Relation, bool>) (o => o.PartID == position.ObjectID)).Count<Relation>() > 0;
  }

  private bool IsValidSubstitute(IEnumerable<Relation> relations, Substitute substitute)
  {
    return substitute.Positions.Where<SubstitutePosition>((System.Func<SubstitutePosition, bool>) (o => !this.IsValidSubstitutePosition(relations, o))).Count<SubstitutePosition>() == 0;
  }

  private bool IsValidSubstituteGroup(
    IEnumerable<Relation> relations,
    SubstituteGroup substituteGroup)
  {
    return substituteGroup.Substitutes.Count >= 2 && substituteGroup.Substitutes.Where<Substitute>((System.Func<Substitute, bool>) (o => o.Type == SubstituteType.Actual)).Count<Substitute>() != 0 && substituteGroup.Substitutes.Where<Substitute>((System.Func<Substitute, bool>) (o => !this.IsValidSubstitute(relations, o))).Count<Substitute>() == 0;
  }

  private List<Relation> FindRelations(SaveSubstitutesParams saveSubstitutesParams)
  {
    return this.FindRelations(saveSubstitutesParams.ProjectVersionID, saveSubstitutesParams);
  }

  private List<Relation> FindRelations(
    long projectVersionID,
    SaveSubstitutesParams saveSubstitutesParams)
  {
    return this.FindRelations(projectVersionID, saveSubstitutesParams.RelationTypeID);
  }

  private List<Relation> FindRelations(long projectVersionID, int relationTypeID)
  {
    IRelationRepository relationRepository = this._relationRepository.Value;
    FindRelationsParams findRelationsParams = new FindRelationsParams();
    findRelationsParams.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure()
      {
        Attribute = (object) ObligatoryObjectAttributes.F_PROJ_ID,
        RelationalOperator = RelationalOperators.Equal,
        Value = (object) projectVersionID,
        SQL = ""
      }
    };
    findRelationsParams.DisableFiltration = true;
    findRelationsParams.RelationTypeID = relationTypeID;
    FindRelationsParams @params = findRelationsParams;
    return relationRepository.Find(@params);
  }

  private void RemoveSubstitutesInternal(
    long objectVersionID,
    int relationTypeID,
    bool deleteAuxiliaryPositionRelations)
  {
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<long> longList3 = new List<long>();
    foreach (Relation relation in this.FindRelations(objectVersionID, relationTypeID))
    {
      object attributeValue1 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID);
      string attributeValue2 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNameAttributeTypeID) as string;
      object attributeValue3 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNumberAttributeTypeID);
      string attributeValue4 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNameAttributeTypeID) as string;
      object attributeValue5 = relation.Attributes.GetAttributeValue(SubstitutesConstants.DesignActualVariantAttributeTypeID);
      bool flag = relation.Attributes.HasAttribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID);
      if (((attributeValue1 != null || attributeValue2 != null || attributeValue3 != null || attributeValue4 != null ? 1 : (attributeValue5 != null ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        if (attributeValue1 != null && attributeValue3 != null)
        {
          if (longList3.Contains(relation.PartID))
          {
            if (deleteAuxiliaryPositionRelations)
              longList1.Add(relation.ID);
            else
              longList2.Add(relation.ID);
          }
          else
            longList2.Add(relation.ID);
          if (!longList3.Contains(relation.PartID))
            longList3.Add(relation.PartID);
        }
        else
          longList2.Add(relation.ID);
      }
    }
    foreach (long num in longList2)
    {
      Relation relation = new Relation();
      relation.ID = num;
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) null));
      this._relationRepository.Value.AddOrUpdate(relation);
    }
    foreach (long relationID in longList1)
    {
      Relation relation = new Relation();
      relation.ID = relationID;
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) null));
      this._relationRepository.Value.AddOrUpdate(relation);
      this._relationRepository.Value.Remove(relationID);
    }
  }

  private List<long> FindInstansesVersionIds(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session.GetCustomService(typeof (IArticleService)) as IArticleService).GetListInstances(objectVersionID, (object) sessionKeeper.Session.SessionGUID);
  }

  private void Checkin(long projectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(projectVersionID).CheckIn();
  }

  private long Checkout(long projectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(projectVersionID).CheckOut().ObjectID;
  }

  private bool NeedCheckout(long projectVersionID, int relationTypeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(projectVersionID);
      if (ObjectHelper.IsUnknownObjectVersionID(dbObject.CheckoutBy))
      {
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          if (this._objectTypeApplicabilityRepository.Value.Find(dbObject.TypeID, relationTypeID).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (o => o.IsContent)).Count<IMSApplicability>() > 0)
            return true;
        }
      }
    }
    return false;
  }

  private void SaveSubstitutesInternal(
    long projectVersionID,
    AnalyzeSaveSubsitutesResult.SaveSubsitutesChangesPack saveSubstitutesChangesPack)
  {
    foreach (Relation toAddRelation in saveSubstitutesChangesPack.ToAddRelations)
      this._relationRepository.Value.AddOrUpdate(toAddRelation);
    foreach (Relation toChangeRelation in saveSubstitutesChangesPack.ToChangeRelations)
      this._relationRepository.Value.AddOrUpdate(toChangeRelation);
    foreach (long toClearRelationId in saveSubstitutesChangesPack.ToClearRelationIds)
    {
      Relation relation = new Relation();
      relation.ID = toClearRelationId;
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) null));
      this._relationRepository.Value.AddOrUpdate(relation);
    }
    foreach (long removeRelationId in saveSubstitutesChangesPack.ToRemoveRelationIds)
    {
      Relation relation = new Relation();
      relation.ID = removeRelationId;
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null));
      relation.Attributes.Add(new _Attribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) null));
      this._relationRepository.Value.AddOrUpdate(relation);
      this._relationRepository.Value.Remove(removeRelationId);
    }
  }

  private long[] GetExistsSubstituteGroupNumbersFromOtherInstances(
    long objectVersionID,
    int relationTypeID)
  {
    List<long> longList = new List<long>();
    List<long> instansesVersionIds = this.FindInstansesVersionIds(objectVersionID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
      foreach (long projectID in instansesVersionIds)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams()
        {
          Columns = new object[1]
          {
            (object) SubstitutesConstants.SubstituteGroupNumberAttributeTypeID
          }
        };
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, projectID).Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, -1L);
          if (int64Value != -1L && !longList.Contains(int64Value))
            longList.Add(int64Value);
        }
      }
    }
    return longList.ToArray();
  }
}

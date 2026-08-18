// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.MbomServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Mbom;

public sealed class MbomServerService : LongLifeObject, IMbomServerService
{
  private long _thingMeasureUnitObjectVersionID;

  public void AddToMbom(Guid userSessionGuid, AddingToMbomParams addingToMbomParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (addingToMbomParams == null)
        throw new ArgumentNullException(nameof (addingToMbomParams));
      if (!AddingToMbomParams.Check(addingToMbomParams))
        throw new ArgumentException();
      this.AddToMbom(addingToMbomParams);
    }
  }

  public long CreateMbom(Guid userSessionGuid, long ebomVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(ebomVersionID) ? this.CreateMbom(ebomVersionID) : throw new ArgumentException();
  }

  public AddingToMbomInfo FindAddingToMbomInfo(Guid userSessionGuid, long ebomVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(ebomVersionID))
        throw new ArgumentException();
      return !this.IsEmptyMbomBinding(ebomVersionID, new MbomServerService.FindAddingToMbomInfoOptimizationContext()) ? this.FindAddingToMbomInfo(ebomVersionID) : throw new ArgumentException();
    }
  }

  public long FindEbomForMbom(Guid userSessionGuid, long mbomVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(mbomVersionID) ? this.FindEbomForMbom(mbomVersionID, new MbomServerService.FindAddingToMbomInfoOptimizationContext()) : throw new ArgumentException();
  }

  public long FindMbomForEbom(Guid userSessionGuid, long ebomVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(ebomVersionID) ? this.FindMbomForEbom(ebomVersionID, new MbomServerService.FindAddingToMbomInfoOptimizationContext()) : throw new ArgumentException();
  }

  public void AddTauToMbom(Guid userSessionGuid, long mbomVersionID, long tauVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(mbomVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(tauVersionID))
        throw new ArgumentException();
      this.AddTauToMbom(mbomVersionID, tauVersionID);
    }
  }

  public void TransferTauToMbom(
    Guid userSessionGuid,
    long destinationMbomVersionID,
    long tauVersionID,
    long sourceRelationID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(destinationMbomVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(tauVersionID))
        throw new ArgumentException();
      if (RelationHelper.IsUnknownRelationID(sourceRelationID))
        throw new ArgumentException();
      this.TransferTauToMbom(destinationMbomVersionID, tauVersionID, sourceRelationID);
    }
  }

  private void AddToMbom(AddingToMbomParams addingToMbomParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(addingToMbomParams.ObjectVersionID);
        IDBRelation dbRelation = sessionKeeper.Session.GetRelation(addingToMbomParams.MbomVersionID, dbObject.ID) ?? sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomCompositionRelationTypeID).Create(addingToMbomParams.MbomVersionID, addingToMbomParams.ObjectVersionID);
        IDBAttribute attributeById = dbRelation.GetAttributeByID(Constants.CountAttributeTypeID);
        MeasuredValue firstMeasuredValue = (attributeById != null ? attributeById.Value as MeasuredValue : (MeasuredValue) null) ?? this.GetDefaultQuantityMeasuredValue();
        dbRelation.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(Constants.CountAttributeTypeID, (object) this.SumMeasuredValues(firstMeasuredValue, addingToMbomParams.Count))
        });
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private long CreateMbom(long ebomVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObjectCollection(MbomConstants.MbomObjectTypeID).Create();
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(ebomVersionID);
        IDBAttribute byId1 = dbObject2.Attributes.FindByID(Constants.DesignationAttributeTypeID);
        IDBAttribute byId2 = dbObject2.Attributes.FindByID(Constants.NameAttributeTypeID);
        dbObject1.SetAttributesValues(new AttributeValues[2]
        {
          new AttributeValues(Constants.DesignationAttributeTypeID, (object) string.Format("T" + byId1.AsString)),
          new AttributeValues(Constants.NameAttributeTypeID, (object) byId2.AsString)
        });
        dbObject1.CommitCreation(true, true);
        sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomBindingRelationTypeID).Create(dbObject2.ObjectID, dbObject1.ObjectID);
        customService.Commit();
        return dbObject1.ObjectID;
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private MeasuredValue GetDefaultQuantityMeasuredValue() => new MeasuredValue(0.0, 0L);

  private AddingToMbomInfo FindAddingToMbomInfo(long ebomVersionID)
  {
    AddingToMbomInfo infoForBindedEbom = this.FindAddingToMbomInfoForBindedEbom(ebomVersionID, (MbomServerService.FindAddingToMbomInfoContext) null, new MbomServerService.FindAddingToMbomInfoOptimizationContext());
    AddingToMbomInfo[] array = infoForBindedEbom.GetDescendantsAndSelf().Where<AddingToMbomInfo>((System.Func<AddingToMbomInfo, bool>) (o => o.Statuses == AddingToMbomStatuses.TotalCountError)).ToArray<AddingToMbomInfo>();
    if (array.Length == 0)
      return infoForBindedEbom;
    throw new Exception($"Невозможно рассчитать суммарное количество для следующих объектов:{Environment.NewLine}{string.Join("," + Environment.NewLine, ((IEnumerable<AddingToMbomInfo>) array).Select<AddingToMbomInfo, long>((System.Func<AddingToMbomInfo, long>) (o => o.ObjectVersionID)).Distinct<long>().Select<long, string>((System.Func<long, string>) (o => $"#{o} {this.GetObjectCaption(o)}")))}.");
  }

  private AddingToMbomInfo FindAddingToMbomInfoForBindedEbom(
    long ebomVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    AddingToMbomInfo addingToMbomInfo1 = new AddingToMbomInfo(ebomVersionID);
    if (context == null)
    {
      addingToMbomInfo1.Statuses = AddingToMbomStatuses.AddingError;
      long mbomForEbom = this.FindMbomForEbom(ebomVersionID, optimizationContext);
      addingToMbomInfo1.ErrorMessage = $"ЭСИ #{ebomVersionID} '{this.GetObjectCaption(ebomVersionID)}' связана с ТЭСИ #{mbomForEbom} '{this.GetObjectCaption(mbomForEbom)}'";
      MbomServerService.FindAddingToMbomInfoContext context1 = new MbomServerService.FindAddingToMbomInfoContext(ebomVersionID, mbomForEbom, context);
      foreach (Tuple<long, int, MeasuredValue> tuple in this.FindEbomComposition(ebomVersionID, optimizationContext))
      {
        AddingToMbomInfo addingToMbomInfo2 = this.FindAddingToMbomInfo(tuple.Item1, tuple.Item2, context1, optimizationContext);
        addingToMbomInfo1.Children.Add(addingToMbomInfo2.ObjectVersionID, addingToMbomInfo2);
      }
    }
    else
    {
      addingToMbomInfo1.Statuses |= AddingToMbomStatuses.BindedEbom;
      this.FindTotalAndRemainingCount(ebomVersionID, context, optimizationContext, addingToMbomInfo1);
      if (this.IsInMbomComposition(context.MbomVersionID, this.GetObjectTypeID(context.MbomVersionID), ebomVersionID, context, optimizationContext))
        addingToMbomInfo1.Statuses |= AddingToMbomStatuses.InMbomComposition;
    }
    return addingToMbomInfo1;
  }

  private AddingToMbomInfo FindAddingToMbomInfo(
    long objectVersionID,
    int objectTypeID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    AddingToMbomInfo addingToMbomInfo;
    if (context.AddingToMbomInfoDictionatryByObjectVersionID.ContainsKey(objectVersionID))
    {
      addingToMbomInfo = context.AddingToMbomInfoDictionatryByObjectVersionID[objectVersionID];
    }
    else
    {
      try
      {
        addingToMbomInfo = !MbomHelper.IsEbomObjectType(objectTypeID) ? this.FindAddingToMbomInfoForNotEbom(objectVersionID, context, optimizationContext) : (!this.IsEmptyMbomBinding(objectVersionID, optimizationContext) ? this.FindAddingToMbomInfoForBindedEbom(objectVersionID, context, optimizationContext) : this.FindAddingToMbomInfoForNotBindedEbom(objectVersionID, context, optimizationContext));
      }
      catch
      {
        addingToMbomInfo = new AddingToMbomInfo(objectVersionID);
        addingToMbomInfo.Statuses = AddingToMbomStatuses.TotalCountError;
      }
      context.AddingToMbomInfoDictionatryByObjectVersionID.Add(addingToMbomInfo.ObjectVersionID, addingToMbomInfo);
    }
    return addingToMbomInfo;
  }

  private AddingToMbomInfo FindAddingToMbomInfoForNotBindedEbom(
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    AddingToMbomInfo addingToMbomInfo1 = new AddingToMbomInfo(objectVersionID);
    addingToMbomInfo1.Statuses |= AddingToMbomStatuses.NotBindedEbom;
    this.FindTotalAndRemainingCount(objectVersionID, context, optimizationContext, addingToMbomInfo1);
    if (this.IsInMbomComposition(context.MbomVersionID, this.GetObjectTypeID(context.MbomVersionID), objectVersionID, context, optimizationContext))
    {
      addingToMbomInfo1.Statuses |= AddingToMbomStatuses.InMbomComposition;
    }
    else
    {
      foreach (Tuple<long, int, MeasuredValue> tuple in this.FindEbomComposition(objectVersionID, optimizationContext))
      {
        AddingToMbomInfo addingToMbomInfo2 = this.FindAddingToMbomInfo(tuple.Item1, tuple.Item2, context, optimizationContext);
        addingToMbomInfo1.Children.Add(addingToMbomInfo2.ObjectVersionID, addingToMbomInfo2);
      }
    }
    return addingToMbomInfo1;
  }

  private AddingToMbomInfo FindAddingToMbomInfoForNotEbom(
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    AddingToMbomInfo addingToMbomInfo = new AddingToMbomInfo(objectVersionID);
    addingToMbomInfo.Statuses |= AddingToMbomStatuses.NotEbom;
    this.FindTotalAndRemainingCount(objectVersionID, context, optimizationContext, addingToMbomInfo);
    return addingToMbomInfo;
  }

  private void FindTotalAndRemainingCount(
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext,
    AddingToMbomInfo addingToMbomInfo)
  {
    addingToMbomInfo.TotalCount = this.CountObjectInEbomAndDescendantsComposition(context.EbomVersionID, this.GetObjectTypeID(context.EbomVersionID), objectVersionID, context, optimizationContext);
    MeasuredValue secondMeasuredValue = this.CountObjectInMbomAndDescendantsComposition(context.MbomVersionID, this.GetObjectTypeID(context.MbomVersionID), objectVersionID, context, optimizationContext);
    addingToMbomInfo.RemainingCount = this.SubstractMeasuredValue(addingToMbomInfo.TotalCount, secondMeasuredValue);
    if (addingToMbomInfo.RemainingCount.Value == 0.0)
    {
      addingToMbomInfo.Statuses |= AddingToMbomStatuses.AddingError;
      addingToMbomInfo.ErrorMessage = "Значения общего количества и оставшегося равны.";
    }
    else
    {
      if (addingToMbomInfo.RemainingCount.Value >= 0.0)
        return;
      addingToMbomInfo.Statuses |= AddingToMbomStatuses.AddingError;
      addingToMbomInfo.ErrorMessage = "Значение оставшегося количества больше общего количества.";
    }
  }

  private MeasuredValue CountObjectInEbomAndDescendantsComposition(
    long ebomVersionID,
    int ebomTypeID,
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    MeasuredValue firstMeasuredValue1 = this.CountObjectInEbomComposition(ebomVersionID, objectVersionID, optimizationContext);
    foreach (Tuple<long, int, MeasuredValue> tuple in this.FindEbomComposition(ebomVersionID, optimizationContext))
    {
      if (MbomHelper.IsEbomObjectType(tuple.Item2) && this.IsEmptyMbomBinding(tuple.Item1, optimizationContext) && !this.IsInMbomComposition(context.MbomVersionID, this.GetObjectTypeID(context.MbomVersionID), tuple.Item1, context, optimizationContext))
      {
        MeasuredValue firstMeasuredValue2 = this.CountObjectInEbomAndDescendantsComposition(tuple.Item1, tuple.Item2, objectVersionID, context, optimizationContext);
        firstMeasuredValue1 = this.SumMeasuredValues(firstMeasuredValue1, this.MultiplayMeasuredValues(firstMeasuredValue2, tuple.Item3));
      }
    }
    return firstMeasuredValue1;
  }

  private MeasuredValue MultiplayMeasuredValues(
    MeasuredValue firstMeasuredValue,
    MeasuredValue secondMeasuredValue)
  {
    return new MeasuredValue(firstMeasuredValue.Value * secondMeasuredValue.Value, firstMeasuredValue.MeasureID);
  }

  private long GetThingMeasureUnitObjectVersionID()
  {
    if (ObjectHelper.IsUnknownObjectVersionID(this._thingMeasureUnitObjectVersionID))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._thingMeasureUnitObjectVersionID = sessionKeeper.Session.GetObject(MbomConstants.ThingMeasureUnitObjectVersionGuid).ObjectID;
    }
    return this._thingMeasureUnitObjectVersionID;
  }

  private MeasuredValue SumMeasuredValues(
    MeasuredValue firstMeasuredValue,
    MeasuredValue secondMeasuredValue)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(firstMeasuredValue.MeasureID) || firstMeasuredValue.Value == 0.0)
      firstMeasuredValue = new MeasuredValue(firstMeasuredValue.Value, secondMeasuredValue.MeasureID);
    else if (ObjectHelper.IsUnknownObjectVersionID(secondMeasuredValue.MeasureID) || secondMeasuredValue.Value == 0.0)
      secondMeasuredValue = new MeasuredValue(secondMeasuredValue.Value, firstMeasuredValue.MeasureID);
    MeasuredValue measuredValue = (MeasuredValue) firstMeasuredValue.Clone();
    measuredValue.Add(secondMeasuredValue);
    return measuredValue;
  }

  private MeasuredValue CountObjectInEbomComposition(
    long ebomVersionID,
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    Tuple<long, int, MeasuredValue> tuple = ((IEnumerable<Tuple<long, int, MeasuredValue>>) this.FindEbomComposition(ebomVersionID, optimizationContext)).FirstOrDefault<Tuple<long, int, MeasuredValue>>((System.Func<Tuple<long, int, MeasuredValue>, bool>) (o => o.Item1 == objectVersionID));
    return tuple != null ? tuple.Item3 : this.GetDefaultQuantityMeasuredValue();
  }

  private MeasuredValue CountObjectInMbomAndDescendantsComposition(
    long mbomVersionID,
    int mbomTypeID,
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    MeasuredValue firstMeasuredValue1 = this.CountObjectInMbomComposition(mbomVersionID, mbomTypeID, objectVersionID, optimizationContext);
    foreach (Tuple<long, int, MeasuredValue> tuple in this.FindMbomComposition(mbomVersionID, mbomTypeID, optimizationContext))
    {
      if (MbomHelper.IsMbomOrSimilarObjectType(tuple.Item2) && this.IsEmptyEbomBinding(tuple.Item1, optimizationContext))
      {
        MeasuredValue firstMeasuredValue2 = this.CountObjectInMbomAndDescendantsComposition(tuple.Item1, tuple.Item2, objectVersionID, context, optimizationContext);
        firstMeasuredValue1 = this.SumMeasuredValues(firstMeasuredValue1, this.MultiplayMeasuredValues(firstMeasuredValue2, tuple.Item3));
      }
    }
    return firstMeasuredValue1;
  }

  private MeasuredValue CountObjectInMbomComposition(
    long mbomVersionID,
    int mbomTypeID,
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    Tuple<long, int, MeasuredValue> tuple = ((IEnumerable<Tuple<long, int, MeasuredValue>>) this.FindMbomComposition(mbomVersionID, mbomTypeID, optimizationContext)).FirstOrDefault<Tuple<long, int, MeasuredValue>>((System.Func<Tuple<long, int, MeasuredValue>, bool>) (o => o.Item1 == objectVersionID));
    return tuple != null ? tuple.Item3 : this.GetDefaultQuantityMeasuredValue();
  }

  private MeasuredValue SubstractMeasuredValue(
    MeasuredValue firstMeasuredValue,
    MeasuredValue secondMeasuredValue)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(firstMeasuredValue.MeasureID) || firstMeasuredValue.Value == 0.0)
      firstMeasuredValue = new MeasuredValue(firstMeasuredValue.Value, secondMeasuredValue.MeasureID);
    else if (ObjectHelper.IsUnknownObjectVersionID(secondMeasuredValue.MeasureID) || secondMeasuredValue.Value == 0.0)
      secondMeasuredValue = new MeasuredValue(secondMeasuredValue.Value, firstMeasuredValue.MeasureID);
    MeasuredValue measuredValue = (MeasuredValue) firstMeasuredValue.Clone();
    measuredValue.Substract(secondMeasuredValue);
    return measuredValue;
  }

  private long GetObjectID(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectVersionID).ID;
  }

  private int GetObjectTypeID(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectVersionID).ObjectType;
  }

  private string GetObjectCaption(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectVersionID).Caption;
  }

  private bool IsInMbomComposition(
    long mbomVersionID,
    int mbomTypeID,
    long objectVersionID,
    MbomServerService.FindAddingToMbomInfoContext context,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    Tuple<long, int, MeasuredValue>[] mbomComposition = this.FindMbomComposition(mbomVersionID, mbomTypeID, optimizationContext);
    if (((IEnumerable<Tuple<long, int, MeasuredValue>>) mbomComposition).Any<Tuple<long, int, MeasuredValue>>((System.Func<Tuple<long, int, MeasuredValue>, bool>) (o => o.Item1 == objectVersionID)))
      return true;
    foreach (Tuple<long, int, MeasuredValue> tuple in mbomComposition)
    {
      if (MbomHelper.IsMbomOrSimilarObjectType(tuple.Item2) && this.IsEmptyEbomBinding(tuple.Item1, optimizationContext) && this.IsInMbomComposition(tuple.Item1, tuple.Item2, objectVersionID, context, optimizationContext))
        return true;
    }
    return false;
  }

  private bool IsEmptyMbomBinding(
    long ebomVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    return ObjectHelper.IsUnknownObjectVersionID(this.FindMbomForEbom(ebomVersionID, optimizationContext));
  }

  private long FindMbomForEbom(
    long ebomVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    if (optimizationContext.MbomVersionIDDictioanryByEbomVersionID.ContainsKey(ebomVersionID))
      return optimizationContext.MbomVersionIDDictioanryByEbomVersionID[ebomVersionID];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomBindingRelationTypeID).ConsistFrom(new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }
      }, ebomVersionID);
      long mbomForEbom = 0;
      if (dataTable.Rows.Count > 0)
        mbomForEbom = DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
      optimizationContext.MbomVersionIDDictioanryByEbomVersionID[ebomVersionID] = mbomForEbom;
      return mbomForEbom;
    }
  }

  private bool IsEmptyEbomBinding(
    long mbomVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    return ObjectHelper.IsUnknownObjectVersionID(this.FindEbomForMbom(mbomVersionID, optimizationContext));
  }

  private Tuple<long, int, MeasuredValue>[] FindEbomComposition(
    long ebomVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    if (optimizationContext.EbomCompositionDictionaryByEbomVersionID.ContainsKey(ebomVersionID))
      return optimizationContext.EbomCompositionDictionaryByEbomVersionID[ebomVersionID];
    Tuple<long, int, MeasuredValue>[] objectComposition = this.FindObjectComposition(ebomVersionID, MbomConstants.EbomCompositionRelationTypeID, new long[2]
    {
      0L,
      1L
    });
    optimizationContext.EbomCompositionDictionaryByEbomVersionID[ebomVersionID] = objectComposition;
    return objectComposition;
  }

  private Tuple<long, int, MeasuredValue>[] FindMbomComposition(
    long mbomVersionID,
    int mbomTypeID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    if (optimizationContext.MbomCompositionDictionaryByMbomVersionID.ContainsKey(mbomVersionID))
      return optimizationContext.MbomCompositionDictionaryByMbomVersionID[mbomVersionID];
    Tuple<long, int, MeasuredValue>[] objectComposition = this.FindObjectComposition(mbomVersionID, MbomHelper.GetRelationTypeIDForMbomOrSimilarObjectType(mbomTypeID), new long[2]
    {
      0L,
      2L
    });
    optimizationContext.MbomCompositionDictionaryByMbomVersionID[mbomVersionID] = objectComposition;
    return objectComposition;
  }

  private Tuple<long, int, MeasuredValue>[] FindObjectComposition(
    long objectVersionID,
    int relationTypeID,
    long[] contexts)
  {
    List<Tuple<long, int, MeasuredValue>> source = new List<Tuple<long, int, MeasuredValue>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
      DBRecordSetParams paramSet;
      // ISSUE: explicit reference operation
      ^ref paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
        new ColumnDescriptor((object) Constants.CountAttributeTypeID)
        {
          AttributeSource = AttributeSourceTypes.Relation
        }
      });
      if (paramSet.Tags == null)
        paramSet.Tags = new HybridDictionary();
      paramSet.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) contexts;
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, objectVersionID).Rows)
      {
        long partVersionID = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
        MeasuredValue quantityMeasuredValue = this.GetDefaultQuantityMeasuredValue();
        quantityMeasuredValue.Value = 1.0;
        MeasuredValue measuredValue = DataSetProcessor.GetMeasuredValue(row, 2, quantityMeasuredValue);
        Tuple<long, int, MeasuredValue> tuple = source.FirstOrDefault<Tuple<long, int, MeasuredValue>>((System.Func<Tuple<long, int, MeasuredValue>, bool>) (o => o.Item1 == partVersionID));
        if (tuple != null)
          tuple.Item3.Add(measuredValue);
        else
          source.Add(new Tuple<long, int, MeasuredValue>(partVersionID, int32Value, measuredValue));
      }
    }
    return source.ToArray();
  }

  private long FindEbomForMbom(
    long mbomVersionID,
    MbomServerService.FindAddingToMbomInfoOptimizationContext optimizationContext)
  {
    if (optimizationContext.EbomVersionIDDictionaryByMbomVersionID.ContainsKey(mbomVersionID))
      return optimizationContext.EbomVersionIDDictionaryByMbomVersionID[mbomVersionID];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomBindingRelationTypeID).EntersInVersion(new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PROJ_ID
        }
      }, mbomVersionID);
      long ebomForMbom = 0;
      if (dataTable.Rows.Count > 0)
        ebomForMbom = DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
      optimizationContext.EbomVersionIDDictionaryByMbomVersionID[mbomVersionID] = ebomForMbom;
      return ebomForMbom;
    }
  }

  private void AddTauToMbom(long mbomVersionID, long tauVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomCompositionRelationTypeID);
      MeasuredValue quantityMeasuredValue = this.GetDefaultQuantityMeasuredValue();
      quantityMeasuredValue.Value = 1.0;
      AttributeValues[] vals = new AttributeValues[1]
      {
        new AttributeValues(Constants.CountAttributeTypeID)
        {
          Values = new object[1]
          {
            (object) quantityMeasuredValue
          }
        }
      };
      relationCollection.Create(mbomVersionID, tauVersionID, vals);
    }
  }

  private void TransferTauToMbom(
    long destinationMbomVersionID,
    long tauVersionID,
    long sourceRelationID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MbomConstants.MbomCompositionRelationTypeID);
        IDBObject dbObject = sessionKeeper.Session.GetObject(tauVersionID);
        NewRelationProperties properties = new NewRelationProperties()
        {
          BeginDate = DateTime.MinValue,
          EndDate = DateTime.MaxValue,
          PartID = dbObject.ID,
          PartObjectID = tauVersionID,
          ProjectObjectID = destinationMbomVersionID,
          PrototypeRelationID = sourceRelationID
        };
        relationCollection.Create(properties);
        sessionKeeper.Session.GetRelation(sourceRelationID).Delete((long) Consts.PurgeMode);
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private sealed class FindAddingToMbomInfoContext
  {
    public FindAddingToMbomInfoContext(
      long ebomVersionID,
      long mbomVersionID,
      MbomServerService.FindAddingToMbomInfoContext parentFindAddingToMbomInfoContext = null)
    {
      if (ObjectHelper.IsUnknownObjectVersionID(ebomVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(mbomVersionID))
        throw new ArgumentException();
      this.EbomVersionID = ebomVersionID;
      this.MbomVersionID = mbomVersionID;
      this.AddingToMbomInfoDictionatryByObjectVersionID = new Dictionary<long, AddingToMbomInfo>();
      this.Parent = parentFindAddingToMbomInfoContext;
    }

    public long EbomVersionID { get; private set; }

    public long MbomVersionID { get; private set; }

    public MbomServerService.FindAddingToMbomInfoContext Parent { get; private set; }

    public Dictionary<long, AddingToMbomInfo> AddingToMbomInfoDictionatryByObjectVersionID { get; private set; }

    private IEnumerable<MbomServerService.FindAddingToMbomInfoContext> GetAncestorsAndSelf()
    {
      for (MbomServerService.FindAddingToMbomInfoContext currentFindAddingToMbomInfoContext = this; currentFindAddingToMbomInfoContext != null; currentFindAddingToMbomInfoContext = currentFindAddingToMbomInfoContext.Parent)
        yield return currentFindAddingToMbomInfoContext;
    }
  }

  private sealed class FindAddingToMbomInfoOptimizationContext
  {
    public FindAddingToMbomInfoOptimizationContext()
    {
      this.EbomVersionIDDictionaryByMbomVersionID = new Dictionary<long, long>();
      this.MbomVersionIDDictioanryByEbomVersionID = new Dictionary<long, long>();
      this.EbomCompositionDictionaryByEbomVersionID = new Dictionary<long, Tuple<long, int, MeasuredValue>[]>();
      this.MbomCompositionDictionaryByMbomVersionID = new Dictionary<long, Tuple<long, int, MeasuredValue>[]>();
    }

    public Dictionary<long, long> EbomVersionIDDictionaryByMbomVersionID { get; private set; }

    public Dictionary<long, long> MbomVersionIDDictioanryByEbomVersionID { get; private set; }

    public Dictionary<long, Tuple<long, int, MeasuredValue>[]> EbomCompositionDictionaryByEbomVersionID { get; private set; }

    public Dictionary<long, Tuple<long, int, MeasuredValue>[]> MbomCompositionDictionaryByMbomVersionID { get; private set; }
  }
}

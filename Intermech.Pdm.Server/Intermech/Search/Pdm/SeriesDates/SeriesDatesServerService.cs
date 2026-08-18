// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Sets;
using Intermech.Kernel;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

public sealed class SeriesDatesServerService : LongLifeObject, ISeriesDatesServerService
{
  private LazyService<IAttributeTypeForObjectRepository> _attributeTypeForObjectRepository = new LazyService<IAttributeTypeForObjectRepository>();

  public SeriesDatesPack FindSeriesDates(Guid userSessionGuid, long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null)
        throw new ArgumentNullException(nameof (objectVersionIds));
      if (!this.CheckObjectVersionIds(objectVersionIds))
        throw new ArgumentException();
      return SeriesDatesHelper.CheckObjectsForFindSeriesDates(objectVersionIds) ? this.FindSeriesDatesInternal(objectVersionIds) : throw new ArgumentException();
    }
  }

  public Dictionary<long, Dictionary<long, SeriesDatesPack>> FindSeriesDatesForOtherVersions(
    Guid userSessionGuid,
    long[] objectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null)
        throw new ArgumentNullException(nameof (objectVersionIds));
      if (!this.CheckObjectVersionIds(objectVersionIds))
        throw new ArgumentException();
      return SeriesDatesHelper.CheckObjectsForFindSeriesDates(objectVersionIds) ? this.FindSeriesDatesForOtherVersionsInternal(objectVersionIds) : throw new ArgumentException();
    }
  }

  public void SaveSeriesDates(
    Guid userSessionGuid,
    long[] objectVersionIds,
    SeriesDatesPack seriesDatesPack)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (objectVersionIds == null)
        throw new ArgumentNullException(nameof (objectVersionIds));
      if (!this.CheckObjectVersionIds(objectVersionIds))
        throw new ArgumentException();
      if (!SeriesDatesHelper.CheckObjectsForFindSeriesDates(objectVersionIds))
        throw new ArgumentException();
      if (!SeriesDatesHelper.CheckObjectsForSaveSeriesDates(objectVersionIds))
        throw new ArgumentException();
      if (seriesDatesPack == null)
        throw new ArgumentNullException(nameof (seriesDatesPack));
      string error = (string) null;
      if (!this.CheckSeriesDatesForIntersectionsWithOtherVersions(objectVersionIds, seriesDatesPack, out error))
        throw new ArgumentException(error);
      this.SaveSeriesDatesInternal(objectVersionIds, seriesDatesPack);
    }
  }

  private bool CheckObjectVersionIds(long[] objectVersionIds)
  {
    return objectVersionIds.Length != 0 && ((IEnumerable<long>) objectVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() == 0;
  }

  private bool CheckSeriesDatesForIntersectionsWithOtherVersions(
    long[] objectVersionIds,
    SeriesDatesPack seriesDatesPack,
    out string error)
  {
    Dictionary<long, Dictionary<long, SeriesDatesPack>> versionsInternal = this.FindSeriesDatesForOtherVersionsInternal(objectVersionIds);
    return SeriesDatesHelper.CheckSeriesDatesIntersectionsWithOtherVersions(seriesDatesPack, versionsInternal, out error);
  }

  private SeriesDatesPack FindSeriesDatesInternal(long[] objectVersionIds)
  {
    SeriesDatesPack seriesDatesInternal = (SeriesDatesPack) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectVersionId in objectVersionIds)
      {
        SeriesDatesPack seriesDatesPack = this.ConvertSeriesDatesApplicabilityCollectionToSeriesDatesPack(new SeriesDatesApplicabilityCollection((object) sessionKeeper.Session.GetObject(objectVersionId)));
        seriesDatesInternal = seriesDatesInternal != null ? seriesDatesInternal.Intersect(seriesDatesPack) : seriesDatesPack;
      }
    }
    return seriesDatesInternal;
  }

  private Dictionary<long, Dictionary<long, SeriesDatesPack>> FindSeriesDatesForOtherVersionsInternal(
    long[] objectVersionIds)
  {
    Dictionary<long, Dictionary<long, SeriesDatesPack>> versionsInternal = new Dictionary<long, Dictionary<long, SeriesDatesPack>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectVersionId in objectVersionIds)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId);
        List<long> longList = sessionKeeper.Session.GetObjectVersions(dbObject.ID) ?? new List<long>();
        Dictionary<long, SeriesDatesPack> dictionary = new Dictionary<long, SeriesDatesPack>();
        foreach (long key in longList)
        {
          if (key != objectVersionId && key != -objectVersionId)
          {
            SeriesDatesPack seriesDatesInternal = this.FindSeriesDatesInternal(new long[1]
            {
              key
            });
            seriesDatesInternal.ObjectVersionID = key;
            if (seriesDatesInternal.Groups.Count != 0)
              dictionary.Add(key, seriesDatesInternal);
          }
        }
        versionsInternal.Add(objectVersionId, dictionary);
      }
    }
    return versionsInternal;
  }

  private void SaveSeriesDatesInternal(long[] objectVersionIds, SeriesDatesPack seriesDatesPack)
  {
    SeriesDatesApplicabilityCollection applicabilityCollection = this.ConvertSeriesDatesPackToSeriesDatesApplicabilityCollection(seriesDatesPack);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      customService.StartTransaction();
      try
      {
        List<long> longList = new List<long>();
        foreach (long objectID in ((IEnumerable<long>) objectVersionIds).Distinct<long>())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false) ?? sessionKeeper.Session.GetObject(-objectID);
          IMSAttribute4ObjectType attribute4ObjectType = this._attributeTypeForObjectRepository.Value.Find(dbObject.TypeID).First<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (o => o.AttributeID == SeriesDatesConstants.SeriesDatesApplicabilityAttributeTypeID));
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy != sessionKeeper.Session.UserID && !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
          {
            dbObject.CheckOut();
            longList.Add(-objectID);
            dbObject = sessionKeeper.Session.GetObject(-objectID);
          }
          applicabilityCollection.SaveToObject((IDBAttributable) dbObject);
        }
        foreach (long objectID in longList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
          if (dbObject != null && !ObjectHelper.IsUnknownObjectVersionID(dbObject.CheckoutBy))
            dbObject.CheckIn();
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

  private SeriesDatesPack ConvertSeriesDatesApplicabilityCollectionToSeriesDatesPack(
    SeriesDatesApplicabilityCollection seriesDatesApplicabilityCollection)
  {
    SeriesDatesPack seriesDatesPack = new SeriesDatesPack();
    foreach (SeriesDatesApplicability datesApplicability in seriesDatesApplicabilityCollection.Items)
    {
      SeriesDatesApplicability seriesDatesApplicability = datesApplicability;
      SeriesDatesGroup seriesDatesGroup = seriesDatesPack.Groups.SingleOrDefault<SeriesDatesGroup>((Func<SeriesDatesGroup, bool>) (o => o.HeadProductVersionID == seriesDatesApplicability.MainObjectID)) ?? new SeriesDatesGroup(seriesDatesApplicability.MainObjectID);
      if (seriesDatesApplicability.Applicability == ApplicabilityBy.Date)
      {
        foreach (DateTimeRange range in (seriesDatesApplicability.Set as Set<DateTime>).Ranges)
          seriesDatesGroup.Dates.Add(new DateRange(range.MinValue, range.MaxValue));
      }
      else if (seriesDatesApplicability.Applicability == ApplicabilityBy.Series)
      {
        foreach (Int32Range range in (seriesDatesApplicability.Set as Set<int>).Ranges)
          seriesDatesGroup.Series.Add(new SeriesRange(range.MinValue, range.MaxValue));
      }
      seriesDatesPack.Groups.Add(seriesDatesGroup);
    }
    return seriesDatesPack;
  }

  private SeriesDatesApplicabilityCollection ConvertSeriesDatesPackToSeriesDatesApplicabilityCollection(
    SeriesDatesPack seriesDatesPack)
  {
    SeriesDatesApplicabilityCollection applicabilityCollection = new SeriesDatesApplicabilityCollection();
    foreach (SeriesDatesGroup group in (Collection<SeriesDatesGroup>) seriesDatesPack.Groups)
    {
      group.Dates.Normalize();
      if (group.Dates.Count > 0)
      {
        Set<DateTime> set = new Set<DateTime>();
        SeriesDatesApplicability datesApplicability = new SeriesDatesApplicability()
        {
          Applicability = ApplicabilityBy.Date,
          MainObjectID = group.HeadProductVersionID,
          Set = (ISet) set
        };
        foreach (DateRange date in (Collection<DateRange>) group.Dates)
        {
          DateTime minValue = date.Start != DateRange.MinValue ? date.Start : DateTime.MinValue;
          DateTime maxValue = date.End != DateRange.MaxValue ? date.End : DateTime.MaxValue;
          set.Ranges.Add((IRange<DateTime>) new DateTimeRange(minValue, maxValue));
        }
        applicabilityCollection.Items.Add(datesApplicability);
      }
      group.Series.Normalize();
      if (group.Series.Count > 0)
      {
        Set<int> set = new Set<int>();
        SeriesDatesApplicability datesApplicability = new SeriesDatesApplicability()
        {
          Applicability = ApplicabilityBy.Series,
          MainObjectID = group.HeadProductVersionID,
          Set = (ISet) set
        };
        foreach (SeriesRange seriesRange in (Collection<SeriesRange>) group.Series)
          set.Ranges.Add((IRange<int>) new Int32Range(seriesRange.Start, seriesRange.End));
        applicabilityCollection.Items.Add(datesApplicability);
      }
    }
    return applicabilityCollection;
  }
}

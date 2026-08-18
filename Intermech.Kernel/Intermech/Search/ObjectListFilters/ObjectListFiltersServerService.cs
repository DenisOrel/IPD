// Decompiled with JetBrains decompiler
// Type: Intermech.Search.ObjectListFilters.ObjectListFiltersServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Search.ObjectListFilters;

public sealed class ObjectListFiltersServerService : LongLifeObject, IObjectListFiltersServerService
{
  public ObjectListFilter[] FindAllFilters(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.FindAllFilters();
  }

  public ObjectListFilter CreateNewFilter(
    Guid userSessionGuid,
    string name,
    ObjectListFilterType type)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.CreateNewFilter(name, type);
  }

  public void RemoveFilter(Guid userSessionGuid, long selectionVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(selectionVersionID))
        throw new ArgumentException();
      this.RemoveFilter(selectionVersionID);
    }
  }

  private ObjectListFilter[] FindAllFilters()
  {
    List<ObjectListFilter> source = new List<ObjectListFilter>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Constants.SelectionObjectTypeID);
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION,
        (object) ObligatoryObjectAttributes.F_GUID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) Constants.SelectionTypeAttributeTypeID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) 6,
          SQL = string.Empty
        }
      };
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams with
      {
        Tags = new HybridDictionary()
      };
      paramSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
          string stringValue = DataSetProcessor.GetStringValue(row, 1, string.Empty);
          Guid guidValue = DataSetProcessor.GetGuidValue(row, 2, Guid.Empty);
          int[] array = ((IEnumerable<object>) (sessionKeeper.Session.GetObject(int64Value).GetAttributeByID(Constants.ObjectTypesGuidsAttributeTypeID).Values ?? new object[0])).Select<object, Guid>((System.Func<object, Guid>) (o => DataSetProcessor.GetGuidValue(o, Guid.Empty))).Cast<Guid>().Where<Guid>((System.Func<Guid, bool>) (o => o != Guid.Empty)).Select<Guid, int>((System.Func<Guid, int>) (o => MetaDataHelper.GetObjectTypeID(o))).Where<int>((System.Func<int, bool>) (o => !ObjectTypeHelper.IsUnknownObjectTypeID(o))).Distinct<int>().ToArray<int>();
          ObjectListFilter objectListFilter = new ObjectListFilter(int64Value, guidValue, stringValue, array);
          source.Add(objectListFilter);
        }
      }
    }
    List<ObjectListFilter> list = source.OrderBy<ObjectListFilter, string>((System.Func<ObjectListFilter, string>) (o => o.Name)).ToList<ObjectListFilter>();
    list.Insert(0, ObjectListFilter.AllObjectsFilter);
    return list.ToArray();
  }

  private ObjectListFilter CreateNewFilter(string name, ObjectListFilterType type)
  {
    int objectType = type == ObjectListFilterType.Common ? Constants.CommonSelectionObjectTypeID : Constants.PersonalSelectionObjectTypeID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(objectType).Create();
      dbObject.SetAttributesValues(new AttributeValues[2]
      {
        new AttributeValues(Constants.NameAttributeTypeID, (object) name),
        new AttributeValues(Constants.SelectionTypeAttributeTypeID, (object) 6)
      });
      dbObject.CommitCreation(true);
      return new ObjectListFilter(dbObject.ObjectID, dbObject.ObjectGUID, name, new int[0]);
    }
  }

  private void RemoveFilter(long selectionVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(selectionVersionID, false)?.Delete((long) Consts.PurgeMode);
  }
}

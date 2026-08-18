// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMeasureObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBMeasureObject(UserSession uSession, DataTable objectParams) : 
  DBObject(uSession, objectParams),
  IDBMeasureObject
{
  private long _BaseUnitID;
  public static int KoefAttributeID;
  public static int DefaultAttributeID;
  public static int OperationsListAttributeID;
  public static int DopNamesAttributeID;

  public virtual string ShortMUName
  {
    get => this.GetAttributeByID(this.UserSession.IdentHelper.ShortNameID).AsString;
  }

  public static void LoadMeasuresList(IUserSession uSession)
  {
    if (DBMeasureObject.KoefAttributeID == 0)
    {
      IIDHelper service = ServerServices.GetService(typeof (IIDHelper)) as IIDHelper;
      DBMeasureObject.KoefAttributeID = service.GetAttributeID("cad00025-306c-11d8-b4e9-00304f19f545");
      DBMeasureObject.DefaultAttributeID = service.GetAttributeID("cad001a7-306c-11d8-b4e9-00304f19f545");
      DBMeasureObject.OperationsListAttributeID = service.GetAttributeID("cad0036c-306c-11d8-b4e9-00304f19f545");
      DBMeasureObject.DopNamesAttributeID = service.GetAttributeID("cadd93b1-306c-11d8-b4e9-00304f19f545");
      if (DBMeasureObject.OperationsListAttributeID <= 0)
        DBMeasureObject.OperationsListAttributeID = service.GetAttributeID("cad00018-306c-11d8-b4e9-00304f19f545");
      if (DBMeasureObject.DopNamesAttributeID <= 0)
        DBMeasureObject.DopNamesAttributeID = service.GetAttributeID("cad00018-306c-11d8-b4e9-00304f19f545");
    }
    ArrayList arrayList = new ArrayList();
    ServerServices.GetService(typeof (IDBTimedEvents));
    UserSession userSession = uSession as UserSession;
    IDBObjectCollection objectCollection1 = userSession.GetObjectCollection(userSession.IdentHelper.PhysicValueTypeID);
    DataTable dataTable;
    try
    {
      dataTable = objectCollection1.Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -12
      }));
    }
    catch
    {
      dataTable = userSession.GetObjectCollection(-1).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -12
      }));
    }
    IDBObjectCollection objectCollection2 = userSession.GetObjectCollection(userSession.IdentHelper.MeasureTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(0, RelationalOperators.EntersIn, (object) 0, LogicalOperators.NONE, 0, true);
    object[] columns = new object[8]
    {
      (object) -2,
      (object) DBMeasureObject.DefaultAttributeID,
      (object) DBMeasureObject.KoefAttributeID,
      (object) userSession.IdentHelper.ShortNameID,
      (object) userSession.IdentHelper.NameID,
      (object) DBMeasureObject.OperationsListAttributeID,
      (object) DBMeasureObject.DopNamesAttributeID,
      (object) -12
    };
    foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row1[0]);
      conditionStructure.Value = (object) int64;
      foreach (DataRow row2 in (InternalDataCollectionBase) objectCollection2.Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure
      }, columns)).Rows)
      {
        MeasureDescriptor measureDescriptor = new MeasureDescriptor();
        measureDescriptor.MeasureID = Convert.ToInt64(row2[0]);
        measureDescriptor.IsDefault = row2[1] != DBNull.Value && Convert.ToInt32(row2[1]) != 0;
        measureDescriptor.K = Convert.ToDouble(row2[2]);
        measureDescriptor.PhysicalQuantityID = int64;
        measureDescriptor.ShortName = row2[3].ToString();
        measureDescriptor.LongName = row2[4].ToString();
        measureDescriptor.MeasureGuid = new Guid(row2[7].ToString());
        if (row2[6].ToString() == string.Empty)
        {
          measureDescriptor.ShortNameIndex = new string[1]
          {
            userSession.StringNormalizer.GetIndexedString(measureDescriptor.ShortName)
          };
        }
        else
        {
          IDBAttribute attributeByGuid = userSession.GetObject(measureDescriptor.MeasureID).GetAttributeByGuid(new Guid("cadd93b1-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            int num1 = 0;
            for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
            {
              attributeByGuid.Index = index;
              if (!attributeByGuid.IsNull && attributeByGuid.AsString.Trim() != string.Empty)
                ++num1;
            }
            measureDescriptor.ShortNameIndex = new string[num1 + 1];
            measureDescriptor.ShortNameIndex[0] = userSession.StringNormalizer.GetIndexedString(measureDescriptor.ShortName);
            int num2 = 1;
            for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
            {
              attributeByGuid.Index = index;
              if (!attributeByGuid.IsNull && attributeByGuid.AsString.Trim() != string.Empty)
                measureDescriptor.ShortNameIndex[num2++] = userSession.StringNormalizer.GetIndexedString(attributeByGuid.AsString.Trim());
            }
          }
          else
            measureDescriptor.ShortNameIndex = new string[1]
            {
              userSession.StringNormalizer.GetIndexedString(measureDescriptor.ShortName)
            };
        }
        if (row2[5].ToString() == string.Empty)
        {
          measureDescriptor.OperationsList = new string[0];
        }
        else
        {
          IDBAttribute attributeById = userSession.GetObject(measureDescriptor.MeasureID).GetAttributeByID(DBMeasureObject.OperationsListAttributeID);
          measureDescriptor.OperationsList = new string[attributeById.ValuesCount];
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            attributeById.Index = index;
            measureDescriptor.OperationsList[index] = attributeById.AsString;
          }
        }
        measureDescriptor.PhysicalQuantityGuid = new Guid(row1[1].ToString());
        arrayList.Add((object) measureDescriptor);
      }
    }
    MeasureHelper.Init((MeasureDescriptor[]) arrayList.ToArray(typeof (MeasureDescriptor)));
  }

  public virtual string MUName
  {
    get => this.GetAttributeByID(this.UserSession.IdentHelper.NameID).AsString;
  }

  protected override void DoDelete()
  {
    if (this.GetAttributeByGuid(new Guid("cad00025-306c-11d8-b4e9-00304f19f545")).AsDouble == 1.0)
      throw new KernelExceptionID(sc_13300.ssp_appserver_13301(1577174442), (object) this.Caption);
    base.DoDelete();
  }

  protected override void DoAfterCommitCreation()
  {
    base.DoAfterCommitCreation();
    MeasureDescriptor measureDescriptor = this.GetMeasureDescriptor();
    if (measureDescriptor.PhysicalQuantityID <= 0L)
      return;
    MeasureHelper.AddDescriptor(measureDescriptor);
  }

  public MeasureDescriptor GetMeasureDescriptor()
  {
    MeasureDescriptor measureDescriptor = new MeasureDescriptor();
    measureDescriptor.MeasureID = Math.Abs(this.ObjectID);
    IDBAttribute byId1 = this.Attributes.FindByID(DBMeasureObject.DefaultAttributeID);
    measureDescriptor.IsDefault = byId1 != null && !byId1.IsNull && byId1.AsBoolean;
    measureDescriptor.K = this.Attributes.FindByID(DBMeasureObject.KoefAttributeID).AsDouble;
    measureDescriptor.ShortName = this.Attributes.FindByID(this.UserSession.IdentHelper.ShortNameID).AsString;
    measureDescriptor.LongName = this.Attributes.FindByID(this.UserSession.IdentHelper.NameID).AsString;
    IDBAttribute byGuid = this.Attributes.FindByGUID(new Guid("cadd93b1-306c-11d8-b4e9-00304f19f545"));
    measureDescriptor.MeasureGuid = this.ObjectGUID;
    if (byGuid == null)
    {
      measureDescriptor.ShortNameIndex = new string[1]
      {
        this.UserSession.StringNormalizer.GetIndexedString(measureDescriptor.ShortName)
      };
    }
    else
    {
      int num1 = 0;
      for (int index = 0; index < byGuid.ValuesCount; ++index)
      {
        byGuid.Index = index;
        if (!byGuid.IsNull && byGuid.AsString.Trim() != string.Empty)
          ++num1;
      }
      measureDescriptor.ShortNameIndex = new string[num1 + 1];
      measureDescriptor.ShortNameIndex[0] = this.UserSession.StringNormalizer.GetIndexedString(measureDescriptor.ShortName);
      int num2 = 1;
      for (int index = 0; index < byGuid.ValuesCount; ++index)
      {
        byGuid.Index = index;
        if (!byGuid.IsNull && byGuid.AsString.Trim() != string.Empty)
          measureDescriptor.ShortNameIndex[num2++] = this.UserSession.StringNormalizer.GetIndexedString(byGuid.AsString.Trim());
      }
    }
    IDBAttribute byId2 = this.Attributes.FindByID(DBMeasureObject.OperationsListAttributeID);
    if (byId2 == null || byId2.AsString == string.Empty)
    {
      measureDescriptor.OperationsList = new string[0];
    }
    else
    {
      measureDescriptor.OperationsList = new string[byId2.ValuesCount];
      for (int index = 0; index < byId2.ValuesCount; ++index)
      {
        byId2.Index = index;
        measureDescriptor.OperationsList[index] = byId2.AsString;
      }
    }
    DataTable dataTable = this.UserSession.GetObjectCollection(this.UserSession.IdentHelper.PhysicValueTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) this.ID, LogicalOperators.NONE, 0, true)
    }, new object[2]{ (object) -2, (object) -12 }));
    if (dataTable.Rows.Count > 0)
    {
      measureDescriptor.PhysicalQuantityGuid = new Guid(dataTable.Rows[0][1].ToString());
      measureDescriptor.PhysicalQuantityID = Convert.ToInt64(dataTable.Rows[0][0]);
    }
    return measureDescriptor;
  }

  protected override void DoNextLCStep(IDBLifecycleStep nextstep)
  {
    base.DoNextLCStep(nextstep);
    DBMeasureObject.LoadMeasuresList((IUserSession) this.UserSession);
  }

  public virtual bool IsBaseUnit => this.BaseUnitID == this.ObjectID;

  public virtual long BaseUnitID => this._BaseUnitID;

  internal static void WriteKoefValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (args.Value == DBNull.Value || Math.Round(Convert.ToDouble(args.Value), Consts.MaxPrecision) == 0.0)
      throw new KernelExceptionID(383);
  }

  internal static void Init()
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddAttributeWriteHandler((object) new Guid("cad00025-306c-11d8-b4e9-00304f19f545"), new WriteAttributeValueHandler(DBMeasureObject.WriteKoefValue));
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    AttributeValues[] attributeValuesArray = base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
    if (valuesList.Length == 0 || MeasureHelper.FindDescriptor(this.ObjectID).Empty)
      return attributeValuesArray;
    MeasureHelper.ReplaceDescriptor(this.GetMeasureDescriptor());
    return attributeValuesArray;
  }
}

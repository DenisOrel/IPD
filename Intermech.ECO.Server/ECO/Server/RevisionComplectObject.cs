// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevisionComplectObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.ECO;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.ECO.Server;

public class RevisionComplectObject(UserSession uSession, DataTable objectsTable) : 
  DBEditingContextsObject(uSession, objectsTable),
  IRevComplectObject
{
  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    return base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
  }

  protected override void DoAfterAddAttribute(IDBAttribute attribute)
  {
    base.DoAfterAddAttribute(attribute);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    this.CheckAttributes(attribute);
  }

  private void CheckAttributes(IDBAttribute attribute)
  {
    if (attribute.AttributeID == RevisionComplect.Attr_TermOfChange)
    {
      foreach (long compositionObject in (this.UserSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).LoadCompositionObjects((object) this.UserSession, this.ObjectID, RevisionComplect.RevisionComplectRelation_TypeId, "cad001e2-306c-11d8-b4e9-00304f19f545"))
      {
        ECOObject ecoObject = this.UserSession.GetObject(compositionObject, false) as ECOObject;
        bool flag = false;
        if (ecoObject != null && ecoObject.ObjectModifyMode == ObjectModifyModes.Checkout && ecoObject.ObjectID > 0L)
        {
          IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(RevisionComplect.Revision_TypeId, RevisionComplect.Attr_TermOfChange);
          if (attribute4ObjectType != null && !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
          {
            flag = true;
            ecoObject = ecoObject.CheckOut(true) as ECOObject;
          }
        }
        if (ecoObject != null)
        {
          ecoObject.LockCheckAttributes = true;
          try
          {
            ecoObject.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(attribute.AttributeID, attribute.Value)
            });
          }
          finally
          {
            ecoObject.LockCheckAttributes = false;
            if (flag)
              ecoObject.CheckIn();
          }
        }
      }
    }
    if (attribute.AttributeID != RevisionComplect.Attr_Designation)
      return;
    this.SetDesignation(attribute.AsString);
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  private List<RevisionComplectObject.OTDObj> GetEcoObjects()
  {
    DataTable dataTable = (this.UserSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).LoadComposition((object) this.UserSession, this.ObjectID, RevisionComplect.RevisionComplectRelation_TypeId, (IEnumerable<ColumnDescriptor>) ((IEnumerable<ColumnDescriptor>) new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      new ColumnDescriptor((object) RevisionComplect.Attr_Sort)
    }).ToList<ColumnDescriptor>(), "cad001e2-306c-11d8-b4e9-00304f19f545");
    List<RevisionComplectObject.OTDObj> ecoObjects = new List<RevisionComplectObject.OTDObj>();
    if (dataTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32 = Convert.ToInt32(row[2]);
        if (int32 == RevisionComplect.Revision_TypeId || MetaDataHelper.IsObjectTypeChildOf(int32, RevisionComplect.Revision_TypeId))
          ecoObjects.Add(new RevisionComplectObject.OTDObj()
          {
            RelationId = Convert.ToInt64(row[0]),
            Id = Convert.ToInt64(row[1]),
            SortIndex = Convert.ToInt32(row[3])
          });
      }
      ecoObjects.Sort();
    }
    return ecoObjects;
  }

  private void SetDesignation(string value)
  {
    List<RevisionComplectObject.OTDObj> ecoObjects = this.GetEcoObjects();
    int count = ecoObjects.Count;
    int num = 0;
    foreach (RevisionComplectObject.OTDObj otdObj in ecoObjects)
    {
      ++num;
      ECOObject ecoObject = this.UserSession.GetObject(otdObj.Id, false) as ECOObject;
      string str = $".{num.ToString()}/{count.ToString()}";
      string initValue = value + str;
      bool flag = false;
      if (ecoObject != null && ecoObject.ObjectModifyMode == ObjectModifyModes.Checkout && ecoObject.ObjectID > 0L)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(RevisionComplect.Revision_TypeId, RevisionComplect.Attr_Designation);
        if (attribute4ObjectType != null && !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
        {
          flag = true;
          ecoObject = ecoObject.CheckOut(true) as ECOObject;
        }
      }
      if (ecoObject != null)
      {
        ecoObject.LockCheckAttributes = true;
        try
        {
          ecoObject.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(RevisionComplect.Attr_Designation, (object) initValue)
          });
        }
        finally
        {
          ecoObject.LockCheckAttributes = false;
          if (flag)
            ecoObject.CheckIn();
        }
      }
    }
  }

  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    ICompositionLoadService customService = this.UserSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
    int num = 0;
    UserSession userSession = this.UserSession;
    long objectId = this.ObjectID;
    int complectRelationTypeId = RevisionComplect.RevisionComplectRelation_TypeId;
    List<ColumnDescriptor> list = ((IEnumerable<ColumnDescriptor>) new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) RevisionComplect.Attr_Sort)
    }).ToList<ColumnDescriptor>();
    int[] numArray = Array.Empty<int>();
    DataTable dataTable = customService.LoadComposition((object) userSession, objectId, complectRelationTypeId, (IEnumerable<ColumnDescriptor>) list, "cad001e2-306c-11d8-b4e9-00304f19f545", numArray);
    if (dataTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row[0] != DBNull.Value)
        {
          int int32 = Convert.ToInt32(row[0]);
          if (int32 > num)
            num = int32;
        }
      }
    }
    int initValue = num + 100;
    newrelation.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(RevisionComplect.Attr_Sort, (object) initValue)
    });
    base.DoAfterCreateRelation(newrelation);
    IDBAttribute attributeById = this.GetAttributeByID(RevisionComplect.Attr_Designation);
    if (attributeById == null)
      return;
    this.SetDesignation(attributeById.AsString);
  }

  public override void DoBeforeCreateRelation(
    DBRelationCollection dBRelationCollection,
    long partID,
    long partObjectID,
    long prjlinkID,
    IDBRelation prototype)
  {
    base.DoBeforeCreateRelation(dBRelationCollection, partID, partObjectID, prjlinkID, prototype);
    ECOObject ecoObject1 = this.UserSession.GetObject(partObjectID, false) as ECOObject;
    IDBObject dbObject = (IDBObject) this;
    bool flag1 = true;
    string message = "";
    if (ecoObject1 != null)
    {
      IDBAttribute attributeById = ecoObject1.GetAttributeByID(RevisionComplect.Attr_TermOfChange);
      if (attributeById != null && !attributeById.IsNull)
      {
        flag1 = false;
        message = "Запрещено включать ИИ с указанным сроком внесения изменений";
      }
      long parentKi = ecoObject1.GetParentKI((IUserSession) this.UserSession);
      if (parentKi != 0L && parentKi != dbObject.ObjectID)
      {
        flag1 = false;
        message = "ИИ уже включен в состав другого Комплекта извещений";
      }
    }
    if (!flag1)
      throw new Exception(message);
    ECOObject ecoObject2 = ecoObject1;
    if (ecoObject2 == null)
      return;
    bool flag2 = false;
    if (ecoObject2.ObjectModifyMode == ObjectModifyModes.Checkout && ecoObject2.ObjectID > 0L)
    {
      IMSAttribute4ObjectType attribute4ObjectType1 = MetaDataHelper.GetAttribute4ObjectType(RevisionComplect.Revision_TypeId, RevisionComplect.Attr_Designation);
      IMSAttribute4ObjectType attribute4ObjectType2 = MetaDataHelper.GetAttribute4ObjectType(RevisionComplect.Revision_TypeId, RevisionComplect.Attr_TermOfChange);
      if (attribute4ObjectType1 != null && !attribute4ObjectType1.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
        flag2 = true;
      if (attribute4ObjectType2 != null && !attribute4ObjectType2.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
        flag2 = true;
      if (flag2)
        ecoObject2 = ecoObject2.CheckOut(true) as ECOObject;
    }
    if (ecoObject2 == null)
      return;
    try
    {
      ecoObject2.LockCheckAttributes = true;
      IDBAttribute attributeById1 = this.GetAttributeByID(RevisionComplect.Attr_Designation);
      if (attributeById1 != null)
      {
        object initValue = attributeById1.Value;
        if (initValue != DBNull.Value)
          ecoObject2.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(RevisionComplect.Attr_Designation, initValue)
          });
      }
      IDBAttribute attributeById2 = this.GetAttributeByID(RevisionComplect.Attr_TermOfChange);
      if (attributeById2 != null)
      {
        object initValue = attributeById2.Value;
        if (initValue != DBNull.Value)
          ecoObject2.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(RevisionComplect.Attr_TermOfChange, initValue)
          });
      }
      IDBEditingContextsObject editingContextsObject1 = (IDBEditingContextsObject) ecoObject2;
      IDBEditingContextsObject editingContextsObject2 = dbObject as IDBEditingContextsObject;
      if (editingContextsObject1 == null || editingContextsObject2 == null)
        return;
      editingContextsObject1.LinkedContextNumber = Math.Abs(editingContextsObject2.LinkedContextNumber);
    }
    finally
    {
      ecoObject2.LockCheckAttributes = false;
      if (flag2)
        ecoObject2.CheckIn();
    }
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    base.DoBeforeDeleteRelation(relation, deleteMode);
    if (!(this.UserSession.GetObjectByID(relation.PartID, false) is IDBEditingContextsObject objectById))
      return;
    objectById.LinkedContextNumber = Math.Abs(objectById.ObjectID);
  }

  void IRevComplectObject.SetInventoryNumbers(string template)
  {
    IDBAttribute attributeById = this.GetAttributeByID(RevisionComplect.Attr_InventoryNumber);
    if (attributeById == null)
      return;
    string asString = attributeById.AsString;
  }

  private class OTDObj : IComparable<RevisionComplectObject.OTDObj>
  {
    private string designation;
    private long id;
    private int sortIndex;
    private long relationId;

    public string Designation
    {
      get => this.designation;
      set => this.designation = value;
    }

    public long Id
    {
      get => this.id;
      set => this.id = value;
    }

    public int SortIndex
    {
      get => this.sortIndex;
      set => this.sortIndex = value;
    }

    public long RelationId
    {
      get => this.relationId;
      set => this.relationId = value;
    }

    int IComparable<RevisionComplectObject.OTDObj>.CompareTo(RevisionComplectObject.OTDObj other)
    {
      int num = this.SortIndex.CompareTo(other.SortIndex);
      if (num == 0)
        num = this.Id.CompareTo(other.Id);
      return num;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.ClassifiedObjectType
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

#nullable disable
namespace Intermech.PropertyEditors;

public class ClassifiedObjectType : ICategoryProps4ObjectType, ICategoryProps
{
  private PropDescriptor _propertyDescriptor;
  private Hashtable _possibleValues;
  private string _subscriberID = string.Empty;
  private string _description = string.Empty;
  private int _attributeValue = -1;
  private int _categoryID = 4;
  private int _id = -1;

  public ClassifiedObjectType()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
      this._subscriberID = attributeType.Name;
      this._description = attributeType.Note;
      DataTable possibleValues = attributeType.GetPossibleValues();
      if (possibleValues == null)
        return;
      this._possibleValues = new Hashtable();
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        this._possibleValues.Add(row[attributeType.ValueFieldName], row["F_DESCRIPTION"]);
    }
  }

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
  }

  public string Description => this._description;

  public string SubscriberID => this._subscriberID;

  public PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id)
  {
    if (category == this._categoryID && Convert.ToInt32(id) == this._id && this._propertyDescriptor != null)
      return new PropDescriptor[1]
      {
        this._propertyDescriptor
      };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
        return (PropDescriptor[]) null;
      this._propertyDescriptor = (PropDescriptor) null;
      foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
      {
        if (propDescriptor.DisplayName.Equals(this.SubscriberID))
        {
          this._propertyDescriptor = propDescriptor;
          break;
        }
      }
      IUserSession session = sessionKeeper.Session;
      IDBObject containerForObjectType = Convert.ToInt32(id) < 0 ? (IDBObject) null : customService.GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, Convert.ToInt32(id));
      int key = 0;
      if (containerForObjectType != null)
      {
        IDBAttribute attributeByGuid = containerForObjectType.GetAttributeByGuid(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && attributeByGuid.Value != null && attributeByGuid.Value.ToString() != string.Empty)
          key = Convert.ToInt32(attributeByGuid.Value);
      }
      this._attributeValue = key;
      if (this._propertyDescriptor != null)
        this._propertyDescriptor.SetValue((object) this, (object) this.GetCurrentAttributeValue((long) key));
      else
        this._propertyDescriptor = new PropDescriptor(0, (object) null, this.SubscriberID, (object) this.GetCurrentAttributeValue((long) key), typeof (AttributePossibleValuesClass), (TypeConverter) new AttributePossibleValuesConverter(this._possibleValues), (object) null, string.Empty, this.Description, false, true, false);
      this._categoryID = category;
      this._id = Convert.ToInt32(id);
      PropDescriptor[] propDescriptors;
      if (this._propertyDescriptor == null)
        propDescriptors = (PropDescriptor[]) null;
      else
        propDescriptors = new PropDescriptor[1]
        {
          this._propertyDescriptor
        };
      return propDescriptors;
    }
  }

  private AttributePossibleValuesClass GetCurrentAttributeValue(long key)
  {
    IDictionaryEnumerator enumerator = this._possibleValues.GetEnumerator();
    string empty = string.Empty;
    while (enumerator.MoveNext())
    {
      if (Convert.ToInt64(enumerator.Key) == key)
      {
        empty = enumerator.Value.ToString();
        break;
      }
    }
    return new AttributePossibleValuesClass((object) key, empty);
  }

  public bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
        return false;
      foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
      {
        if (propDescriptor.DisplayName.Equals(this.SubscriberID))
        {
          int int32 = Convert.ToInt32(((AttributePossibleValuesClass) propDescriptor.GetValue(id)).AttributeValue);
          if (int32 != this._attributeValue)
          {
            this.AddAttributeToContainer(customService, sessionKeeper.Session, Convert.ToInt32(id), int32);
            this._attributeValue = int32;
            propDescriptor.ChangedValueApplied = true;
            break;
          }
          break;
        }
      }
      return false;
    }
  }

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
  }

  public void ApplyValuesOnSubfolders(
    PropDescriptorHolder pdh,
    int category,
    object id,
    PropertyDescriptor[] pdList)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
        return;
      foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
      {
        if (propDescriptor.DisplayName.Equals(this.SubscriberID))
        {
          int int32 = Convert.ToInt32(((AttributePossibleValuesClass) propDescriptor.GetValue(id)).AttributeValue);
          DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(Convert.ToInt32(id)).SelectRecursive(string.Empty);
          if (dataTable.Rows.Count > 0)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
              this.AddAttributeToContainer(customService, sessionKeeper.Session, Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"]), int32);
          }
          propDescriptor.ChangedValueApplied = false;
          break;
        }
      }
    }
  }

  private void AddAttributeToContainer(
    IContainerService containerService,
    IUserSession session,
    int objTypeID,
    int value)
  {
    IDBObject containerForObjectType = containerService.GetContainerForObjectType((object) session.SessionGUID, objTypeID, true);
    if (containerForObjectType == null)
      return;
    IDBAttribute dbAttribute = containerForObjectType.GetAttributeByGuid(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
    if (dbAttribute == null)
    {
      IDBAttributeType attributeType = session.GetAttributeType(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
      dbAttribute = containerForObjectType.Attributes.AddAttribute(attributeType.AttributeID, true, (object[]) null);
    }
    dbAttribute.Value = (object) value;
  }
}

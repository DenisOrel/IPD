
// Type: Intermech.Search.ObjectAttributeEditingState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search;

public sealed class ObjectAttributeEditingState : AttributeEditingState
{
  public static readonly ObjectAttributeEditingState Instance = new ObjectAttributeEditingState();

  private ObjectAttributeEditingState()
  {
  }

  public override void AcceptChanges(AttributeEditingComponent component)
  {
    if (component == null)
      throw new ArgumentNullException("attributeEditingComponent");
    long elementIdentifier = ((AttributeValuesEditor.LocalElementInfo) component.Editor.ElementInfo).ElementIdentifier;
    List<AttributeValues> source = new List<AttributeValues>()
    {
      new AttributeValues(component.Editor.AttributeTypeID)
      {
        Values = component.Editor.Values
      }
    };
    int typeId = component.NodeID is NodeID nodeId ? nodeId.ObjectTypeID : -1;
    if (component.AttributesValues != null)
    {
      AttributeValues attributeValues1 = ((IEnumerable<AttributeValues>) component.AttributesValues).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (o => o.AttributeID == component.Editor.AttributeTypeID));
      AttributeValues attributeValues2 = attributeValues1 != null ? attributeValues1.Clone() as AttributeValues : new AttributeValues(component.Editor.AttributeTypeID);
      attributeValues2.Values = component.Editor.Values;
      source = new List<AttributeValues>()
      {
        attributeValues2
      };
    }
    if (ObjectTypeHelper.IsUnknownObjectTypeID(typeId))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(elementIdentifier, false);
        if (dbObject != null)
          typeId = dbObject.ObjectType;
      }
    }
    foreach (IMSAttribute4ObjectType attribute4ObjectType in ((IEnumerable<int>) component.GetPresentAttributes()).Select<int, IMSAttribute4ObjectType>((Func<int, IMSAttribute4ObjectType>) (o => MetaDataHelper.GetAttribute4ObjectType(typeId, o))).Where<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (o => o != null && o.MasterAttributeID == component.Editor.AttributeTypeID)).ToArray<IMSAttribute4ObjectType>())
    {
      IMSAttribute4ObjectType dependentAttributeType = attribute4ObjectType;
      AttributeValues attributeValues = source.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (o => o.AttributeID == dependentAttributeType.AttributeID));
      if (attributeValues == null)
      {
        attributeValues = new AttributeValues(dependentAttributeType.AttributeID);
        source.Add(attributeValues);
      }
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(component.Editor.AttributeTypeID);
      if (dependentAttributeType.FieldType == FieldTypes.ftString || dependentAttributeType.FieldType == FieldTypes.ftMemo)
      {
        if (attributeType.FieldType == FieldTypes.ftObjectLink || attributeType.FieldType == FieldTypes.ftObjectLinkByID)
        {
          if (component.Editor.Value is long objectID)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
              if (dbObject != null)
                attributeValues.Values = new object[1]
                {
                  (object) dbObject.Caption
                };
            }
          }
        }
        else
          attributeValues.Values = (object[]) ((IEnumerable<object>) component.Editor.Values).Select<object, string>((Func<object, string>) (o => o?.ToString())).ToArray<string>();
      }
      else if (dependentAttributeType.FieldType == FieldTypes.ftInteger && (attributeType.FieldType == FieldTypes.ftInteger || attributeType.FieldType == FieldTypes.ftObjectLink || attributeType.FieldType == FieldTypes.ftObjectLinkByID))
        attributeValues.Values = component.Editor.Values;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(elementIdentifier, false)?.SetAttributesValues(source.ToArray());
    if (component.NotificationService == null)
      return;
    component.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", elementIdentifier, typeId, component.AttributesValues, source.ToArray()));
  }

  protected override void DoInitializeEditor(AttributeEditingComponent component)
  {
    long objectVersionId = this.GetObjectVersionID(component.NodeID);
    if (!ObjectHelper.IsUnknownObjectVersionID(objectVersionId))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, false);
        if (dbObject != null)
        {
          ICollection<int> lockedAttributes = ((IAttributesLockService) ServicesManager.GetService(typeof (IAttributesLockService))).GetLockedAttributes(AttributableElements.Object, dbObject.ObjectID, dbObject.ObjectType);
          if (lockedAttributes != null && lockedAttributes.Contains(component.NodeColumn.Attribute.AttributeID))
          {
            component.SetUndetermined();
          }
          else
          {
            component.Editor.ObjectTypeID = dbObject.ObjectType;
            component.Editor.ElementInfo = (IElementInfo) new AttributeValuesEditor.LocalElementInfo(dbObject.ObjectID, AttributableElements.Object);
            AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeCaption);
            component.AttributesValues = attributesValues;
            AttributeValues attributeValues = ((IEnumerable<AttributeValues>) attributesValues).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (o => o.AttributeID == component.NodeColumn.Attribute.AttributeID));
            if (attributeValues != null)
            {
              if (!attributeValues.ReadOnly && !AttributeTypeHelper.IsManualEditingDisabled4ObjectType(dbObject.ObjectType, attributeValues.AttributeID))
                component.Editor.Values = AttributeHelper.GetAttributeValues(attributeValues);
              else
                component.SetUndetermined();
            }
            else if (ObjectTypeHelper.IsManualOrAnyAttribute(dbObject.ObjectType, component.NodeColumn.Attribute.AttributeID))
              component.Editor.Value = (object) null;
            else
              component.SetUndetermined();
          }
        }
        else
          component.SetUndetermined();
      }
    }
    else
      component.SetUndetermined();
  }

  private long GetObjectVersionID(INodeID nodeID)
  {
    return !(nodeID is NodeID nodeId) ? 0L : nodeId.ObjectID;
  }
}

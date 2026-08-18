
// Type: Intermech.Search.RelationAttributeEditingState
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

public sealed class RelationAttributeEditingState : AttributeEditingState
{
  public static readonly RelationAttributeEditingState Instance = new RelationAttributeEditingState();

  private RelationAttributeEditingState()
  {
  }

  public override void AcceptChanges(AttributeEditingComponent component)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    long elementIdentifier = ((AttributeValuesEditor.LocalElementInfo) component.Editor.ElementInfo).ElementIdentifier;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(elementIdentifier, false);
      if (relation != null)
        relation.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(component.Editor.AttributeTypeID)
          {
            Values = component.Editor.Values
          }
        });
    }
    if (component.NotificationService == null)
      return;
    component.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", elementIdentifier));
  }

  protected override void DoInitializeEditor(AttributeEditingComponent component)
  {
    long relationId = this.GetRelationID(component.NodeID);
    if (!RelationHelper.IsUnknownRelationID(relationId))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(relationId, false);
        if (relation != null)
        {
          component.Editor.RelationTypeID = relation.RelationType;
          component.Editor.ElementInfo = (IElementInfo) new AttributeValuesEditor.LocalElementInfo(relationId, AttributableElements.Relation);
          AttributeValues[] attributesValues = relation.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes);
          component.AttributesValues = attributesValues;
          AttributeValues attributeValues = ((IEnumerable<AttributeValues>) attributesValues).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (o => o.AttributeID == component.NodeColumn.Attribute.AttributeID));
          if (attributeValues != null)
          {
            if (!attributeValues.ReadOnly && !AttributeTypeHelper.IsManualEditingDisabled4RelationType(relation.RelationType, attributeValues.AttributeID))
              component.Editor.Values = AttributeHelper.GetAttributeValues(attributeValues);
            else
              component.SetUndetermined();
          }
          else if (RelationTypeHelper.IsManualOrAnyAttribute(relation.RelationType, component.NodeColumn.Attribute.AttributeID))
            component.Editor.Value = (object) null;
          else
            component.SetUndetermined();
        }
        else
          component.SetUndetermined();
      }
    }
    else
      component.SetUndetermined();
  }

  private long GetRelationID(INodeID nodeID) => !(nodeID is NodeID nodeId) ? 0L : nodeId.PrjLinkID;
}

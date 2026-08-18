// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CheckoutOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class CheckoutOperations
{
  public bool RequireCheckoutOnRelationModification(
    int relationType,
    SectionEntity projectItem,
    SectionEntity partItem)
  {
    if (relationType == -1)
      throw new ArgumentException();
    int objectType1 = ObjectSection.GetObjectType(projectItem);
    int objectType2 = ObjectSection.GetObjectType(partItem);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(relationType, objectType2, objectType1);
      return applicability != null && applicability.IsContent;
    }
  }

  public bool RequireCheckoutOnRelationModification(
    int relationType,
    SectionEntity projectItem,
    IDBObjectRef partItem)
  {
    if (relationType == -1)
      throw new ArgumentException();
    int objectType1 = ObjectSection.GetObjectType(projectItem);
    int objectType2 = -1;
    if (partItem is IDBTypedEntityRef dbTypedEntityRef)
      objectType2 = dbTypedEntityRef.GetEntityType();
    if (objectType2 == -1)
      objectType2 = DBHelper.GetObjectType(partItem.GetObjectId());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(relationType, objectType2, objectType1);
      return applicability != null && applicability.IsContent;
    }
  }

  public bool RequireCheckoutOnRelationAttribute(
    int relationType,
    SectionEntity projectItem,
    SectionEntity partItem,
    StringKey attribute)
  {
    return this.RequireCheckoutOnRelationAttribute(relationType, attribute) && this.RequireCheckoutOnRelationModification(relationType, projectItem, partItem);
  }

  public bool RequireCheckoutOnRelationAttribute(
    int relationType,
    SectionEntity projectItem,
    IDBObjectRef partItem,
    StringKey attribute)
  {
    return this.RequireCheckoutOnRelationAttribute(relationType, attribute) && this.RequireCheckoutOnRelationModification(relationType, projectItem, partItem);
  }

  private bool RequireCheckoutOnRelationAttribute(int relationType, StringKey attribute)
  {
    if (relationType == -1)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetRelationType(relationType, true).GetAttributeType((string) attribute);
      return attributeType != null ? attributeType.IsContent : sessionKeeper.Session.GetAttributeType((string) attribute, true).IsContent;
    }
  }

  public bool RequireCheckoutOnObjectAttribute(int objectType, StringKey attribute)
  {
    if (objectType == -1)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetObjectType(objectType, true).GetAttributeType((string) attribute);
      return attributeType != null ? attributeType.IsContent : sessionKeeper.Session.GetAttributeType((string) attribute, true).IsContent;
    }
  }
}

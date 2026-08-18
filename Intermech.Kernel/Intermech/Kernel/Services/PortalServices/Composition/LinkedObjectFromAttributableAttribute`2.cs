// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.LinkedObjectFromAttributableAttribute`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal abstract class LinkedObjectFromAttributableAttribute<TPublishCompositionAttributable, TObjectHandler> : 
  LinkedObjectFromAttribute<TPublishCompositionAttributable, TObjectHandler>
  where TPublishCompositionAttributable : IIncludeTyped
{
  private readonly IIDLinkTranslate _linkService;
  private readonly ExtendedPublishOptions _options;

  public LinkedObjectFromAttributableAttribute(
    ExtendedPublishOptions options,
    ICustomObjectAnalyzer analyzer,
    List<PublishCompositionObject> objects)
    : base(analyzer, objects)
  {
    this._options = options;
    this._linkService = ServerServices.GetService(typeof (IIDLinkTranslate)) as IIDLinkTranslate;
  }

  protected override List<IDBObject> GetLinkedObjects(
    IUserSession session,
    TPublishCompositionAttributable attributable)
  {
    IDBAttributableType attributableType = this.GetAttributableType(session, attributable);
    List<int> attributes = (List<int>) null;
    if (!attributableType.AnyAttributes)
    {
      attributes = this.GetTypeLinkAttributes(session, attributableType.Attributes);
      if (attributes.Count == 0)
        return (List<IDBObject>) null;
    }
    return this.GetLinkedObjectsFromAttributable(session, this.GetAttributable(session, attributable), attributes);
  }

  private List<IDBObject> GetLinkedObjectsFromAttributable(
    IUserSession session,
    IDBAttributable attributable,
    List<int> attributes)
  {
    List<IDBObject> result = new List<IDBObject>();
    if (attributes == null)
    {
      foreach (AttributeValues attributesValue in attributable.GetAttributesValues(GetAttributeValuesModes.None))
      {
        if (attributesValue.Value != null && attributesValue.Value != DBNull.Value && this.CheckAttribute(session.GetAttributeType(attributesValue.AttributeID)))
          this.HandleAttributeValue(session, attributesValue.AsInteger, attributesValue.AttributeType, result);
      }
    }
    else
    {
      foreach (int attribute in attributes)
      {
        IDBAttribute attributeById = attributable.GetAttributeByID(attribute);
        if (attributeById != null)
          this.HandleAttributeValue(session, attributeById.AsInteger, attributeById.AttributeType.AttributeType, result);
      }
    }
    return result;
  }

  private void HandleAttributeValue(
    IUserSession session,
    long integerValue,
    FieldTypes attributeType,
    List<IDBObject> result)
  {
    if (integerValue == 0L || this._objects.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID == integerValue)))
      return;
    IDBObject dbObject = attributeType == FieldTypes.ftObjectLinkByID ? session.GetObjectBaseVersionByID(integerValue, false) : session.GetObject(integerValue, false);
    if (dbObject == null)
      return;
    result.Add(dbObject);
  }

  private List<int> GetTypeLinkAttributes(
    IUserSession session,
    IDBAttribute4TypeCollection attributesCollection)
  {
    List<int> typeLinkAttributes = new List<int>();
    foreach (DataRow row in (InternalDataCollectionBase) attributesCollection.Select("F_ATTRIBUTE_ID").Rows)
    {
      IDBAttributeType attributeType = session.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      if (this.CheckAttribute(attributeType))
        typeLinkAttributes.Add(attributeType.AttributeID);
    }
    return typeLinkAttributes;
  }

  private bool CheckAttribute(IDBAttributeType attributeType)
  {
    if (attributeType.AttributeType != FieldTypes.ftObjectLink && attributeType.AttributeType != FieldTypes.ftObjectLinkByID && (attributeType.AttributeType != FieldTypes.ftInteger || !this._linkService.IsIDLink(attributeType.AttributeID)))
      return false;
    return attributeType.SizeType <= 0L || this._options.EnableTypes == null || this._options.EnableTypes.Contains(Convert.ToInt32(attributeType.SizeType));
  }

  protected abstract IDBAttributableType GetAttributableType(
    IUserSession session,
    TPublishCompositionAttributable attributable);

  protected abstract IDBAttributable GetAttributable(
    IUserSession session,
    TPublishCompositionAttributable attributable);
}

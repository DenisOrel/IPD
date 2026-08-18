
// Type: Intermech.Interfaces.Data.Actions.FixRelationAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Localization;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Data.Actions;

public sealed class FixRelationAction : IAction
{
  private IDBRelationRef relationRef;
  private IDBObjectRef fixedPartRef;
  private RevisionInstantiationMode fixMode;

  public FixRelationAction(
    IDBRelationRef relationRef,
    IDBObjectRef fixedPartRef,
    RevisionInstantiationMode fixMode)
  {
    if (relationRef == null)
      throw new ArgumentNullException();
    if (fixedPartRef == null)
      throw new ArgumentNullException();
    this.relationRef = relationRef;
    this.fixedPartRef = fixedPartRef;
    this.fixMode = fixMode;
  }

  public FixRelationAction(IDBRelationRef relationRef, IDBObjectRef fixedPartRef)
    : this(relationRef, fixedPartRef, RevisionInstantiationMode.Default)
  {
  }

  public void Perform()
  {
    long projectId = this.relationRef.GetProjectId();
    Guid relationGuid = this.relationRef.GetRelationGuid();
    long num = Math.Abs(this.fixedPartRef.GetObjectId());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relationGuid, projectId, true);
      relation.Attributes.AddAttribute(FixRelationAction.InternalCaches.IDCache.FixedRelation.Id, false, new object[1]
      {
        (object) num
      });
      IDBAttribute attributeById = relation.GetAttributeByID(FixRelationAction.InternalCaches.IDCache.FixedRelationMode.Id);
      if (attributeById == null)
      {
        if (this.fixMode == RevisionInstantiationMode.Default)
          return;
        relation.Attributes.AddAttribute(FixRelationAction.InternalCaches.IDCache.FixedRelationMode.Id, false, new object[1]
        {
          (object) (long) this.fixMode
        });
      }
      else
        attributeById.AsInteger = (long) this.fixMode;
    }
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1654");

  private static class InternalCaches
  {
    private static readonly FixRelationAction.InternalIDCache idCache = new FixRelationAction.InternalIDCache(MetadataResolvers.Factory);

    public static FixRelationAction.InternalIDCache IDCache
    {
      [DebuggerStepThrough] get => FixRelationAction.InternalCaches.idCache;
    }
  }

  private sealed class InternalIDCache
  {
    public InternalIDCache(MetadataResolverFactory metadataResolvers)
    {
      this.FixedRelation = metadataResolvers.AttributeTypeResolver(new Guid("CAD001C2-306C-11D8-B4E9-00304F19F545"));
      this.FixedRelationMode = metadataResolvers.AttributeTypeResolver(new Guid("CADD9609-306C-11D8-B4E9-00304F19F545"));
    }

    public AttributeTypeResolver FixedRelation { get; private set; }

    public AttributeTypeResolver FixedRelationMode { get; private set; }
  }
}

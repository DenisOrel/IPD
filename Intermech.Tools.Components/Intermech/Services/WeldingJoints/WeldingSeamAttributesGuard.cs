// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamAttributesGuard
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Kernel.Entities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Защитник атрибутов сварных швов, созданных по CAD-модели. Он запрещает изменение через интерфейс пользователя
/// атрибутов сварных швов, которые забираются интегратором из файла CAD-модели.
/// </summary>
internal sealed class WeldingSeamAttributesGuard
{
  private IAttributesLockService attributesLockService;
  private Lazy<IWeldingSeamsModelRoot> modelRoot;
  private bool isStarted;
  private GlobalId<int> weldingSeamsType;
  private List<GlobalId<int>> weldingSeamsAttributes;
  private GlobalId<int> basedOnCADModelAttribute;

  public WeldingSeamAttributesGuard(
    IAttributesLockService attributesLockService,
    Lazy<IWeldingSeamsModelRoot> modelRoot)
  {
    if (attributesLockService == null)
      throw new ArgumentNullException(nameof (attributesLockService));
    if (modelRoot == null)
      throw new ArgumentNullException(nameof (modelRoot));
    this.attributesLockService = attributesLockService;
    this.modelRoot = modelRoot;
  }

  public void Start()
  {
    if (this.isStarted)
      throw new InvalidOperationException($"A handler '{this.GetType()}' is already started.");
    try
    {
      DBMetadataInfoService metadataInfoService = this.modelRoot.Value.GetMetadataInfoService();
      this.weldingSeamsType = metadataInfoService.GetTypeId<WeldingSeamEntity>();
      this.basedOnCADModelAttribute = metadataInfoService.GetAttributeId<WeldingSeamEntity, bool>((Expression<Func<WeldingSeamEntity, bool>>) (e => e.BasedOnCADModel));
      this.weldingSeamsAttributes = metadataInfoService.GetAttributeIdList<WeldingSeamEntity>();
      this.weldingSeamsAttributes.RemoveAll((Predicate<GlobalId<int>>) (x => x.Id < 0));
      this.weldingSeamsAttributes.Remove(this.basedOnCADModelAttribute);
      this.attributesLockService.GetLockedAttributesHandler += new EventHandler<AttributesLockArgs>(this.HandleEvent);
      this.isStarted = true;
    }
    catch
    {
      this.StopInternal();
      throw;
    }
  }

  public void Stop()
  {
    if (!this.isStarted)
      return;
    this.StopInternal();
    this.isStarted = false;
  }

  private void StopInternal()
  {
    this.attributesLockService.GetLockedAttributesHandler -= new EventHandler<AttributesLockArgs>(this.HandleEvent);
    this.weldingSeamsType = (GlobalId<int>) null;
    this.basedOnCADModelAttribute = (GlobalId<int>) null;
    this.weldingSeamsAttributes = (List<GlobalId<int>>) null;
  }

  private void HandleEvent(object sender, AttributesLockArgs e)
  {
    if (e.ElementKind != AttributableElements.Object || e.ElementType != this.weldingSeamsType.Id || !this.IsGuardedWeldingSeam(e.ElementId))
      return;
    foreach (GlobalId<int> weldingSeamsAttribute in this.weldingSeamsAttributes)
      e.LockedAttributes.Add(weldingSeamsAttribute.Id);
  }

  private bool IsGuardedWeldingSeam(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
      {
        object[] valuesById = dbObject.GetValuesByID(this.basedOnCADModelAttribute.Id, false);
        if (valuesById != null)
        {
          if (valuesById.Length != 0)
          {
            if (object.Equals(valuesById[0], (object) true))
              return true;
          }
        }
      }
    }
    return false;
  }
}

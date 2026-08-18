// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CompositionObjectHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class CompositionObjectHandler : InfoRequiredService
{
  private readonly CaptionCache _cacheCaptions;
  private readonly bool _linked;
  private readonly Dictionary<int, int> _customIndexes;
  private readonly List<long> _handledRelations;

  public CompositionObjectHandler(
    CaptionCache cache,
    bool infoRequired,
    bool firstLevel,
    bool linked,
    Dictionary<int, int> customIndexes)
    : base(infoRequired)
  {
    this._cacheCaptions = cache;
    this.FirstLevel = firstLevel;
    this._linked = linked;
    this._customIndexes = customIndexes;
    this._handledRelations = new List<long>();
  }

  public bool NotInArray(
    List<PublishCompositionObject> array,
    List<PublishCompositionObject> array2,
    PublishCompositionObject pco)
  {
    return !array.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(pco.ObjectID))) && !array2.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(pco.ObjectID)));
  }

  public bool NotInArray(
    List<PublishCompositionObject> array,
    List<PublishCompositionObject> array2,
    long objectID)
  {
    return !array.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(objectID))) && !array2.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(objectID)));
  }

  public bool NotInArray(List<PublishCompositionObject> array, long objectID)
  {
    return !array.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(objectID)));
  }

  public bool NotInArray(List<PublishCompositionObject> array, PublishCompositionObject pco)
  {
    return !array.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID.Equals(pco.ObjectID)));
  }

  public void HandleObject(
    IUserSession session,
    PublishCompositionObject pco,
    PublishCompositionRelation pcr,
    List<PublishCompositionObject> filteredRootObjects,
    List<PublishCompositionObject> resultListObjects,
    List<PublishCompositionRelation> resultReations)
  {
    this.HandleObject(session, pco, pcr, filteredRootObjects, resultListObjects, resultReations, false);
  }

  public void HandleObject(
    IUserSession session,
    PublishCompositionObject pco,
    PublishCompositionRelation pcr,
    List<PublishCompositionObject> filteredRootObjects,
    List<PublishCompositionObject> resultListObjects,
    List<PublishCompositionRelation> resultReations,
    bool withoutMessage)
  {
    if (pcr != null)
    {
      if (this._handledRelations.IndexOf(pcr.PrjLinkID) >= 0)
        return;
      this._handledRelations.Add(pcr.PrjLinkID);
    }
    if (!this.HandleFilterIncludes(pco, withoutMessage))
    {
      if (pco.Include == IncludeTypes.NoChanged)
      {
        if (this.NotInArray(filteredRootObjects, resultListObjects, pco))
          filteredRootObjects.Add(pco);
        if (!withoutMessage)
        {
          if (pco.ProjID != 0L)
            this.AddReasonInfo(pco, $"Входит в состав {this._cacheCaptions.GetCaption(pco.ProjID)}");
          else
            this.AddReasonInfo(pco, "Выбран для публикации");
          this.AddNoChangedsMessage(pco);
        }
      }
      else if (PublishOptionsHelper.NormalPublish(pco.Include))
      {
        if (this.NotInArray(filteredRootObjects, resultListObjects, pco))
          filteredRootObjects.Add(pco);
        if (!withoutMessage)
        {
          if (this.FirstLevel)
          {
            if (!this._linked && string.IsNullOrEmpty(pco.ReasonInfo))
            {
              this.AddReasonInfo(pco, "Выбран для публикации");
              if (pco.Include == IncludeTypes.FCAttributesOnly || pco.Include == IncludeTypes.FCFileAttributesOnly)
                this.AddReasonInfo(pco, Helper.MessageFCAttribute);
            }
          }
          else
          {
            this.AddReasonInfo(pco, $"Входит в состав {this._cacheCaptions.GetCaption(pco.ProjID)}");
            if (pco.Include == IncludeTypes.FCAttributesOnly || pco.Include == IncludeTypes.FCFileAttributesOnly)
              this.AddReasonInfo(pco, Helper.MessageFCAttribute);
          }
        }
      }
    }
    this._cacheCaptions.Add(pco.ObjectID, pco.ObjectType, pco.Caption);
    if (this.NotInArray(resultListObjects, pco) && PublishOptionsHelper.NormalPublish(pco.Include))
      resultListObjects.Add(pco);
    if (pcr == null || resultReations.Find((Predicate<PublishCompositionRelation>) (x => x.PrjLinkID == pcr.PrjLinkID)) != null || this.NotInArray(filteredRootObjects, resultListObjects, pco) && !PublishOptionsHelper.DummyPublish(pcr.Include))
      return;
    resultReations.Add(pcr);
  }

  public bool FirstLevel { get; set; }
}

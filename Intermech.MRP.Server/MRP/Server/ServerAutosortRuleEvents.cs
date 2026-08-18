// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.ServerAutosortRuleEvents
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal static class ServerAutosortRuleEvents
{
  private static int _relTypeArticles = -1;
  private static Guid _relTypeArticlesGuid = new Guid("cad00023-306c-11d8-b4e9-00304f19f545");
  private static int _relTypeDocuments = -1;
  private static Guid _relTypeDocumentsGuid = new Guid("cad00154-306c-11d8-b4e9-00304f19f545");
  private static int _relTypeDocCompositions = -1;
  private static Guid _relTypeDocCompositionsGuid = new Guid("cad0057c-306c-11d8-b4e9-00304f19f545");

  private static void CheckConsts()
  {
    if (ServerAutosortRuleEvents._relTypeArticles != -1)
      return;
    ServerAutosortRuleEvents._relTypeArticles = MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
    ServerAutosortRuleEvents._relTypeDocuments = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    ServerAutosortRuleEvents._relTypeDocCompositions = MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545");
  }

  public static void CompositionsGetVisibleRelationsEventHandler(
    object sender,
    CompositionsAutosortRuleEventArgs e)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(e.ObjectType);
    if (objectType == null)
      return;
    ServerAutosortRuleEvents.CheckConsts();
    List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(objectType.ObjectTypeID);
    if (applicabilityRelationTypesId.IndexOf(ServerAutosortRuleEvents._relTypeArticles) >= 0 && e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeArticles) < 0)
      e.VisibleRelTypes.Insert(0, ServerAutosortRuleEvents._relTypeArticles);
    if (!(ServerServices.GetService(typeof (IMRPSettings)) is IMRPSettings service) || !service.UseDocumentation)
      return;
    if (applicabilityRelationTypesId.IndexOf(ServerAutosortRuleEvents._relTypeDocuments) >= 0 && e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeDocuments) < 0)
      e.VisibleRelTypes.Add(ServerAutosortRuleEvents._relTypeDocuments);
    if (applicabilityRelationTypesId.IndexOf(ServerAutosortRuleEvents._relTypeDocCompositions) < 0 || e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeDocCompositions) >= 0)
      return;
    e.VisibleRelTypes.Add(ServerAutosortRuleEvents._relTypeDocCompositions);
  }

  public static void CompositionsGetVisibleRelationsGuidEventHandler(
    object sender,
    CompositionsAutosortRuleGuidEventArgs e)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(e.ObjectType);
    if (objectType == null)
      return;
    ServerAutosortRuleEvents.CheckConsts();
    List<Guid> relationTypesGuids = MetaDataHelper.GetApplicabilityRelationTypesGuids(objectType.Guid);
    if (relationTypesGuids.IndexOf(ServerAutosortRuleEvents._relTypeArticlesGuid) >= 0 && e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeArticlesGuid) < 0)
      e.VisibleRelTypes.Insert(0, ServerAutosortRuleEvents._relTypeArticlesGuid);
    if (!(ServerServices.GetService(typeof (IMRPSettings)) is IMRPSettings service) || !service.UseDocumentation)
      return;
    if (relationTypesGuids.IndexOf(ServerAutosortRuleEvents._relTypeDocumentsGuid) >= 0 && e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeDocumentsGuid) < 0)
      e.VisibleRelTypes.Add(ServerAutosortRuleEvents._relTypeDocumentsGuid);
    if (relationTypesGuids.IndexOf(ServerAutosortRuleEvents._relTypeDocCompositionsGuid) < 0 || e.VisibleRelTypes.IndexOf(ServerAutosortRuleEvents._relTypeDocCompositionsGuid) >= 0)
      return;
    e.VisibleRelTypes.Add(ServerAutosortRuleEvents._relTypeDocCompositionsGuid);
  }
}

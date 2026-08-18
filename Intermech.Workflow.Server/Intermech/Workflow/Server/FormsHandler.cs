// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.FormsHandler
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

public class FormsHandler
{
  private static void FormDesignerService_Update(object sender, UpdateHandlerEventArgs args)
  {
    if (!(args.Parent is IActivity))
      return;
    IDBAttribute attributeById = args.Parent.GetAttributeByID(wfConsts.AttrFormID);
    if (attributeById == null || attributeById.AsInteger <= 0L)
      return;
    args.NewList = new List<FormInformation>();
    IDBObject iDBObj = args.Parent.Session.GetObject(attributeById.AsInteger, false);
    if (iDBObj != null)
      args.NewList.Add(new FormInformation(iDBObj));
    args.ContinueProcessing = false;
    args.StoreInVersionCache = true;
    args.StoreInTypesCache = false;
  }

  public static void RegisterHandlers(IFormDesignerServer formsServer, IUserSession session)
  {
    formsServer.Register(wfConsts.ActivitiesTypeID, AttributableElements.Object, new UpdateHandlerInfo(10, new UpdateHandler(FormsHandler.FormDesignerService_Update)), session);
  }
}

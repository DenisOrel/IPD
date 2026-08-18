// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.AttributesHandlerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class AttributesHandlerService : 
  LongLifeObject,
  IExportAttributesHandlerService,
  IImportAttributesHandlerService
{
  private Dictionary<int, ExportAttributeHandler> _exportAttributeHandlers;
  private Dictionary<int, ImportAttributeHandler> _importAttributeHandlers;

  public AttributesHandlerService(IUserSession session)
  {
    this._exportAttributeHandlers = new Dictionary<int, ExportAttributeHandler>();
    this._exportAttributeHandlers.Add(session.IdentHelper.LoginNameID, (ExportAttributeHandler) new LoginExportAttributeHandler());
    this._exportAttributeHandlers.Add(session.IdentHelper.UserNameID, (ExportAttributeHandler) new UserNameExportAttributeHandler());
    this._importAttributeHandlers = new Dictionary<int, ImportAttributeHandler>();
    this._importAttributeHandlers.Add(session.GetAttributeType(new Guid("cad001af-306c-11d8-b4e9-00304f19f545")).AttributeID, (ImportAttributeHandler) new AreaImportAttributeHandler());
    this._importAttributeHandlers.Add(session.GetAttributeType(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545")).AttributeID, (ImportAttributeHandler) new FolderKeyImportAttributeHandler());
    ObjectTypeGuidImportAttributeHandler attributeHandler = new ObjectTypeGuidImportAttributeHandler();
    this._importAttributeHandlers.Add(session.GetAttributeType(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545")).AttributeID, (ImportAttributeHandler) attributeHandler);
    this._importAttributeHandlers.Add(session.GetAttributeType(new Guid("cad00149-306c-11d8-b4e9-00304f19f545")).AttributeID, (ImportAttributeHandler) attributeHandler);
  }

  public ExportAttributeHandler ChangeValue(IUserSession session, IDBAttribute attribute)
  {
    ExportAttributeHandler attributeHandler = (ExportAttributeHandler) null;
    if (this._exportAttributeHandlers.TryGetValue(attribute.AttributeID, out attributeHandler))
      attributeHandler.Handle(session, attribute);
    return attributeHandler;
  }

  public void HandleValue(SpecHandleAttributeEventArgs e, Dictionary<string, object> tag)
  {
    ImportAttributeHandler attributeHandler = (ImportAttributeHandler) null;
    if (!this._importAttributeHandlers.TryGetValue(e.AttributeID, out attributeHandler))
      return;
    attributeHandler.Handle(e, tag);
  }

  public void Register(int attributeID, ExportAttributeHandler handler)
  {
    this._exportAttributeHandlers.Add(attributeID, handler);
  }

  public void Register(int attributeID, ImportAttributeHandler handler)
  {
    this._importAttributeHandlers.Add(attributeID, handler);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportUnitHandlerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportUnitHandlerService : LongLifeObject, IImportUnitHandlerService
{
  private readonly List<ImportRelationHandler> _relationHandlers = new List<ImportRelationHandler>();

  public void Register(ImportRelationHandler handler) => this._relationHandlers.Add(handler);

  public void HandleImportRelation(
    RelationRecord relationRecord,
    ImportedInfo project,
    ImportedInfo part)
  {
    using (List<ImportRelationHandler>.Enumerator enumerator = this._relationHandlers.GetEnumerator())
    {
      do
        ;
      while (enumerator.MoveNext() && !enumerator.Current.Handle(relationRecord, project, part));
    }
  }
}

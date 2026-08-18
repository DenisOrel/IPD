// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.DBObjectPrototypeFixup
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class DBObjectPrototypeFixup : ServiceExtender
{
  private IObjectCreatorService objectCreatorService;

  public DBObjectPrototypeFixup(IObjectCreatorService objectCreatorService)
  {
    this.objectCreatorService = objectCreatorService;
  }

  protected override void DoEnable()
  {
    base.DoEnable();
    this.objectCreatorService.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.FixupNewObject);
    this.objectCreatorService.FilesRenamedEvent += new FilesRenamedEventHandler(this.FixupPrototypedObject);
  }

  protected override void DoDisable()
  {
    base.DoDisable();
    this.objectCreatorService.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this.FixupNewObject);
    this.objectCreatorService.FilesRenamedEvent -= new FilesRenamedEventHandler(this.FixupPrototypedObject);
  }

  private void FixupNewObject(object sender, AfterObjectCreatedEventArgs args)
  {
    DBObjectTypeFileHandlingRules fileHandlingRules = IntegratorServices.GetFileHandlingRules(args.ObjectTypeID);
    if (fileHandlingRules.IntegratorRef == null || !fileHandlingRules.RequireNormalEditMode)
      return;
    IntegratorServices.GetService<IPrepareNewObjectsService>(fileHandlingRules.IntegratorRef, false)?.PrepareNewObject(args.ObjectID);
  }

  private void FixupPrototypedObject(object sender, FilesRenamedEventArgs args)
  {
    DBObjectTypeFileHandlingRules fileHandlingRules = IntegratorServices.GetFileHandlingRules(DBHelper.GetObjectType(args.ObjectID));
    if (fileHandlingRules.IntegratorRef == null || !fileHandlingRules.RequireNormalEditMode)
      return;
    IntegratorServices.GetService<IPrepareNewObjectsService>(fileHandlingRules.IntegratorRef, false)?.PreparePrototypedObjectFiles(args.ObjectID, args.PrototypeID);
  }
}

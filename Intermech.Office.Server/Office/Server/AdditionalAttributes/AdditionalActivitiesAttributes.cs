// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.AdditionalAttributes.AdditionalActivitiesAttributes
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Office.Server.AdditionalAttributes;

internal abstract class AdditionalActivitiesAttributes : IAdditionalActivitiesAttributes
{
  public void CreateAttributes(
    IUserSession session,
    IProcess process,
    ResolutionProcessExecuteArgs args)
  {
    if (!this.EnableCreate(session, process, args))
      return;
    foreach (IDBAttributable activity in process.Activities)
      this.AddValue(activity.Attributes.AddAttribute(this.AdditionalAttribute, false));
  }

  protected abstract int AdditionalAttribute { get; }

  protected abstract void AddValue([NotNull] IDBAttribute attribute);

  protected virtual bool EnableCreate(
    [NotNull] IUserSession session,
    [NotNull] IProcess process,
    [NotNull] ResolutionProcessExecuteArgs args)
  {
    return true;
  }
}

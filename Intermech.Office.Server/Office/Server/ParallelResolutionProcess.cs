// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.ParallelResolutionProcess
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Office.Interfaces;
using Intermech.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Office.Server;

internal class ParallelResolutionProcess([NotNull] string name, bool controlResolution) : 
  ResolutionProcess(Intermech.Diagnostics.Check.ArgumentNotNull<string>(name, nameof (name)), controlResolution)
{
  protected override void Initialize([NotNull] OrderProcessTemplates processTemplates)
  {
    this._ProcessTemplate = this._Control ? processTemplates.Control : processTemplates.NoControl;
  }

  protected override void OnExecute(
    IUserSession session,
    IDBObject resolution,
    [NotNull] IProcess process,
    [NotNull] IList<long> executorIDs)
  {
    IVariable variable = process.StartActivity.Variables.Find("COMMISSION_USER");
    if (variable == null)
      throw new VariableMissingException("COMMISSION_USER");
    ParticipantList participantList = new ParticipantList(session);
    foreach (long executorId in (IEnumerable<long>) executorIDs)
      participantList.AddParticipant(ParticipantKind.User, executorId);
    variable.Value = participantList.AsString;
  }
}

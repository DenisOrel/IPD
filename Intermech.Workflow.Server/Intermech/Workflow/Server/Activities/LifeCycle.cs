// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.LifeCycle
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class LifeCycle(UserSession uSession, DataTable objectsTable) : SystemActivity(uSession, objectsTable)
{
  public override ActivityKind Kind => ActivityKind.LCStep;

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    if (this.LCList.Count == 0 || this.LCList.Invalid)
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString("Workflow.Server_25"), (object) this.Name));
    return s;
  }
}

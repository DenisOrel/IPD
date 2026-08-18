// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseProcesses
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

public class BriefcaseProcesses : LongLifeObject, IBriefcaseProcesses
{
  private Dictionary<Guid, int> _briefcaseIndexes;
  private int _maxIndex;

  public BriefcaseProcesses()
  {
    this._briefcaseIndexes = new Dictionary<Guid, int>(1);
    this._maxIndex = 0;
  }

  public int StartImport(Guid BriefcaseGuid)
  {
    int index = ++this._maxIndex;
    this._briefcaseIndexes.Add(BriefcaseGuid, index);
    StartImportEventHandler startImportEvent = this.StartImportEvent;
    if (startImportEvent != null)
      startImportEvent((object) this, new BriefcaseInfoEventArgs(index));
    return index;
  }

  public void StopImport(Guid BriefcaseGuid)
  {
    int index = 0;
    if (this._briefcaseIndexes.TryGetValue(BriefcaseGuid, out index))
    {
      StopImportEventHandler stopImportEvent = this.StopImportEvent;
      if (stopImportEvent != null)
        stopImportEvent((object) this, new BriefcaseInfoEventArgs(index));
      this._briefcaseIndexes.Remove(BriefcaseGuid);
    }
    if (this._briefcaseIndexes.Count != 0)
      return;
    this._maxIndex = 0;
  }

  public void ImportObject(int briefcaseIndex, IDBObject newObject, ImportingObject oldObject)
  {
    ImportObjectEventHandler importObjectEvent = this.ImportObjectEvent;
    if (importObjectEvent == null)
      return;
    importObjectEvent((object) this, new ImportedObjectInfoEventArgs(newObject, oldObject, new BriefcaseInfoEventArgs(briefcaseIndex)));
  }

  public event StartImportEventHandler StartImportEvent;

  public event StopImportEventHandler StopImportEvent;

  public event ImportObjectEventHandler ImportObjectEvent;
}

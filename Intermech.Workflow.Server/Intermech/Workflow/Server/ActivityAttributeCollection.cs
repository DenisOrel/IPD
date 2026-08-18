// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ActivityAttributeCollection
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;

#nullable disable
namespace Intermech.Workflow.Server;

internal class ActivityAttributeCollection(
  UserSession uSession,
  long objectID,
  int objectType,
  IDBAttributable parent) : DBObjectAttributeCollection(uSession, objectID, objectType, parent)
{
  internal bool VirtualAdded;

  private void DeleteExtraTempAttributes(IDBAttributeCollection sourceAttributes)
  {
    for (int AttrIndex = this.Count - 1; AttrIndex >= 0; --AttrIndex)
    {
      if (this[AttrIndex].TemporaryAttribute)
      {
        IDBAttribute byId = sourceAttributes.FindByID(this[AttrIndex].AttributeID);
        if (byId != null && !byId.TemporaryAttribute)
          this[AttrIndex].Delete(0L);
      }
    }
  }

  public override void Assign(IDBAttributeCollection sourceAttributes, int assignMode)
  {
    if (this._Parent is WFActivity parent)
      parent.InAssignAttributes = true;
    try
    {
      this.DeleteExtraTempAttributes(sourceAttributes);
      base.Assign(sourceAttributes, assignMode);
    }
    finally
    {
      if (parent != null)
        parent.InAssignAttributes = false;
    }
  }

  public override int[] AssignPossibleAttributes(
    IDBAttributeCollection sourceAttributes,
    int assignMode)
  {
    string key = $"AssignPossibleAttributes_{this.ObjectID}";
    try
    {
      this.DeleteExtraTempAttributes(sourceAttributes);
      this.UserSession.SetSessionPluginsData((object) key, (object) true);
      return base.AssignPossibleAttributes(sourceAttributes, assignMode);
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }
}

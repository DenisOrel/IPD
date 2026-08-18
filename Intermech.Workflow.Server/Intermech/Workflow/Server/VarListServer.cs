// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.VarListServer
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public static class VarListServer
{
  public static void AddVirtualAttributes(this VarList vars, IDBObject obj)
  {
    bool virtualAdded = vars.VirtualAdded;
    ActivityAttributeCollection attributes = obj.Attributes as ActivityAttributeCollection;
    if (virtualAdded && attributes != null)
      virtualAdded = attributes.VirtualAdded;
    if (virtualAdded)
      return;
    vars.VirtualAdded = true;
    if (attributes != null)
      attributes.VirtualAdded = true;
    vars.EditableVarIDs.Clear();
    for (int index = 0; index < vars.Count; ++index)
    {
      Variable var = vars[index];
      if (var.AttrTypeID != 0)
      {
        IDBAttribute byId = obj.Attributes.FindByID(var.AttrTypeID);
        try
        {
          if (var.VarType == VarType.DateTime && string.IsNullOrEmpty(var.Value))
          {
            DateTime now = DateTime.Now;
            if (byId != null)
              byId.Value = (object) now;
            else
              obj.Attributes.AddTemporaryAttribute(var.AttrTypeID, false, new object[1]
              {
                (object) now
              });
          }
          else if (byId != null)
            byId.Value = var.TypedValue;
          else
            obj.Attributes.AddTemporaryAttribute(var.AttrTypeID, false, new object[1]
            {
              var.TypedValue
            });
        }
        catch
        {
        }
        if (!var.Calculated)
          vars.EditableVarIDs.Add(var.AttrTypeID);
      }
    }
  }
}

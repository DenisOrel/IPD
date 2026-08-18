// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBMessage
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBMessage(UserSession uSession, DataTable objectsTable) : DBMailObject(uSession, objectsTable)
{
  public override AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    AttributeValues[] attributesValues = base.GetAttributesValues(modes);
    bool flag = false;
    foreach (AttributeValues attributeValues in attributesValues)
    {
      attributeValues.ReadOnly = true;
      if (!flag && wfConsts.ProtectedAttributeTypes.Contains(attributeValues.AttributeID))
        flag = true;
    }
    if (!flag)
      return attributesValues;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (!wfConsts.ProtectedAttributeTypes.Contains(attributeValues.AttributeID))
        attributeValuesList.Add(attributeValues);
    }
    return attributeValuesList.ToArray();
  }
}

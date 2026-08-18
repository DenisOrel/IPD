// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBScriptRelation
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBScriptRelation(UserSession uSession, DataTable relationsTable) : DBRelation(uSession, relationsTable)
{
  public override AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    AttributeValues[] attributesValues = base.GetAttributesValues(GetAttributeValuesModes.CheckWriteAccess | modes);
    for (int index = 0; index < attributesValues.Length; ++index)
    {
      AttributeValues attributeValues = attributesValues[index];
      if (!this.UserSession.IsAdmin)
        attributeValues.ReadOnly = true;
    }
    return attributesValues;
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes)
  {
    return this.CheckAccess(ActionType.EditProperties) ? base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes) : (AttributeValues[]) null;
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    return this.CheckAccess(ActionType.EditProperties) ? base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList) : (AttributeValues[]) null;
  }

  public override bool GetDefaultAccess(ActionType at) => this.UserSession.IsAdmin;

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.EditProperties, this.GetDefaultAccess(ActionType.EditProperties));
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttachmentsDictNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Создать экземпляр узла</summary>
/// <param name="objectIDs">Типизированные коллекции версий объектов</param>
/// <param name="expandNode">Признак раскрытия состава дочерних элементов</param>
internal sealed class AttachmentsDictNode(Dictionary<int, List<long>> objectIDs, bool expandNode) : 
  ObjectsDictNode(objectIDs, expandNode)
{
  protected override INodePart GetPart(
    IConditionsProvider conditionProvider,
    IList objectIDs,
    int objectTypeID)
  {
    return (INodePart) new AttachmentsListPart(objectIDs, conditionProvider, this.Services, objectTypeID, this._expandNode);
  }
}

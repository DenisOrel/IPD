// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttachmentsDescriptor
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

internal sealed class AttachmentsDescriptor : DictDescriptor
{
  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public AttachmentsDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public AttachmentsDescriptor(Dictionary<int, List<long>> objectIDs)
    : base(Holder.CategoryAttachmentsID, 0, string.Empty, objectIDs)
  {
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new AttachmentsDictNode(this._objectIDs, this._expandNodes);
  }
}

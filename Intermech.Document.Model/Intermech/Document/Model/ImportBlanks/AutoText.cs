// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.AutoText
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Программно заполняемый текст </summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class AutoText(GroupPrimitive owner) : TextPrimitive(owner)
{
  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    TextBoxElement newDocumentNode = new TextBoxElement();
    this.SetNodeId((DocumentTreeNode) newDocumentNode);
    parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    newDocumentNode.AssignReadOnly(true);
    return (DocumentTreeNode) newDocumentNode;
  }
}

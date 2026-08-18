// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ReferenceToNodeAttribute
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Ссылка на атрибут узла</summary>
[Editor(typeof (ReferenceToNodeAttributeEditor), typeof (UITypeEditor))]
[TypeConverter(typeof (ReferenceToNodeAttributeConverter))]
[Serializable]
public class ReferenceToNodeAttribute : ReferenceToNodeAttributeBase
{
  /// <summary>Создать пустой экземпляр класса с инициализацией только самых необходимых полей.
  /// Используется в словаре конструкторов.</summary>
  public static object EmptyConstructor() => (object) new ReferenceToNodeAttribute();

  public override string GetAttributeStringValue(List<DocumentTreeNode> callChain)
  {
    return this.ReferenceBaseType == BaseReferenceNodeType.ntParentDocument && this.OwnerDocument is ImDocument && (this.OwnerDocument as ImDocument).DocumentControl != null && (this.OwnerDocument as ImDocument).DocumentControl.ReadOnly && !(this.OwnerDocument as ImDocument).DocumentControl.DocumentViewMode.HasFlag((Enum) DocumentViewMode.ShowDocumentReferences) ? "" : base.GetAttributeStringValue(callChain);
  }

  /// <summary>Конструктор</summary>
  public ReferenceToNodeAttribute()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Узел владелец</param>
  public ReferenceToNodeAttribute(DocumentTreeNode ownerNode)
    : base(ownerNode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerNode">Владелец ссылки</param>
  /// <param name="referenceBaseType">Тип базового узла ссылки</param>
  /// <param name="nodeId">Идентификатор узла на который ссылается</param>
  /// <param name="attributeName">Имя атрибута на который ссылается</param>
  public ReferenceToNodeAttribute(
    DocumentTreeNode ownerNode,
    BaseReferenceNodeType referenceBaseType,
    string nodeId,
    string attributeName)
    : base(ownerNode, referenceBaseType, nodeId, attributeName)
  {
  }

  /// <summary>Можно ли вызвать диалог выбора объекта для ссылки</summary>
  public override bool CanCallSelectObjectDialog
  {
    get => this.referenceBaseType == BaseReferenceNodeType.ntSelectedNode;
  }

  /// <summary>Вызвать диалог выбора объекта для ссылки</summary>
  public override void CallSelectObjectDialog()
  {
    DocumentTreeNode nodeLink1 = this.NodeLink;
    DocumentTreeNode rootNode = (DocumentTreeNode) null;
    if (this.OwnerNode != null)
      rootNode = (DocumentTreeNode) this.OwnerNode.GetDocTreeRoot() ?? this.OwnerNode;
    DocumentTreeNode nodeLink2 = SelectNodeDlg.Execute(typeof (DocumentTreeNode), nodeLink1, rootNode, LocalizationHolder.rm.GetString("Document.Model_505"), 1);
    if (nodeLink2 == null)
      return;
    this.SetReference(nodeLink2);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.OlePrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>OLE примитив</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class OlePrimitive(GroupPrimitive owner) : RectPrimitive(owner)
{
  /// <summary>Пользователь может изменять размер изображения</summary>
  public bool userResize;

  /// <summary>Пользователь может изменять размер изображения</summary>
  public bool UserResize
  {
    [DebuggerStepThrough] get => this.userResize;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.userResize = reader.ReadBoolean();
    if (loader.LoadingVersion >= 170 && !loader.CurrentPrimitiveIsLoaded)
      this.needFrame = reader.ReadBoolean();
    else
      this.needFrame = false;
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    ContainerElement newDocumentNode = new ContainerElement();
    this.SetNodeId((DocumentTreeNode) newDocumentNode);
    parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    return (DocumentTreeNode) newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    node.SetAttributeValue("BLN.TYPE", this.GetType().Name, false, false, false);
    ((PageElementNode) node).GeometryChangingBlocked = this.userResize;
  }
}

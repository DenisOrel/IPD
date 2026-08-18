// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.GroupClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон группы</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class GroupClone(GroupClone owner, RectPrimitive origin) : CloneBase(owner, origin)
{
  /// <summary>Список дочерних клонов</summary>
  public List<CloneBase> ChildList = new List<CloneBase>();

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    base.Load(ueDoc);
    int num = reader.ReadInt32();
    for (int index = 0; index < num; ++index)
    {
      CloneBase cloneBase = ueDoc.LoadClone(this);
      if (cloneBase != null)
        this.ChildList.Add(cloneBase);
    }
    if (this.origin == null)
      return;
    GroupPrimitive origin = this.origin as GroupPrimitive;
    for (int index1 = 0; index1 < origin.ChildList.Count; ++index1)
    {
      if (origin.ChildList[index1] is RectPrimitive)
      {
        int index2 = 0;
        while (index2 < this.ChildList.Count && !(origin.ChildList[index1].Id == this.ChildList[index2].Id))
          ++index2;
        if (index2 >= this.ChildList.Count)
          ueDoc.CreateChild(this, origin.ChildList[index1] as RectPrimitive);
      }
    }
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (!(this is AreaClone))
      node.ApplyTemplateTreeStructure(true, false, false, false);
    for (int index = 0; index < this.ChildList.Count; ++index)
      this.ChildList[index].CreateNewDocumentNode(node);
  }
}

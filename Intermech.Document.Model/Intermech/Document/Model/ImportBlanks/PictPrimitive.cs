// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PictPrimitive
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Изображение</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class PictPrimitive(GroupPrimitive owner) : RectPrimitive(owner)
{
  /// <summary>Поток с изображением</summary>
  public MemoryStream pictStream = new MemoryStream();
  /// <summary>Пользователь может изменять размер изображения</summary>
  public bool userResize;
  /// <summary>?</summary>
  public bool isConstant;
  /// <summary>Имя файла с картинкой</summary>
  public string fileName;

  /// <summary>Поток с изображением</summary>
  public MemoryStream PictStream
  {
    [DebuggerStepThrough] get => this.pictStream;
  }

  /// <summary>Пользователь может изменять размер изображения</summary>
  public bool UserResize
  {
    [DebuggerStepThrough] get => this.userResize;
  }

  /// <summary>?</summary>
  public bool IsConstant
  {
    [DebuggerStepThrough] get => this.isConstant;
  }

  /// <summary>Имя файла с картинкой</summary>
  public string FileName
  {
    [DebuggerStepThrough] get => this.fileName;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    this.fileName = loader.ReadString();
    this.userResize = reader.ReadBoolean();
    this.isConstant = reader.ReadBoolean();
    if (loader.LoadingVersion >= 270 && !loader.CurrentPrimitiveIsLoaded)
      this.needFrame = reader.ReadBoolean();
    else
      this.needFrame = false;
    if (loader.LoadingVersion < 272 || loader.CurrentPrimitiveIsLoaded)
      return;
    int num = reader.ReadInt32();
    byte[] buffer = new byte[4096 /*0x1000*/];
    int count1;
    for (; num > 0; num -= count1)
    {
      int count2 = num <= 4096 /*0x1000*/ ? num : 4096 /*0x1000*/;
      count1 = reader.Read(buffer, 0, count2);
      if (count1 != 0)
        this.PictStream.Write(buffer, 0, count1);
      else
        break;
    }
    this.PictStream.Position = 0L;
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
    ContainerElement containerElement = (ContainerElement) node;
    if (this.pictStream != null && this.pictStream.Length != 0L)
    {
      this.pictStream.Position = 0L;
      if (!string.IsNullOrEmpty(this.FileName))
        containerElement.AssignFileDataStream((Stream) this.pictStream, this.FileName, ArcMethods.NotPacked, DataSourceType.Unknown, false, false, true);
      else
        containerElement.AssignDataStream((Stream) this.pictStream, DataSourceType.Unknown, false, false, true);
    }
    else if (!string.IsNullOrEmpty(this.FileName))
      containerElement.SetAttributeValue("BLN.PictFileName", this.FileName, false, false, false);
    containerElement.GeometryChangingBlocked = this.UserResize;
    if (!this.isConstant)
      return;
    containerElement.SetAttributeValue("BLN.IsConstant", this.isConstant.ToString((IFormatProvider) CultureInfo.InvariantCulture), false, false, false);
  }
}

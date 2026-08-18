// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.OLEClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон OLE контейнера</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class OLEClone(GroupClone owner, RectPrimitive origin) : CloneBase(owner, origin)
{
  /// <summary>Поток содержащий OLE объект</summary>
  public MemoryStream objectStream = new MemoryStream();
  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] metafileBuffer;

  /// <summary>Поток содержащий OLE объект</summary>
  public MemoryStream ObjectStream => this.objectStream;

  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] MetafileBuffer => this.metafileBuffer;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    base.Load(ueDoc);
    int num = reader.ReadInt32();
    byte[] numArray1 = new byte[4096 /*0x1000*/];
    bool flag = true;
    while (num > 0)
    {
      int count = num <= 4096 /*0x1000*/ ? num : 4096 /*0x1000*/;
      reader.Read(numArray1, 0, count);
      byte[] numArray2 = numArray1;
      if (flag)
      {
        numArray2 = new byte[4084];
        Array.Copy((Array) numArray1, 12, (Array) numArray2, 0, numArray1.Length - 12);
      }
      this.objectStream.Write(numArray2, 0, numArray2.Length);
      num -= count;
      flag = false;
    }
    if (ueDoc.LoadingVersion < 314)
      return;
    int count1 = reader.ReadInt32();
    if (count1 == 0)
      return;
    this.metafileBuffer = new byte[count1 + 1];
    reader.Read(this.metafileBuffer, 0, count1);
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    ContainerElement containerElement = (ContainerElement) node;
    containerElement.AssignDataStream((Stream) this.ObjectStream, DataSourceType.OLE, true, false, false, true);
    if (this.metafileBuffer == null || this.metafileBuffer.Length == 0)
      return;
    Image image = Image.FromStream((Stream) new MemoryStream(this.metafileBuffer));
    containerElement.AssignImage(image, SizeF.Empty, false, false, true);
  }
}

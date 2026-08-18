// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PictClone
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

/// <summary>Клон рисунка</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class PictClone(GroupClone owner, RectPrimitive origin) : CloneBase(owner, origin)
{
  /// <summary>Поток с рисунком</summary>
  public MemoryStream objectStream = new MemoryStream();
  /// <summary>?</summary>
  public MemoryStream rlfStream = new MemoryStream();
  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] metafileBuffer;
  /// <summary>Имя файла с рисунком</summary>
  public string fileName;
  /// <summary>Отображаемый слой</summary>
  public string eskizLayer;
  /// <summary>Рисунок хранится в документе</summary>
  public bool builtInPicture;
  /// <summary>Рисовать все без слоев</summary>
  public bool drawAllWithoutLayers;
  /// <summary>слои</summary>
  public List<string> layers = new List<string>();

  /// <summary>Поток с рисунком</summary>
  public MemoryStream ObjectStream => this.objectStream;

  /// <summary>?</summary>
  public MemoryStream RLFStream => this.rlfStream;

  /// <summary>Метафайл - буфер изображения</summary>
  public byte[] MetafileBuffer => this.metafileBuffer;

  /// <summary>Имя файла с рисунком</summary>
  public string FileName => this.fileName;

  /// <summary>Отображаемый слой</summary>
  public string EskizLayer => this.eskizLayer;

  /// <summary>Рисунок хранится в документе</summary>
  public bool BuiltInPicture => this.builtInPicture;

  /// <summary>Рисовать все без слоев</summary>
  public bool DrawAllWithoutLayers => this.drawAllWithoutLayers;

  /// <summary>слои</summary>
  public List<string> Layers => this.layers;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    base.Load(ueDoc);
    int num1 = reader.ReadInt32();
    bool flag = num1 < 0;
    int num2 = Math.Abs(num1);
    byte[] buffer = new byte[4096 /*0x1000*/];
    int count1;
    for (; num2 > 0; num2 -= count1)
    {
      count1 = num2 <= 4096 /*0x1000*/ ? num2 : 4096 /*0x1000*/;
      reader.Read(buffer, 0, count1);
      this.objectStream.Write(buffer, 0, count1);
    }
    int count2;
    if (flag)
    {
      for (int index = reader.ReadInt32(); index > 0; index -= count2)
      {
        count2 = index <= 4096 /*0x1000*/ ? index : 4096 /*0x1000*/;
        reader.Read(buffer, 0, count2);
        this.rlfStream.Write(buffer, 0, count2);
      }
    }
    this.fileName = PrimitiveLoader.ReadString(reader);
    this.builtInPicture = reader.ReadBoolean();
    this.drawAllWithoutLayers = reader.ReadBoolean();
    int num3 = reader.ReadInt32();
    for (int index = 0; index < num3; ++index)
      this.layers.Add(PrimitiveLoader.ReadString(reader));
    this.eskizLayer = PrimitiveLoader.ReadString(reader);
    if (ueDoc.CurrentCloneIsLoaded || ueDoc.LoadingVersion < 316)
      return;
    int count3 = reader.ReadInt32();
    if (count3 == 0)
      return;
    this.metafileBuffer = new byte[count3 + 1];
    reader.Read(this.metafileBuffer, 0, count3);
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    ContainerElement containerElement = (ContainerElement) node;
    MemoryStream objectStream = this.objectStream;
    try
    {
      if (this.objectStream != null && this.objectStream.Length != 0L)
      {
        if (!string.IsNullOrEmpty(this.FileName))
          containerElement.AssignFileDataStream((Stream) this.objectStream, this.FileName, ArcMethods.NotPacked, DataSourceType.Unknown, false, false, true);
        else
          containerElement.AssignDataStream((Stream) this.objectStream, DataSourceType.Unknown, false, false, true);
      }
      else if (!string.IsNullOrEmpty(this.FileName))
      {
        FileInfo fileInfo = new FileInfo(this.FileName);
        if (fileInfo.Exists)
        {
          using (FileStream sourceStream = fileInfo.OpenRead())
          {
            ContainerData.LoadToMemoryStream((Stream) sourceStream, (int) sourceStream.Length);
            if (sourceStream.Length != 0L)
              containerElement.AssignFileDataStream((Stream) this.objectStream, this.FileName, ArcMethods.NotPacked, DataSourceType.Unknown, false, false, true);
          }
        }
        else
          containerElement.SetAttributeValue("ImageFileName", this.FileName);
      }
    }
    catch (Exception ex)
    {
      LogManager.AddLine("Не удалось загрузить изображение для элемента: " + this.Id);
      LogManager.AddLine(ex);
    }
    if (this.DrawAllWithoutLayers)
      return;
    containerElement.AssignLayers(this.layers, false, false, true);
  }
}

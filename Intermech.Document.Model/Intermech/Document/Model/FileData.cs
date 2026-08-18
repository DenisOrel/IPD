// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FileData
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Класс данных о файле редактирования</summary>
internal class FileData
{
  private DateTime editStartTime;
  private DateTime lastSaveTime;
  private DocumentTreeNode node;
  private string fileName;

  /// <summary>Время когда запустили</summary>
  public DateTime EditStartTime
  {
    get => this.editStartTime;
    set => this.editStartTime = value;
  }

  /// <summary>Время последнего сохранения</summary>
  public DateTime LastSaveTime
  {
    get => this.lastSaveTime;
    set => this.lastSaveTime = value;
  }

  /// <summary>Нод которому принадлежит файл</summary>
  public DocumentTreeNode Node
  {
    get => this.node;
    set => this.node = value;
  }

  /// <summary>Имя файла</summary>
  public string FileName
  {
    get => this.fileName;
    set => this.fileName = value;
  }
}

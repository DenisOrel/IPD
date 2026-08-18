// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.IFileDocument
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Файловый документ</summary>
public interface IFileDocument
{
  /// <summary>Путь и имя файла</summary>
  string FilePath { get; }
}

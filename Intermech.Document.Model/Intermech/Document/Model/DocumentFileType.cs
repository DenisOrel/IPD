// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.DocumentFileType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Тип файла документа</summary>
public enum DocumentFileType
{
  /// <summary>Неизвестный тип файла документа</summary>
  Unknown,
  /// <summary>Бланк старого формата для программы Blanks2</summary>
  OldBlank,
  /// <summary>Документ AVS старого формата</summary>
  OldAVS,
  /// <summary>Документ старого формата для программы UEdit</summary>
  OldUEditDocument,
  /// <summary>Библиотека примитивов старого формата</summary>
  OldPrimitiveLib,
  /// <summary>Документ Интермех</summary>
  ImDocument,
  /// <summary>Документ Интермех сохранённый со сжатием</summary>
  ImDocument_IsPacked,
  /// <summary>Комплект документов Интермех</summary>
  ImDocumentsComplect,
  /// <summary>Комплект документов Интермех сохранённый со сжатием</summary>
  ImDocumentsComplect_IsPacked,
}

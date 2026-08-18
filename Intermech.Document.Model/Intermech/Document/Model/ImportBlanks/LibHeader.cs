// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.LibHeader
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Заголовок файла библиотеки</summary>
/// <summary>Конструктор</summary>
/// <param name="signatureLenght">Длина сигнатуры</param>
internal struct LibHeader(int signatureLenght)
{
  /// <summary>Массив символов сигнатуры</summary>
  public char[] Signature = new char[signatureLenght];
  /// <summary>длина заголовка</summary>
  public ushort HeaderLen = 0;
  /// <summary>Версия формата файла</summary>
  public ushort VersionNum = 0;

  /// <summary>Сигнатура</summary>
  public string SignatureStr
  {
    [DebuggerStepThrough] get => new string(this.Signature);
  }
}

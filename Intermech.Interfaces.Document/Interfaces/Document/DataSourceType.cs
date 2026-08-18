// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DataSourceType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum DataSourceType
{
  /// <summary>Тип неизвестен</summary>
  Unknown,
  /// <summary>Картинка типа поддерживаемая типом Image</summary>
  Image,
  /// <summary>Картинка типа поддерживаемая ShowObject</summary>
  ShowNET,
  /// <summary>OLE объект</summary>
  OLE,
  /// <summary>
  /// OLE объект в котором хранится файл, т.к. из потока создать объект нельзя
  /// </summary>
  OLE_File,
  /// <summary>OLE объект из буфера обмена</summary>
  OLE_Clipboard,
}

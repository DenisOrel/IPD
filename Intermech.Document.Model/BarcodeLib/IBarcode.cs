// Decompiled with JetBrains decompiler
// Type: BarcodeLib.IBarcode
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections.Generic;

#nullable disable
namespace BarcodeLib;

/// <summary>
///  Barcode interface for symbology layout.
///  Written by: Brad Barnhill
/// </summary>
internal interface IBarcode
{
  string Encoded_Value { get; }

  string RawData { get; }

  List<string> Errors { get; }
}

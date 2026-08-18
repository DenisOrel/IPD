// Decompiled with JetBrains decompiler
// Type: BarcodeLib.Symbologies.Blank
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace BarcodeLib.Symbologies;

/// <summary>
///  Blank encoding template
///  Written by: Brad Barnhill
/// </summary>
internal class Blank : BarcodeCommon, IBarcode
{
  public string Encoded_Value => throw new NotImplementedException();
}

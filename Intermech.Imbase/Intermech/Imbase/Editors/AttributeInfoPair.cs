// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.AttributeInfoPair
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.Editors;

internal class AttributeInfoPair
{
  internal AttributeInfo _source;
  internal AttributeInfo _dest;

  public AttributeInfoPair(AttributeInfo source, AttributeInfo dest)
  {
    this._dest = dest;
    this._source = source;
  }
}

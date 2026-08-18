
// Type: Intermech.PropertyEditors.StringMaskedPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.PropertyEditors;

[Serializable]
public class StringMaskedPropertyClass : StringPropertyClass
{
  public StringMaskedPropertyClass(string aMask)
  {
    this.masked = true;
    this.mask = aMask;
  }

  public StringMaskedPropertyClass(string aString, string aMask)
    : base(aString)
  {
    this.masked = true;
    this.mask = aMask;
  }

  public override string ToString() => base.ToString();
}

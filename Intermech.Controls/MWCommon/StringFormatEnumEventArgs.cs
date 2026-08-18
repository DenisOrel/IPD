
// Type: MWCommon.StringFormatEnumEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace MWCommon;

/// <summary>ShadowDirectionEventArgs class.</summary>
public class StringFormatEnumEventArgs : EventArgs
{
  private StringFormatEnum sfeOldStringFormatEnum;
  private StringFormatEnum sfeNewStringFormatEnum;

  /// <summary>Standard Constructor.</summary>
  /// <param name="sfeOld">The old StringFormatEnum before the property was changed.</param>
  /// <param name="sfeNew">The new StringFormatEnum after the property was changed.</param>
  public StringFormatEnumEventArgs(StringFormatEnum sfeOld, StringFormatEnum sfeNew)
  {
    this.sfeOldStringFormatEnum = sfeOld;
    this.sfeNewStringFormatEnum = sfeNew;
  }

  /// <summary>
  /// The old StringFormatEnum before the property was changed.
  /// </summary>
  public StringFormatEnum OldStringFormatEnum => this.sfeOldStringFormatEnum;

  /// <summary>
  /// The new StringFormatEnum after the property was changed.
  /// </summary>
  public StringFormatEnum NewStringFormatEnum => this.sfeNewStringFormatEnum;
}

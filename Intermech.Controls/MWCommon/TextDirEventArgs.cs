
// Type: MWCommon.TextDirEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace MWCommon;

/// <summary>ShadowDirectionEventArgs class.</summary>
public class TextDirEventArgs : EventArgs
{
  private TextDir tdOldTextDir;
  private TextDir tdNewTextDir;

  /// <summary>Standard Constructor.</summary>
  /// <param name="tdOld">The old TextDir before the property was changed.</param>
  /// <param name="tdNew">The new TextDir after the property was changed.</param>
  public TextDirEventArgs(TextDir tdOld, TextDir tdNew)
  {
    this.tdOldTextDir = tdOld;
    this.tdNewTextDir = tdNew;
  }

  /// <summary>The old TextDir before the property was changed.</summary>
  public TextDir OldTextDir => this.tdOldTextDir;

  /// <summary>The new TextDir after the property was changed.</summary>
  public TextDir NewTextDir => this.tdNewTextDir;
}

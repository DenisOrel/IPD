
// Type: Intermech.PropertyEditors.LCLevelPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (LCLevelEditor), typeof (UITypeEditor))]
public class LCLevelPropertyClass
{
  private string _caption;

  public int LCLevel { get; }

  public LCLevelPropertyClass(int level)
    : this(level, (string) null)
  {
  }

  public LCLevelPropertyClass(int level, string caption)
  {
    this.LCLevel = level;
    this._caption = caption;
  }

  public override string ToString()
  {
    if (this._caption != null)
      return this._caption;
    if (this.LCLevel == 0)
      return LocalizationHolder.rm.GetString("Client.Core_976");
    this._caption = MetaDataHelper.GetLCLevelName(this.LCLevel);
    return this._caption;
  }
}

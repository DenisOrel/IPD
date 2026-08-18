
// Type: Intermech.Search.UI.VirtualTree._ObjectRowBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.UI.VirtualTree;

public sealed class _ObjectRowBinding : RowBindingBase
{
  public _ObjectRowBinding()
  {
    this.Type = typeof (_Object);
    this.Extensions.Add((IRowBindingExtension) new NodeColumnTransformRowBindingExtension());
    this.Extensions.Add((IRowBindingExtension) new ColorSchemesRowBindingExtension());
    this.Extensions.Add((IRowBindingExtension) new CategoryTypeIconRowBindingExtension());
    this.Extensions.Add((IRowBindingExtension) new StatusesRowBindingExtension());
  }
}

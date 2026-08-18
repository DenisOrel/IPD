
// Type: Intermech.Search.UI.VirtualTree.RowBindingExtensionCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.ComponentModel;
using System;


namespace Intermech.Search.UI.VirtualTree;

public class RowBindingExtensionCollection : BindingListBase<IRowBindingExtension>
{
  public RowBindingExtensionCollection(RowBindingBase rowBinding)
  {
    this.RowBinding = rowBinding != null ? rowBinding : throw new ArgumentNullException(nameof (rowBinding));
  }

  public RowBindingBase RowBinding { get; private set; }
}

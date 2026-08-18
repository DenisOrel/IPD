
// Type: Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ItemsEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView;

/// <summary>
/// Provides data for the ButtonedListView.AddItem event of the ButtonedListView control.
/// </summary>
public class ItemsEventArgs : EventArgs
{
  public ItemsEventArgs(ListView.ListViewItemCollection items) => this.Items = items;

  public ListView.ListViewItemCollection Items { get; private set; }
}

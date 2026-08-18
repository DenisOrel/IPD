
// Type: Intermech.Navigator.Controls.IHistoryProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

internal interface IHistoryProvider
{
  HistoryItem CurrentItem { get; }

  void ApplyItem(HistoryItem item);
}

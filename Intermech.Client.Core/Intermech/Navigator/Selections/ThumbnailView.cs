
// Type: Intermech.Navigator.Selections.ThumbnailView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Selections;

internal class ThumbnailView : Intermech.Client.Core.Thumbnail.ThumbnailView
{
  protected override ContentType ContentType => ContentType.Folders;

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_439");
}

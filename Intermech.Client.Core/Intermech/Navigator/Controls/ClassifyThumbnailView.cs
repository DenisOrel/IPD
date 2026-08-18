
// Type: Intermech.Navigator.Controls.ClassifyThumbnailView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

internal class ClassifyThumbnailView : ThumbnailView
{
  protected override ContentType ContentType => ContentType.Folders;
}

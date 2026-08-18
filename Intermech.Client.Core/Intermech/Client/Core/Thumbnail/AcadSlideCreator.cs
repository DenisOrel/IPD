
// Type: Intermech.Client.Core.Thumbnail.AcadSlideCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.IO;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Summary description for AcadSlideCreator.</summary>
public class AcadSlideCreator : IThumbImageCreator
{
  public object CreateFromStream(Stream stream, string ext)
  {
    ext.ToLower();
    return ext == "sld" ? (object) new AcadSlide(stream) : (object) null;
  }
}

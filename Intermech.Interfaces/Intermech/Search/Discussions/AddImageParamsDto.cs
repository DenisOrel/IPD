
// Type: Intermech.Search.Discussions.AddImageParamsDto
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.Discussions
{
    [Serializable]
    public sealed class AddImageParamsDto
    {
      public static bool Check(AddImageParamsDto addImageParams)
      {
        if (addImageParams == null)
          throw new ArgumentNullException(nameof (addImageParams));
        return (!ObjectHelper.IsUnknownObjectVersionID(addImageParams.ObjectVersionId) || !ObjectHelper.IsUnknownObjectVersionID(addImageParams.DiscussionVersionId)) && addImageParams.Blob != null && addImageParams.Blob.Length != 0 && !string.IsNullOrEmpty(addImageParams.FileName);
      }

      public long ObjectVersionId { get; set; }

      public long DiscussionVersionId { get; set; }

      public byte[] Blob { get; set; }

      public string FileName { get; set; }
    }
}

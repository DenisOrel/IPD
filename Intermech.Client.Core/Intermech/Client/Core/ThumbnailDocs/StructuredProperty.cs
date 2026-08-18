
// Type: Intermech.Client.Core.ThumbnailDocs.StructuredProperty
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.ThumbnailDocs;

public sealed class StructuredProperty
{
  public StructuredProperty(Guid formatId, string name, int id)
  {
    this.FormatId = formatId;
    this.Name = name;
    this.Id = id;
  }

  public Guid FormatId { get; private set; }

  public string Name { get; private set; }

  public int Id { get; private set; }

  public object Value { get; set; }

  public override string ToString() => this.Name;
}

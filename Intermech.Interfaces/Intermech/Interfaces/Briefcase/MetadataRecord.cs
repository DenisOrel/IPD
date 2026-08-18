
// Type: Intermech.Interfaces.Briefcase.MetadataRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Briefcase
{
    public struct MetadataRecord(int category, object id, object externalId)
    {
      public int Category = category;
      public object Id = id;
      public object ExternalId = externalId;
    }
}

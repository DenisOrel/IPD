
// Type: Intermech.Search.Data.Repositories.FindObjectCountOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.Data.Repositories
{
    public sealed class FindObjectCountOptions
    {
      public FindObjectCountOptions() => this.ObjectTypeID = -1;

      public int ObjectTypeID { get; set; }

      public string SearchText { get; set; }
    }
}

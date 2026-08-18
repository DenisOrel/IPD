
// Type: Intermech.Data.SectionEntities.SectionEntity
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb;


namespace Intermech.Data.SectionEntities
{
    public class SectionEntity : IEntity
    {
      private readonly SectionCollection sections;
      private EntityDatabase db;
      private long uniqueId;

      public SectionEntity() => this.sections = new SectionCollection();

      public SectionCollection Sections => this.sections;

      public EntityDatabase Database
      {
        get => this.db;
        set => this.db = value;
      }

      public long UniqueId
      {
        get => this.uniqueId;
        set => this.uniqueId = value;
      }
    }
}


// Type: Intermech.Interfaces.WebPortal.ObjectTag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    ///  Дополнительные данные, передаваемые с публикуемым объектом
    /// </summary>
    [Serializable]
    public class ObjectTag : TransferedObjectTag
    {
      /// <summary>
      /// Флаг того, что объект публикуется/импортируется в составе
      /// </summary>
      public bool InComposition;
      /// <summary>Флаг того, что объект публикуется вместе с составом</summary>
      public bool WithComposition;
      /// <summary>Код узла, создавшего этот объект</summary>
      public char CreatorCode;
      /// <summary>Код узла, владеющего этим объектом</summary>
      public char? OwnerCode;
      /// <summary>Код узла, владеющего составом этого объекта</summary>
      public char? CompositionOwnerCode;
      /// <summary>Корневой тип публикуемого объекта</summary>
      public PublishObjectRootType RootType;
      /// <summary>Разрешенные узлы</summary>
      public string EnableSites;

      public ObjectTag()
      {
      }

      public ObjectTag(
        bool inComposition,
        bool withComposition,
        char creatorCode,
        PublishObjectRootType rootType)
      {
        this.InComposition = inComposition;
        this.CreatorCode = creatorCode;
        this.WithComposition = withComposition;
        this.RootType = rootType;
        this.EnableSites = string.Empty;
      }

      public override void Save(BinaryWriter bw)
      {
        bw.Write(this.InComposition);
        bw.Write(this.WithComposition);
        bw.Write(this.CreatorCode);
        if (this.OwnerCode.HasValue)
        {
          bw.Write(true);
          bw.Write(this.OwnerCode.Value);
        }
        else
          bw.Write(false);
        if (this.CompositionOwnerCode.HasValue)
        {
          bw.Write(true);
          bw.Write(this.CompositionOwnerCode.Value);
        }
        else
          bw.Write(false);
        bw.Write((int) this.RootType);
        this.SaveString(bw, this.EnableSites);
      }

      public override void Load(BinaryReader br)
      {
        this.InComposition = br.ReadBoolean();
        this.WithComposition = br.ReadBoolean();
        this.CreatorCode = br.ReadChar();
        if (br.ReadBoolean())
          this.OwnerCode = new char?(br.ReadChar());
        if (br.ReadBoolean())
          this.CompositionOwnerCode = new char?(br.ReadChar());
        this.RootType = (PublishObjectRootType) br.ReadInt32();
        this.EnableSites = this.LoadString(br);
      }

      public override TransferedObjectTag Clone()
      {
        return (TransferedObjectTag) new ObjectTag(this.InComposition, this.WithComposition, this.CreatorCode, this.RootType)
        {
          CompositionOwnerCode = this.CompositionOwnerCode,
          EnableSites = this.EnableSites,
          OwnerCode = this.OwnerCode
        };
      }
    }
}

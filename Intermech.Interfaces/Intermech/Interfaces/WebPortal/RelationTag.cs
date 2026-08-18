
// Type: Intermech.Interfaces.WebPortal.RelationTag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    ///  Дополнительные данные, передаваемые с публикуемой связью
    /// </summary>
    [Serializable]
    public class RelationTag : TransferedObjectTag
    {
      public string ProjectTransferedObjectGuid;
      public string PartTransferedObjectGuid;

      public RelationTag()
      {
      }

      public RelationTag(string projectTransferedObjectGuid, string partTransferedObjectGuid)
      {
        this.ProjectTransferedObjectGuid = projectTransferedObjectGuid;
        this.PartTransferedObjectGuid = partTransferedObjectGuid;
      }

      public override void Save(BinaryWriter bw)
      {
        this.SaveString(bw, this.ProjectTransferedObjectGuid);
        this.SaveString(bw, this.PartTransferedObjectGuid);
      }

      public override void Load(BinaryReader br)
      {
        this.ProjectTransferedObjectGuid = this.LoadString(br);
        this.PartTransferedObjectGuid = this.LoadString(br);
      }

      public override TransferedObjectTag Clone()
      {
        return (TransferedObjectTag) new RelationTag(this.ProjectTransferedObjectGuid, this.PartTransferedObjectGuid);
      }
    }
}

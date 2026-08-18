
// Type: Intermech.Interfaces.WebPortal.IncompleteRelationTag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class IncompleteRelationTag : TransferedObjectTag
    {
      public string Guid;
      public string ProjectGuid;
      public string PartGuid;

      public IncompleteRelationTag()
      {
      }

      public IncompleteRelationTag(string guid, string projectGuid, string partGuid)
      {
        this.Guid = guid;
        this.ProjectGuid = projectGuid;
        this.PartGuid = partGuid;
      }

      public override TransferedObjectTag Clone()
      {
        return (TransferedObjectTag) new IncompleteRelationTag(this.Guid, this.ProjectGuid, this.PartGuid);
      }

      public override void Load(BinaryReader br)
      {
        this.Guid = this.LoadString(br);
        this.ProjectGuid = this.LoadString(br);
        this.PartGuid = this.LoadString(br);
      }

      public override void Save(BinaryWriter bw)
      {
        this.SaveString(bw, this.Guid);
        this.SaveString(bw, this.ProjectGuid);
        this.SaveString(bw, this.PartGuid);
      }
    }
}

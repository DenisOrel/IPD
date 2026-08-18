
// Type: Intermech.Interfaces.WebPortal.ITransferedObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces.WebPortal
{
    public interface ITransferedObject
    {
      string GUID { get; set; }

      string[] DataFiles { get; set; }

      TransferedObjectCategory Category { get; set; }

      TransferedObjectTag Tag { get; set; }

      void Load(BinaryReader reader);

      void Save(BinaryWriter writer);
    }
}

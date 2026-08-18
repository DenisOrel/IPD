
// Type: Intermech.Interfaces.WebPortal.TransferedObjectTag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Дополнительные данные, передаваемые с публикуемым объектом/связью
    /// </summary>
    [XmlInclude(typeof (ObjectTag))]
    [XmlInclude(typeof (RelationTag))]
    [XmlInclude(typeof (PacketTag))]
    [XmlInclude(typeof (IncompleteRelationTag))]
    [Serializable]
    public abstract class TransferedObjectTag
    {
      public abstract void Save(BinaryWriter bw);

      public abstract void Load(BinaryReader br);

      public abstract TransferedObjectTag Clone();

      protected void SaveString(BinaryWriter bw, string savedString)
      {
        if (!string.IsNullOrEmpty(savedString))
        {
          bw.Write(true);
          bw.Write(savedString.Length);
          bw.Write(savedString.ToCharArray());
        }
        else
          bw.Write(false);
      }

      protected string LoadString(BinaryReader br)
      {
        if (!br.ReadBoolean())
          return string.Empty;
        int count = br.ReadInt32();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(br.ReadChars(count));
        return stringBuilder.ToString();
      }
    }
}

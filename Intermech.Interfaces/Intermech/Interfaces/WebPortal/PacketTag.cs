
// Type: Intermech.Interfaces.WebPortal.PacketTag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Text;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class PacketTag : TransferedObjectTag
    {
      public long PacketID;
      public Guid PacketGuid;
      public bool ReceiptNeed;
      public string Caption;
      public string EnableSites;

      public PacketTag()
      {
      }

      public PacketTag(
        long packetID,
        Guid packetGuid,
        string caption,
        string enableSites,
        bool receiptNeed)
      {
        this.PacketID = packetID;
        this.PacketGuid = packetGuid;
        this.Caption = caption;
        this.EnableSites = enableSites;
        this.ReceiptNeed = receiptNeed;
      }

      public override void Save(BinaryWriter bw)
      {
        bw.Write(this.PacketID);
        bw.Write(this.PacketGuid.ToString().Length);
        bw.Write(this.PacketGuid.ToString().ToCharArray());
        if (!string.IsNullOrEmpty(this.Caption))
        {
          bw.Write(this.Caption.Length);
          bw.Write(this.Caption.ToCharArray());
        }
        else
          bw.Write(0);
        if (!string.IsNullOrEmpty(this.EnableSites))
        {
          bw.Write(this.EnableSites.Length);
          bw.Write(this.EnableSites.ToCharArray());
        }
        else
          bw.Write(0);
        bw.Write(this.ReceiptNeed);
      }

      public override void Load(BinaryReader br)
      {
        this.PacketID = br.ReadInt64();
        int count1 = br.ReadInt32();
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.Append(br.ReadChars(count1));
        this.PacketGuid = new Guid(stringBuilder1.ToString());
        int count2 = br.ReadInt32();
        if (count2 > 0)
        {
          StringBuilder stringBuilder2 = new StringBuilder();
          stringBuilder2.Append(br.ReadChars(count2));
          this.Caption = stringBuilder2.ToString();
        }
        int count3 = br.ReadInt32();
        if (count3 > 0)
        {
          StringBuilder stringBuilder3 = new StringBuilder();
          stringBuilder3.Append(br.ReadChars(count3));
          this.EnableSites = stringBuilder3.ToString();
        }
        this.ReceiptNeed = br.ReadBoolean();
      }

      public override TransferedObjectTag Clone()
      {
        return (TransferedObjectTag) new PacketTag(this.PacketID, this.PacketGuid, this.Caption, this.EnableSites, this.ReceiptNeed);
      }
    }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Receptures.ReceptureItemInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Imbase;

#nullable disable
namespace Intermech.Interfaces.Imbase.Receptures;

/// <summary>Информация об элементе рецептуры</summary>
public class ReceptureItemInfo
{
  public ReceptureItemInfo(long linkId, long recordId)
  {
    this.LinkId = linkId;
    this.RecordId = recordId;
  }

  public long LinkId { get; }

  public long RecordId { get; }

  public string ImbaseKey => ImbaseHelper.MakeInternalImbaseKey(this.LinkId, this.RecordId);

  public override bool Equals(object obj)
  {
    return obj is ReceptureItemInfo receptureItemInfo && this.LinkId == receptureItemInfo.LinkId && this.RecordId == receptureItemInfo.RecordId;
  }

  public override int GetHashCode()
  {
    long num = this.LinkId;
    int hashCode1 = num.GetHashCode();
    num = this.RecordId;
    int hashCode2 = num.GetHashCode();
    return hashCode1 ^ hashCode2;
  }
}

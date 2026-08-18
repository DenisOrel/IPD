// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ExportHashHolder
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Briefcase;
using System.Collections;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ExportHashHolder : CategoryHolder
{
  public Hashtable this[int category] => (Hashtable) this[(object) category];

  public override void InitData()
  {
    this.Clear();
    this.Add((object) 1, (object) new Hashtable());
    this.Add((object) 2, (object) new Hashtable());
    this.Add((object) 3, (object) new Hashtable());
    this.Add((object) 4, (object) new Hashtable());
    this.Add((object) 5, (object) new Hashtable());
    this.Add((object) 6, (object) new Hashtable());
    this.Add((object) 7, (object) new Hashtable());
    this.Add((object) 8, (object) new Hashtable());
    this.Add((object) 9, (object) new Hashtable());
    this.Add((object) 10, (object) new Hashtable());
    this.Add((object) 11, (object) new Hashtable());
    this.Add((object) 12, (object) new Hashtable());
    this.Add((object) 16 /*0x10*/, (object) new Hashtable());
  }

  public override void ClearData()
  {
    foreach (DictionaryEntry dictionaryEntry in (HybridDictionary) this)
      ((Hashtable) dictionaryEntry.Value).Clear();
  }

  public void ClearMetadataInfo()
  {
    for (int index = 0; index < BriefcaseConsts.MetadataInfoCategories.Length; ++index)
      this[BriefcaseConsts.MetadataInfoCategories[index]].Clear();
  }

  public void AssignExternalId(int category, object id, object externalId)
  {
    id = BriefcaseProcs.ProcessIfDecimal(category, id);
    Hashtable hashtable = this[category];
    if (hashtable == null)
      return;
    hashtable[id] = externalId;
  }

  public void RemoveExternalId(int category, object id)
  {
    id = BriefcaseProcs.ProcessIfDecimal(category, id);
    this[category]?.Remove(id);
  }

  public object GetExternalId(int category, object id)
  {
    id = BriefcaseProcs.ProcessIfDecimal(category, id);
    return this[category]?[id];
  }

  internal bool CheckExternalID(int category, object externalId)
  {
    foreach (DictionaryEntry dictionaryEntry in this[category])
    {
      if (dictionaryEntry.Value.Equals(externalId))
        return true;
    }
    return false;
  }
}

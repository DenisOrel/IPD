// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ExportCategoryHolder
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Collections;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ExportCategoryHolder : CategoryHolder
{
  public ArrayList this[int category] => (ArrayList) this[(object) category];

  public override void InitData()
  {
    this.Clear();
    this.Add((object) 1, (object) new ArrayList());
    this.Add((object) 2, (object) new ArrayList());
    this.Add((object) 3, (object) new ArrayList());
    this.Add((object) 4, (object) new ArrayList());
    this.Add((object) 5, (object) new ArrayList());
    this.Add((object) 6, (object) new ArrayList());
    this.Add((object) 7, (object) new ArrayList());
    this.Add((object) 8, (object) new ArrayList());
    this.Add((object) 9, (object) new ArrayList());
    this.Add((object) 10, (object) new ArrayList());
    this.Add((object) 11, (object) new ArrayList());
    this.Add((object) 12, (object) new ArrayList());
    this.Add((object) 16 /*0x10*/, (object) new ArrayList());
  }

  public override void ClearData()
  {
    foreach (DictionaryEntry dictionaryEntry in (HybridDictionary) this)
      ((ArrayList) dictionaryEntry.Value).Clear();
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCDocument
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Localization;
using Intermech.Map;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCDocument : MapDocument
{
  public LCDocument()
  {
    this.Name = LocalizationHolder.rm.GetString("DatabaseConfigurator_6");
    this.MaintainsPartID = true;
    this.IsModified = false;
  }

  public LCNode FindNodeByStepId(int aStepId)
  {
    LCNode nodeByStepId = (LCNode) null;
    foreach (MapObject mapObject in (MapDocument) this)
    {
      if (mapObject is LCNode lcNode && lcNode.LCStepObject.LCStepProperties.LCStep == aStepId)
      {
        nodeByStepId = lcNode;
        break;
      }
    }
    return nodeByStepId;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpObjectExporter
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpObjectExporter : ICategoryExport
{
  public string ExporterName => "Expert.ExpObjectExporter";

  public long[] GetLinkedObjectVersions(IUserSession session, int category, object id)
  {
    return (long[]) null;
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    if (kind != AttributableElements.Object)
      return (ExportAttribute[]) null;
    IDBObject dbObject = iDBAttributable is IDBObject ? iDBAttributable as IDBObject : session.GetObject(id);
    if (dbObject == null || !(dbObject is IExpertObject))
      return (ExportAttribute[]) null;
    AttribPair[] usedAttrs = (dbObject as IExpertObject).usedAttrs;
    if (usedAttrs == null)
      return (ExportAttribute[]) null;
    ExportAttribute[] linkedDataByAttribute = new ExportAttribute[2];
    object[] aIdentifiers1 = new object[usedAttrs.Length];
    object[] aIdentifiers2 = new object[usedAttrs.Length];
    for (int index = 0; index < usedAttrs.Length; ++index)
    {
      aIdentifiers1[index] = (object) usedAttrs[index].attribID;
      aIdentifiers2[index] = (object) usedAttrs[index].objTypeID;
    }
    linkedDataByAttribute[0] = new ExportAttribute(3, aIdentifiers1);
    linkedDataByAttribute[1] = new ExportAttribute(4, aIdentifiers2);
    return linkedDataByAttribute;
  }

  public bool ProcessShortBlobs => false;
}

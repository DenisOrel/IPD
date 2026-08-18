// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Briefcase.ICategoryExport
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Briefcase;

public interface ICategoryExport
{
  string ExporterName { get; }

  long[] GetLinkedObjectVersions(IUserSession session, int category, object id);

  ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent);

  bool ProcessShortBlobs { get; }
}

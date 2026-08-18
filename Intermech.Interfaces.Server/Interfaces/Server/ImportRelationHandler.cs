// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ImportRelationHandler
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public abstract class ImportRelationHandler
{
  public abstract bool Handle(
    RelationRecord relationRecord,
    ImportedInfo project,
    ImportedInfo part);
}

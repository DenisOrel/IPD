// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBAttributeService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBAttributeService
{
  IDBAttribute CreateAttribute(
    IUserSession uSession,
    DataTable table,
    int index,
    bool temporary,
    IDBAttributable parent);

  IDBAttribute GetObjectAttribute(
    IUserSession uSession,
    long objectID,
    int attributeID,
    IDBAttributable parent);

  IDBAttribute GetRelationAttribute(
    IUserSession uSession,
    long relationID,
    int attributeID,
    IDBAttributable parent);
}

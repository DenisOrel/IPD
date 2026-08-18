// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IFiltrationTableService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IFiltrationTableService
{
  void AddValue(IDbManager db, long objectID, long filterID, string strValue);

  void DeleteValue(IDbManager db, long objectID, long filterID);

  void UpdateValue(IDbManager db, long objectID, long filterID, string strValue);

  void AddOrUpdateValue(IDbManager db, long objectID, long filterID, string strValue);

  string GetValue(IDbManager db, long objectID, long filterID);

  long[] GetFilterIDs(IDbManager db, long objectID);
}

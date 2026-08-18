// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IDBMRPProductionCopy
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

public interface IDBMRPProductionCopy : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  IDBObject CheckOutMRP(bool throwModifyModeException);
}

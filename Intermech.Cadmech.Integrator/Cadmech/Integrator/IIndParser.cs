// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.IIndParser
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal interface IIndParser
{
  void Unpack(string indValue, DataRow row);

  bool CanUnpack(string indValue);
}

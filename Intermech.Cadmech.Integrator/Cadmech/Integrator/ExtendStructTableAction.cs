// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ExtendStructTableAction
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class ExtendStructTableAction : IDecodeAction
{
  public void Run(DecodeData decodeData)
  {
    DataTable structTable = decodeData.StructTable;
    for (int index = 0; index < StructTableColumns.VirtualColumns.Length; ++index)
      structTable.Columns.Add(StructTableColumns.CreateDataColumn(StructTableColumns.VirtualColumns[index]));
  }
}

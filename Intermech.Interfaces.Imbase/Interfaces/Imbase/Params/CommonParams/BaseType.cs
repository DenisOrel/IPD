// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.CommonParams.BaseType
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params.CommonParams;

public enum BaseType
{
  [Description("Interbase")] Interbase,
  [Description("MSSQL")] MSSQL,
  [Description("Oracle")] Oracle,
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRP2Status
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.MRP;

public enum MRP2Status
{
  None = 0,
  [Description("Связь скопирована из конструкторского состава")] Copied = 1,
  [Description("Связь добавлена вручную пользователем")] Added = 2,
  [Description("Связь удалена вручную пользователем")] Deleted = 4,
}

// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.AssemblyTreeFilterMode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>
/// Режим фильтрации дерева сборочных единиц в окне привязки входимости
/// </summary>
internal enum AssemblyTreeFilterMode
{
  [Description("Без фильтрации")] NoFilter,
  [Description("Только привязанные")] OnlyChecked,
  [Description("Только не привязанные")] OnlyNoChecked,
  [Description("Отсутствующая применяемость")] OnlyIncorrectAssembly,
}

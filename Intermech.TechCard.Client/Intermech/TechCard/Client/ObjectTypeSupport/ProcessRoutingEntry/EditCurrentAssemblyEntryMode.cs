// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.EditCurrentAssemblyEntryMode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>Режим изменения привязки входимости сборки</summary>
internal enum EditCurrentAssemblyEntryMode
{
  /// <summary>Добавить текущую сборку в входимость</summary>
  Add,
  /// <summary>Исключить текущую сборку из входимости</summary>
  Remove,
}

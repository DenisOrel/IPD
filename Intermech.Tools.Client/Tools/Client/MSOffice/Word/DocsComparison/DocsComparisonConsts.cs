// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MSOffice.Word.DocsComparison.DocsComparisonConsts
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.MSOffice.Word.DocsComparison;

internal static class DocsComparisonConsts
{
  public static string PluginUniqueName
  {
    [DebuggerStepThrough] get => "MSWordDocsComparisonPlugin";
  }

  public static string PluginNameInMessages
  {
    [DebuggerStepThrough] get => "Плагин для сравнения документов MS Word";
  }

  public static string CompareDocsCommandName
  {
    [DebuggerStepThrough] get => "CompareDocs";
  }

  public static string CompareDocsDisplayName
  {
    [DebuggerStepThrough] get => "Сравнить документы";
  }

  public static string СompareToBaseCommandName
  {
    [DebuggerStepThrough] get => "СompareToBase";
  }

  public static string СompareToBaseDisplayName
  {
    [DebuggerStepThrough] get => "Сравнить с базовой версией";
  }
}

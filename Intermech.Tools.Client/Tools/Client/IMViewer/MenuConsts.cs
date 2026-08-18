// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.MenuConsts
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class MenuConsts
{
  public static string UpdateIMVFilesCommandName
  {
    [DebuggerStepThrough] get => "UpdateIMVFiles";
  }

  public static string UpdateIMVFilesDisplayName
  {
    [DebuggerStepThrough] get => "Обновить файлы IMViewer";
  }

  public static string UpdateIMVFilesRecursiveCommandName
  {
    [DebuggerStepThrough] get => "UpdateIMVFilesRecursive";
  }

  public static string UpdateIMVFilesRecursiveDisplayName
  {
    [DebuggerStepThrough] get => "Обновить файлы IMViewer в ветке";
  }
}

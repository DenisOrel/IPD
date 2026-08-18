
// Type: Intermech.Files.ShellKnownFolders
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Files;

/// <summary>
/// Позволяет получить пути к папкам в профиле пользователя. Используется как замена для устаревшей Environment.GetFolderPath() на системах,
/// начиная с Windows Vista. На более древних Windows возвращает null.
/// </summary>
internal static class ShellKnownFolders
{
  private static readonly Guid linksFolderId = new Guid("BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968");

  /// <summary>
  /// Возвращает путь у указанной папке профиля пользователя или операционной системы.
  /// </summary>
  /// <param name="folderId">Идентификатор папки</param>
  /// <returns>Путь к папке или null, если указанная папка не поддерживается в текущей версии операционной системы</returns>
  public static string GetFolderPath(Guid folderId)
  {
    if (Environment.OSVersion.Version.Major <= 5)
      return (string) null;
    IntPtr pathPtr = IntPtr.Zero;
    try
    {
      int knownFolderPath = ShellKnownFolders.NativeMethods.SHGetKnownFolderPath(ref folderId, 0U, IntPtr.Zero, out pathPtr);
      if (knownFolderPath != 0)
        throw Marshal.GetExceptionForHR(knownFolderPath);
      return Marshal.PtrToStringUni(pathPtr);
    }
    finally
    {
      if (pathPtr != IntPtr.Zero)
        Marshal.FreeCoTaskMem(pathPtr);
    }
  }

  public static Guid LinksFolderId => ShellKnownFolders.linksFolderId;

  private static class NativeMethods
  {
    [DllImport("shell32.dll")]
    public static extern int SHGetKnownFolderPath(
      ref Guid knownFolderId,
      uint flags,
      IntPtr userToken,
      out IntPtr pathPtr);
  }
}

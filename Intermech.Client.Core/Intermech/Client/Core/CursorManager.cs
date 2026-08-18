
// Type: Intermech.Client.Core.CursorManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Класс для работы с курсором</summary>
public class CursorManager
{
  /// <summary>установка waiting курсора для приложения</summary>
  public static void SetWaitingCursor() => Cursor.Current = Cursors.WaitCursor;

  /// <summary>установка waiting курсора для control</summary>
  public static void SetWaitingCursor(Control control) => control.Cursor = Cursors.WaitCursor;

  /// <summary>установка default курсора для приложения</summary>
  public static void SetDefaultCursor() => Cursor.Current = Cursors.Default;

  /// <summary>установка default курсора для control</summary>
  public static void SetDefaultCursor(Control control) => control.Cursor = Cursors.Default;

  /// <summary>установка курсора для приложения</summary>
  public static void SetCursor(Cursor cursor) => Cursor.Current = cursor;

  /// <summary>установка курсора для control</summary>
  public static void SetCursor(Control control, Cursor cursor) => control.Cursor = cursor;

  /// <summary>получение курсора для приложения</summary>
  public static Cursor GetCursor() => Cursor.Current;

  /// <summary>получение курсора для control</summary>
  public static Cursor GetCursor(Control control) => control.Cursor;
}

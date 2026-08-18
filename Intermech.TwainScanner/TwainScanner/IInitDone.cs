// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.IInitDone
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

[Guid("145EF088-0DD5-4113-B1E8-8F98D0818A9C")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInitDone
{
  /// <summary>Инициализация компонента</summary>
  /// <param name="connection">reference to IDispatch</param>
  void Init([MarshalAs(UnmanagedType.IDispatch)] object connection);

  /// <summary>Вызывается перед уничтожением компонента</summary>
  void Done();

  /// <summary>Возвращается инициализационная информация</summary>
  /// <param name="info">Component information</param>
  void GetInfo([MarshalAs(UnmanagedType.IDispatch)] ref object connection);
}

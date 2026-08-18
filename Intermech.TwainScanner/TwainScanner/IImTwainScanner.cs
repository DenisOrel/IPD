// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.IImTwainScanner
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

[Guid("932F0738-6A3C-49D0-916D-E42CE41FE15B")]
[ComVisible(true)]
public interface IImTwainScanner
{
  float FloatProperty { get; set; }

  void AcquireDoc(string fileExt);

  void Init([MarshalAs(UnmanagedType.IDispatch)] object connection);

  string HelloWorld();

  byte[] GetData(byte[] data);

  void GetProcessThreadID(out uint processId, out uint threadId);

  void ChangeProgress(int val);

  object IPSPlugin { get; set; }

  object DimirObject { get; set; }

  event ProgressChangedEventHandler ProgressChanged;

  event FloatPropertyChangingEventHandler FloatPropertyChanging;

  /// <summary>Событие. Получение данных от сканера</summary>
  event OnImageTransferEventHandler OnImageTransfer;

  /// <summary>Событие завершения сканирования</summary>
  event OnEndScaningEventHandler OnEndScaning;

  /// <summary>Вызывается перед уничтожением компонента</summary>
  void Done();

  /// <summary>Возвращается инициализационная информация</summary>
  /// <param name="info">Component information</param>
  void GetInfo([MarshalAs(UnmanagedType.IDispatch)] ref object connection);
}

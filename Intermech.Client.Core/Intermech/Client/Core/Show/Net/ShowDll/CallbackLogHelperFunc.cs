
// Type: Intermech.Client.Core.Show.Net.ShowDll.CallbackLogHelperFunc
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>получить сообщение об ошибке из DLL</summary>
/// <param name="message">сообщение об ошибке из ShowIPSx86.DLL, ShowIPSx64.DLL и ShowARX.DLL</param>
public delegate void CallbackLogHelperFunc([MarshalAs(UnmanagedType.LPWStr)] string message);


// Type: Intermech.Client.Core.AdditionalCommandProviderException
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

/// <summary>Внутренняя ошибка.</summary>
/// <summary>Конструктор.</summary>
/// <param name="msg">Сообщение</param>
public class AdditionalCommandProviderException(string msg) : Exception(msg)
{
  public bool BtnSkipVisible = true;
  public bool BtnSkipAllVisible = true;
}

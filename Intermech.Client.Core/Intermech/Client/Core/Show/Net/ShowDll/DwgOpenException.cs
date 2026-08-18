
// Type: Intermech.Client.Core.Show.Net.ShowDll.DwgOpenException
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>исключение при чтении файла</summary>
[Serializable]
public class DwgOpenException : Exception
{
  private DwgOpenException.ReturnType returnCode;

  public DwgOpenException.ReturnType ReturnCode => this.returnCode;

  /// <summary>Инициализирует новый экземпляр класса DwgOpenException.</summary>
  public DwgOpenException()
  {
  }

  /// <summary>Инициализирует новый экземпляр класса DwgOpenException с заданным сообщением об ошибке.</summary>
  /// <param name="message">Сообщение об ошибке с объяснением причин исключения.</param>
  public DwgOpenException(string message)
    : base(message)
  {
  }

  /// <summary>Инициализирует новый экземпляр класса Exception с заданным сообщением об ошибке и ссылкой на внутреннее исключение, являющееся его причиной.</summary>
  /// <param name="message">Сообщение об ошибке с объяснением причин исключения.</param>
  /// <param name="inner">Исключение, являющееся причиной текущего исключения. Если параметр innerException не является пустой ссылкой (Nothing в Visual Basic), то текущее исключение вызывается в блоке catch, обрабатывающем внутреннее исключение.</param>
  public DwgOpenException(string message, Exception inner)
    : base(message, inner)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message">Сообщение об ошибке с объяснением причин исключения.</param>
  /// <param name="value"></param>
  public DwgOpenException(string message, DwgOpenException.ReturnType value)
    : base(message)
  {
    this.returnCode = value;
  }

  /// <summary>Инициализирует новый экземпляр класса Exception с сериализованными данными.</summary>
  /// <param name="info">Объект SerializationInfo, содержащий данные из сериализованных объектов о созданном исключении.</param>
  /// <param name="context">Свойство StreamingContext, которое содержит контекстные сведения об источнике или назначении.</param>
  protected DwgOpenException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.returnCode = (DwgOpenException.ReturnType) info.GetInt32(nameof (ReturnCode));
  }

  /// <summary>Устанавливает SerializationInfo с именем параметра и дополнительными сведениями об исключении.</summary>
  /// <param name="info">Объект, содержащий сведения о серийных объектах.</param>
  /// <param name="context">Контекстные сведения об источнике или назначении.</param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ReturnCode", (object) this.returnCode);
  }

  /// <summary>Получает сообщение об ошибке и имя параметра или только сообщение об ошибке, если никакой параметр не установлен.</summary>
  public override string Message
  {
    get => base.Message + Environment.NewLine + $"ReturnCode: {this.returnCode}";
  }

  /// <summary>коды завершения чтения</summary>
  public enum ReturnType
  {
    /// <summary>файл прочитан без ошибок</summary>
    exOk = 0,
    exFileNotInit = 1,
    /// <summary>файл не найден</summary>
    exFileNotFound = 2,
    /// <summary>неизвестная версия</summary>
    exNotDetectedDWG = 3,
    /// <summary>неизвестная версия DWG</summary>
    exInvalidVerDWG = 4,
    /// <summary>версия DWG не поддерживается</summary>
    exNotDrawVer = 5,
    exNotKEY = 7,
  }
}

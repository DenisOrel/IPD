// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.IIPSAddIn
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Плагин (расширение) в Altium Designer</summary>
public interface IIPSAddIn
{
  /// <summary>Открыть файл в редакторе</summary>
  /// <param name="fileName">Путь и имя файла</param>
  void OpenObject(string fileName);

  /// <summary>Сохранить файл</summary>
  /// <param name="fileName">Путь и имя файла</param>
  void SaveObject(string fileName);

  /// <summary>Закрыть файл в редакторе</summary>
  /// <param name="fileName">Путь и имя файла</param>
  void CloseObject(string fileName);

  /// <summary>Возвращает handle главного окна Altiun Designer</summary>
  /// <returns>Handle главного окна</returns>
  IntPtr GetMainWindowHandle();

  /// <summary>Возвращает версию Altium Designer</summary>
  /// <returns>Строка, содержащая версию</returns>
  string GetVersion();

  /// <summary>
  /// Получить интерфейс на серверный документ - электричесую схему
  /// </summary>
  /// <param name="fileName">Путь и имя файла электрической схемы</param>
  /// <param name="open">Предварительно загрузить файл в редакторе</param>
  /// <returns>Интерфейс на серверный документ</returns>
  ISchDocument GetSchDocument(string fileName, bool open);

  /// <summary>
  /// Получить интерфейс на серверный документ - плату (PCB)
  /// </summary>
  /// <param name="fileName">Путь и имя pcb-файла</param>
  /// <returns>Интерфейс на серверный документ</returns>
  IPCBDocument GetPCBDocument(string fileName);

  /// <summary>
  /// Получить интерфейс на серверный документ - проект AltiumDesigner
  /// </summary>
  /// <param name="fileName">Путь и имя файла проекта AltiumDesigner</param>
  /// <returns>Интерфейс на серверный документ</returns>
  IADProject GetProject(string fileName);

  /// <summary>Найти среди открытых документов схему</summary>
  /// <param name="fileName">Путь и имя файла схемы</param>
  /// <returns>Интерфейс на серверный документ</returns>
  ISchDocument FindSCHObject(string fileName);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISimpleExcelReports
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса, позволяющего генерировать простые отчёты в Excel, используя COM (позднее связывание)
/// </summary>
public interface ISimpleExcelReports
{
  /// <summary>
  /// Получить ссылку на существующий экземпляр Excel или запустить новый
  /// </summary>
  /// <returns>Ссылка на существующий или вновь запущенный экземпляр Excel</returns>
  object GetExcelInstance();

  /// <summary>
  /// Получить ссылку на существующий экземпляр Excel или запустить новый, проверить ранее полученную ссылку (жив объект или недоступен)
  /// </summary>
  /// <param name="instance">Ранее полученная ссылка или null</param>
  /// <returns>Ссылка на существующий или вновь запущенный экземпляр Excel</returns>
  object GetExcelInstance(object instance);

  /// <summary>
  /// Получить ссылку на существующий экземпляр Excel или запустить новый, проверить ранее полученную ссылку (жив объект или недоступен)
  /// </summary>
  /// <param name="instance">Ранее полученная ссылка или null</param>
  /// <param name="caption">Если значение не пустое, будет установлено как заголовок приложения Excel</param>
  /// <returns>Ссылка на существующий или вновь запущенный экземпляр Excel</returns>
  object GetExcelInstance(object instance, string caption);

  /// <summary>Освободить ссылку на Excel</summary>
  /// <param name="instance">Ссылка на экземпляр Excel</param>
  void ReleaseExcelInstance(object instance);

  /// <summary>Установить видимость указанному экземпляру Excel</summary>
  /// <param name="instance">Ссылка на экзезмпляр Excel</param>
  /// <param name="visible">Видимость</param>
  void SetVisible(object instance, bool visible);

  /// <summary>
  /// Создать новую книгу, указать название её первой страницы, задать название книги, имя автора и компанию
  /// </summary>
  /// <param name="instance">Ссылка на существующий экземпляр Excel</param>
  /// <param name="caption">Название первой страницы в новой книге</param>
  /// <param name="title">Название книги</param>
  /// <param name="author">Имя автора</param>
  /// <param name="company">Компания</param>
  /// <returns>Ссылка на новую книгу</returns>
  object CreateWorkbook(
    object instance,
    string caption,
    string title,
    string author,
    string company);
}

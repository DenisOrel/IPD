// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IDocumentCADApiService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Сервис фасада для API документов, предоставляемого интегрируемым приложением.
/// </summary>
public interface IDocumentCADApiService
{
  /// <summary>
  /// Возвращает обработчик для файловых зависимостей документа. Метод может вернуть null, если у документа не может быть файловых зависимостей.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Обработчик файловых зависимостей документа или null</returns>
  IFileDependenciesHandler TryGetFileDependenciesHandler(SectionEntity docItem);

  /// <summary>
  /// Возвращает имя виртуального атрибута документа, в котором сохраняется имя типа документа. У новых документов, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Метод может вернуть null или пустую строку, если подходящего атрибута в файле документа нет.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа документа</returns>
  string GetDocumentTypeAttributeName(SectionEntity docItem);

  /// <summary>
  /// <para>
  /// Позволяет определить тип для нового импортируемого документа, прочитав его из файла документа. Если тип документа не может быть
  /// определен однозначно, то метод должен вернуть все возможные типы документов.</para>
  /// <para>
  /// Этот метод вызывается даже тогда, когда метод <see cref="M:Intermech.Tools.Integrators.Mechanical.IDocumentCADApiService.GetDocumentTypeAttributeName(Intermech.Data.SectionEntities.SectionEntity)" /> возвращает null или пустую строку.
  /// Так сделано потому, что иногда тип документа можно определить эвристически без явного хранения имени типа в файле документа.
  /// При реализации метода также нужно учитывать, что он вызывается в самом начале анализа импортируемого документа, и его рабочий элемент практически пуст.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список возможных типов для импортируемого документа</returns>
  List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem);

  /// <summary>
  /// Читает и возвращает значения свойств документа, хранящиеся в его файле. Если отсутствует API или возможность записи свойство обратно в документ,
  /// то возвращаемые значения параметров должны быть read-only, а свойство ContainerValues.IsOpenMetadata должно быть установлено в false.
  /// </summary>
  /// <remarks>
  /// Свойства документа - это именованные значения, хранящиеся в файле документа и доступные для изменения средствами редактора документа. Они
  /// используются для хранения атрибутов объекта IPS.
  /// </remarks>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Контейнер со свойствами документа, прочитанными из файла</returns>
  ContainerValues ReadDocumentProperties(SectionEntity docItem);

  /// <summary>
  /// Записывает измененные значения свойств документа в файл. Метод вызывается только при наличии изменений в свойствах. Метод должен записать
  /// только те значения, которые может, остальные он должен игнорировать.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="fileProperties">Контейнер со свойствами документа</param>
  /// <returns>true, если запись в документ имела место</returns>
  bool WriteDocumentProperties(SectionEntity docItem, ContainerValues fileProperties);

  /// <summary>
  /// Выполняет сохранение файла измененного документа на диск. Этот метод вызывается в двух случаях: если интегратор изменял документ в процессе его
  /// анализа, а также если документ взят на редактирование. Реализация этого метода должна проверить, открыт ли документ в приложении, а также имеет
  /// ли он несохраненные изменения. Только в этом случае метод должен обновить файл документа на диске.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  void SaveDocumentFile(SectionEntity docItem);

  /// <summary>
  /// Выполняет преобразование прочитанных ранее свойств файла в значения атрибутов документа. Декодированные значения атрибутов должны быть
  /// доступны для модификации, независимо от значений свойства ReadOnly у исходных параметров.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с атрибутами документа</returns>
  ValueBag DecodeDocumentAttributes(SectionEntity docItem, ContainerValues fileProperties);

  /// <summary>
  /// Выполняет преобразование значений атрибутов документа в свойства файла. Если отсутствует API или возможность записи свойств в файл,
  /// то этот метод не должен что-либо делать.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="attributeKeys">Список имен преобразуемых атрибутов</param>
  /// <param name="attributes">Контейнер с атрибутами документа</param>
  /// <param name="fileProperties">Контейнер с параметрами документа</param>
  void EncodeDocumentAttributes(
    SectionEntity docItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties);

  /// <summary>
  /// Позволяет обработать значения атрибутов документа непосредственно перед синхронизацией значений между файлом документа и объектом документа в базе IPS.
  /// </summary>
  /// <param name="documentItem">Рабочий элемент документа</param>
  /// <param name="workingSet">Рабочий набор атрибутов документа, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов документа, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">documentItem || workingSet || databaseSet</exception>
  void ProcessDocumentAttributes(
    SectionEntity documentItem,
    ValueBag workingSet,
    ValueBag databaseSet);

  /// <summary>
  /// Возвращает список имен атрибутов, значения которых необходимо перенести из файла документа в объект IPS. В данный список можно не включать ряд атрибутов, копируемых
  /// всегда - обозначение, наименование, тип документа, код документа. Если список атрибутов содержит атрибуты, которые не могут существовать у документа
  /// данного типа, то такие атрибуты будут проигнорированы.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список имен атрибутов</returns>
  ICollection<StringKey> GetDocumentSyncAttributes(SectionEntity docItem);

  /// <summary>
  /// <para>
  /// Возвращает дополнительные файлы документа, отличающиеся от мастер-файла документа не только расширением файла. Для определения таких файлов следует использовать
  /// API приложения, с которым осуществляется интеграция. Если у документов приложения нет таких дополнительных файлов, то этот метод должен вернуть
  /// пустой список.</para>
  /// <para>Те дополнительные файлы, которые указаны в настройках типа документа в базе IPS, будут добавлены к документу автоматически.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список абсолютных путей к дополнительным файлам документа</returns>
  List<string> GetSatelliteFiles(SectionEntity docItem);

  /// <summary>
  /// Возвращает персональные дополнительные файлы документа. Это такие файлы, которые не должны копироваться, при использовании этого документа в качестве прототипа.
  /// Как правило, это файлы конфигураций модели детали или сборочной единицы. Если у документа таких файлов нет, то этот метод должен вернуть пустой список.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список абсолютных путей к дополнительным файлам документа</returns>
  List<string> GetPrivateFiles(SectionEntity docItem);

  /// <summary>
  /// Возвращает информацию об изделиях, которые выпускаются по документу. Метод возвращает не готовые сущности для изделий, а объекты-заготовки,
  /// которые позже будут использованы стандартным обработчиком изделий для создания сущностей изделий.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Контейнер с заготовками сущностей изделий</returns>
  /// <exception cref="T:ArgumentNullException">documentItem</exception>
  ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem);

  /// <summary>
  /// Читает и возвращает значения атрибутов связи между документами. Метод может возвращать null, если у приложения нет таких атрибутов.
  /// </summary>
  /// <param name="projectDocument">Родительский документ</param>
  /// <param name="partDocument">Дочерний документ</param>
  /// <returns>Контейнер с значениями атрибутов или null</returns>
  ValueBag TryReadDocumentRelationAttributes(
    SectionEntity projectDocument,
    SectionEntity partDocument);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.IDocumentEditorPluginScriptService
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using System;

#nullable disable
namespace Intermech.Document.Client;

public interface IDocumentEditorPluginScriptService
{
  /// <summary>Является ли данный тип объекта типом "Шаблон документа"</summary>
  /// <param name="objType">Тип объекта</param>
  /// <returns>Тип объекта является типом "Шаблон документа"</returns>
  bool IsDocumentTemplateType(int objType);

  /// <summary>Является ли данный тип объекта типом "Документ Интермех"</summary>
  /// <param name="objType">Тип объекта</param>
  /// <returns>Тип объекта является типом "Документ Интермех"</returns>
  bool IsDocumentType(int objType);

  /// <summary>Сохранить документ в файловый атрибут объекта</summary>
  /// <param name="docObjectID">Идентификатор объекта</param>
  /// <param name="document">Документ</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта</param>
  /// <param name="isNewDocument">Новый документ. Используется для игнорирования флага SaveModificationDate</param>
  void SaveImDocumentObjectFile(
    long docObjectID,
    ImDocument document,
    string fileName,
    int fileIndex,
    bool isNewDocument);

  /// <summary>Загрузить документ из файлового атрибута объекта. Если файловый атрибут пустой, то создается пустой документ!</summary>
  /// <param name="docObjectID">Идентификатор объекта</param>
  /// <param name="fileIndex">Индекс файлового атрибута объекта. -1 - если неизвестно в каком файле хранится документ.
  /// В этом случае будет выбран первый документ нового формата, или, если его нет, то старого формата.</param>
  /// <param name="createIfNotFound">Создать пустой документ, если нет файла</param>
  /// <param name="updateDoc">Обновить документ после загрузки</param>
  /// <param name="loadInThread">Загружать в фоновом потоке</param>
  /// <returns>Документ. Если файловый атрибут пустой, то создается пустой документ!</returns>
  ImDocument LoadDocumentFromDBObject(
    long docObjectID,
    int fileIndex = -1,
    bool createIfNotFound = false,
    bool updateDoc = true,
    bool loadInThread = false);

  /// <summary>Получить идентификатор шаблона привязанный к типу документа через настройки инструмента "Редактор документов"</summary>
  /// <returns>Идентификатор типа документа</returns>
  Guid GetDocumentTemplateIDFromIMDocSettings(Guid documentType);
}

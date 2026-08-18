// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImMetaDataConverter
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Сервис позволяет преобразовывать Guid типов метаданных XML в IPS
/// </summary>
public interface IImMetaDataConverter
{
  /// <summary>
  /// Отыскать идентификатор типа атрибута в IPS на основании указанного Guid
  /// </summary>
  /// <param name="xmlAttrType">Guid типа атрибута из XML</param>
  /// <returns>-1 или идентификатор типа атрибута из IPS</returns>
  /// <remarks>Метод возвращает первое из значений атрибута с таки Guid из общей секции настроек</remarks>
  int GetIPSAttributeTypeID(Guid xmlAttrType);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа атрибута в IPS на основании его идентификатора в XML
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlAttrID">Идентификатор типа атрибута в XML</param>
  /// <returns>Guid типа атрибута в IPS</returns>
  Guid GetIPSAttributeTypeGuid(IServiceProvider services, int xmlAttrID);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа атрибута в IPS на основании его идентификатора в XML и принадлежности
  /// указанному типу объекта
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlAttrID">Идентификатор типа атрибута в XML</param>
  /// <param name="xmlObjTypeID">Ид. типа объекта</param>
  /// <param name="xmlObjType">Гуид типа объекта</param>
  /// <returns>Guid типа атрибута в IPS для указанного типа объекта</returns>
  Guid GetIPSAttributeType4ObjGuid(
    IServiceProvider services,
    int xmlAttrID,
    int xmlObjTypeID,
    Guid? xmlObjType = null);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа атрибута в IPS на основании его идентификатора в XML и принадлежности
  /// указанному типу cвязи
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlAttrID">Идентификатор типа атрибута в XML</param>
  /// <param name="xmlRelTypeID">Тип связи</param>
  /// <param name="xmlRelType">Гуид типа связи</param>
  /// <returns>Guid типа атрибута в IPS для указанного типа cвязи</returns>
  Guid GetIPSAttributeType4RelGuid(
    IServiceProvider services,
    int xmlAttrID,
    int xmlRelTypeID,
    Guid? xmlRelType = null);

  /// <summary>
  /// Получить имя поля СУБД для указанного обязательного атрибута
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlAttrGuid">Уникальный глобальный идентификатор типа обязательного атрибута</param>
  /// <returns>Имя поля СУБД для указанного обязательного атрибута или String.Empty</returns>
  string GetIPSAttributeTypeFieldName(IServiceProvider services, Guid xmlAttrGuid);

  /// <summary>
  /// Отыскать идентификатор типа объекта в IPS на основании указанного Guid
  /// </summary>
  /// <param name="xmlObjType">Guid типа объекта из XML</param>
  /// <returns>-1 или идентификатор типа объекта из IPS</returns>
  int GetIPSObjectTypeID(Guid xmlObjType);

  /// <summary>
  /// Отыскать Guid типа объекта в IPS на основании его наименования
  /// </summary>
  /// <param name="xmlObjTypeName">Наименование типа объекта из XML</param>
  /// <returns>Guid типа объекта из IPS</returns>
  Guid GetIPSObjectTypeGuid(string xmlObjTypeName);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа объекта в IPS на основании его идентификатора в XML
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlObjTypeID">Идентификатор типа объекта в XML</param>
  /// <returns>Guid типа объекта в IPS</returns>
  Guid GetIPSObjectTypeGuid(IServiceProvider services, int xmlObjTypeID);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа объекта в IPS на основании его идентификатора в XML
  /// </summary>
  /// <param name="kernel">Микроядро IPS</param>
  /// <param name="xmlObj">Описание объекта из XML</param>
  /// <returns>Guid типа объекта в IPS</returns>
  Guid GetIPSObjectTypeGuid(IKernel kernel, IImObject xmlObj);

  /// <summary>
  /// Отыскать идентификатор типа связи в IPS на основании указанного Guid
  /// </summary>
  /// <param name="xmlRelType">Guid типа связи из XML</param>
  /// <returns>-1 или идентификатор типа связи из IPS</returns>
  int GetIPSRelationTypeID(Guid xmlRelType);

  /// <summary>
  /// Отыскать Guid типа связи в IPS на основании его наименования
  /// </summary>
  /// <param name="xmlRelTypeName">Наименование типа связи из XML</param>
  /// <returns>Guid типа связи из IPS</returns>
  Guid GetIPSRelationTypeGuid(string xmlRelTypeName);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа связи в IPS на основании его параметров в XML
  /// </summary>
  /// <param name="services">Контейнер сервисов, в котором содержится конфигурация импорта</param>
  /// <param name="xmlRelTypeID">Идентификатор типа связи в XML</param>
  /// <returns>Guid типа связи в IPS</returns>
  Guid GetIPSRelationTypeGuid(IServiceProvider services, int xmlRelTypeID);

  /// <summary>
  /// Определить по конфигурации импорта из контейнера сервисов
  /// Guid типа связи в IPS на основании ее параметров в XML
  /// </summary>
  /// <param name="kernel">Микроядро IPS</param>
  /// <param name="xmlRel">Описание связи из XML</param>
  /// <returns>Guid типа связи в IPS</returns>
  Guid GetIPSRelationTypeGuid(IKernel kernel, IImRelation xmlRel);
}

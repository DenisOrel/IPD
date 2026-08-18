// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleExternalKeysService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Необязательный сервис для работы с внешними ключами изделий - уникальными идентификаторами изделий, используемыми для сопоставления
/// изделий с объектами в базе IPS. Внешние ключи записываются на связь типа "Документация на изделие", поэтому ключи могуть быть только у тех изделий,
/// которые связаны с конструкторским документом.
/// </summary>
public interface IArticleExternalKeysService
{
  /// <summary>
  /// Проверяет, поддерживается ли указанное изделие механизмом внешних ключей.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>true, если механизм внешних ключей поддерживает указанное изделие</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  bool HasExternalKeySupport(SectionEntity articleItem, SectionEntity modelItem);

  /// <summary>
  /// Присваивает всем новым изделиям внешние ключи, а для существующих изделий выполняет проверку валидности ключей. Если ключ не валиден,
  /// то он должен быть перегенерирован.
  /// </summary>
  /// <param name="articleItems">Список рабочих элементов изделий, поддерживаемых механизмом внешних ключей</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Одно из указанных изделий не поддерживается механизмом внешних ключей</exception>
  void CorrectExternalKeys(List<SectionEntity> articleItems, SectionEntity modelItem);

  /// <summary>Возвращает внешний ключ изделия.</summary>
  /// <param name="articleItem">Рабочий элемент изделия, поддерживаемый механизмом внешних ключей</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>Значение внешнего ключа изделия</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Указанное изделие не поддерживается механизмом внешних ключей</exception>
  string GetExternalKey(SectionEntity articleItem, SectionEntity modelItem);
}

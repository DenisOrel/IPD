// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISearchScheme
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс на серверный кэш схем</summary>
public interface ISearchScheme
{
  /// <summary>Добавить схему в кэш</summary>
  /// <param name="session">Сессия, в рамках которой проходит действие</param>
  /// <param name="schemeID">Идентификатор схемы</param>
  void AddScheme(IUserSession session, long schemeID);

  /// <summary>Изменить схему в кэше</summary>
  /// <param name="session">Сессия, в рамках которой проходит действие</param>
  /// <param name="schemeID">Идентификатор схемы</param>
  void ChangeScheme(IUserSession session, long schemeID);

  /// <summary>Удалить схему из кэша</summary>
  /// <param name="session">Сессия, в рамках которой проходит действие</param>
  /// <param name="schemeID">Идентификатор схемы</param>
  void DeleteScheme(IUserSession session, long schemeID);
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MovingItemSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Настройки применяются для записи, которая перемещается по составу от одного
/// родительского элемента к другому во время изменения типа связи.
/// Сохраняется в настройках по полному пути к дочернему объекту состава.
/// </summary>
[Serializable]
public sealed class MovingItemSettings : BaseOrderItemSetting
{
  /// <summary>
  /// Идентификатор версии родительского объекта, в составе которого находится заменяемая
  /// версия экземпляра/партии
  /// </summary>
  public long SourceProjID;
  /// <summary>
  /// Идентификатор связи между родительским объектом и заменяемой версией экземпляра/партии.
  /// После обработки состава данная связь должна быть удалена
  /// </summary>
  public long SourceLinkID;
  /// <summary>
  /// Идентификатор типа связи между родительским объектом и заменяемой версией экземпляра/партии
  /// </summary>
  public int SourceLinkTypeID = -1;
  /// <summary>Идентификатор версии заменяемого экземпляра/партии</summary>
  public long SourceInstanceID;
  /// <summary>
  /// Идентификатор типа заменяемой версии экземпляра/партии
  /// </summary>
  public int SourceInstanceTypeID = -1;
  /// <summary>
  /// Идентификатор версии нового изделия, на основе которого требуется сгенерировать новый экземпляр/партию, либо отыскать существующий
  /// </summary>
  public long NewArticleID;
  /// <summary>
  /// Идентификатор версии типа изделия, на основе которого требуется сгенерировать новый экземпляр/партию, либо отыскать существующий
  /// </summary>
  public int NewArticleTypeID = -1;
  /// <summary>
  /// Идентификатор связи между производственным заказом и новой версией изделия.
  /// После обработки заказа данную связь требуется уничтожить
  /// </summary>
  public long NewArticleLinkID;
  /// <summary>
  /// Идентификатор новой версии экземпляра/партии (должна быть создана связь между ней и SourceProjID)
  /// </summary>
  public long NewInstanceID;

  /// <summary>Создать пустой экземпляр класса</summary>
  public MovingItemSettings()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MovingItemSettings(object source) => this.Assign(source);

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="sourceProjID">Идентификатор версии родительского объекта, в составе которого находится заменяемая
  /// версия экземпляра/партии</param>
  /// <param name="sourceLinkID">Идентификатор связи между родительским объектом и заменяемой версией экземпляра/партии.
  /// После обработки состава данная связь должна быть удалена</param>
  /// <param name="sourceLinkTypeID">Идентификатор типа связи между родительским объектом и заменяемой версией экземпляра/партии</param>
  /// <param name="sourceInstanceID">Идентификатор версии заменяемого экземпляра/партии</param>
  /// <param name="sourceInstanceTypeID">Идентификатор типа заменяемой версии экземпляра/партии</param>
  /// <param name="newArticleID">Идентификатор версии нового изделия, на основе которого требуется сгенерировать новый экземпляр/партию, либо отыскать существующий</param>
  /// <param name="newArticleTypeID">Идентификатор версии типа изделия, на основе которого требуется сгенерировать новый экземпляр/партию, либо отыскать существующий</param>
  /// <param name="newArticleLinkID">Идентификатор связи между производственным заказом и новой версией изделия</param>
  public MovingItemSettings(
    long sourceProjID,
    long sourceLinkID,
    int sourceLinkTypeID,
    long sourceInstanceID,
    int sourceInstanceTypeID,
    long newArticleID,
    int newArticleTypeID,
    long newArticleLinkID)
  {
    this.SourceProjID = sourceProjID;
    this.SourceLinkID = sourceLinkID;
    this.SourceLinkTypeID = sourceLinkTypeID;
    this.SourceInstanceID = sourceInstanceID;
    this.SourceInstanceTypeID = sourceInstanceTypeID;
    this.NewArticleID = newArticleID;
    this.NewArticleTypeID = newArticleTypeID;
    this.NewArticleLinkID = newArticleLinkID;
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    lock (this.syncRoot)
    {
      this.SourceProjID = 0L;
      this.SourceLinkID = 0L;
      this.SourceLinkTypeID = -1;
      this.SourceInstanceID = 0L;
      this.SourceInstanceTypeID = -1;
      this.NewArticleID = 0L;
      this.NewArticleTypeID = -1;
      this.NewArticleLinkID = 0L;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MovingItemSettings movingItemSettings))
      return;
    lock (this.syncRoot)
    {
      this.SourceProjID = movingItemSettings.SourceProjID;
      this.SourceLinkID = movingItemSettings.SourceLinkID;
      this.SourceLinkTypeID = movingItemSettings.SourceLinkTypeID;
      this.SourceInstanceID = movingItemSettings.SourceInstanceID;
      this.SourceInstanceTypeID = movingItemSettings.SourceInstanceTypeID;
      this.NewArticleID = movingItemSettings.NewArticleID;
      this.NewArticleTypeID = movingItemSettings.NewArticleTypeID;
      this.NewArticleLinkID = movingItemSettings.NewArticleLinkID;
    }
  }

  /// <summary>
  /// Редактируемые данные (возвращаем ссылку на самого себя)
  /// </summary>
  public override object Data
  {
    [DebuggerStepThrough] get => (object) this;
  }
}


// Type: Intermech.Client.Core.FormDesigner.Navigator.FormDesignerViewRelation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;


namespace Intermech.Client.Core.FormDesigner.Navigator;

/// <summary>Форма редактирования атрибутов связи.</summary>
public class FormDesignerViewRelation : FormDesignerView
{
  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок при выводе на экран.
  /// Навигатор сортирует отображаемые закладки в порядке возрастания этого значения.
  /// </summary>
  /// <remarks>Значение этого свойства навигатор получает после того, как закладка будет проинициализирована в методе Initialize</remarks>
  public override int OrderID => 9;

  /// <summary>Выполняет инициализацию закладки после ее создания.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка</param>
  /// <remarks>Реализация этого метода должна работать быстро, т.е. все длительные операции желательно выполнять при первом вызове метода Activate</remarks>
  public override void Initialize(ISelectedItems items, IServiceProvider provider)
  {
    if (items == null)
      return;
    this.ObjID = items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData ? parentData.ObjectID : 0L;
    if (!(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData))
      return;
    this._relID = itemData.Value;
    this._info = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(this._relID, AttributableElements.Relation);
    base.Initialize(items, provider);
  }
}

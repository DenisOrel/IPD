// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.DocumentTypedAttributeEditor
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.DataFormats;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Обработчик изменения значений атрибута с типом выбираемого объекта "Документы" или их потомки при добавленном модуле "Архивы документов"
/// </summary>
/// <summary>Инициализация списка типов объектов id атрибута.</summary>
/// <param name="attributeId"></param>
internal class DocumentTypedAttributeEditor(int attributeId) : ObjectEditor(attributeId)
{
  private ITypeDescriptorContext _context;

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    this._context = context;
    return base.EditValue(context, sp, value);
  }

  /// <summary>
  /// Возвращает массив идентификаторов версий объектов, выбранных в SelectorForm.
  /// </summary>
  /// <param name="newValue">Выбранный ранее идентификатор версии объекта</param>
  /// <returns></returns>
  protected override IDBObjectID[] GetObjectsIDs(long newValue, bool _objectVersionProcessed = true)
  {
    IDescriptor descriptor = (IDescriptor) new HiveDescriptor();
    return SelectorForm.SelectObjects((int[]) this.mainObjTypeList.ToArray(typeof (int)), new long[1]
    {
      newValue
    }, (this.MultiSelect ? 1 : 0) != 0, true, true, this._context == null || !(this._context is IConditionHolder) ? (ConditionStructure[]) null : ((IConditionHolder) this._context).Conditions, (_objectVersionProcessed ? 1 : 0) != 0, descriptor);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.AnyFileCaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Реализует драйвер захвата изменений для простых документов, состоящих только из мастер-файла и
/// дополнительных файлов и не требующих обмена атрибутами.
/// </summary>
public sealed class AnyFileCaptureChangesDriver : SingleFileCaptureChangesBase
{
  /// <summary>
  /// <para>
  /// Позволяет определить тип для нового импортируемого документа, прочитав его из файла документа. Если тип документа не может быть
  /// определен однозначно, то метод должен вернуть все возможные типы документов. Если множество возможных типов не является
  /// ограниченным, то этот метод должен вернуть пустой список, а фактический выбор типа для документа должен быть реализован в методе
  /// <see cref="M:DetectFallbackDocumentType" />.</para>
  /// <para>
  /// Этот метод вызывается даже тогда, когда метод <see cref="M:GetDocumentTypeParameterName" /> возвращает null или пустую строку.
  /// Так сделано потому, что иногда тип документа можно определить эвристически без явного хранения имени типа в файле документа.
  /// При реализации метода также нужно учитывать, что он вызывается в самом начале анализа импортируемого документа, и его рабочий элемент практически пуст.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список возможных типов для импортируемого документа</returns>
  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    return (List<LocalId<int>>) null;
  }

  protected override SelectedObjectType DetectFallbackDocumentType(SectionEntity docItem)
  {
    return new AuxiliaryExtensionBaseTypeSelector().SelectDocumentType(docItem);
  }

  public override bool IsDocumentTypeSupported(int documentType) => true;

  /// <summary>
  /// Переводит тип документа IPS в вид документа приложения, который используется для выбора обработчика документа. Каждому виду документов соответствует свой обработчик.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Идентификатор вида документа приложения</returns>
  protected sealed override object DoMapDocumentTypeToKind(int documentType) => (object) null;

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return (IAction) new FileOnlyDocumentHandler((DocumentCaptureChangesDriver) this, this.DriverContext, docItem);
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FillEmptyDocumentIdentityHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Заполняет один из идентифицирующих атрибутов документа именем файла, если все идентифицирующие атрибуты документа пусты.
/// </summary>
public sealed class FillEmptyDocumentIdentityHandler : IAction
{
  private readonly DocumentCaptureChangesDriver driver;
  private readonly SectionEntity docItem;
  private readonly StringKey attributeKey;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="docItem">Объект документа в базе данных анализатора</param>
  /// <param name="attributeKey">Заполняемый атрибут документа</param>
  public FillEmptyDocumentIdentityHandler(
    DocumentCaptureChangesDriver driver,
    SectionEntity docItem,
    StringKey attributeKey)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (attributeKey == (StringKey) null)
      throw new ArgumentNullException(nameof (attributeKey));
    this.driver = driver;
    this.docItem = docItem;
    this.attributeKey = attributeKey;
  }

  /// <summary>
  /// Создает объект. Заполняемым атрибутом будет обозначение документа.
  /// </summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="docItem">Объект документа в базе данных анализатора</param>
  public FillEmptyDocumentIdentityHandler(
    DocumentCaptureChangesDriver driver,
    SectionEntity docItem)
    : this(driver, docItem, (StringKey) IDCache.Default.Designation.Text)
  {
  }

  /// <summary>Выполняет действие.</summary>
  public void Perform()
  {
    if (DbOperations.FindIdentityAttribute(this.docItem.Sections.Get<AttributesSection>().WorkingSet, (IEnumerable<StringKey>) this.driver.Operations.Documents.GetIdentityKeys(), false) != null)
      return;
    string newValue = this.MakeIdentity();
    AttributesSection attributesSection = this.docItem.Sections.Get<AttributesSection>();
    attributesSection.WorkingSet.Update(this.attributeKey, (object) newValue);
    attributesSection.WorkingSet.SetFlag(this.attributeKey, NamedFlags.ThrowSetException);
  }

  private string MakeIdentity()
  {
    return DocumentDesignationHelper.AppendDocCode(Path.GetFileNameWithoutExtension(FilesSection.GetMasterFile(this.docItem)), ObjectSection.GetObjectType(this.docItem));
  }
}

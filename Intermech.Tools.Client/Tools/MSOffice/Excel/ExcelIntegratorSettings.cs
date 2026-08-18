// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelIntegratorSettings
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.PropertyEditors;
using Intermech.Tools.Integrators.Simple;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelIntegratorSettings : SingleFileSettings
{
  private bool runAutoOpenMacro;

  public ExcelIntegratorSettings() => this.runAutoOpenMacro = false;

  protected override SingleFileSettings CreateClone()
  {
    return (SingleFileSettings) new ExcelIntegratorSettings();
  }

  protected override void FillClone(SingleFileSettings clonedObj)
  {
    base.FillClone(clonedObj);
    ExcelIntegratorSettings integratorSettings = (ExcelIntegratorSettings) clonedObj;
    integratorSettings.RunAutoOpenMacro = this.RunAutoOpenMacro;
    integratorSettings.SynchronizeObjectsReferencesInDocumentWithDocumentComposition = this.SynchronizeObjectsReferencesInDocumentWithDocumentComposition;
  }

  [Category("2. Вычисляемые поля в документе")]
  [DisplayName("Использовать макрос AutoOpen")]
  [Description("Включает и выключает вызов макроса AutoOpen при каждом изменении интегратором атрибутов в файле документа. Используется для обновления вычисляемых полей в документе.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool RunAutoOpenMacro
  {
    get => this.runAutoOpenMacro;
    set => this.runAutoOpenMacro = value;
  }

  [Category("3. Прочие")]
  [DisplayName("Синхронизировать ссылки на объекты в документе с составом документа")]
  [Description("Включает/выключает механизм создания состава документа на основании ссылок в содержательной части ДЭ.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool SynchronizeObjectsReferencesInDocumentWithDocumentComposition { get; set; }
}

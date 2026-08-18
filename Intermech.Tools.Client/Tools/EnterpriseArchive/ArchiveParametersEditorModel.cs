// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ArchiveParametersEditorModel
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.UI.PropertyPages;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ArchiveParametersEditorModel : IPropertyPageMvpModel
{
  private bool isInitialized;
  private CommonArchiveParameters editableState;
  private CommonArchiveParameters originalState;

  public ArchiveParametersEditorModel() => this.isInitialized = false;

  public CommonArchiveParameters EditableState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.editableState;
    }
  }

  public CommonArchiveParameters OriginalState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.originalState;
    }
  }

  private void LazyInitialize()
  {
    if (this.isInitialized)
      return;
    this.LoadAllFromDatabase();
    this.isInitialized = true;
  }

  public void Reset()
  {
    if (!this.isInitialized)
      return;
    this.LoadAllFromDatabase();
  }

  public void SaveChanges()
  {
    if (!this.isInitialized || !this.HasAnyChanges())
      return;
    this.SaveAllToDatabase();
  }

  private void LoadAllFromDatabase()
  {
    this.editableState = ArchiveParameters.Common.Clone();
    this.originalState = this.editableState.Clone();
  }

  private void SaveAllToDatabase()
  {
    this.editableState.Validate();
    string firstError = this.editableState.GetFirstError();
    if (!string.IsNullOrEmpty(firstError))
      throw new FaultException(firstError);
    ArchiveParameters.Common.Assign((object) this.editableState);
    this.originalState = this.editableState.Clone();
  }

  private bool HasAnyChanges() => this.HasLocationChanged() || this.HasImportBatchSizeChanged();

  private bool HasLocationChanged()
  {
    return string.Compare(this.editableState.Location.RawValue, this.originalState.Location.RawValue) != 0;
  }

  private bool HasImportBatchSizeChanged()
  {
    return this.editableState.ImportBatchSize.RawValue != this.originalState.ImportBatchSize.RawValue;
  }
}

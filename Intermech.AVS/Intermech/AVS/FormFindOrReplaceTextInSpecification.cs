// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.FormFindOrReplaceTextInSpecification
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary> Форрма для поиска и замены текста в спецификации </summary>
public class FormFindOrReplaceTextInSpecification : FormBaseFindOrReplaceTextInAttributes
{
  private AVSWindow _avsWindow;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public FormFindOrReplaceTextInSpecification()
  {
    this.InitializeComponent();
    this._userControlFindReplaceTextInAttributes.UpdatePositions();
  }

  protected override void OnResize(EventArgs e) => base.OnResize(e);

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary> Ссылка на окно редактирования спецификации в котором должен производиться поиск и замена текста  </summary>
  public AVSWindow AVSWindow
  {
    get => this._avsWindow;
    set => this.AttachToWindow(value != null ? (IWindowWithFind) value : (IWindowWithFind) null);
  }

  /// <summary> Вызывается после того, как окно </summary>
  protected override void AfterConnectedToView(IWindowWithFind iWindowWithFind)
  {
    base.AfterConnectedToView(iWindowWithFind);
    AVSWindow avsWindow = iWindowWithFind != null ? iWindowWithFind as AVSWindow : (AVSWindow) null;
    if (this._avsWindow == avsWindow)
      return;
    if (this._avsWindow != null)
      this._avsWindow.ViewModeSwitched -= new EventHandler(this._avsWindow_ViewModeSwitched);
    this._avsWindow = avsWindow;
    if (this._avsWindow != null)
      this._avsWindow.ViewModeSwitched += new EventHandler(this._avsWindow_ViewModeSwitched);
    this.ReloadParams();
  }

  private void ReloadParams()
  {
    if (this.InterfaceObject == null || !(this.InterfaceObject is IAttributesSelection))
      return;
    IAttributesSelection interfaceObject = this.InterfaceObject as IAttributesSelection;
    interfaceObject.BeginUpdate();
    try
    {
      interfaceObject.ClearAttributesList();
      if (this._avsWindow == null)
        return;
      AvsRowAttributeInfo[] rowAttributeInfoArray;
      switch (this._avsWindow.ViewMode)
      {
        case AVSViewMode.Page:
          rowAttributeInfoArray = this._avsWindow.AVSDocument.AvsDocumentForm == AVSDocumentForm.V ? this._avsWindow.AVSDocument.docRowFields_VarFormV.ToArray() : this._avsWindow.AVSDocument.docRowFields.ToArray();
          break;
        case AVSViewMode.Grid:
          rowAttributeInfoArray = this._avsWindow.GetGridViewColumns().ToArray();
          break;
        default:
          return;
      }
      IntList intList1 = new IntList();
      IntList intList2 = new IntList();
      foreach (AvsRowAttributeInfo rowAttributeInfo in rowAttributeInfoArray)
      {
        if (rowAttributeInfo != null && rowAttributeInfo.AttributeId != -1)
        {
          if (rowAttributeInfo.IsRelationAttribute)
          {
            if (!intList2.Contains((object) rowAttributeInfo.AttributeId))
              intList2.Add((object) rowAttributeInfo.AttributeId);
          }
          else if (!intList1.Contains((object) rowAttributeInfo.AttributeId))
            intList1.Add((object) rowAttributeInfo.AttributeId);
        }
      }
      if (intList2.Count > 0)
        interfaceObject.AddAttributes((int[]) intList2.ToArray(typeof (int)), true);
      if (intList1.Count > 0)
        interfaceObject.AddAttributes((int[]) intList1.ToArray(typeof (int)), false);
      interfaceObject.CheckAllAttributes();
    }
    finally
    {
      interfaceObject.EndUpdate();
    }
  }

  private void _avsWindow_ViewModeSwitched(object sender, EventArgs e) => this.ReloadParams();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (this._avsWindow != null)
      this._avsWindow.ViewModeSwitched -= new EventHandler(this._avsWindow_ViewModeSwitched);
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormFindOrReplaceTextInSpecification));
    this.SuspendLayout();
    this._userControlFindReplaceTextInAttributes.PossibleSearchPlaces = new string[2]
    {
      "Во всем документе",
      "В текущем разделе"
    };
    componentResourceManager.ApplyResources((object) this._userControlFindReplaceTextInAttributes, "_userControlFindReplaceTextInAttributes");
    componentResourceManager.ApplyResources((object) this._tabControlFindOrReplace, "_tabControlFindOrReplace");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (FormFindOrReplaceTextInSpecification);
    this.ResumeLayout(false);
  }
}

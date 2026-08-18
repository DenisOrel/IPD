// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchDocumentProxy
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SchDocumentProxy : FileDocumentProxy, IPropertiesCollection, IDisposable
{
  private readonly ADIntegratorSettings _settings;
  private Parameter[] _obligatoryParameters;
  private readonly ADClientSponsor _sponsor;
  private readonly ISchDocument _document;
  private int? _sheetNumber;

  public SchDocumentProxy(ISchDocument document, string path, ADIntegratorSettings settings)
    : base(path)
  {
    this._document = document;
    this._settings = settings;
    this._sponsor = new ADClientSponsor();
    this._sponsor.Register((object) document);
  }

  protected override string GetDocumentName()
  {
    Parameter parameter = Array.Find<Parameter>(this.ObligatoryParameters, (Predicate<Parameter>) (element => element.Name == "DocumentNumber"));
    return parameter == null ? string.Empty : (string) parameter.Value;
  }

  public int SheetNumber
  {
    get
    {
      if (!this._sheetNumber.HasValue)
      {
        Parameter parameter = Array.Find<Parameter>(this.ObligatoryParameters, (Predicate<Parameter>) (element => element.Name == nameof (SheetNumber)));
        this._sheetNumber = new int?(parameter != null ? (int) parameter.Value : -1);
      }
      return this._sheetNumber.Value;
    }
  }

  public Parameter[] ObligatoryParameters
  {
    get
    {
      if (this._obligatoryParameters == null)
        this._obligatoryParameters = this._document.ObligatoryParameters;
      return this._obligatoryParameters;
    }
  }

  public List<IElectricalComponent> Components
  {
    get
    {
      List<IElectricalComponent> components = new List<IElectricalComponent>();
      for (ISchComponent nextComponent = this._document.GetNextComponent(); nextComponent != null; nextComponent = this._document.GetNextComponent())
        components.Add((IElectricalComponent) new ADComponent(nextComponent, this._settings, (IDocumentFile) this));
      return components;
    }
  }

  public object GetPropertyValue(string propertyName)
  {
    return Array.Find<Parameter>(this._document.Parameters, (Predicate<Parameter>) (x => x.Name.Equals(propertyName)))?.Value;
  }

  public void SetPropertyValue(string propertyName, object value)
  {
    if (value != null)
      this._document.SetParameterValue(propertyName, value.GetType(), value);
    else
      this._document.SetParameterValue(propertyName, typeof (string), (object) string.Empty);
  }

  public IComponentProperty GetProperty(string propertyName)
  {
    Parameter adParameter = Array.Find<Parameter>(this._document.Parameters, (Predicate<Parameter>) (x => x.Name.Equals(propertyName)));
    return adParameter == null ? (IComponentProperty) null : (IComponentProperty) new ADComponentProperty(adParameter);
  }

  public ISchSheetSymbol GetNextSheetSymbol()
  {
    ISchSheetSymbol nextSheetSymbol = this._document.GetNextSheetSymbol();
    return nextSheetSymbol != null ? (ISchSheetSymbol) new ADSchSheetSymbol(nextSheetSymbol) : (ISchSheetSymbol) null;
  }

  public void Dispose() => this._sponsor.Dispose();

  public IADProject Project => (IADProject) new ADProject(this._document.Project);
}

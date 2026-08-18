// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADComponent
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Electrical;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADComponent : 
  ParametersContainer,
  IElectricalComponent,
  IPropertiesCollection,
  IFunctionalGroupComponent,
  IImbaseComponent,
  IValueBagContainer
{
  private readonly ADIntegratorSettings _settings;
  private readonly ISchComponent _component;

  public ADComponent(ISchComponent component, ADIntegratorSettings settings, IDocumentFile parent)
    : this(component, settings, parent, (FunctionalGroup) null)
  {
  }

  public ADComponent(
    ISchComponent component,
    ADIntegratorSettings settings,
    IDocumentFile parent,
    FunctionalGroup functionalGroup)
    : base((IParametrable) component)
  {
    this._component = component;
    this._settings = settings;
    this.FunctionalGroup = functionalGroup;
    this.Parent = parent;
  }

  public string UID => this._component.InternalId;

  public Guid PosGuid
  {
    get
    {
      string str = Convert.ToString(this.GetPropertyValue(ElectricalConsts.PosGuidAttribute));
      Guid empty = Guid.Empty;
      Guid posGuid;
      if (!string.IsNullOrEmpty(str))
      {
        Guid guid = GuidHelper.IsGuid(str) ? new Guid(str) : Guid.Empty;
        if (guid != Guid.Empty)
        {
          posGuid = guid;
        }
        else
        {
          posGuid = Guid.NewGuid();
          this.SetPropertyValue(ElectricalConsts.PosGuidAttribute, (object) posGuid.ToString());
        }
      }
      else
      {
        posGuid = Guid.NewGuid();
        this.SetPropertyValue(ElectricalConsts.PosGuidAttribute, (object) posGuid.ToString());
      }
      return posGuid;
    }
  }

  public string PartNumber
  {
    get
    {
      string str1 = Convert.ToString(this.GetPropertyValue(ParametersHelper.GetParameterName(this._settings, IDCache.Default.Designation.Text, true)));
      string str2 = Convert.ToString(this.GetPropertyValue(ParametersHelper.GetParameterName(this._settings, IDCache.Default.Name.Text, true)));
      return !string.IsNullOrEmpty(str1) ? $"{str1} ({str2})" : str2;
    }
  }

  public string PosDesignation => this._component.DesignatorText;

  public FunctionalGroup FunctionalGroup { get; set; }

  public bool ImbaseBinding()
  {
    string imbaseKey = Convert.ToString(this.GetPropertyValue(IDCache.Default.ImbaseKey.Text));
    if (!string.IsNullOrEmpty(imbaseKey))
      return false;
    string parameterName1 = ParametersHelper.GetParameterName(this._settings, IDCache.Default.Designation.Text, true);
    string parameterName2 = ParametersHelper.GetParameterName(this._settings, IDCache.Default.OKPCode.Text, true);
    Tuple<long, int, string> createImbaseObject = ImbaseHelper.FindOrCreateImbaseObject(imbaseKey, string.IsNullOrEmpty(parameterName1) ? string.Empty : Convert.ToString(this.GetPropertyValue(parameterName1)), string.IsNullOrEmpty(parameterName2) ? string.Empty : Convert.ToString(this.GetPropertyValue(parameterName2)), this.PartNumber);
    if (createImbaseObject == null)
      return false;
    this.SetPropertyValue(IDCache.Default.ImbaseKey.Text, (object) createImbaseObject.Item3);
    return true;
  }

  public object GetPropertyValue(string propertyName)
  {
    return CompoundHelper.isCompound(propertyName) ? (object) ElectricalComponentCompoundValue.HandleValue((IElectricalComponent) this, propertyName) : ParametersHelper.GetParameterValue(this.Parameters, propertyName);
  }

  public void SetPropertyValue(string propertyName, object value)
  {
    Parameter parameter = Array.Find<Parameter>(this.Parameters, (Predicate<Parameter>) (x => string.Compare(x.Name, propertyName, true) == 0));
    if (parameter == null)
    {
      if (value != null)
        this._component.AddNewParameter(new Parameter(propertyName, value, false, value.GetType()));
      else
        this._component.AddNewParameter(new Parameter(propertyName, (object) string.Empty, false, typeof (string)));
    }
    else
      parameter.Value = value;
  }

  public IComponentProperty GetProperty(string propertyName)
  {
    Parameter adParameter = Array.Find<Parameter>(this.Parameters, (Predicate<Parameter>) (x => string.Compare(x.Name, propertyName, true) == 0));
    return adParameter == null ? (IComponentProperty) null : (IComponentProperty) new ADComponentProperty(adParameter);
  }

  public ADComponent Clone()
  {
    return new ADComponent(this._component, this._settings, this.Parent, this.FunctionalGroup);
  }

  public IDocumentFile Parent { get; set; }

  public string ASPosDesignation
  {
    get
    {
      return !string.IsNullOrEmpty(this._settings.ASPosDesignation) ? Convert.ToString(this.GetPropertyValue(this._settings.ASPosDesignation)) : (string) null;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.EcoPropertiesService
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

#nullable disable
namespace Intermech.ECO.Client;

internal class EcoPropertiesService : IEcoPropertiesService
{
  public static bool isAdmin;
  private EcoProperties _properties;

  public EcoPropertiesService(bool isAdm) => EcoPropertiesService.isAdmin = isAdm;

  public IEcoProperties Current
  {
    get
    {
      return this._properties != null ? (IEcoProperties) this._properties : (IEcoProperties) (this.LoadFromBase() as EcoProperties);
    }
    set
    {
      this._properties = value as EcoProperties;
      this.SaveToBase((IEcoProperties) this._properties);
    }
  }

  public void SaveToBase(IEcoProperties properties)
  {
    if (this._properties == null)
      return;
    this._properties.SaveToBase();
  }

  public IEcoProperties LoadFromBase()
  {
    this._properties = EcoProperties.LoadFromBase();
    return (IEcoProperties) this._properties;
  }
}

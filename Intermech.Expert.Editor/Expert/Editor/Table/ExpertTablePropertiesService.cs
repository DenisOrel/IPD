// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTablePropertiesService
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

#nullable disable
namespace Intermech.Expert.Editor.Table;

internal class ExpertTablePropertiesService : IExpertTablePropertiesService
{
  private ExpertTableProperties _properties;

  public IExpertTableProperties Current
  {
    get
    {
      return this._properties != null ? (IExpertTableProperties) this._properties : (IExpertTableProperties) (this.LoadFromBase() as ExpertTableProperties);
    }
    set
    {
      this._properties = value as ExpertTableProperties;
      this.SaveToBase((IExpertTableProperties) this._properties);
    }
  }

  public void SaveToBase(IExpertTableProperties properties)
  {
    if (this._properties == null)
      return;
    this._properties.SaveToBase();
  }

  public IExpertTableProperties LoadFromBase()
  {
    this._properties = ExpertTableProperties.LoadFromBase();
    return (IExpertTableProperties) this._properties;
  }
}

// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.FileDocumentProxy
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal abstract class FileDocumentProxy : IDocumentFile
{
  private string _name;
  private string _path;

  public FileDocumentProxy(string path) => this._path = path;

  public string FileName => Path.GetFileName(this._path);

  public string Name
  {
    get
    {
      if (this._name == null)
        this._name = this.GetDocumentName();
      return this._name;
    }
  }

  protected abstract string GetDocumentName();
}

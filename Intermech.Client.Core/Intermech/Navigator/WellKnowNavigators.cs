
// Type: Intermech.Navigator.WellKnowNavigators
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.Navigator;

internal class WellKnowNavigators : IWellKnownNavigators
{
  private Hashtable _forms = new Hashtable();

  public void Register(string name, Control window) => this._forms[(object) name] = (object) window;

  public void Unregister(Control window)
  {
    foreach (DictionaryEntry form in this._forms)
    {
      if (form.Value.Equals((object) window))
      {
        this._forms.Remove(form.Key);
        break;
      }
    }
  }

  public Control Get(string name) => (Control) this._forms[(object) name];
}

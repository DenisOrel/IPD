
// Type: Intermech.Protection.ApplicationEntry
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Aladdin.HASP;
using System;


namespace Intermech.Protection
{
    internal class ApplicationEntry : IDisposable
    {
      private string _appName;
      private int _appId;
      private Hasp _key;

      public ApplicationEntry(string name, int id)
      {
        this._appId = id;
        this._appName = name;
      }

      public bool Checked
      {
        get => this._key != (object) 0;
        set
        {
          if (value)
            return;
          if (this._key != (object) null)
          {
            int num = (int) this._key.Logout();
          }
          this._key = (Hasp) null;
        }
      }

      public Hasp Key
      {
        get => this._key;
        internal set
        {
          if (this._key != (object) null)
            this._key.Dispose();
          this._key = value;
        }
      }

      public string ApplicationName => this._appName;

      public int ApplicationId => this._appId;

      public override string ToString() => $"{this._appId} : {this._appName} [{this.Checked}]";

      public void Dispose()
      {
        if (this._key != (object) null)
          this._key.Dispose();
        this._key = (Hasp) null;
      }
    }
}


// Type: IMClient.AboutBox.PluginInfo




using System;


namespace IMClient.AboutBox
{
    internal class PluginInfo : IComparable, IComparable<PluginInfo>
    {
      internal string Name;
      internal string Version;

      public PluginInfo(string name, string version)
      {
        this.Name = name;
        this.Version = version;
      }

      public int CompareTo(object obj) => this.CompareTo(obj as PluginInfo);

      public int CompareTo(PluginInfo other) => other == null ? 1 : this.Name.CompareTo(other.Name);
    }
}

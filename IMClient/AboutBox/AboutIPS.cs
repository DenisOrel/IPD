using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace IMClient.AboutBox
{
    internal partial class AboutIPS : Form
    {
        internal static int _packageId = -1;

        public AboutIPS()
        {
            this.InitializeComponent();

            string ipsServicePack = AssemblyAttributes.IPSServicePack;
            this.labelVersion.Text = string.IsNullOrEmpty(ipsServicePack) 
                ? string.Format(LocalizationHolder.rm.GetString("IMClient_78"), (object) AssemblyAttributes.IPSVersion) 
                : string.Format(LocalizationHolder.rm.GetString("IMClient_78_sp"), (object) AssemblyAttributes.IPSVersion, (object) ipsServicePack);

            string s1 = AssemblyAttributes.IPSBuildDate;
            string s2 = AssemblyAttributes.IPSBuildTime;
            DateTime result;

            if (DateTime.TryParse(s1, out result))
            {
                result = result.ToLocalTime();
                s1 = result.ToShortDateString();
            }

            if (DateTime.TryParse(s2, out result))
            {
                result = result.ToLocalTime();
                s2 = result.ToShortTimeString();
            }

            FileInfo fileInfo = new FileInfo(typeof(XMLSettingsStorage).Assembly.Location);
            object[] customAttributes = typeof(XMLSettingsStorage).Assembly.GetCustomAttributes(typeof(AssemblyBuildDate), true);
            AssemblyBuildDate assemblyBuildDate = customAttributes == null || customAttributes.Length == 0 ? null : customAttributes[0] as AssemblyBuildDate;

            this.labelDateTime.Text = $"{s1} {s2}";
            this.labelCopyright.Text = $"© 2003-{(assemblyBuildDate != null ? (object) assemblyBuildDate.AssemblyBuildYear : (object) DateTime.UtcNow.Year.ToString())} INTERMECH";

            if (ServicesManager.GetService(typeof(INamedImageList)) is INamedImageList service)
            {
                this.listPlugins.LargeImageList = service.ImageList;
                this.listPlugins.SmallImageList = service.ImageList;
                AboutIPS._packageId = service.ImageIndex("imgPackage");
            }

            this.FillPluginsList();
        }

        internal static void ShowAboutBox()
        {
            using (AboutIPS aboutIps = new AboutIPS())
            {
                aboutIps.ShowDialog();
            }
        }

        private void FillPluginsList()
        {
            try
            {
                List<PluginInfo> pluginInfoList = new List<PluginInfo>();
                this.listPlugins.BeginUpdate();
                this.listPlugins.Items.Clear();

                foreach (IPlugin plugin in (IEnumerable<IPlugin>) (ServicesManager.GetService(typeof(IPluginManager)) as IPluginManager).Plugins)
                {
                    foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
                    {
                        string location = plugin.Location;
                        string version = package.GetType().Assembly.GetName().Version.ToString();
                        pluginInfoList.Add(new PluginInfo(package.Name, version));
                    }
                }

                pluginInfoList.Sort();

                for (int index = 0; index < pluginInfoList.Count; ++index)
                {
                    this.listPlugins.Items.Add(new ListViewItem(pluginInfoList[index].Name, AboutIPS._packageId)
                    {
                        SubItems = {
                            pluginInfoList[index].Version
                        }
                    });
                }
            }
            finally
            {
                this.listPlugins.EndUpdate();
            }
        }
    }
}
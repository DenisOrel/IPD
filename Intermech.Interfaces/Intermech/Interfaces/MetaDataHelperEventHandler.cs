
// Type: Intermech.Interfaces.MetaDataHelperEventHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Делегат для вызова событий из MetaDataHelper</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    [Serializable]
    public delegate void MetaDataHelperEventHandler(object sender, EventArgs e);
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings
{
    [DataContract]
    internal class WatermarkSettings : FreezableObject, ICloneable
    {
        private string _text;
        private WatermarkPosition _position;
        private int _angle;
        private int _fontSize;
        private WatermarkLayer _layer;

        public WatermarkSettings()
        {
            this.Text = "";
            this.Position = WatermarkPosition.DownLeft;
            this.Angle = 45;
            this.FontSize = 50;
            this.Layer = WatermarkLayer.Above;
        }

        [DataMember]
        public string Text
        {
            get => this._text;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Text));
                this._text = value;
            }
        }

        [DataMember]
        public WatermarkPosition Position
        {
            get => this._position;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Position));
                this._position = value;
            }
        }

        [DataMember]
        public int Angle
        {
            get => this._angle;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Angle));
                this._angle = value;
            }
        }

        [DataMember]
        public int FontSize
        {
            get => this._fontSize;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(FontSize));
                this._fontSize = value;
            }
        }

        [DataMember]
        public WatermarkLayer Layer
        {
            get => this._layer;
            set
            {
                this.RequireNotFrozenBeforePropertyChange(nameof(Layer));
                this._layer = value;
            }
        }

        public object Clone()
        {
            return (object)new Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings()
            {
                Text = this.Text,
                Position = this.Position,
                Angle = this.Angle,
                FontSize = this.FontSize,
                Layer = this.Layer
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings && this.Text == watermarkSettings.Text && this.Position == watermarkSettings.Position && this.Angle == watermarkSettings.Angle && this.FontSize == watermarkSettings.FontSize && this.Layer == watermarkSettings.Layer;
        }

        public override int GetHashCode()
        {
            return ((((1756130895 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Text)) * -1521134295 + this.Position.GetHashCode()) * -1521134295 + this.Angle.GetHashCode()) * -1521134295 + this.FontSize.GetHashCode()) * -1521134295 + this.Layer.GetHashCode();
        }
    }
}

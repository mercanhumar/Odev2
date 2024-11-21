using System;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace TestAssignment
{
    public partial class RenkSecici : ContentPage
    {
        public RenkSecici()
        {
            InitializeComponent();

            RedSlider.Value = 0;
            GreenSlider.Value = 0;
            BlueSlider.Value = 0;

            GuncelleRenkOnizleme();
        }

        private void OnColorSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RedValueLabel.Text = ((int)RedSlider.Value).ToString();
            GreenValueLabel.Text = ((int)GreenSlider.Value).ToString();
            BlueValueLabel.Text = ((int)BlueSlider.Value).ToString();

            GuncelleRenkOnizleme();
        }

        private void GuncelleRenkOnizleme()
        {
            int kirmizi = (int)RedSlider.Value;
            int yesil = (int)GreenSlider.Value;
            int mavi = (int)BlueSlider.Value;

            Console.WriteLine($"Kýrmýzý: {kirmizi}, Yeþil: {yesil}, Mavi: {mavi}");

            Color renk = Color.FromRgb(kirmizi, yesil, mavi);

            ColorPreviewBox.Color = renk;

            HexCodeEntry.Text = $"#{kirmizi:X2}{yesil:X2}{mavi:X2}";
        }

        private async void OnCopyHexCodeClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(HexCodeEntry.Text);
            await DisplayAlert("Kopyalandý", "Hex kodu baþarýyla panoya kopyalandý!", "Tamam");
        }

        private void OnRandomColorClicked(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            RedSlider.Value = rastgele.Next(0, 256);
            GreenSlider.Value = rastgele.Next(0, 256);
            BlueSlider.Value = rastgele.Next(0, 256);

            GuncelleRenkOnizleme();
        }
    }
}
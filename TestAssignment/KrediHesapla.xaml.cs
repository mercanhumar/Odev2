using System;
using Microsoft.Maui.Controls;

namespace TestAssignment
{
    public partial class KrediHesapla : ContentPage
    {
        public KrediHesapla()
        {
            InitializeComponent();

            HesaplaButton.Clicked += OnHesaplamaTiklandi;
        }

        private void OnHesaplamaTiklandi(object sender, EventArgs e)
        {
            string secilenKrediTuru = KrediTuruPicker.SelectedItem?.ToString()?.Trim();

            bool krediMiktariGecerli = double.TryParse(KrediTutarEntry.Text, out double krediMiktari);
            bool faizOraniGecerli = double.TryParse(FaizOraniEntry.Text, out double faizOrani);
            bool vadeGecerli = int.TryParse(VadeEntry.Text, out int vade);

            if (string.IsNullOrEmpty(secilenKrediTuru) || !krediMiktariGecerli || !faizOraniGecerli || !vadeGecerli)
            {
                DisplayAlert("Hata", "L�tfen ge�erli giri�ler yap�n�z.", "Tamam");
                return;
            }

            double brutFaizOrani;
            if (string.Equals(secilenKrediTuru, "�htiya� Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaizOrani = ((faizOrani + (faizOrani * 0.1) + (faizOrani * 0.15)) / 100);
            }
            else if (string.Equals(secilenKrediTuru, "Ta��t Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaizOrani = ((faizOrani + (faizOrani * 0.15) + (faizOrani * 0.05)) / 100);
            }
            else if (string.Equals(secilenKrediTuru, "Konut Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaizOrani = (faizOrani / 100);
            }
            else if (string.Equals(secilenKrediTuru, "Ticari Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaizOrani = ((faizOrani + (faizOrani * 0.5) + (faizOrani * 0.05)) / 100);
            }
            else
            {
                DisplayAlert("Hata", "Ge�erli bir kredi t�r� se�iniz.", "Tamam");
                return;
            }

            double aylikTaksit = ((Math.Pow(1 + brutFaizOrani, vade) * brutFaizOrani) / (Math.Pow(1 + brutFaizOrani, vade) - 1)) * krediMiktari;
            double toplamOdeme = aylikTaksit * vade;
            double toplamFaiz = Math.Round(toplamOdeme - krediMiktari);

            AylikTaksitLabel.Text = $"Ayl�k Taksit: {aylikTaksit:F2} TL";
            ToplamOdemeLabel.Text = $"Toplam �deme: {toplamOdeme:F2} TL";
            ToplamFaizLabel.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
        }

        private void OnVadeSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double yeniDeger = e.NewValue;
            VadeEntry.Text = yeniDeger.ToString("0");
        }
    }
}


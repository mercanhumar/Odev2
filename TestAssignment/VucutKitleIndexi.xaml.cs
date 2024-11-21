
using System;
using Microsoft.Maui.Controls;

namespace TestAssignment
{
    public partial class VucutKitleIndexi : ContentPage
    {
        public VucutKitleIndexi()
        {
            InitializeComponent();

            HesaplaVKI();
        }

        private void OnKiloSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            int kiloDegeri = (int)e.NewValue;
            KiloLabel.Text = kiloDegeri.ToString();
            HesaplaVKI();
        }

        private void OnBoySliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            int boyDegeri = (int)e.NewValue;
            BoyLabel.Text = boyDegeri.ToString();
            HesaplaVKI();
        }

        private void HesaplaVKI()
        {
            bool kiloDogru = int.TryParse(KiloLabel.Text, out int kiloDegeri);
            bool boyDogru = int.TryParse(BoyLabel.Text, out int boyDegeri);

            if (kiloDogru && boyDogru && boyDegeri > 0)
            {
                double boyMetreCinsinden = boyDegeri / 100.0;
                double vkiHesapla = kiloDegeri / (boyMetreCinsinden * boyMetreCinsinden);
                VkiLabel.Text = vkiHesapla.ToString("F2");

                string durum = VKIDurumuAl(vkiHesapla);
                StatusLabel.Text = durum;
            }
        }

        private string VKIDurumuAl(double vki)
        {
            if (vki < 16)
                return "Ýleri Düzeyde Zayýf";
            else if (vki >= 16 && vki <= 16.99)
                return "Orta Düzeyde Zayýf";
            else if (vki >= 17 && vki <= 18.49)
                return "Hafif Düzeyde Zayýf";
            else if (vki >= 18.5 && vki <= 24.99)
                return "Normal Kilolu";
            else if (vki >= 25 && vki <= 29.99)
                return "Hafif Þiþman / Fazla Kilolu";
            else if (vki >= 30 && vki <= 34.99)
                return "1. Derecede Obez";
            else if (vki >= 35 && vki <= 39.99)
                return "2. Derecede Obez";
            else
                return "3. Derecede Obez / Morbid Obez";
        }
    }
}

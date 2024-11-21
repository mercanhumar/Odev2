using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;

namespace TestAssignment
{
    public partial class ToDoList : ContentPage
    {
        private List<string> tasks; // Changed back to 'tasks'

        public ToDoList()
        {
            InitializeComponent();

            if (Preferences.ContainsKey("tasks"))
            {
                string tasksJson = Preferences.Get("tasks", string.Empty);
                if (!string.IsNullOrWhiteSpace(tasksJson))
                {
                    tasks = System.Text.Json.JsonSerializer.Deserialize<List<string>>(tasksJson);
                }
            }
            else
            {
                tasks = new List<string>();
            }

            tasksCollectionView.ItemsSource = tasks;

            notEkleButton.Clicked += NotEkleButton_Clicked;

            var kaydetButton = (Button)FindByName("kaydetButton");
            if (kaydetButton != null)
            {
                kaydetButton.Clicked += KaydetButton_Clicked;
            }
        }

        private async void NotEkleButton_Clicked(object sender, EventArgs e)
        {
            string yeniGorev = await DisplayPromptAsync(
                "Yeni Görev",
                "Eklemek istediðiniz görevi yazýn:",
                "Kaydet",
                "Ýptal",
                "Örnek: Görev 1",
                100,
                Keyboard.Text);

            if (!string.IsNullOrWhiteSpace(yeniGorev))
            {
                tasks.Add(yeniGorev);
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }

        private async void KaydetButton_Clicked(object sender, EventArgs e)
        {
            string tasksJson = System.Text.Json.JsonSerializer.Serialize(tasks);

            Preferences.Set("tasks", tasksJson);

            await DisplayAlert("Baþarýlý", "Tüm görevler kaydedildi.", "Tamam");
        }

        private async void EditTask_Clicked(object sender, EventArgs e)
        {
            var dugme = sender as ImageButton;
            if (dugme == null) return;
            var secilenGorev = dugme.BindingContext as string;
            if (secilenGorev == null) return;

            int index = tasks.IndexOf(secilenGorev);
            string yeniGorev = await DisplayPromptAsync(
                "Görevi Düzenle",
                "Yeni görevi yazýn:",
                "Kaydet",
                "Ýptal",
                secilenGorev);

            if (!string.IsNullOrWhiteSpace(yeniGorev))
            {
                tasks[index] = yeniGorev;
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            var dugme = sender as ImageButton;
            if (dugme == null) return;

            var silinecekGorev = dugme.BindingContext as string;
            if (silinecekGorev == null) return;

            bool onay = await DisplayAlert("Görev Silme Onayý", "Bu görevi silmek istediðinize emin misiniz?", "Evet", "Hayýr");

            if (onay)
            {
                tasks.Remove(silinecekGorev);
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }
    }
}

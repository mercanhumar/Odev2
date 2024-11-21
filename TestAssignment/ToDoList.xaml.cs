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
                "Yeni G�rev",
                "Eklemek istedi�iniz g�revi yaz�n:",
                "Kaydet",
                "�ptal",
                "�rnek: G�rev 1",
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

            await DisplayAlert("Ba�ar�l�", "T�m g�revler kaydedildi.", "Tamam");
        }

        private async void EditTask_Clicked(object sender, EventArgs e)
        {
            var dugme = sender as ImageButton;
            if (dugme == null) return;
            var secilenGorev = dugme.BindingContext as string;
            if (secilenGorev == null) return;

            int index = tasks.IndexOf(secilenGorev);
            string yeniGorev = await DisplayPromptAsync(
                "G�revi D�zenle",
                "Yeni g�revi yaz�n:",
                "Kaydet",
                "�ptal",
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

            bool onay = await DisplayAlert("G�rev Silme Onay�", "Bu g�revi silmek istedi�inize emin misiniz?", "Evet", "Hay�r");

            if (onay)
            {
                tasks.Remove(silinecekGorev);
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }
    }
}

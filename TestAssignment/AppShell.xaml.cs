using Microsoft.Maui.Controls;

namespace TestAssignment
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Gerekirse rota tanımlayabilirsiniz
            Routing.RegisterRoute(nameof(KrediHesapla), typeof(KrediHesapla));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(VucutKitleIndexi), typeof(VucutKitleIndexi));
            Routing.RegisterRoute(nameof(RenkSecici), typeof(RenkSecici));
            Routing.RegisterRoute(nameof(ToDoList), typeof(ToDoList));

        }
    }

}

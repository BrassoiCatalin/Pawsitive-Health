﻿using Firebase.Database.Query;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Views;

namespace YourPetsHealth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeAsync();


            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LogInView());
        }

        private async Task InitializeAsync()
        {
            var firebaseClient = new Firebase.Database.FirebaseClient("https://your-pets-health-76f3b-default-rtdb.europe-west1.firebasedatabase.app/");
            try
            {
                //var clinicId = new Guid("aed64ad9-f256-47af-af4e-b47a8881c1f3");
                //await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(clinicId);
                //add a product
                //var product = new Product()
                //{
                //    Id = Guid.NewGuid(),
                //    Name = "Pampersi catelusi",
                //    Price = 29.99,
                //    ClinicId = new Guid("aed64ad9-f256-47af-af4e-b47a8881c1f3")
                //};
                //await firebaseClient
                //    .Child(nameof(Product))
                //    .Child(product.Id.ToString())
                //    .PutAsync(product);

                //add an user
                //await firebaseClient
                //    .Child(nameof(User))
                //    .Child("4")
                //    .PutAsync(new User()
                //    {
                //        Id = 4,
                //        FirstName = "Test",
                //        LastName = "Test"
                //    });

                //var userList = (await firebaseClient.Child(nameof(User))
                //    .OnceAsync<User>())
                //    .Where(x => x.Object != null)
                //    .Select(x => x.Object)
                //    .ToList();
                //var xx = 3;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

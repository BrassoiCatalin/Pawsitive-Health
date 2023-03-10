using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class NewAppointmentViewModel : ObservableObject
    {
        #region Constructors...

        public NewAppointmentViewModel()
        {
            
        }
        public NewAppointmentViewModel(Clinic clinic, List<Procedure> procedures)
        {
            SelectedClinic = clinic;
            Procedures = procedures;

            InitializePage();
            _navigationService = new NavigationService();
            SelectedProcedures = new List<object>();
            IsTimePickerVisible = false;

            SelectedDate = DateTime.Today;
            AvailableHours = new ObservableCollection<TimeSpan>();
        }

        #endregion

        #region Properties...

        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private bool _isTimePickerVisible;
        [ObservableProperty]
        private Clinic _selectedClinic;

        [ObservableProperty]
        private List<Pet> _activeUserPets;
        [ObservableProperty]
        private Pet _selectedPet;

        [ObservableProperty]
        private List<Procedure> _procedures;
        [ObservableProperty]
        private List<object> _selectedProcedures;

        [ObservableProperty]
        private ObservableCollection<TimeSpan> _availableHours;
        [ObservableProperty]
        private TimeSpan _selectedHour;

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                ChangedDate();
            }
        }

        #endregion

        #region Commands...

        [RelayCommand]
        private async void CreateAppointment(object obj)
        {
            var procedureList = new List<Procedure>();
            foreach (var item in SelectedProcedures)
            {
                procedureList.Add((Procedure)item);
            }

            if (!CheckPet() || !CheckProcedures(procedureList) || !CheckHour())
            {
                return;
            }

            var totalSumToPay = procedureList.Sum(x => x.Price);
            var totalTime = procedureList.Sum(x => x.Time);

            var response = await App.Current.MainPage.DisplayAlert("Atentie",
                $"Total de plata: {totalSumToPay} lei; timp de asteptare: {totalTime} minute." +
                $" Doresti sa contiui?",
                "Da", "Nu");

            if (response)
            {
                var selectedDateTime = new DateTime(SelectedDate.Year,
                    SelectedDate.Month,
                    SelectedDate.Day,
                    SelectedHour.Hours,
                    SelectedHour.Minutes,
                    SelectedHour.Seconds);

                var appointment = new Appointment()
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = selectedDateTime,
                    ClinicId = SelectedClinic.Id,
                    UserId = ActiveUser.User.Id,
                    Procedures = procedureList.Aggregate("", (current, s) => current + (s.Name + ", ")),
                    TotalTime = totalTime,
                    TotalPrice = totalSumToPay,
                    ActivePet = SelectedPet,
                    IsActive = true
                };

                await ApiDatabaseService.DatabaseService.CreateNewAppointment(appointment);
                await App.Current.MainPage.DisplayAlert("Succes", "Programarea a fost creata cu succes!", "OK");
                await _navigationService.PopAsync();
            }
            else
            {
                return;
            }
        }

        [RelayCommand]
        private void CalculateScheduleByDate()
        {
            if (AvailableHours.Count != 0)
            {
                return;
            }
            else
            {
                var index = SelectedClinic.StartHour;
                while (index != SelectedClinic.EndHour || index < SelectedClinic.EndHour)
                {
                    AvailableHours.Add(index);
                    index += TimeSpan.FromMinutes(15);
                }
            }

            //o sa fie nevoie de mai multe verificari pentru programari existente...
            //si sa mai adaugi ca se poate schimba un program al unei clinici!

            IsTimePickerVisible = true;
        }

        #endregion

        #region Private Methods...

        private async void InitializePage()
        {
            ActiveUserPets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);
        }

        private void ChangedDate()
        {
            IsTimePickerVisible = false;
            AvailableHours = new ObservableCollection<TimeSpan>();
        }

        private bool CheckPet()
        {
            if (SelectedPet == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare", "Trebuie sa selectezi un animal de companie!", "OK");
                return false;
            }
            return true;
        }

        private bool CheckProcedures(List<Procedure> procedures)
        {
            if (procedures.Count == 0)
            {
                App.Current.MainPage.DisplayAlert("Eroare", "Trebuie selectata cel putin o procedura!", "OK");
                return false;
            }
            return true;
        }

        private bool CheckHour()
        {
            if (SelectedHour.TotalHours == 0)
            {
                App.Current.MainPage.DisplayAlert("Eroare", "Nu ai selectat ora!", "OK");
                return false;
            }
            return true;
        }

        #endregion

    }
}

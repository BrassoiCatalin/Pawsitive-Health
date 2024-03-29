﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using YourPetsHealth.Models;
using YourPetsHealth.Utility;

namespace YourPetsHealth.Services
{
    public class ApiDatabaseService
    {
        private readonly FirebaseClient _firebaseClient;
        private static ApiDatabaseService _databaseService;

        public static ApiDatabaseService DatabaseService
        {
            get
            {
                if (_databaseService == null)
                    _databaseService = new ApiDatabaseService();
                return _databaseService;
            }
            set { _databaseService = value; }
        }

        private ApiDatabaseService()
        {
            _firebaseClient = new FirebaseClient("https://your-pets-health-76f3b-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task Register(User user)
        {
            await _firebaseClient
                .Child(nameof(User))
                .Child(user.Id.ToString())
                .PutAsync(user);
        }

        public async Task<User> Login(string email, string password)
        {
            var result = (await _firebaseClient
                .Child(nameof(User))
                .OnceAsync<User>())
                .FirstOrDefault(x => x.Object.Email == email && x.Object.Password == password);

            return result?.Object;
        }

        public async Task UpdateUser(User currentUser)
        {
            await _firebaseClient
                .Child(nameof(User))
                .Child(currentUser.Id.ToString())
                .PutAsync(currentUser);
        }

        public async Task DeleteUser(User user)
        {
            await _firebaseClient
                .Child(nameof(User))
                .Child(user.Id.ToString())
                .DeleteAsync();
        }

        public async Task UpdateClinicIdForUser(Guid guid)
        {
            ActiveUser.User.ClinicId = guid;

            await _firebaseClient
                .Child(nameof(User))
                .Child(ActiveUser.User.Id.ToString())
                .PutAsync(ActiveUser.User);
        }

        public async Task CreateNewClinic(Clinic clinic)
        {
            await _firebaseClient
                    .Child(nameof(Clinic))
                    .Child(clinic.Id.ToString())
                    .PutAsync(clinic);
        }

        public async Task<Clinic> GetClinicByActiveUserId()
        {
            var result = (await _firebaseClient
                .Child(nameof(Clinic))
                .OnceAsync<Clinic>())
                .FirstOrDefault(x => x.Object.Id == ActiveUser.User.ClinicId);
            return result?.Object;
        }

        public async Task UpdateUserRoleToShopOwner()
        {
            ActiveUser.User.Role = "ShopOwner";

            await _firebaseClient
                .Child(nameof(User))
                .Child(ActiveUser.User.Id.ToString())
                .PutAsync(ActiveUser.User);
        }

        public async Task UpdateUserRoleToCustomer()
        {
            ActiveUser.User.Role = "Customer";

            await _firebaseClient
                .Child(nameof(User))
                .Child(ActiveUser.User.Id.ToString())
                .PutAsync(ActiveUser.User);
        }

        public async Task<List<Product>> GetAllProductsByClinicId(Guid clinicId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Product))
                .OnceAsync<Product>())
                .Where(x => x.Object.ClinicId == clinicId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task CreateNewProduct(Product product)
        {
            await _firebaseClient
                    .Child(nameof(Product))
                    .Child(product.Id.ToString())
                    .PutAsync(product);
        }

        public async Task<List<Procedure>> GetAllProceduresByClinicId(Guid clinicId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Procedure))
                .OnceAsync<Procedure>())
                .Where(x => x.Object.ClinicId == clinicId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task CreateNewProcedure(Procedure procedure)
        {
            await _firebaseClient
                    .Child(nameof(Procedure))
                    .Child(procedure.Id.ToString())
                    .PutAsync(procedure);
        }

        public async Task DeleteProduct(Product product)
        {
            await _firebaseClient
                .Child(nameof(Product))
                .Child(product.Id.ToString())
                .DeleteAsync();
        }

        public async Task DeleteProcedure(Procedure procedure)
        {
            await _firebaseClient
                .Child(nameof(Procedure))
                .Child(procedure.Id.ToString())
                .DeleteAsync();
        }

        public async Task<List<Clinic>> GetAllClinics()
        {
            var result = (await _firebaseClient
                .Child(nameof(Clinic))
                .OnceAsync<Clinic>())
                .Where(x => x.Object != null)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task<User> GetUserByClinicId(Guid clinicId)
        {
            var result = (await _firebaseClient
                .Child(nameof(User))
                .OnceAsync<User>())
                .FirstOrDefault(x => x.Object.ClinicId == clinicId);

            return result?.Object;

            //var result 
        }

        public async Task CreateNewPet(Pet pet)
        {
            await _firebaseClient
                    .Child(nameof(Pet))
                    .Child(pet.Id.ToString())
                    .PutAsync(pet);
        }

        public async Task UpdatePet(Pet pet)
        {
            await _firebaseClient
                   .Child(nameof(Pet))
                   .Child(pet.Id.ToString())
                   .PutAsync(pet);
        }

        public async Task<List<Pet>> GetAllPetsByUserId(Guid userId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Pet))
                .OnceAsync<Pet>())
                .Where(x => x.Object.UserId == userId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task DeletePet(Pet pet)
        {
            await _firebaseClient
                .Child(nameof(Pet))
                .Child(pet.Id.ToString())
                .DeleteAsync();
        }

        public async Task CreateNewOrder(Order order)
        {
            await _firebaseClient
                   .Child(nameof(Order))
                   .Child(order.Id.ToString())
                   .PutAsync(order);
        }

        public async Task CreateNewAppointment(Appointment appointment)
        {
            await _firebaseClient
                    .Child(nameof(Appointment))
                    .Child(appointment.Id.ToString())
                    .PutAsync(appointment);
        }

        public async Task<List<Appointment>> GetAllAppointmentsByClinicId(Guid clinicId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Appointment))
                .OnceAsync<Appointment>())
                .Where(x => x.Object.ClinicId == clinicId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task<List<Order>> GetAllOrdersByUserId(Guid userId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Order))
                .OnceAsync<Order>())
                .Where(x => x.Object.UserId == userId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task<List<Appointment>> GetAllAppointmentsByUserId(Guid userId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Appointment))
                .OnceAsync<Appointment>())
                .Where(x => x.Object.UserId == userId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }

        public async Task DeleteOrder(Order order)
        {
            await _firebaseClient
                .Child(nameof(Order))
                .Child(order.Id.ToString())
                .DeleteAsync();
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            await _firebaseClient
                .Child(nameof(Appointment))
                .Child(appointment.Id.ToString())
                .DeleteAsync();
        }

        public async Task DeleteClinic(Clinic clinic)
        {
            await _firebaseClient
                .Child(nameof(Clinic))
                .Child(clinic.Id.ToString())
                .DeleteAsync();
        }

        public async Task<List<Order>> GetAllOrdersByClinicId(Guid clinicId)
        {
            var result = (await _firebaseClient
                .Child(nameof(Order))
                .OnceAsync<Order>())
                .Where(x => x.Object.ClinicId == clinicId)
                .Select(x => x.Object)
                .ToList();

            return result;
        }


        /*de adaugat try-catch la toate */
        /*SA NU FOLOSESTI FUNCTIILE ASTEA FARA AWAIT CA ITI CRAPA*/
    }
}

using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Register(User user, Address address)
        {
            try
            {
                await _firebaseClient
                    .Child(nameof(User))
                    .Child(user.Id.ToString())
                    .PutAsync(user);

                //await _firebaseClient
                //    .Child(nameof(Address))
                //    .Child(address.Id.ToString())
                //    .PutAsync(address);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<User> Login(string email, string password)
        {
            var result = (await _firebaseClient
                .Child(nameof(User))
                .OnceAsync<User>())
                .FirstOrDefault(x => x.Object.Email == email && x.Object.Password == password);

            return result?.Object;
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
    }
}

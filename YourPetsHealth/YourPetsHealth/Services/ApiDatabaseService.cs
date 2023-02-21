using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourPetsHealth.Models;

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

        //public async Task Register(User user)
        //{

        //}
    }
}

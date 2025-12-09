using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace SignUpApp
{
    public class UserRepository: IUserRepository
    {
        public async Task<bool> Insert(User user)
        {
            using (IDbConnection db = new SqlConnection(AppHelper.ConnectionString))
            {
                var result = await db.ExecuteAsync(SignUpApp.Properties.Resources.InsertUser, new
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    EMail = user.EMail,
                    Password = user.Password,
                    BirthDate = user.BirthDate
                });
                    return result > 0;
            }
            
        }
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUsersAuthentication.Models
{
    public class UserAuth
    {
        public bool userExists { get; set; }
        public int userRole { get; set; }

        public UserAuth UserAuthentication(string userName, string password)
        {
            using (SqlConnection sql = new SqlConnection("workstation id=proyectopos-database.mssql.somee.com;packet size=4096;user id=Diplan0120_SQLLogin_1;pwd=9x6c3nomcd;data source=proyectopos-database.mssql.somee.com;persist security info=False;initial catalog=proyectopos-database"))
            {
                SqlDataAdapter procedureUserExists = new SqlDataAdapter($"DECLARE @return_value int EXEC @return_value = [dbo].[SP_ValUser] @varUserName = '{userName}', @varPassword = '{password}' SELECT 'Return Value' = @return_value", sql);
                DataSet dataUserExists = new DataSet();

                procedureUserExists.Fill(dataUserExists);
                var userExistsResponse = dataUserExists.Tables[0].Rows[0][0].ToString();

                SqlDataAdapter procedureUserRole = new SqlDataAdapter($"DECLARE @return_value int EXEC @return_value = [dbo].[SP_ValAdmin] @varUserName = '{userName}', @varPassword = '{password}' SELECT 'Return Value' = @return_value", sql);
                DataSet dataUserRole = new DataSet();

                procedureUserRole.Fill(dataUserRole);
                var userRoleResponse = dataUserRole.Tables[0].Rows[0][0].ToString();

                return new UserAuth
                {
                    userExists = userExistsResponse == "1",
                    userRole = int.Parse(userRoleResponse)
                };

            }
        }
    }
}

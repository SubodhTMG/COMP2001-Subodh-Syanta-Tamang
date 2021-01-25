using System;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace COMP2001.Models
{
    public partial class DataAccess : DbContext
    {
       
        public DataAccess(DbContextOptions<DataAccess> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Database=COMP2001_SSyantanTamang;User Id=SSyantanTamang; Password=BiwU734*");
            }
        }

        string connectionString = "Data Source=socem1.uopnet.plymouth.ac.uk;Initial Catalog=COMP2001_SSyantanTamang;Persist Security Info=True;User ID=SSyantanTamang;Password=BiwU734*";
        public bool Validate(User ValidateUser)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("ValidateUser", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Email", ValidateUser.Email.ToString()));
            cmd.Parameters.Add(new SqlParameter("@Password", ValidateUser.Password.ToString()));
            cmd.Parameters.Add("@Validate", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            connection.Close();

            int returnValue = int.Parse(cmd.Parameters["@Validate"].Value.ToString());

            if (returnValue == 1)
                    {
                return true;
            }
            return false;  
        }

        public void Register(User Registeruser, string message)
        {

            SqlParameter response = new SqlParameter("@Message", System.Data.SqlDbType.VarChar, 50);
            response.Direction = System.Data.ParameterDirection.Output;
            Database.ExecuteSqlRaw("EXEC RegisterUser @FirstName, @LastName, @Email, @Password, @Message OUTPUT",
            new SqlParameter("@FirstName", Registeruser.FirstName),
            new SqlParameter("@LastName", Registeruser.LastName),
            new SqlParameter("@Email", Registeruser.Email),
            new SqlParameter("@Password", (Registeruser.Password)),
                response);
            return;
        }

        public void Update(int Userid, User UpdateUser)
        {

            Database.ExecuteSqlRaw("EXEC UpdateUser @FirstName, @LastName, @Email, @Password",
                new SqlParameter("@UserID", Userid),
                new SqlParameter("@FirstName", UpdateUser.FirstName),
                new SqlParameter("@LastName", UpdateUser.LastName),
                new SqlParameter("Email", UpdateUser.Email),
                new SqlParameter("@Password", (UpdateUser.Password)));
            return;

        }

        public void Delete(int UserID)
        {
            Database.ExecuteSqlRaw("EXEC DeleteUser @UserID",
                new SqlParameter("@UserID", UserID));
            return;
        }

    }
}

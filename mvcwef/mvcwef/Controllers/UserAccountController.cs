using mvcwef.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace mvcwef.Controllers
{
    public class UserAccountController : Controller
    {
        private string stringConnection = ConfigurationManager.ConnectionStrings["conexionTienda"].ConnectionString;
        // GET: UserAccount
        public ActionResult Index()
        {
            return RedirectToAction("Login","UserAccount");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccountModel usuarioNuevo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conexion = new SqlConnection(stringConnection))
                    {
                        conexion.Open();
                        string sqlInsert = "INSERT INTO [dbo].[UserAccount]([FirstName],[SecondName],[LastName],[Email],[UserName],[Password]) " +
                               "VALUES(@FirstName, @SecondName, @LastName, @Email, @UserName, @Password)";
                        SqlCommand comando = new SqlCommand(sqlInsert, conexion);
                        comando.Parameters.AddWithValue("@FirstName", usuarioNuevo.FirstName);
                        comando.Parameters.AddWithValue("@SecondName", usuarioNuevo.SecondName);
                        comando.Parameters.AddWithValue("@LastName", usuarioNuevo.LastName);
                        comando.Parameters.AddWithValue("@Email", usuarioNuevo.Email);
                        comando.Parameters.AddWithValue("@UserName", usuarioNuevo.UserName);
                        comando.Parameters.AddWithValue("@Password", usuarioNuevo.Password);
                        comando.ExecuteNonQuery();
                    }

                    ModelState.Clear();
                    ViewBag.Message = usuarioNuevo.FirstName + " " + usuarioNuevo.SecondName + " " + usuarioNuevo.LastName + " registrado exitosamente.";
                }
                catch
                {

                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Login()
        {
            return View(new UserAccountModel());
        }

        [HttpPost]
        public ActionResult Login(UserAccountModel usuario)
        {
            DataTable usuarioObtenido = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(stringConnection))
                {
                    conexion.Open();
                    string sqlQuery = "SELECT [UserId],[FirstName],[SecondName],[LastName],[Code],[Email],[UserName],[Password],[AdmissionDate],[Status] FROM [dbo].[UserAccount] " +
                                      "where UserName = @UserName and Password = @Password; ";

                    SqlDataAdapter sqlData = new SqlDataAdapter(sqlQuery, conexion);
                    sqlData.SelectCommand.Parameters.AddWithValue("@UserName", usuario.UserName);
                    sqlData.SelectCommand.Parameters.AddWithValue("@Password", usuario.Password);

                    sqlData.Fill(usuarioObtenido);
                }

                if (usuarioObtenido.Rows.Count == 1)
                {
                    Session["UserId"]  = (usuarioObtenido.Rows[0][0].ToString());
                    Session["UserName"] = (usuarioObtenido.Rows[0][6].ToString());
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("","Usuario o Password Incorrecta");
                }
            }
            catch
            {

            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
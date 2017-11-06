using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using mvcwef.Models;
using System.Globalization;
using System.Configuration;

namespace mvcwef.Controllers
{
    public class ProductController : Controller
    {
        private string stringConnection = ConfigurationManager.ConnectionStrings["conexionTienda"].ConnectionString;

        [HttpGet]
        public ActionResult Index()
        {
            DataTable productos = new DataTable();
            using (SqlConnection conexion = new SqlConnection(stringConnection))
            {
                conexion.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT ProductId, ProductName, Price, Count,(isnull(Price,0)*isnull(Count,0)) total FROM  Product where State = 1", conexion);
                sqlData.Fill(productos);
            }
            return View(productos);
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel nuevoProducto)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(stringConnection))
                {
                    conexion.Open();
                    string sqlInsert = "Insert into Product(ProductName,Price,Count) values(@ProductName,@Price,@Count)";
                    SqlCommand comando = new SqlCommand(sqlInsert, conexion);
                    comando.Parameters.AddWithValue("@ProductName", nuevoProducto.ProductName);
                    comando.Parameters.AddWithValue("@Price", nuevoProducto.Price);
                    comando.Parameters.AddWithValue("@Count", nuevoProducto.Count);
                    comando.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel producto = new ProductModel();
            DataTable productos = new DataTable();
            using (SqlConnection conexion = new SqlConnection(stringConnection))
            {
                conexion.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT ProductId, ProductName, Price, Count FROM  Product where ProductId = @ProductId", conexion);
                sqlData.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqlData.Fill(productos);
            }

            if (productos.Rows.Count == 1)
            {
                producto.Id = long.Parse(productos.Rows[0][0].ToString());
                producto.ProductName = productos.Rows[0][1].ToString();
                producto.Price = decimal.Parse(productos.Rows[0][2].ToString());
                producto.Count = long.Parse(productos.Rows[0][3].ToString());
                return View(producto);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel producto)
        {
            try
            {
                decimal amount = decimal.Parse(producto.Price.ToString(CultureInfo.InvariantCulture));
                using (SqlConnection conexion = new SqlConnection(stringConnection))
                {
                    conexion.Open();
                    SqlCommand comand = new SqlCommand("Update Product set ProductName=@ProductName, Price=@Price, Count=@Count where ProductId = @ProductId", conexion);
                    comand.Parameters.AddWithValue("@ProductId", producto.Id);
                    comand.Parameters.AddWithValue("@ProductName", producto.ProductName);
                    comand.Parameters.AddWithValue("@Price", producto.Price);
                    comand.Parameters.AddWithValue("@Count", producto.Count);
                    comand.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
         {
            ProductModel producto = new ProductModel();
            DataTable productos = new DataTable();
            using (SqlConnection conexion = new SqlConnection(stringConnection))
            {
                conexion.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT ProductId, ProductName, Price, Count FROM  Product where ProductId = @ProductId and State = 1", conexion);
                sqlData.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqlData.Fill(productos);
            }

            if (productos.Rows.Count == 1)
            {
                producto.Id = long.Parse(productos.Rows[0][0].ToString());
                producto.ProductName = productos.Rows[0][1].ToString();
                producto.Price = decimal.Parse(productos.Rows[0][2].ToString());
                producto.Count = long.Parse(productos.Rows[0][3].ToString());
                return View(producto);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(ProductModel producto)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(stringConnection))
                {
                    conexion.Open();
                    SqlCommand comand = new SqlCommand("Update Product set State=0 where ProductId = @ProductId", conexion);
                    comand.Parameters.AddWithValue("@ProductId", producto.Id);
                    comand.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


    }
}

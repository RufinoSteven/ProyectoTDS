using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using MainService.Helpers;
using System.Data;

namespace MainService.Models
{
    public class Detalles
    {
        public int id { get; set; }

        public string itemName { get; set; }
        public string unidad { get; set; }
        public long qty { get; set; }
        public decimal price { get; set; }
        public string descripcion { get; set; }
        public string familia { get; set; }

        public List<Detalles> GetDetalles(int analisisID)
        {
            var result = new List<Detalles>();
            using (SqlConnection sql = new SqlConnection("workstation id=proyectopos-database.mssql.somee.com;packet size=4096;user id=Diplan0120_SQLLogin_1;pwd=9x6c3nomcd;data source=proyectopos-database.mssql.somee.com;persist security info=False;initial catalog=proyectopos-database"))
            {
                SqlDataAdapter getDetalle = new SqlDataAdapter($"EXEC [dbo].[getDetalle] @AnalisisID = {analisisID}", sql);
                DataTable detalle = new DataTable();
                getDetalle.Fill(detalle);
                var list = detalle.ToList<Detalles>();
                foreach(Detalles d in list)
                {
                    result.Add(d);
                }
            }
            return result;
        }
    }
}

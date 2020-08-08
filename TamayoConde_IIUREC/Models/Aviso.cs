namespace TamayoConde_IIUREC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Web;
    using TamayoConde_IIUREC.Funciones;

    [Table("Aviso")]
    public partial class Aviso
    {
        string _conexion = string.Empty;
        public Aviso()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }

        [Key]
        public int aviso_id { get; set; }

        public int categoria_id { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [StringLength(10)]
        public string tipo { get; set; }

        [StringLength(250)]
        public string imagen { get; set; }

        [StringLength(250)]
        public string thumbail { get; set; }

        [StringLength(100)]
        public string urlvideo { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public TimeSpan hora { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public HttpPostedFileBase fileimagen { get; set; }

        //public virtual Aviso Aviso { get; set; }


        public List<Aviso> ListarAvisos(int categoria_id)
        {
            List<Aviso> lista = new List<Aviso>();
            string consulta = @"SELECT [aviso_id]
                                  ,[categoria_id]
                                  ,[nombre]
                                  ,[descripcion]
                                  ,[tipo]
                                  ,[imagen]
                                  ,[thumbail]
                                  ,[urlvideo]
                                  ,[fecha]
                                  ,[hora]
                                  ,[estado]
                              FROM [dbo].[Aviso] where categoria_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(categoria_id));
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var obj = new Aviso
                            {
                                aviso_id = Utilitarios.ValidarInteger(dr["aviso_id"]),
                                nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                descripcion = Utilitarios.ValidarStr(dr["descripcion"]),
                                estado = Utilitarios.ValidarStr(dr["estado"]),

                            };
                            lista.Add(obj);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }

            return lista;
        }

        public bool RegistrarAviso(Aviso obj)
        {
            bool respuesta = false;
            string consulta = @"INSERT INTO [dbo].[Aviso]
           ([categoria_id]
           ,[nombre]
           ,[descripcion]
           ,[tipo]
           ,[imagen]
           ,[thumbail]
           ,[urlvideo]
           ,[fecha]
           ,[hora]
           ,[estado])
     VALUES
           (@p0
           ,@p1
           ,@p2
           ,@p3
           ,@p4
           ,@p5
           ,@p6
           ,@p7
           ,@p8
           ,@p9)";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(obj.categoria_id));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(obj.nombre));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(obj.descripcion));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(obj.tipo));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(obj.imagen));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(obj.thumbail));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarStr(obj.urlvideo));
                    query.Parameters.AddWithValue("@p7", Utilitarios.ValidarDate(DateTime.Now));
                    query.Parameters.AddWithValue("@p8", Utilitarios.ValidarDate(DateTime.Now));
                    query.Parameters.AddWithValue("@p9", Utilitarios.ValidarStr("Activo"));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }

            return respuesta;
        }

        public Aviso AvisoDetalle(int aviso_id)
        {
            Aviso obj = new Aviso();
            string consulta = @"select * from aviso where aviso_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", aviso_id);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                obj.aviso_id = Utilitarios.ValidarInteger(dr["aviso_id"]);
                                obj.categoria_id = Utilitarios.ValidarInteger(dr["categoria_id"]);
                                obj.nombre = Utilitarios.ValidarStr(dr["nombre"]);
                                obj.descripcion = Utilitarios.ValidarStr(dr["descripcion"]);
                                obj.tipo = Utilitarios.ValidarStr(dr["tipo"]);
                                obj.imagen = Utilitarios.ValidarStr(dr["imagen"]);
                                obj.thumbail = Utilitarios.ValidarStr(dr["thumbail"]);
                                obj.urlvideo = Utilitarios.ValidarStr(dr["urlvideo"]);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }

            return obj;
        }

        public bool EditarAviso(Aviso obj)
        {
            bool respuesta = false;
            string consulta = @"UPDATE [dbo].[Aviso]
                               SET [categoria_id] = @p1
                                  ,[nombre] = @p2
                                  ,[descripcion] = @p3
                                  ,[tipo] = @p4
                                  ,[imagen] = @p5
                                  ,[thumbail] = @p6
                                  ,[urlvideo] = @p7
                                  ,[fecha] = @p8
                                  ,[hora] = @p9
                             WHERE aviso_id = @p0";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(obj.aviso_id));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarInteger(obj.categoria_id));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(obj.nombre));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(obj.descripcion));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(obj.tipo));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(obj.imagen));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarStr(obj.thumbail));
                    query.Parameters.AddWithValue("@p7", Utilitarios.ValidarStr(obj.urlvideo));
                    query.Parameters.AddWithValue("@p8", Utilitarios.ValidarDate(DateTime.Now));
                    query.Parameters.AddWithValue("@p9", Utilitarios.ValidarDate(DateTime.Now));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }

            return respuesta;
        }

        public bool ActualizarEstadoAviso(int aviso_id, int estado)
        {
            bool resultado = false;
            string consulta = @"update Aviso set estado = @p1
                                where aviso_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", aviso_id);
                    query.Parameters.AddWithValue("@p1", estado);
                    query.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (Exception ex)
            {
            }
            return resultado;
        }


    }
}

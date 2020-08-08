namespace TamayoConde_IIUREC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using TamayoConde_IIUREC.Funciones;

    [Table("Categoria")]
    public partial class Categoria
    {
        string _conexion = string.Empty;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoria()
        {
            Aviso = new HashSet<Aviso>();
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }

        [Key]
        public int categoria_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        public int? estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aviso> Aviso { get; set; }

        public int cantidadAvisos { get; set; }
        public string porcentaje {get;set;}


        public List<Categoria> ListarCategoriaAviso()
        {
            List<Categoria> lista = new List<Categoria>();
            string consulta = @"select 
                            c.nombre
                            ,(SELECT COUNT(a.aviso_id) FROM aviso as a where a.categoria_id = c.categoria_id ) as cantidad
                            ,CONCAT(((SELECT COUNT(a.aviso_id) FROM aviso as a where a.categoria_id = c.categoria_id )*100/(select count(ap.aviso_id) from aviso as ap)),'%') as porcentaje
                            from categoria as c";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var obj = new Categoria
                            {
                                nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                cantidadAvisos = Utilitarios.ValidarInteger(dr["cantidad"]),
                                descripcion = Utilitarios.ValidarStr(dr["porcentaje"]),

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

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            string consulta = @"select 
                                c.categoria_id,
                                c.nombre,
                                c.descripcion,
                                (select count(a.aviso_id) from Aviso a where a.categoria_id = c.categoria_id) as cantidadAvisos,
                                c.estado
                                from Categoria c";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var obj = new Categoria
                            {
                                categoria_id = Utilitarios.ValidarInteger(dr["categoria_id"]),
                                nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                descripcion = Utilitarios.ValidarStr(dr["descripcion"]),
                                cantidadAvisos = Utilitarios.ValidarInteger(dr["cantidadAvisos"]),
                                estado = Utilitarios.ValidarInteger(dr["estado"]),

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

        public bool EliminarCategoria(int categoria_id)
        {
            bool resultado = false;
            string consulta = @"DELETE FROM [dbo].[Categoria]
                                where categoria_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", categoria_id);
                    query.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public bool ActualizarEstadoCategoria(int categoria_id, int estado)
        {
            bool resultado = false;
            string consulta = @"update Categoria set estado = @p1
                                where categoria_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", categoria_id);
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

        public bool RegistrarCategoria(Categoria obj)
        {
            bool respuesta = false;
            string consulta = @"INSERT INTO [dbo].[Categoria]
                                ([nombre]
                                ,[descripcion]
                                ,[estado])
                            VALUES
                                (@p0,@p1,@p2)";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(obj.nombre));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(obj.descripcion));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarInteger(1));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }

            return respuesta;
        }

        public Categoria CategoriaDetalle(int categoria_id)
        {
            Categoria obj = new Categoria();
            string consulta = @"select * from categoria where categoria_id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", categoria_id);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                obj.categoria_id = Utilitarios.ValidarInteger(dr["categoria_id"]);
                                obj.nombre = Utilitarios.ValidarStr(dr["nombre"]);
                                obj.descripcion = Utilitarios.ValidarStr(dr["descripcion"]);
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

        public bool EditarCategoria(Categoria obj)
        {
            bool respuesta = false;
            string consulta = @"UPDATE [dbo].[Categoria]
                            SET [nombre] = @p1
                                ,[descripcion] = @p2
                            WHERE categoria_id = @p0";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(obj.categoria_id));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(obj.nombre));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(obj.descripcion));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }

            return respuesta;
        }

    }
}
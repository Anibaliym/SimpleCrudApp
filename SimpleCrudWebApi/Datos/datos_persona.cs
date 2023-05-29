using SimpleCrudWebApi.Modelo;
using System.Data;
using Dapper;
using SimpleCrudWebApi.Modelo.system;

namespace SimpleCrudWebApi.Datos
{
    public static class datos_persona
    {
        /// <summary>
        /// Lista todas las personas disponibles del sistema.
        /// </summary>
        /// <author>Anibal Yañez</author>
        public static async Task<List<view_persona>> obtieneTodasLasPersonasDelSistema() { 

            List<view_persona> lstPersonas = new List<view_persona>();

            try
            {
                using (var con = conexion_datos.GetOpenConnection())
                {
                    lstPersonas = con.Query<view_persona>("spPersona_obtiene_todasLasPersonaDelSistema", null, commandType: CommandType.StoredProcedure).ToList();
                    con.Close();
                }

                return lstPersonas;
            }
            catch (Exception e)
            {
                return lstPersonas;
            }
        }

        /// <summary>
        /// Obtiene los datos de una persona por su rut.
        /// </summary>
        /// <param name="rut">Rut de la persona a buscar.</param>
        /// <author>Anibal Yañez</author>
        public static async Task<List<view_persona>> obtieneDatosPersonaPorRut(string rut)
        {
            List<view_persona> objPersona = new List<view_persona>();

            try
            {
                using (var con = conexion_datos.GetOpenConnection()) {
                    objPersona = con.Query<view_persona>("spPersona_obtiene_personaPorRut", new { rut }, commandType: CommandType.StoredProcedure).ToList();
                }

                return objPersona;

            }
            catch (Exception error) { 
                return objPersona;
            }
        }


        /// <summary>
        /// Obtiene los datos de una persona por su rut.
        /// </summary>
        /// <param name="persona">objeto persona</param>
        /// <author>Anibal Yañez</author>
        public static async Task<view_persona> obtieneDatosPersonaPorId(view_persona persona) {
            view_persona objPersona = new view_persona();

            try
            {
                using (var con = conexion_datos.GetOpenConnection()) {
                    objPersona = con.Query<view_persona>("spPersona_obtiene_personaPorRut", new { persona = persona.personaId }, commandType: CommandType.StoredProcedure).First();
                }

                return objPersona;

            }
            catch (Exception error)
            {
                return objPersona;
            }
        }

        /// <summary>
        /// Registra una nueva persona en el sistema
        /// </summary>
        /// <param name="persona">objeto persona</param>
        /// <param name="tipoUsuario">Tipo de suario que se esta registrando</param>
        /// <param name="usuarioId">Identificador del usuario gestionador</param>
        /// <author>Anibal Yañez</author>
        public static async Task<view_respuestasSistema> registraDatosNuevaPersona(view_persona persona, view_usuario usuario, long usuarioId) {
            view_respuestasSistema respuesta = new view_respuestasSistema();
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@rut", persona.rut, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@nombre", persona.nombre, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@apellidoPaterno", persona.apellidoPaterno, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@apellidoMaterno", persona.apellidoMaterno, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@nacimiento", persona.nacimiento, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@sexo", persona.sexo, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@tipoUsuario", usuario.tipoUsuario, DbType.Int32, direction: ParameterDirection.Input);
            parametros.Add("@usuarioId", usuarioId, DbType.String, direction: ParameterDirection.Input);

            //Parametros de salida
            parametros.Add("@registrosCreados", DbType.String, direction: ParameterDirection.Output);
            parametros.Add("@errorCode", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var con = conexion_datos.GetOpenConnection()) {
                    con.Execute("spPersona_crear_persona", parametros, commandType: CommandType.StoredProcedure);

                    respuesta.registrosCreados = parametros.Get<int>("registrosCreados");
                    respuesta.errorCode = parametros.Get<int>("errorCode"); 
                }
            }
            catch (Exception error) {
                respuesta.errorCode = 1;
                respuesta.registrosCreados = 0; 
            }

            return respuesta; 
        }

        /// <summary>
        /// Actualiza una persona existente en el sistema.
        /// </summary>
        /// <param name="persona">objeto persona</param>
        /// <param name="tipoUsuario">Tipo de suario que se esta registrando</param>
        /// <param name="usuarioId">Identificador del usuario gestionador</param>
        /// <author>Anibal Yañez</author>
        public static async Task<view_respuestasSistema> actualizaDatosPersona(view_persona persona, int tipoUsuario, long usuarioId)
        {
            view_respuestasSistema respuesta = new view_respuestasSistema();
            DynamicParameters parametros = new DynamicParameters();

            //parametros de entrada.
            parametros.Add("@rut", persona.rut, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@nombre", persona.nombre, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@apellidoPaterno", persona.apellidoPaterno, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@apellidoMaterno", persona.apellidoMaterno, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@nacimiento", persona.nacimiento, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@sexo", persona.sexo, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@usuarioId", usuarioId, DbType.String, direction: ParameterDirection.Input);

            //parametros de salida. 
            parametros.Add("@registrosActualizados", DbType.String, direction: ParameterDirection.Output);
            parametros.Add("@errorCode", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var con = conexion_datos.GetOpenConnection()) {
                    con.Execute("spPersona_actualiza_persona", parametros, commandType: CommandType.StoredProcedure); 

                    respuesta.registrosActualizados = parametros.Get<int>("registrosActualizados");
                    respuesta.errorCode = parametros.Get<int>("errorCode");
                }
            }
            catch (Exception error) {
                respuesta.errorCode = 1;
                respuesta.registrosActualizados = 0;
            }

            return respuesta; 
        }

        /// <summary>
        /// Elimina una persona existente en el sistema.
        /// </summary>
        /// <param name="persona">objeto persona</param>
        /// <param name="usuarioId">Identificador del usuario gestionador</param>
        /// <author>Anibal Yañez</author>        
        public static async Task<view_respuestasSistema> eliminaDatosPersona(string rut, long usuarioId)
        {
            view_respuestasSistema respuesta = new view_respuestasSistema();
            DynamicParameters parametros = new DynamicParameters();

            //parametros de entrada
            parametros.Add("@rut", rut, DbType.String, direction: ParameterDirection.Input);
            parametros.Add("@usuarioId", usuarioId, DbType.String, direction: ParameterDirection.Input);
            
            //parametros de salida
            parametros.Add("@registrosEliminados", DbType.String, direction: ParameterDirection.Output);
            parametros.Add("@errorCode", DbType.String, direction: ParameterDirection.Output);

            try
            {
                using (var con = conexion_datos.GetOpenConnection()) {
                    con.Execute("spPersona_elimina_persona", parametros, commandType: CommandType.StoredProcedure); 

                    respuesta.registrosEliminados = parametros.Get<int>("registrosEliminados");
                    respuesta.errorCode = parametros.Get<int>("errorCode");
                }

                return respuesta;
            }
            catch (Exception error) {
                respuesta.registrosEliminados = 0;
                respuesta.errorCode = 1; 
            }

            return respuesta; 
        }
    }
}

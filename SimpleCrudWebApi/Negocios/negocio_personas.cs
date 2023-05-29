using Dapper;
using SimpleCrudWebApi.Datos;
using SimpleCrudWebApi.Modelo;
using SimpleCrudWebApi.Modelo.system;
using System.Data;

namespace SimpleCrudWebApi.Negocios
{
    public static class negocio_personas
    {
        public static async Task<object> obtieneTodasLasPersonasDelSistema()
        {
            List<view_persona> personas = await datos_persona.obtieneTodasLasPersonasDelSistema();

            return new {
                success = true,
                totalPersonasObtenidas = personas.Count(),
                listadoDePersonas = personas
            };
        }

        public static async Task<object> obtieneDatosPersonaPorRut(string rut)
        {
            List<view_persona> resultado = await datos_persona.obtieneDatosPersonaPorRut(rut);

            return new {
                success = true,
                personas = resultado
            };
        }

        public static async Task<view_persona> obtieneDatosPersonaPorId(view_persona persona)
        {
            return await datos_persona.obtieneDatosPersonaPorId(persona);
        }

        public static async Task<object> registraDatosNuevaPersona(view_persona persona, view_usuario usuario, long usuarioId)
        {
            view_respuestasSistema respuesta = await datos_persona.registraDatosNuevaPersona(persona, usuario, usuarioId);

            if (respuesta.errorCode == 1)
                respuesta.detalles = "Ocurrio un error al intentar registrar a la persona.";
            else { 
                if(respuesta.registrosCreados == 0)
                    respuesta.detalles = "La persona que esta intentando registrar, ya existe en nuestros sistemas.";
                else
                    respuesta.detalles = "Persona registrada correctamente.";
            }

            return new {
                success = (respuesta.errorCode == 0) ? true : false,
                message = respuesta.detalles,
                personaRegistrada = (respuesta.registrosCreados > 0) ? true : false 
            };
        }

        public static async Task<object> actualizaDatosPersona(view_persona persona, int tipoUsuario, long usuarioId)
        {
            view_respuestasSistema respuesta = await datos_persona.actualizaDatosPersona(persona, tipoUsuario, usuarioId);

            if (respuesta.errorCode == 1)
                respuesta.detalles = "Ocurrio un error al intentar actualizar la persona.";
            else
            {
                if (respuesta.registrosActualizados == 0)
                    respuesta.detalles = "La persona que esta intentando actualizar, no existe en nuestros registros.";
                else
                    respuesta.detalles = "Persona actualizada correctamente.";
            }

            return new {
                success = (respuesta.errorCode == 0) ? true : false,
                message = respuesta.detalles,
                personaRegistrada = (respuesta.registrosActualizados > 0) ? true : false
            };
        }

        public static async Task<object> eliminaDatosPersona(string rut, long usuarioId)
        {
            view_respuestasSistema respuesta = await datos_persona.eliminaDatosPersona(rut, usuarioId);

            if (respuesta.errorCode == 1)
                respuesta.detalles = "Ocurrio un error al intentar eliminar la persona.";
            else
            {
                if (respuesta.registrosEliminados == 0)
                    respuesta.detalles = "La persona que esta intentando eliminar, no existe en nuestros registros.";
                else
                    respuesta.detalles = "Persona eliminada correctamente.";
            }

            return new {
                success = (respuesta.errorCode == 0) ? true : false,
                message = respuesta.detalles,
                personaRegistrada = (respuesta.registrosActualizados > 0) ? true : false

            };
        }
    }
}

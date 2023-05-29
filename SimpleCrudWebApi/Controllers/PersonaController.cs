using Microsoft.AspNetCore.Mvc;
using SimpleCrudWebApi.Modelo;
using SimpleCrudWebApi.Modelo.system;
using SimpleCrudWebApi.Negocios;
using System.Collections.Generic;

namespace SimpleCrudWebApi.Controllers
{
    [ApiController]
    [Route("api/Persona")]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        [Route("obtieneTodasLasPersonasDelSistema")]
        public async Task<object> obtieneTodasLasPersonasDelSistema() {
            return new { response = await negocio_personas.obtieneTodasLasPersonasDelSistema() };
        }

        [HttpGet]
        [Route("obtieneDatosPersonaPorRut")]
        public async Task<object> obtieneDatosPersonaPorRut([FromHeader] string rut)
        {
            return new { response = await negocio_personas.obtieneDatosPersonaPorRut(rut) };
        }

        [HttpPost]
        [Route("registraDatosNuevaPersona")]
        public async Task<object> registraDatosNuevaPersona([FromBody] view_persona persona, string email, string contrasena, int tipoUsuario)
        {
            view_usuario usuario = new view_usuario();

            usuario.email = email;
            usuario.contrasena = contrasena;
            usuario.tipoUsuario = tipoUsuario; 

            return await negocio_personas.registraDatosNuevaPersona(persona, usuario, 1); ;
        }

        [HttpPut]
        [Route("actualizaDatosPersona")]
        public async Task<object> actualizaDatosPersona([FromBody] view_persona persona, string email, string contrasena, int tipoUsuario)
        {
            view_usuario usuario = new view_usuario();

            usuario.email = email;
            usuario.contrasena = contrasena;
            usuario.tipoUsuario = tipoUsuario;

            return await negocio_personas.actualizaDatosPersona(persona, usuario.tipoUsuario, 1);
        }

        [HttpDelete]
        [Route("eliminaDatosPersona")]
        public async Task<object> eliminaDatosPersona([FromHeader] string rut) {
            return await negocio_personas.eliminaDatosPersona(rut, 1); 
        }
    }
}

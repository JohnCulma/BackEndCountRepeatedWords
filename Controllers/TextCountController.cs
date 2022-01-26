using CountRepeatedWords.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountRepeatedWords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class TextCountController : ControllerBase
    {
        private readonly AppDbContext context;
        public TextCountController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<string> Words(string word, string text)
        {

            if (word != null || text != null) {
                string[] splitText = text.ToLower().Split(new char[] { ' ', ',', '.', ';', ':' }); //Convierte text en array           

                int quantityItems = Array.FindAll(splitText, x => x == word.ToLower()).Count(); //cuenta los items de la cadena y

                return "La cantidad de palabras son: " + quantityItems.ToString();
            }

            return "Los datos no pueden ser nulos";
            
        }
    }
}

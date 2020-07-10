using MauticApi.Data;
using MauticApi.Models;
using MauticApi.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MauticApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IService _service;
        private readonly EFContext _context;
        private string _mauticAutorization;
        private string _mauticCookie;

        public ContactController(EFContext context, IService service)
        {
            _context = context;
            _service = service;
        }

        //[EnableCors]
        [HttpPost]
        public JsonResult PostContact(Contact contact)
        {
            var configuration = Configuration.GetConfiguration();
            bool save;
            _mauticAutorization = configuration["Variables:MauticAutorization"];
            _mauticCookie = configuration["Variables:MauticCookie"];
            var response = _service.CreateContact(contact, _mauticAutorization, _mauticCookie);
            var response2 = _service.AddContactInSegment(contact, _mauticAutorization, _mauticCookie);

            try
            {
                _context.Contact.Add(contact);
                _context.SaveChanges();
                save = true;
            }
            catch
            {
                save = false;
            }
            

            return new JsonResult(new { save = save, responseContact = response.StatusCode, responseSegment = response2.StatusCode });
            
        }
    }
}

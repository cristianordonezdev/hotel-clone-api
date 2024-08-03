using Microsoft.AspNetCore.Mvc;
using hotel_clone_api.Repositories;
using AutoMapper;
using hotel_clone_api.Models.DTOs;
using hotel_clone_api.Models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace hotel_clone_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetContacts([FromQuery] int pageNumber = 1, int pageSize = 10, string query = null)
        {
            var contacts = await _contactRepository.GetAllContacts(pageNumber, pageSize, query);
            return Ok(_mapper.Map<List<ContactDto>>(contacts));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContactDto>(contact));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PostContact([FromBody] ContactAddDto contactAdd)
        {
            var contactDomain = _mapper.Map<Contact>(contactAdd);
            await _contactRepository.CreateContact(contactDomain);

            return CreatedAtAction(nameof(GetContact), new { id = contactDomain.Id }, contactDomain);
        }
    }
}

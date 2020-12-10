using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopLista.Web.Interfaces;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZapisiController : ControllerBase
    {
        private readonly IZapisiRepository _zapisiRepository;
        private readonly IMapper _mapper;

        public ZapisiController(IZapisiRepository zapisiRepository, IMapper mapper)
        {
            _zapisiRepository = zapisiRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetZapisi")]
        public ActionResult<IEnumerable<ZapisViewModel>> GetZapisi([FromQuery] bool selectApproved, [FromQuery] FilterViewModel filter)
        {
            IEnumerable<Zapis> zapisi;
            zapisi = _zapisiRepository.GetFilteredZapisi(selectApproved, filter);
            return Ok(_mapper.Map<IEnumerable<ZapisViewModel>>(zapisi));
        }

        [HttpPost]
        public ActionResult<ZapisViewModel> CreateZapis([FromBody] ZapisForCreationViewModel model)
        {
            var zapis = _mapper.Map<Zapis>(model);
            _zapisiRepository.AddZapis(zapis);
            _zapisiRepository.Save();

            var zapisToReturn = _mapper.Map<ZapisViewModel>(zapis);
            return CreatedAtRoute("GetZapisi",
                new { zapisId = zapisToReturn.Id },
                zapisToReturn);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteZapis(int id)
        {
            Zapis zapis = _zapisiRepository.GetZapis(id);
            if (zapis == null)
            {
                return NotFound();
            }
            _zapisiRepository.DeleteZapis(zapis);
            _zapisiRepository.Save();
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public ActionResult UpdateApprovalZapis(int id, [FromBody] ZapisForUpdateViewModel model)
        {
            Zapis zapis = _zapisiRepository.GetZapis(id);
            if(zapis == null)
            {
                return NotFound();
            }
            _zapisiRepository.UpdateApprovalOfZapis(zapis, model.Odobreno);
            _zapisiRepository.Save();
            return NoContent();
        }


    }
}
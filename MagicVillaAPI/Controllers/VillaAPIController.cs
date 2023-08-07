using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("id:int", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if(id == 0)
        {
            return BadRequest();
        }

        var Villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        if(Villa == null)
        {
            return NotFound();
        }

        return Ok(Villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(villaDTO == null)
        {
            return BadRequest(villaDTO);
        }

        if(villaDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDTO);
        
        return CreatedAtRoute("GetVilla", new {id=villaDTO.Id}, villaDTO);
    }
}

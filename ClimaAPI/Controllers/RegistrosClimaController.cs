using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimaAPI.Data;
using ClimaAPI.Models;

namespace ClimaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrosClimaController : ControllerBase
{
    private readonly AppDbContext _context;

    public RegistrosClimaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistroClima>>> GetRegistrosClima()
    {
        return await _context.RegistrosClima.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroClima>> GetRegistroClima(int id)
    {
        var registroclima = await _context.RegistrosClima.FindAsync(id);

        if (registroclima == null)
        {
            return NoContent();
        }

        return registroclima;
    }

    [HttpGet("ciudad/{ciudadId}")]
    public async Task<ActionResult<IEnumerable<RegistroClima>>> GetRegistrosClimaCiudad(int ciudadId)
    {
        return await _context.RegistrosClima.Include(r => r.Ciudad)
                                            .Where(r => r.CiudadId == ciudadId)
                                            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<RegistroClima>> PostRegistroClima(RegistroClima registroClima)
    {

        if (!await _context.Ciudades.AnyAsync(c => c.Id == registroClima.CiudadId))
        {
            return BadRequest("La ciudad no existe.");
        }

        if (registroClima.Fecha > DateTime.Now)
        {
            return BadRequest("La fecha no puede ser futura.");
        }

        registroClima.Descripcion = registroClima.Descripcion?.Trim().ToUpper();

        _context.RegistrosClima.Add(registroClima);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistroClima), new { id = registroClima.Id}, registroClima);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistroClima(int id)
    {
        var registroClima = await _context.RegistrosClima.FindAsync(id);

        if(registroClima == null)
        {
            return NotFound();
        }

        _context.RegistrosClima.Remove(registroClima);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistroClima(int id, RegistroClima registroClima)
    {
        if(id != registroClima.Id)
        {
            return BadRequest("Los Id no coinciden.");
        }

        if(registroClima.Fecha > DateTime.Now)
        {
            return BadRequest("La fecha no puede ser futura.");
        }

        if (!await _context.Ciudades.AnyAsync(c => c.Id == registroClima.CiudadId))
        {
            return BadRequest("La ciudad no existe.");
        }

        var registroExistente = await _context.RegistrosClima.FindAsync(id);

        if(registroExistente == null)
        {
            return NotFound();
        } 

        registroExistente.Fecha = registroClima.Fecha;
        registroExistente.TemperaturaC = registroClima.TemperaturaC;
        registroExistente.Humedad = registroClima.Humedad;
        registroExistente.Descripcion = registroClima.Descripcion?.Trim().ToUpper(); 
        registroExistente.CiudadId = registroClima.CiudadId;

        await _context.SaveChangesAsync();

        return NoContent();      
    } 
}




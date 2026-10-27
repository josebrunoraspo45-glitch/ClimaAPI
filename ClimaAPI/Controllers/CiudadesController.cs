using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClimaAPI.Models;
using ClimaAPI.Data;

namespace ClimaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CiudadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CiudadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudades()
    {
        return await _context.Ciudades.ToListAsync();
    } 


    // Paso A: “Un método público y asincrónico que, más tarde, devuelve una respuesta HTTP con una ciudad especifica segun el ID pedido .”
    // Paso B: Comienza igual que el get, solo que a este se le saca IEnumerable devido a que es uno y no una lista, se agregas int id como parametro en el parentesis, (consulta: se puede agregar mas de un parametro?), para traer solo esa ciudad se declara una variable y ahi se busca en la base de datos, luego se consulta si la ciudad existe para ver si devolver error 404, por ultimo se devuelve la variable 


    [HttpGet("{id}")]
    public async Task<ActionResult<Ciudad>> GetCiudad(int id)
    {
        var ciudad = await _context.Ciudades.FindAsync(id);

        if (ciudad == null)
        {
            return NotFound();
        }

        return ciudad;
    }


    // Paso A: “Un método público y asincrónico que, más tarde, genera una peticion HTTP para guardar una ciudad nueva en la base de datos mientras tanto cumpla con las verificaciones del backend”
    // Paso B: Comienza igual que el get, solo que a este se le saca IEnumerable debido a que es uno y no una lista, se le da parametros ciudad que segun yo hace refernecia a todo el MODELS, el Add lo mas probale es que lo precargue y corrobora que pase las verificaciones. El SaveChangesAsync si guarda los cambios en la base de datos. El JSON no manda un Id pero la base de datos le asigna uno. la 3ra pregunta no lo se tengo 2 razonamientos: 200 es para get y 201 es para post y asi con los demas metodos o puede ser que 201 se deba al primer id creado aunque no creo que sea pór eso 

    [HttpPost]
    public async Task<ActionResult<Ciudad>> PostCiudad(Ciudad ciudad)
    {
        ciudad.Nombre = ciudad.Nombre?.Trim().ToUpper();


        _context.Ciudades.Add(ciudad);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCiudad), new { id = ciudad.Id }, ciudad);
    }



// Paso A: “Un método público y asincrónico que, más tarde, recibe una peticion para eliminar un registro segun el id que recibio el parametro. Al igual que el get por id hay que traerlo en una variable y consultar si existe para luego proceder a eliminarla y guardar los cambios. Se devuelve 204 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCiudad(int id)
    {
        var ciudad = await _context.Ciudades.FindAsync(id);

        if (ciudad == null)
        {
            return NotFound();
        }

        _context.Ciudades.Remove(ciudad);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutCiudad(int id, Ciudad ciudad)
    {
        if (id != ciudad.Id)
        {
            return BadRequest();
        }

        var ciudadExistente = await _context.Ciudades.FindAsync(id);

        if (ciudadExistente == null)
        {
            return NotFound();
        }

        ciudadExistente.Nombre = ciudad.Nombre?.Trim().ToUpper();
        ciudadExistente.Pais = ciudad.Pais?.Trim().ToUpper();
        ciudadExistente.Latitud = ciudad.Latitud;
        ciudadExistente.Longitud = ciudad.Longitud;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("pais/{pais}")]
    public async Task<ActionResult<IEnumerable<Ciudad>>> GetPais(string pais)
    {
        return await _context.Ciudades.Where(c => c.Pais == pais.Trim().ToUpper()).ToListAsync();
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celesetialObjects = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            if (!celesetialObjects.Any())
                return NotFound();
            foreach(var celestialObject in celesetialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();

            }
            return Ok(celesetialObjects);
        }

        [HttpGet]
        public IActionResult GetALl()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            foreach(var celesetialObject in celestialObjects)
            {
                celesetialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celesetialObject.Id).ToList();

            }
            return Ok(celestialObjects);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication66.Models;

namespace WebApplication66.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalsContext _context;

        public AnimalsController(AnimalsContext context)
        {
            _context = context;

            #region Добавление данных
            if (!_context.SkinColors.Any())
            {
                _context.SkinColors.Add(new SkinColor { Name = "Белый" });
                _context.SkinColors.Add(new SkinColor { Name = "Серый" });
                _context.SkinColors.Add(new SkinColor { Name = "Бурый" });
                _context.SkinColors.Add(new SkinColor { Name = "Рыжий" });

                _context.SaveChanges();
            }

            if (!_context.KindOfAnimals.Any())
            {
                _context.KindOfAnimals.Add(new KindOfAnimal { Name = "Млекопитающее" });
                _context.KindOfAnimals.Add(new KindOfAnimal { Name = "Рыба" });
                _context.KindOfAnimals.Add(new KindOfAnimal { Name = "Птица" });

                _context.SaveChanges();
            }

            if (!_context.Locations.Any())
            {
                _context.Locations.Add(new Location { Name = "Озерск" });
                _context.Locations.Add(new Location { Name = "Снежинск" });
                _context.Locations.Add(new Location { Name = "Челябинск" });
                _context.Locations.Add(new Location { Name = "Тагил" });
                _context.Locations.Add(new Location { Name = "Екатеринрбург"});

                _context.SaveChanges();
            }

            if (!_context.Regions.Any())
            {
                _context.Regions.Add(new Region { Code = 74 });
                _context.Regions.Add(new Region { Code = 96 });

                _context.SaveChanges();
            }

            if (!_context.Animals.Any())
            {
                _context.Animals.Add(new Animal
                {
                    Name = "Лиса",
                    KindOfAnimal = _context.KindOfAnimals.Where(w => w.Name == "Млекопитающее").FirstOrDefault(),
                    SkinColor = _context.SkinColors.Where(w => w.Name == "Рыжий").FirstOrDefault(),
                    Location = _context.Locations.Where(w => w.Name == "Снежинск").FirstOrDefault(),
                    Region = _context.Regions.Where(w => w.Code == 74).FirstOrDefault()
                });

                _context.Animals.Add(new Animal
                {
                    Name = "Карась",
                    KindOfAnimal = _context.KindOfAnimals.Where(w => w.Name == "Рыба").FirstOrDefault(),
                    SkinColor = _context.SkinColors.Where(w => w.Name == "Серый").FirstOrDefault(),
                    Location = _context.Locations.Where(w => w.Name == "Озерск").FirstOrDefault(),
                    Region = _context.Regions.Where(w => w.Code == 74).FirstOrDefault()
                });

                _context.Animals.Add(new Animal
                {
                    Name = "Курица",
                    KindOfAnimal = _context.KindOfAnimals.Where(w => w.Name == "Птица").FirstOrDefault(),
                    SkinColor = _context.SkinColors.Where(w => w.Name == "Бурый").FirstOrDefault(),
                    Location = _context.Locations.Where(w => w.Name == "Челябинск").FirstOrDefault(),
                    Region = _context.Regions.Where(w => w.Code == 74).FirstOrDefault()
                });

                _context.Animals.Add(new Animal
                {
                    Name = "Голубь",
                    KindOfAnimal = _context.KindOfAnimals.Where(w => w.Name == "Птица").FirstOrDefault(),
                    SkinColor = _context.SkinColors.Where(w => w.Name == "Серый").FirstOrDefault(),
                    Location = _context.Locations.Where(w => w.Name == "Тагил").FirstOrDefault(),
                    Region = _context.Regions.Where(w => w.Code == 96).FirstOrDefault()
                });

                _context.Animals.Add(new Animal
                {
                    Name = "Кошка",
                    KindOfAnimal = _context.KindOfAnimals.Where(w => w.Name == "Млекопитающее").FirstOrDefault(),
                    SkinColor = _context.SkinColors.Where(w => w.Name == "Серый").FirstOrDefault(),
                    Location = _context.Locations.Where(w => w.Name == "Екатеринрбург").FirstOrDefault(),
                    Region = _context.Regions.Where(w => w.Code == 96).FirstOrDefault()

                });
                _context.SaveChanges();
            }
 #endregion
        }

        //GET: api/Animals
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        //GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }

        //Поиск
        //api/animals/name?params=л&params=а
        [HttpGet("name")]
        public IEnumerable<Animal> GetAnimalsByName([FromQuery] string[] param)
        {
            List<Animal> listAnmals = new List<Animal>();
            
            foreach (var c in param)
                listAnmals.AddRange(_context.Animals.Where(x => x.Name.ToLower().Contains(c)).ToList());

            return listAnmals.Distinct();
        }

        //api/animals/region?params=7
        [HttpGet("region")]
        public IEnumerable<Animal> GetAnimalsByRegion([FromQuery] string[] param)
        {
            List<Animal> listAnmals = new List<Animal>();

            foreach (var c in param)
                listAnmals.AddRange(_context.Animals.Where(x => x.Region.Code.ToString().Contains(c)).ToList());

            return listAnmals.Distinct();
        }

        //api/animals/color?params=л
        [HttpGet("color")]
        public IEnumerable<Animal> GetAnimalsByColor([FromQuery] string[] param)
        {
            List<Animal> listAnmals = new List<Animal>();

            foreach (var c in param)
                listAnmals.AddRange(_context.Animals.Where(x => x.SkinColor.Name.Contains(c)).ToList());

            return listAnmals.Distinct();
        }
    }
}

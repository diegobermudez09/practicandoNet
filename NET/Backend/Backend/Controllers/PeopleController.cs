using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = Repository.People.FirstOrDefault(x => x.Id == id);
            if (people == null)
            {
                return NotFound();
            }
            return Ok(people);
        }
        
        [HttpGet("search/{search}")]
        public List<People> Get(String search) =>
               Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult add(People people)
        {
            if (string.IsNullOrEmpty(people.Name))
            {
                return BadRequest();
            }
            Repository.People.Add(people);
            return NoContent();
        }
    
    }

    public class Repository
    {

        public static List<People> People = new List<People>
        {
            new People()
            {
                Id=1,
                Name ="Pedro",
                Birthdate = new DateTime(1999,12,3)
            },
            new People()
            {
                Id=2,
                Name ="Luis",
                Birthdate = new DateTime(2000,3,12)
            },
            new People()
            {
                Id=3,
                Name ="Carmen",
                Birthdate = new DateTime(2010,4,14)
            }
        };

    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}

using DirectoryWebApi.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaporsController : ControllerBase
    {
        private readonly RehberDbContext _context;

        public RaporsController(RehberDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var konumlar = _context.IletisimBilgileris.GroupBy(x => x.Konum).Select(s => s.Key).ToList();
            var result = from konum in konumlar
                         select new
                         {
                             Konum = konum,
                             KisiSayisi = _context.IletisimBilgileris.Where(x => x.Konum == konum).Count(),
                             TelefonSayisi = _context.IletisimBilgileris.Where(x => x.Konum == konum && x.TelefonNumarasi != "").Count()
                         };
            return Ok(result.OrderBy(x => x.KisiSayisi).ToList());
        }
    }
}

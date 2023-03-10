using AutoMapper;
using DirectoryWebApi.Models.Context;
using DirectoryWebApi.Models.Dtos;
using DirectoryWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace DirectoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RehbersController : ControllerBase
    {
        private readonly RehberDbContext _context;
        private readonly IMapper _mapper;

        public RehbersController(RehberDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Post(RehberDto rehberDto)
        {
            Rehber rehber = _mapper.Map<Rehber>(rehberDto); 
            await _context.Rehbers.AddAsync(rehber);
            await _context.SaveChangesAsync();
            RemoveCache("rehbers");
            return Ok("Kayıt işlemi başarılı");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id , RehberDto rehberDto)
        {
            Rehber rehber = await _context.Rehbers.FindAsync(id);   
            rehber.Firma= rehberDto.Firma;
            rehber.Ad=rehberDto.Ad;
            rehber.Soyad=rehberDto.Soyad;
            await _context.SaveChangesAsync();

            RemoveCache("rehbers");
            return Ok("Güncelleme işlemi başarılı");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Rehber rehber = await _context.Rehbers.FindAsync(id );
            _context.Rehbers.Remove(rehber);
            await _context.SaveChangesAsync();

            RemoveCache("rehbers");
            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Rehber> rehberListesi = GetCache<List<Rehber>>("rehbers");
            if(rehberListesi == null)
            {
                rehberListesi= await _context.Rehbers.Include(x => x.IletisimBilgileris).ToListAsync();
                SetCache<List<Rehber>>("rehbers", rehberListesi);
            }
            return Ok(rehberListesi);
        }

        void SetCache<T>(string key, T data)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<List<Rehber>> rehbers = redisclient.As<List<Rehber>>();

            redisclient.Set<T>(key, data);

        }

        T GetCache<T>(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<List<Rehber>> rehbers = redisclient.As<List<Rehber>>();

            return redisclient.Get<T>(key);

        }

        void RemoveCache(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<List<Rehber>> rehbers = redisclient.As<List<Rehber>>();

            redisclient.Remove(key);
        }

    }
}

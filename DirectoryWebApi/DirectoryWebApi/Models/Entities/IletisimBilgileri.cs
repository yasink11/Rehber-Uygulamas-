namespace DirectoryWebApi.Models.Entities
{
    public class IletisimBilgileri
    {
        public int Id { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Email { get;set; }
        public string Adres { get; set; }
        public int Konum { get; set;}
        public int RehberId { get; set; }
    }
}

namespace D.W.C.Lib.D.W.C.Models
{
    public class Uzytkownik
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Haslo { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Organizacja { get; set; }
        public string Stanowisko { get; set; }
    }
}
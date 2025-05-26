using Microsoft.EntityFrameworkCore;

namespace s28482_OstatnieZadaniePunktowane.Data;

public class AppDbContext :DbContext
{
    //Zarejestrowanie modelu w kontekscie bazy danych
    
    
    
    
    // Klasa musi implementować konstruktor który umożliwia przekazywnie Opcji
    // 
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
}
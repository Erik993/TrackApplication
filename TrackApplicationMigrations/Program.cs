using Microsoft.EntityFrameworkCore;
using TrackApplicationData.DbContextData;

public class Program
{
    public static void Main()
    {
        using var context = new ApplicationContext();
        context.Database.Migrate();   
    }
}
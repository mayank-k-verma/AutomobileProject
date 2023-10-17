namespace Automobile.Entities.DbSet;

public class Driver : BaseEntity {

    public Driver()
    {
        Achievements = new HashSet<Achievement>();
    }
    public string Name { get; set; }
    public int DriverNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<Achievement> Achievements{ get; set; }
} 
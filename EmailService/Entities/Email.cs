namespace GaragesStructure.Entities;

public class Email : BaseEntity<Guid>
{
    public string? EmailToSend { get; set; }
}
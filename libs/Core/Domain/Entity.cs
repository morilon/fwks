namespace Fwks.Core.Domain;

public abstract class Entity<TKeyType> : Notifiable where TKeyType : struct
{
    public TKeyType Id { get; set; }
}

namespace CleanArch.StarterKit.Domain.Entities;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}

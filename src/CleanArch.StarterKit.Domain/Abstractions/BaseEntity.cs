namespace CleanArch.StarterKit.Domain.Abstractions;

/// <summary>
/// Represents the base class for all entities with an identifier.
/// </summary>
/// <typeparam name="TKey">The type of the entity's identifier.</typeparam>
public abstract class BaseEntity<TKey>
{
    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    public TKey Id { get; set; } = default!;
}

using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.Validation;

namespace FC.CodeFlix.Catalog.Domain.Entities;
public class Category : AggregateRoot
{
    private const int MAX_DESCRIPTION_LENGTH = 10_000;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    internal Category(string name, string description, bool isActive = true) : base()
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public static Category Create(string name, string description, bool isActive = true)
    {
        return new Category(name, description, isActive);
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string aName, string? aDescription = null)
    {
        Name = aName;
        Description = aDescription ?? Description;
        Validate();
    }

    public void Validate()
    {

        DomainValidation.StringHasValue(this.Name, nameof(this.Name));
        DomainValidation.IsNotNull(Description, nameof(this.Description));

        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));
        DomainValidation.MaxLength(Description, MAX_DESCRIPTION_LENGTH, nameof(Description));
    }

}

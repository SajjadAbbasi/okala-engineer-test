namespace Okala.Core.Entities;

public class AvailableCurrency(long id, string code)
{
    public long Id { get; private set; } = id;
    public string Code { get; private set; } = code;
}
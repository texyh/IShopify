namespace IShopify.Core.Security
{
    public enum AccessKeyStatus
    {
        Unknown = 0,
        NotFound = 1,
        Expired = 2,
        NotReady = 3,
        Errored = 4,
        MaxAttemptsExceeded = 5,
        Valid = 6
    }
}

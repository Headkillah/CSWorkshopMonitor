namespace WorkshopMonitor.Overwatch
{
    public interface IOverwatchAssetWrapper
    {
        string TechnicalName { get; }

        ulong SourcePackageId { get; }

        ushort ParentAssetId { get; }
    }
}
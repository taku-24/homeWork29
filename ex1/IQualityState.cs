namespace ex1;

public interface IQualityState
{
    double Coefficient { get; }
    GoodsQuality Quality { get; }
    IQualityState NextQualityState();
}
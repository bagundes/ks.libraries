namespace KS.Core.Base;

public interface IInit
{
    void I00_Dependencies();
    void I10_Configure();
    void I20_Start();
    void I30_Restart();
    void I40_Stop();
    void I90_RollBack();
}
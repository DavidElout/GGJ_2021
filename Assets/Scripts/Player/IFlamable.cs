public interface IFlamable
{
    int SanityPool { get; set; } // Amount of sanity increase before burning out
    int TimeToBurnPerSanity { get; set; }
    bool OnFire { get; set; }
    bool BurnedOut { get; set; }
    bool SanityLimitIncrease { get; set; }
    void Ignite();
    void Extinguish();
    void BurnOut();
}

public interface IFlamable
{
    int SanityPool { get; set; } // Amount of sanity increase before burning out
    bool OnFire { get; set; }
    bool BurnedOut { get; set; }
    void Ignite();
    void Extinguish();
}

using UnityEngine;

public interface IIluminatable
{
    public void OnIlluminated(Color color);
    public void OnStartIlluminated(Color color);
    public void OnEndIlluminated(Color color);
}

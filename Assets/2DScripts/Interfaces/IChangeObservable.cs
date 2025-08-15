using System;

public interface IChangeObservable
{
    public event Action<float,float> ValueChanged;
}

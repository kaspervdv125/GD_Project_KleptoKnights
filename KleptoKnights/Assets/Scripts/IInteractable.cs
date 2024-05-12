using UnityEngine;

public interface IInteractable 
{
    GameObject gameObject { get ; }
    public string Name { get; }
    public string Verb { get; }
    public bool IsInteractable { get; }




    public abstract bool IsAvailable(GameObject interactor);
    public abstract void StartInteract(GameObject interactor);
    public abstract void EndInteract(GameObject interactor);
    
    
}


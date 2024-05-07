using UnityEngine;

public interface IInteractable 
{
    GameObject GameObject { get ; } 
    
    public string Name { get; }
    public string Verb { get; }

    public int MaxUsers { get; }
    public int CurrentUsers { get; }
    public bool IsOccupied => CurrentUsers < MaxUsers;


    public float MaxProgress { get; }
    public float CurrentProgress { get; }


    public bool IsInteractable(KnightCharacter interactor)
    {
        
        return !IsOccupied;

    }   

    public void StartInteract(KnightCharacter interactor)
    {
    }

    public void EndInteract(KnightCharacter interactor)
    {
    }
    
    
}


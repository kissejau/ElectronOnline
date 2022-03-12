using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

 public class Interactable : NetworkBehaviour
{
     public virtual void Interaction() { }
     public virtual void Interaction(NetworkIdentity gameObject){}
}

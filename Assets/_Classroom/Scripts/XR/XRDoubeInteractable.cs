using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDoubeInteractable : XRGrabInteractable
{
    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(interactorsSelecting.Count == 1)
            base.ProcessInteractable(updatePhase);

        else if(interactorsSelecting.Count == 2 && updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            ProcessDoubleInteractable();
    }

    public void ProcessDoubleInteractable()
    {
        Transform firstHand = interactorsSelecting[0].transform;
        Transform secondHand = interactorsSelecting[1].transform;

        float distance = Vector3.Distance(firstHand.position, secondHand.position);
        transform.localScale =  new Vector3(distance, distance, distance);

        Vector3 middlePoint = (firstHand.position + secondHand.position) / 2;
        transform.position = middlePoint;
    }

    protected override void Grab()
    {
        if(interactorsSelecting.Count == 1)
            base.Grab();
    }

    protected override void Drop()
    {
        if(!isSelected)
            base.Drop();
    }


}

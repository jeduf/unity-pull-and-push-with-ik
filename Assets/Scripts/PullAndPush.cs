using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PullAndPush : MonoBehaviour
{
    [Header("Area Variables")]
    public float castingDistance = 10f;
    [Space(10)]
    CharacterController controller;
    Transform pullPos;
    [Header("Pull&Push Variables")]
    public float pullSpeed = 10f;
    public float pushSpeed = 10f;
    bool objectPulled = false;
    Collider closestObject = null;
    [Header("PullRig")]
    public Rig handAimRig;
    [Header("PushRig")]
    public Rig handHitRig;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pullPos = transform.Find("PullPos");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //Declare closestDistance.
            float closestDistance = castingDistance;
            //Check if there is an object pulled already.
            if(!objectPulled)
            {
                //Check Pushable and Pullable objects inside a radius.
                Collider[] hitColliders = Physics.OverlapSphere(controller.transform.position, castingDistance, LayerMask.GetMask("PushPullObject"));
                //Check all objects one by one to determine closest.
                foreach (Collider hitCollider in hitColliders)
                {
                //Find distance of each object.
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                //Get the closest object.
                    if(distance < closestDistance)
                    {
                        closestObject = hitCollider;
                        closestDistance = distance;

                    }
                }
                //If there are 0 object in radius make closest object null.
                if(hitColliders.Length == 0)
                {
                    closestObject = null;
                }
                //Change pull object variable.
                if(closestObject != null)
                {
                    objectPulled = true;
                }
                
            }
            //If there is an object pulled do this.
            else
            {
                //Push object.
                closestObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * pushSpeed);
                //Add gravity to object.
                closestObject.GetComponent<Rigidbody>().useGravity = true;
                //Change pull object variable.
                objectPulled = false;
                //Set IK anims (preferably)
                handHitRig.weight = 1;
                StartCoroutine(SetWeight( 1f, 0f, 0.5f, handAimRig));
                StartCoroutine(SetWeight( 1f, 0f, 0.7f, handHitRig));

            }          
        }
        //Hold the object in destinated area if it pulled.
        if(objectPulled)
            {
                closestObject.transform.position = Vector3.Lerp(closestObject.transform.position, pullPos.position, Time.deltaTime * pullSpeed);
                closestObject.GetComponent<Rigidbody>().useGravity = false;
                //Set IK anim (preferably)
                StartCoroutine(SetWeight( 0f, 1f, 0.5f, handAimRig));

            }
        
    }
    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to casting distance.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, castingDistance);
    }

    //IK Change smoother
    IEnumerator SetWeight( float startValue, float endValue, float duration, Rig rig)
   {
     float elapsed = 0.0f;
     while (elapsed < duration )
     {
       rig.weight = Mathf.Lerp( startValue, endValue, elapsed / duration );
       elapsed += Time.deltaTime;
       yield return null;
     }
     rig.weight = endValue;
   }

}

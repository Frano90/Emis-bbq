using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using FranoW;
[System.Serializable]
public class ParabolicShooter
{
    public float h;
    private Transform myTransform;
    public ParabolicShooter(Transform myObject)
    {
        h = 2;
        myTransform = myObject;
    }
    LaunchData CalculateLaunchData(Vector3 iniPos, Vector3 tgt) {
        float displacementY = tgt.y - iniPos.y;
        Vector3 displacementXZ = new Vector3 (tgt.x - iniPos.x, 0, tgt.z - iniPos.z);
        float time = Mathf.Sqrt(-2*h/-9f) + Mathf.Sqrt(2*(displacementY - h)/-9f); //Mathf.Sqrt(2*(displacementY - h)/Gravity.defaultValue);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * -9f * h);
        Vector3 velocityXZ = displacementXZ / time;
        
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(-9f), time);
    }
    public void DrawPath(Vector3 startPos, Vector3 endPos) {
        LaunchData launchData = CalculateLaunchData (startPos, endPos);
        Vector3 previousDrawPoint = startPos;

        int resolution = 30;
        
        for (int i = 1; i <= resolution; i++) {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *-9f * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = myTransform.position + displacement;
            Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }
    // public Vector3[] GetParabolePath(Vector3 targetPos)
    // {
    //     LaunchData launchData = CalculateLaunchData (myTransform.position, targetPos);
    //     Queue<Vector3> aux = new Queue<Vector3>();
    //
    //     if (float.IsNaN(launchData.timeToTarget))
    //         return aux.ToArray();
    //     
    //     int resolution = 30;
    //     for (int i = 1; i <= resolution; i++) {
    //         float simulationTime = i / (float)resolution * launchData.timeToTarget;
    //         Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *-9f * simulationTime * simulationTime / 2f;
    //         Vector3 drawPoint = myTransform.position + displacement;
    //         aux.Enqueue(drawPoint);
    //     }
    //     return aux.ToArray();
    // }
    // public bool IsInJumpDistance(Vector3 targetPos, float jumpDistance){return Vector3.Distance(myTransform.position, targetPos) <= jumpDistance;}
    // public bool IsInJumpDistance(Vector3 initialPos, Vector3 endPos,float jumpDistance){return Vector3.Distance(initialPos, endPos) <= jumpDistance;}
    // public bool IsAnObstacleInTheWay(Vector3 targetPos)
    // {
    //     h = targetPos.y + 15f;
    //     
    //     if (!CheckIfCeilingInTheWay()) return false;
    //
    //     //////
    //
    //     bool canJump = false;
    //
    //     if (!CheckIfTopNodeIsBlocked(targetPos, ref canJump)) return false;
    //
    //     return canJump;
    // }
    //
    // private bool CheckIfTopNodeIsBlocked(Vector3 targetPos, ref bool canJump)
    // {
    //     Vector3[] path = GetParabolePath(targetPos);
    //
    //     if (path.Length == 0)
    //         return false;
    //
    //
    //     Vector3 topNode = path.OrderByDescending(x => x.y).FirstOrDefault();
    //
    //     Ray r = new Ray(myTransform.position, (topNode - myTransform.position).normalized);
    //
    //     RaycastHit hit;
    //
    //     if (!Physics.Raycast(r, out hit, Vector3.Distance(myTransform.position, topNode)))
    //     {
    //         canJump = true;
    //     }
    //
    //     DebugJump(targetPos, canJump, topNode);
    //     return true;
    // }
    //
    // private bool CheckIfCeilingInTheWay()
    // {
    //     Ray ceelingRay = new Ray(myTransform.position, Vector3.up);
    //     RaycastHit ceelingHit;
    //
    //     if (Physics.Raycast(ceelingRay, out ceelingHit, 400))
    //         return false;
    //     return true;
    // }
    //
    // void DebugJump(Vector3 targetPos, bool canJump, Vector3 topNode)
    // {
    //     DrawPath(myTransform.position, targetPos);
    //     if(canJump)
    //         Debug.DrawLine(myTransform.position, topNode, Color.green, 3f);
    //     else
    //     {
    //         Debug.DrawLine(myTransform.position, topNode, Color.red, 3f);
    //     }
    // }
    struct LaunchData {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData (Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
		
    }
}
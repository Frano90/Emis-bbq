using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using FranoW;
[System.Serializable]
public class ParabolicShooter
{
    private static float h = 2;

    static LaunchData  CalculateLaunchData(Vector3 iniPos, Vector3 tgt) {
        float displacementY = tgt.y - iniPos.y;
        Vector3 displacementXZ = new Vector3 (tgt.x - iniPos.x, 0, tgt.z - iniPos.z);
        float time = Mathf.Sqrt(-2*h/-9f) + Mathf.Sqrt(2*(displacementY - h)/-9f); //Mathf.Sqrt(2*(displacementY - h)/Gravity.defaultValue);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * -9f * h);
        Vector3 velocityXZ = displacementXZ / time;
        
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(-9f), time);
    }
    public static void DrawPath(Vector3 startPos, Vector3 endPos) {
        LaunchData launchData = CalculateLaunchData (startPos, endPos);
        Vector3 previousDrawPoint = startPos;

        int resolution = 30;
        
        for (int i = 1; i <= resolution; i++) {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *-9f * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = startPos + displacement;
            Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }
 
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
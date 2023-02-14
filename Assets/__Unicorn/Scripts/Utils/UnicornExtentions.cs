using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public static class UnicornExtensions
    {
        public static void Shuffle<T> (this List<T> deck) {
            for (int i = 0; i < deck.Count; i++) {
                T temp = deck[i];
                int randomIndex = Random.Range(0, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }
        
        public static Vector3 RandomPoint(this Bounds bounds) {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        
        public static bool IsPointerOverUIObject()
        {
            //check mouse
            if(EventSystem.current.IsPointerOverGameObject())
                return true;
             
            //check touch
            if(Input.touchCount > 0 ){
                if(EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return true;
            }

            return false;
        }

        #region Vector3

        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }

        public static float GetAngleFromVector(Vector3 dir)
        {
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            //int angle = Mathf.RoundToInt(n);

            return angle;
        }

        public static Vector3 Set(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x: x == null ? vector3.x : (float)x,
                y: y == null ? vector3.y : (float)y,
                z: z == null ? vector3.z : (float)z);
        }

        public static Vector3 Move(this Vector3 vector3, Vector3 direction)
        {
            return vector3 + direction;
        }


        public static Vector2 Set(this Vector2 vector2, float? x = null, float? y = null)
        {
            return new Vector2(x: x == null ? vector2.x : (float)x,
                y: y == null ? vector2.y : (float)y);
        }

        public static Vector2 ToVectorXZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    
        public static Vector3 ToVectorXZ(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    
        public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange = 0.0004f)
        {
            return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
        }  

        #endregion
    }

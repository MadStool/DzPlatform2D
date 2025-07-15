using UnityEngine;

public class FollowCanvas : MonoBehaviour
{
    public Transform character;
    public Vector3 offset = new Vector3(0, 1, 0);

    void Update()
    {
        if (character == null) 
            return;

        transform.position = character.position + offset;
        transform.rotation = Quaternion.identity;
    }
}
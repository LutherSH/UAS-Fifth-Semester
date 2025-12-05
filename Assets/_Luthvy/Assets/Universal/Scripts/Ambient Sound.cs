using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
/// ////////////////////////////////////////////////////////////////
/// STUFF
    public AudioClip[] ambientClips;
    private AudioSource source;
    public float minDistance = 1f;
    public float maxDistance = 50f;
    public Color gizmoColours = new Color(0f, 0.5f, 1f, 0.25f);
/// ////////////////////////////////////////////////////////////////
/// START
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;

        if (ambientClips.Length > 0)
        {
            source.clip = ambientClips[Random.Range(0, ambientClips.Length)];
            source.time = Random.Range(0f, source.clip.length);
            source.pitch = Random.Range(0.95f, 1.05f);
            source.Play();
        }
    }
    #if UNITY_EDITOR
/// ////////////////////////////////////////////////////////////////
/// GIZMO DRAW
    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColours;
        Gizmos.DrawSphere(transform.position, minDistance);

        Gizmos.color = new Color(gizmoColours.r, gizmoColours.g, gizmoColours.b, 0.1f);
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
    #endif
}

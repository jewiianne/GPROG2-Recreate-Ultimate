using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    private Animator emote;
    public AudioSource source;

    public List<AudioClip> danceClips;
    public List<AudioClip> macarenaClips;
    public List<AudioClip> northernClips;

    private bool isEmoting = false;

    void Start()
    {
        emote = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && !isEmoting)
        {
            StartCoroutine(PlayEmote("dance", 5f, danceClips));
        }

        if (Input.GetKey(KeyCode.Alpha2) && !isEmoting)
        {
            StartCoroutine(PlayEmote("macarena", 8f, macarenaClips));
        }

        if (Input.GetKey(KeyCode.Alpha3) && !isEmoting)
        {
            StartCoroutine(PlayEmote("northern", 8f, northernClips));
        }
    }

    IEnumerator PlayEmote(string emoteName, float duration, List<AudioClip> clips)
    {
        isEmoting = true;
        emote.SetBool(emoteName, true);
        emote.SetBool("Idle", false);

        // Play each clip in sequence
        foreach (var clip in clips)
        {
            source.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }

        yield return new WaitForSeconds(duration - TotalClipsDuration(clips));

        emote.SetBool(emoteName, false);
        emote.SetBool("Idle", true);
        isEmoting = false;
    }

    private float TotalClipsDuration(List<AudioClip> clips)
    {
        float totalDuration = 0f;
        foreach (var clip in clips)
        {
            totalDuration += clip.length;
        }
        return totalDuration;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes; // List of runes to glow
        public float lerpSpeed = 2f; // Speed of the glow effect
        public string sceneToLoad = "Starting Scene"; // Scene to load
        public ScoreManagerScript scoreManager; // Reference to ScoreManagerScript

        private bool glowing = false; // To prevent multiple coroutine calls

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!glowing && scoreManager != null && scoreManager.score >= scoreManager.TOTAL_SCORE)
            {
                StartCoroutine(GlowRunesSequentially());
            }
        }

        private IEnumerator GlowRunesSequentially()
        {
            glowing = true;

            foreach (var rune in runes)
            {
                // Current rune starts glowing
                Color startColor = rune.color;
                Color targetColor = startColor;
                targetColor.a = 1.0f;

                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime * lerpSpeed;
                    rune.color = Color.Lerp(startColor, targetColor, t);
                    yield return null;
                }
            }

            // After all runes glow, change the scene
            LoadScene();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            glowing = false;
            StopAllCoroutines(); // Stops the glowing process if the player leaves the altar
            ResetRunes(); // Resets all runes to their initial state
        }

        private void ResetRunes()
        {
            foreach (var rune in runes)
            {
                Color color = rune.color;
                color.a = 0f;
                rune.color = color;
            }
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

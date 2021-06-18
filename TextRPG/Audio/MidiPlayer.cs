using System;
using System.Threading;

namespace TextRPG.Audio
{
    public enum Duration
    {
        WHOLE = 1200,
        HALF = WHOLE / 2,
        QUARTER = HALF / 2,
        EIGHTH = QUARTER / 2,
        SIXTEENTH = EIGHTH / 2,
    }
    public enum Tone
    {
        REST = 0,
        F3 = 175,
        F3sharp = 185,
        G3flat = 185,
        G3 = 196,
        G3sharp = 208,
        A3flat = 208,
        A3 = 220,
        A3sharp = 233,
        B3flat = 233,
        B3 = 247,
        C4 = 262,
        C4sharp = 277,
        D4flat = 277,
        D4 = 294,
        D4sharp = 311,
        E4flat = 311,
        E4 = 330,
        F4 = 349,
        F4sharp = 370,
        G4flat = 370,
        G4 = 392,
        G4sharp = 415,
        A4flat = 415,
        A4 = 440,
        A4sharp = 466,
        B4flat = 466,
        B4 = 494,
        C5 = 523,
        C5sharp = 554,
        D5flat = 554,
        D5 = 587,
        D5sharp = 622,
        E5flat = 622,
        E5 = 659,
        F5 = 698,
        F5sharp = 740,
        G5flat = 784,
        G5 = 784,
        G5sharp = 831,
        A5flat = 831,
        A5 = 880,
        A5sharp = 932
    }
    public struct Note
    {
        Tone toneVal;
        Duration durVal;

        public Tone NoteTone { get { return toneVal; } }
        public Duration NoteDuration { get { return durVal; } }
        public Note(Tone frequency, Duration time)
        {
            toneVal = frequency;
            durVal = time;
        }
    }

    public static class MidiPlayer
    {
        public static void Play(Note[] tune)
        {
            foreach(Note n in tune)
            {
                if(n.NoteTone == Tone.REST)
                {
                    Thread.Sleep((int)n.NoteDuration);
                }
                else
                {
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
                }
            }
        }

        public static void PlayVictory()
        {
            Note[] victory =
            {
                new Note(Tone.C4, Duration.EIGHTH),
                new Note(Tone.E4, Duration.EIGHTH),
                new Note(Tone.G4, Duration.EIGHTH),
                new Note(Tone.REST, Duration.EIGHTH),
                new Note(Tone.D4, Duration.EIGHTH),
                new Note(Tone.F4sharp, Duration.EIGHTH),
                new Note(Tone.A4, Duration.EIGHTH),
                new Note(Tone.REST, Duration.EIGHTH),
                new Note(Tone.E4, Duration.EIGHTH),
                new Note(Tone.G4sharp, Duration.EIGHTH),
                new Note(Tone.B4, Duration.EIGHTH),
            };
            Play(victory);
        }

        public static void PlayHealthUp()
        {
            Note[] healthUp =
            {
                new Note(Tone.C4, Duration.EIGHTH),
                new Note(Tone.E4, Duration.EIGHTH),
                new Note(Tone.G4, Duration.EIGHTH)
            };
            Play(healthUp);
        }

        public static void PlayPickup()
        {
            Note[] pickup =
            {
                new Note(Tone.B4, Duration.SIXTEENTH),
                new Note(Tone.E5, Duration.SIXTEENTH)
            };
            Play(pickup);
        }

        public static void PlayUnlock()
        {
            Note[] unlock =
            {
                new Note(Tone.E5, Duration.SIXTEENTH),
                new Note(Tone.B4, Duration.SIXTEENTH)
            };
            Play(unlock);
        }

        public static void PlayFailed()
        {
            Note[] fail =
            {
                new Note(Tone.C4, Duration.SIXTEENTH),
                new Note(Tone.C4, Duration.SIXTEENTH),
            };
            Play(fail);
        }

        public static void PlayAttack()
        {
            Note[] attack =
            {
                new Note(Tone.A4, Duration.SIXTEENTH),
            };
            Play(attack);
        }

        public static void PlayBombAttack()
        {
            Note[] attack =
            {
                new Note(Tone.A4, Duration.SIXTEENTH),
                new Note(Tone.A4, Duration.SIXTEENTH),
            };
            Play(attack);
        }
    }
}

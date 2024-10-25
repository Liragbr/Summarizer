using System;
using System.IO;
using System.Linq;
using System.Speech.Recognition;

namespace YouTubeVideoAnalyzer.Transcription
{
    public class AudioTranscriber
    {
        public string TranscribeAudio(string audioFilePath)
        {
            string transcription = string.Empty;

            try
            {
                var cultureInfo = new System.Globalization.CultureInfo("pt-BR");

                var recognizerInfo = SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault(r => r.Culture.Equals(cultureInfo));

                if (recognizerInfo == null)
                {
                    cultureInfo = new System.Globalization.CultureInfo("en-US");
                    recognizerInfo = SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault(r => r.Culture.Equals(cultureInfo));

                    Console.WriteLine("Idioma pt-BR não disponível. Usando en-US.");
                }

                if (recognizerInfo != null)
                {
                    using (var recognizer = new SpeechRecognitionEngine(recognizerInfo))
                    {
                        recognizer.LoadGrammar(new DictationGrammar());

                        recognizer.SpeechRecognized += (s, e) =>
                        {
                            transcription += e.Result.Text + " ";
                        };

                        using (var audioStream = new FileStream(audioFilePath, FileMode.Open))
                        {
                            recognizer.SetInputToWaveStream(audioStream);
                            recognizer.Recognize();
                        }

                        Console.WriteLine("Transcrição concluída.");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum idioma reconhecido disponível.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Erro de permissão: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao transcrever o áudio: {ex.Message}");
            }

            return transcription;
        }
    }
}

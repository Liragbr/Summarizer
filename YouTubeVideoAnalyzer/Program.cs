using System;
using YouTubeVideoAnalyzer.AudioProcessing;
using YouTubeVideoAnalyzer.Transcription;
using YouTubeVideoAnalyzer.Summarization;

namespace YouTubeVideoAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insira a URL do vídeo do YouTube:");
            string videoUrl = Console.ReadLine();

            string audioFilePath = "audio.mp3";
            string wavFilePath = "audio.wav";

            try
            {
                var downloader = new AudioDownloader();
                downloader.DownloadAudioAsync(videoUrl);

                downloader.ConvertMp3ToWav(audioFilePath, wavFilePath);
                Console.WriteLine("Conversão de MP3 para WAV concluída.");

                var transcriber = new AudioTranscriber();
                var transcription = transcriber.TranscribeAudio(wavFilePath);
                Console.WriteLine("Transcrição:");

                if (string.IsNullOrWhiteSpace(transcription))
                {
                    Console.WriteLine("Nenhuma transcrição foi gerada.");
                }
                else
                {
                    Console.WriteLine(transcription);

                    var summarizer = new TextSummarizer();
                    var summary = summarizer.GenerateSummary(transcription);
                    Console.WriteLine("\nResumo do vídeo:");
                    Console.WriteLine(summary);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante o processo: {ex.Message}");
            }

            Console.WriteLine("Processo concluído. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}
